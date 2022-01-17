using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    public class GLProperties
    {
        public void Collect()
        {
            MaxTessGenLeven = GL.GetInteger(GetPName.MaxTessGenLevel);
        }

        public static int MaxTessGenLeven
        { get; private set; } = 64;
    }
}
