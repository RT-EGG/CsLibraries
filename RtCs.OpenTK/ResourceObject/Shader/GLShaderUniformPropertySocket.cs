using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    /// <summary>
    /// The uniform property socekt in the shader program.
    /// </summary>
    public class GLShaderUniformPropertySocket
    {
        public GLShaderUniformPropertySocket(string inName, int inLocation, ActiveUniformType inType)
        {
            Name = inName;
            Location = inLocation;
            Type = inType;
            return;
        }

        /// <summary>
        /// Name of the uniform property.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Location (nearly id) of the uniform property in the program.
        /// </summary>
        public readonly int Location;
        /// <summary>
        /// Type of the uniform property.
        /// </summary>
        public readonly ActiveUniformType Type;
    }
}
