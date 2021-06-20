using OpenTK.Graphics.OpenGL4;
using System;
using System.IO;

namespace RtCs.OpenGL.ResourceObject.Shader
{
    public class GLShaderBinarySource : GLShaderSource
    {
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

        public byte[] Binary
        { get; set; } = null;
        public string EntryPoint
        { get; set; } = "Main";
    }
}
