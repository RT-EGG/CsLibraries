using OpenTK.Graphics.OpenGL4;
using System;
using System.IO;
using System.Reflection;

namespace RtCs.OpenGL
{
    public class GLShaderTextSource : GLShaderSource
    {
        public static GLShaderTextSource CreateFileSource(string inFilepath)
        {
            GLShaderTextSource result = new GLShaderTextSource();
            result.LoadFromFile(inFilepath);
            return result;
        }

        public static GLShaderTextSource CreateTextSource(string inText)
            => new GLShaderTextSource { Text = inText };

        public static GLShaderTextSource CreateAssemblyTextResourceSource(string inResourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var names = assembly.GetManifestResourceNames();
            using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(inResourceName))) {
                return CreateTextSource(reader.ReadToEnd());
            }
        }

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

        public string Text
        { get; set; }
    }
}
