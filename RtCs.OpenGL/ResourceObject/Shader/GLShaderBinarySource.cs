using OpenTK.Graphics.OpenGL4;
using System;
using System.IO;

namespace RtCs.OpenGL
{
    public class GLShaderBinarySource : GLShaderSource
    {
        /// <summary>
        /// Laod binary source from file.
        /// </summary>
        /// <param name="inFilepath">Path to shader source text file.</param>
        /// <param name="inEntryPoint">Identifier for shader entry point.</param>
        /// <exception cref="FileNotFoundException">Throw if the file is not found.</exception>
        public void LoadFromFile(string inFilepath, string inEntryPoint)
        {
            if (!File.Exists(inFilepath)) {
                throw new FileNotFoundException(inFilepath);
            }

            if (!Uri.TryCreate(inFilepath, UriKind.Absolute, out var _)) {
                inFilepath = Path.GetFullPath(inFilepath);
            }

            using (BinaryReader reader = new BinaryReader(new FileStream(inFilepath, FileMode.Open, FileAccess.Read))) {
                Binary = reader.ReadBytes((int)reader.BaseStream.Length);
            }
            EntryPoint = inEntryPoint;
            return;
        }

        internal override void LoadSource(int inShaderID)
        {
            GL.ShaderBinary(1, ref inShaderID, (BinaryFormat)All.ShaderBinaryFormats, Binary, Binary.Length);
            GL.SpecializeShader(inShaderID, EntryPoint, 0, new int[0], new int[0]);
            return;
        }

        /// <summary>
        /// Raw bytes of shader source.
        /// </summary>
        public byte[] Binary
        { get; set; } = null;
        /// <summary>
        /// Identifier for shader entry point.
        /// </summary>
        public string EntryPoint
        { get; set; } = "Main";
    }
}
