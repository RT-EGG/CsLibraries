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
                VertexColor = new VertexColor();
                Texture = new Texture();
                return;
            }

            public static Color Color
            { get; private set; }
            public static VertexColor VertexColor
            { get; private set; }
            public static Texture Texture
            { get; private set; }
        }

        public abstract partial class PresetType : GLRenderShaderProgram
        {
        }
    }
}
