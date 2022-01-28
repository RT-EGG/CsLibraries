using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RtCs.OpenGL
{
    /// <summary>
    /// The shader compiler object that compile from text source.
    /// </summary>
    public class GLShaderTextCompiler
    {
        /// <summary>
        /// Import shader source from text file, and compile.
        /// </summary>
        /// <param name="inShader">The target of compile.</param>
        /// <param name="inFilepath">The path to source file.</param>
        /// <returns>If the compiling is success returns true, otherwise returns false and set compile errors to shader object.</returns>
        /// <exception cref="FileNotFoundException">The file indicated by inFilepath is not exist.</exception>
        public bool Compile(GLShader inShader, string inFilepath)
        {
            inFilepath = Path.GetFullPath(inFilepath);
            string directory = Path.GetDirectoryName(inFilepath);

            using (StreamReader reader = new StreamReader(new FileStream(inFilepath, FileMode.Open, FileAccess.Read))) {
                return Compile(inShader, inFilepath, reader.ReadToEnd(), directory);
            }
        }

        /// <summary>
        /// Import shader source and compile.
        /// </summary>
        /// <param name="inShader">The target of compile.</param>
        /// <param name="inText">The shader source code.</param>
        /// <param name="inCurrentDirectory">The base directory to solve include.</param>
        /// <returns>If the compiling is success returns true, otherwise returns false and set compile errors to shader object.</returns>
        public bool Compile(GLShader inShader, string inText, string inCurrentDirectory)
            => Compile(inShader, "EntryPoint", inText, inCurrentDirectory);

        private bool Compile(GLShader inShader, string inName, string inText, string inCurrentDirectory)
        {
            m_Errors.Clear();
            inShader.Compiled = false;

            IncludeReferencePositions.Clear();
            try {
                IncludeReferencePositions.Clear();

                List<string> lines = SplitLines(inText);
                GeneratedCode = string.Join("\n", SolveInclude(inName, lines, inCurrentDirectory));

            } catch (GLShaderCompileIncludeFileError error) {
                m_Errors.Add(new GLShaderCompileError {
                    Filepath = error.ReadingFilepath,
                    RowNumber = error.RowNumber,
                    Level = "error",
                    Code = 0,
                    Message = error.Message
                });
                return false;

            } catch (GLShaderCompileIncludeDirectiveError error) {
                m_Errors.Add(new GLShaderCompileError {
                    Filepath = error.ReadingFilepath,
                    RowNumber = error.RowNumber,
                    Level = "error",
                    Code = 0,
                    Message = error.Message
                });
                return false;
            }

            GL.ShaderSource(inShader.ID, GeneratedCode);
            GL.CompileShader(inShader.ID);

            GL.GetShader(inShader.ID, ShaderParameter.CompileStatus, out int compiled);
            inShader.Compiled = compiled != 0;
            if (!inShader.Compiled) {
                foreach (var errorString in GL.GetShaderInfoLog(inShader.ID).Split('\n')) {
                    if (!errorString.IsNullOrEmpty()) {
                        var error = GLShaderCompileError.Parse(errorString);
                        var (path, row) = FindNestedRowNumber(inName, error.RowNumber);

                        error.Filepath = path;
                        error.RowNumber = row;

                        m_Errors.Add(error);
                    }
                }
            }

            inShader.CompileError = new List<GLShaderCompileError>(m_Errors);
            return m_Errors.IsEmpty();
        }

        internal bool CompileFromTextResourceFile(GLShader inShader, string inResourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(inResourceName))) {
                return Compile(inShader, reader.ReadToEnd(), "");
            }
        }

        private List<string> SolveInclude(string inFileName, List<string> inLines, string inCurrentDirectory)
        {
            const string IncludeDirective = "#include";

            if (IncludeReferencePositions.ContainsKey(inFileName)) {
                return new List<string>(0);
            }

            List<FileReferencePosition> refPositions = new List<FileReferencePosition>();
            IncludeReferencePositions.Add(inFileName, refPositions);

            int lineNumber = 0;
            while (lineNumber < inLines.Count) {
                string line = inLines[lineNumber];

                int includePosition = line.IndexOf("#include");
                if (includePosition >= 0) {
                    string content = line.Substring(includePosition + IncludeDirective.Length);
                    if (!TryFindIncludeFileName(content, out string name, out FileReferenceMode refMode)) {
                        throw new GLShaderCompileIncludeDirectiveError(inFileName, lineNumber, "Can not solve include directive.");
                    }

                    FileReferencePosition refPosition = new FileReferencePosition();
                    refPosition.RowStart = lineNumber;

                    string source = "";
                    string newCurrentDirectory = "";
                    switch (refMode) {
                        case FileReferenceMode.RelatedPath:
                            if (!inCurrentDirectory.Contains(":")) {
                                // is in built-in source.
                                throw new GLShaderCompileIncludeDirectiveError(inFileName, lineNumber, "Can not include related path reference in built-in reference source.");
                            }

                            name = Path.Combine(inCurrentDirectory, name);
                            name = Path.GetFullPath(name);
                            name = name.UnifyPathSeparator();
                            newCurrentDirectory = Path.GetDirectoryName(name);

                            try {
                                source = ReadFromFile(name);

                            } catch (FileNotFoundException) {
                                throw new GLShaderCompileIncludeFileError(inFileName, lineNumber, name);
                            }
                            break;

                        case FileReferenceMode.BuiltIn:
                            name = $"<{name}>";
                            if (!GetBuiltInSource(name, out source)) {
                                throw new GLShaderCompileIncludeFileError(inFileName, lineNumber, name);
                            }
                            newCurrentDirectory = "";
                            break;
                    }

                    List<string> lines = SplitLines(source);
                    lines = SolveInclude(name, lines, newCurrentDirectory);

                    inLines.RemoveAt(lineNumber);
                    inLines.InsertRange(lineNumber, lines);

                    lineNumber += lines.Count;
                    refPosition.ReferenceFile = name;
                    refPosition.RowEnd = lineNumber - 1;

                    refPositions.Add(refPosition);

                } else {
                    ++lineNumber;
                }
            }

            return inLines;
        }

        private bool TryFindIncludeFileName(string inText, out string outName, out FileReferenceMode outMode)
        {
            var related = new Regex("\"(?<name>.+?)\"").Match(inText);
            if (related.Success) {
                outName = related.Groups["name"].Value;
                outMode = FileReferenceMode.RelatedPath;
                return true;
            }
            var builtin = new Regex("<(?<name>.+?)>").Match(inText);
            if (builtin.Success) {
                outName = builtin.Groups["name"].Value;
                outMode = FileReferenceMode.BuiltIn;
                return true;
            }
            outName = "";
            outMode = default;
            return false;
        }

        private List<string> SplitLines(string inText)
            => new List<string>(inText.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));

        private string ReadFromFile(string inFilepath)
        {
            if (!File.Exists(inFilepath)) {
                throw new FileNotFoundException($"File \"{inFilepath}\" is not found.");
            }

            using (StreamReader reader = new StreamReader(new FileStream(inFilepath, FileMode.Open, FileAccess.Read))) {
                return reader.ReadToEnd();
            }
        }

        private string LoadBuiltInResourceFile(string inPath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(inPath))) {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Define built-in source for include directive.
        /// </summary>
        /// <param name="inName">The name of include file.</param>
        /// <param name="outSource">The output source.</param>
        /// <returns>If can generate outSource for inName returns true, otherwise false.</returns>
        /// <remarks>
        /// This method will be called when the compiler find #include directive.</b>
        /// e.g. find #include <header.h>, passed "<header.h>" to inName.
        /// </remarks>
        protected virtual bool GetBuiltInSource(string inName, out string outSource)
        {
            switch (inName) {
                case "<Matrix.h>":
                    outSource = LoadBuiltInResourceFile("RtCs.OpenGL.Resources.BuiltIn_Matrix.h.txt");
                    return true;
                case "<Light.h>":
                    outSource = LoadBuiltInResourceFile("RtCs.OpenGL.Resources.BuiltIn_Light.h.txt");
                    return true;

            }
            outSource = "";
            return false;
        }

        private (string, int) FindNestedRowNumber(string inKey, int inRow)
        {
            if (!IncludeReferencePositions.TryGetValue(inKey, out var positions)) {
                return (inKey, inRow);
            }
            
            if (positions.Count == 0) {
                return (inKey, inRow);
            }

            int diff = 0;
            foreach (var position in positions) {
                if ((position.RowStart <= inRow) && (inRow <= position.RowEnd)) {
                    return FindNestedRowNumber(position.ReferenceFile, inRow - position.RowStart);
                }
                diff += position.RowCount - 1;
            }
            return (inKey, inRow - diff);
        }

        private Dictionary<string, List<FileReferencePosition>> IncludeReferencePositions
        { get; } = new Dictionary<string, List<FileReferencePosition>>();
        public string GeneratedCode
        { get; private set; } = "";
        public IReadOnlyList<GLShaderCompileError> Errors => m_Errors;
        private List<GLShaderCompileError> m_Errors = new List<GLShaderCompileError>();
        
        private enum FileReferenceMode
        {
            RelatedPath,
            BuiltIn
        }

        private class FileReferencePosition
        {
            public int RowStart
            { get; set; } = -1;
            public int RowEnd
            { get; set; } = -1;
            public int RowCount => RowEnd - RowStart + 1;

            public string ReferenceFile
            { get; set; } = "";
        }
    }
}
