namespace RtCs.OpenGL
{
    /// <summary>
    /// The Shader storage buffer object socket in the shader program.
    /// </summary>
    public class GLShaderStorageBufferSocket
    {
        public GLShaderStorageBufferSocket(string inName, int inBinding)
        {
            Name = inName;
            Binding = inBinding;
            return;
        }

        /// <summary>
        /// Name of the shader storage buffer in the program.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Location of the shader storage buffer in the program.
        /// </summary>
        public readonly int Binding;
    }
}
