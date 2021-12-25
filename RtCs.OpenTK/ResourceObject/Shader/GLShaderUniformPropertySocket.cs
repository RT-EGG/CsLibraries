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

        public GLShaderUniformProperty CreateDefaultProperty()
            => CreateDefaultProperty(this);

        public override string ToString()
            => $"[{Location}] {Name} ({Type})";

        public static GLShaderUniformProperty CreateDefaultProperty(GLShaderUniformPropertySocket inSocket)
        {
            switch (inSocket.Type) {
                case ActiveUniformType.Int:
                    return new GLShaderUniformProperty.Int(inSocket);
                case ActiveUniformType.Float:
                    return new GLShaderUniformProperty.Float(inSocket);
                case ActiveUniformType.Double:
                    return new GLShaderUniformProperty.Double(inSocket);
                case ActiveUniformType.FloatVec4:
                    return new GLShaderUniformProperty.Vec4(inSocket);
                case ActiveUniformType.FloatMat4:
                    return new GLShaderUniformProperty.Mat4(inSocket);
                case ActiveUniformType.Sampler2D:
                    return new GLShaderUniformProperty.Texture(inSocket);
            }
            return null;
        }
    }
}
