namespace RtCs.OpenGL
{
    /// <summary>
    /// The uniform buffer object socket in the shader program.
    /// </summary>
    public class GLShaderUniformBlockSocket
    {
        public GLShaderUniformBlockSocket(string inName, int inBinding, int inDataSize, GLShaderUniformVariableSocket[] inVariables)
        {
            Name = inName;
            Binding = inBinding;
            DataSize = inDataSize;
            Variables = inVariables;
            return;
        }

        /// <summary>
        /// Name of the uniform buffer in the program.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Binding number of the uniform buffer in the program.
        /// </summary>
        public readonly int Binding;
        /// <summary>
        /// Total data size of the uniform buffer in the program.
        /// </summary>
        public readonly int DataSize;
        /// <summary>
        /// The variables contains in the uniform buffer in the program.
        /// </summary>
        public readonly GLShaderUniformVariableSocket[] Variables;
    }
}
