using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    public class GLShaderUniformPropertySocket
    {
        public GLShaderUniformPropertySocket(string inName, int inLocation, ActiveUniformType inType)
        {
            Name = inName;
            Location = inLocation;
            Type = inType;
            return;
        }

        public readonly string Name;
        public readonly int Location;
        public readonly ActiveUniformType Type;
    }
}
