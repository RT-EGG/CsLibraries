namespace RtCs.OpenGL
{
    public partial class GLRenderShaderProgram
    {
        public abstract class Preset
        {
            static Preset()
            {
                CreatePresets();
                return;
            }

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
        }
    }
}
