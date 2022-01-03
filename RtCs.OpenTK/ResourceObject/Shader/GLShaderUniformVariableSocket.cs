using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    /// <summary>
    /// The uniform property socekt in the shader program.
    /// </summary>
    public class GLShaderUniformVariableSocket
    {
        public GLShaderUniformVariableSocket(string inName, int inLocation, ActiveUniformType inType)
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

        public GLShaderUniformVariable CreateDefaultProperty()
            => CreateDefaultProperty(this);

        public override string ToString()
            => $"[{Location}] {Name} ({Type})";

        public static GLShaderUniformVariable CreateDefaultProperty(GLShaderUniformVariableSocket inSocket)
        {
            switch (inSocket.Type) {
                case ActiveUniformType.Int:
                    return new GLShaderUniformVariable.Int(inSocket);
                case ActiveUniformType.Float:
                    return new GLShaderUniformVariable.Float(inSocket);
                case ActiveUniformType.Double:
                    return new GLShaderUniformVariable.Double(inSocket);
                case ActiveUniformType.FloatVec3:
                    return new GLShaderUniformVariable.Vec3(inSocket);
                case ActiveUniformType.FloatVec4:
                    return new GLShaderUniformVariable.Vec4(inSocket);
                case ActiveUniformType.FloatMat4:
                    return new GLShaderUniformVariable.Mat4(inSocket);
                case ActiveUniformType.Sampler2D:
                    return new GLShaderUniformVariable.Texture(inSocket);
            }
            return null;
        }
    }
}
