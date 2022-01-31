using RtCs.OpenGL;
using System.IO;
using System.Reflection;

namespace Snow.OpenGL
{
    class ShaderCompiler : GLShaderTextCompiler
    {
        protected override bool GetBuiltInSource(string inName, out string outSource)
        {
            if (base.GetBuiltInSource(inName, out outSource)) {
                return true;
            }

            switch (inName) {
                case "<Particle.h>":
                    outSource = ReadTextResourceFile("Snow.Resources.BuiltIn_SnowParticle.h.txt");
                    return true;
            }
            return false;
        }

        internal string ReadTextResourceFile(string inResourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(inResourceName))) {
                return reader.ReadToEnd();
            }
        }

        internal bool CompileFromTextResourceFile(GLShader inShader, string inResourceName)
            => Compile(inShader, ReadTextResourceFile(inResourceName), "");
    }
}
