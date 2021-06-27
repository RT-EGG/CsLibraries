using System.IO;
using System.Reflection;

namespace RtCs.OpenGL
{
    public partial class GLRenderShaderProgram
    {
        public abstract class Preset
        {
            internal static void CreatePresets()
            {
                Color = new Color();
                return;
            }

            public static Color Color
            { get; private set; }
        }

        public abstract partial class PresetType : GLRenderShaderProgram
        {
            protected static string LoadAssemblyText(string inName)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var names = assembly.GetManifestResourceNames();
                using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream($"RtCs.OpenGL.Resources.{inName}"))) {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
