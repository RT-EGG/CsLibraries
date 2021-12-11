using OpenTK.Graphics.OpenGL4;
using System;
using System.IO;
using System.Reflection;

namespace RtCs.OpenGL
{
    /// <summary>
    /// Shader unit text source.
    /// </summary>
    public class GLShaderTextSource : GLShaderSource
    {
        /// <summary>
        /// Create shader source and load from text file.
        /// </summary>
        /// <param name="inFilepath">Path to shader source text file.</param>
        /// <returns>The shader source object has loaded text source file.</returns>
        /// <exception cref="FileNotFoundException">Throw if the file is not found.</exception>
        /// <remarks>
        /// This function create the instance of GLShaderTextSource, call LoadFromFile, then return the instance.
        /// </remarks>
        public static GLShaderTextSource CreateFileSource(string inFilepath)
        {
            GLShaderTextSource result = new GLShaderTextSource();
            result.LoadFromFile(inFilepath);
            return result;
        }

        /// <summary>
        /// Create shader source and set source to Text.
        /// </summary>
        /// <param name="inText">Raw text of shader source.</param>
        /// <returns>The shader source object has set Text property.</returns>
        /// <remarks>
        /// This function create the instance of GLShaderTextSource, set inText to Text property, then return the instance.
        /// </remarks>
        public static GLShaderTextSource CreateTextSource(string inText)
            => new GLShaderTextSource { Text = inText };

        /// <summary>
        /// Create shader source and load text from embeded resource.
        /// </summary>
        /// <param name="inResourceName">Path to embeded resource in project.</param>
        /// <returns>The shader source object loaded Text from embeded resource.</returns>
        /// <remarks>
        /// This function create the instance of GLShaderTextSource, load text from Assembly.GetExecutingAssembly().GetManifestResourceStream(inResourceName).\n
        /// About exceptions thrown this method, see Assembly.GetExecutingAssembly().GetManifestResourceStream().
        /// </remarks>
        public static GLShaderTextSource CreateAssemblyTextResourceSource(string inResourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(inResourceName))) {
                return CreateTextSource(reader.ReadToEnd());
            }
        }

        /// <summary>
        /// Laod text source from file.
        /// </summary>
        /// <param name="inFilepath">Path to shader source text file.</param>
        /// <exception cref="FileNotFoundException">Throw if the file is not found.</exception>
        public void LoadFromFile(string inFilepath)
        {
            if (!File.Exists(inFilepath)) {
                throw new FileNotFoundException(inFilepath);
            }

            if (!Uri.TryCreate(inFilepath, UriKind.Absolute, out var _)) {
                inFilepath = Path.GetFullPath(inFilepath);
            }

            using (StreamReader reader = new StreamReader(new FileStream(inFilepath, FileMode.Open, FileAccess.Read))) {
                Text = reader.ReadToEnd();
            }
            return;
        }

        internal override void LoadSource(int inShaderID)
            => GL.ShaderSource(inShaderID, Text);

        /// <summary>
        /// Raw text of shader source.
        /// </summary>
        public string Text
        { get; set; }
    }
}
