using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    /// <summary>
    /// Pointer object of vertex attribute in shader program.
    /// </summary>
    /// <remarks>
    /// GLMesh object referes pointer to commit vertex attributes.\n
    /// This class is abstract so don't use directlly.\n
    /// Use classes inherit this class (GLVertexAttributePointer.Position, GLVertexAttributePointer.Normal, GLVertexAttributePointer.Color or GLVertexAttributePointer.TexCoord).
    /// </remarks>
    public class GLVertexAttributePointer
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inIndex">Initial value of Index.</param>
        /// <param name="inPointerType">Initial value of PointerType.</param>
        public GLVertexAttributePointer(int inIndex, string inName)
        {
            Index = inIndex;
            Name = inName;
            return;
        }

        /// <summary>
        /// Layout location of vertex attribute in shader program.
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// Name of reference vertex attribute in mesh.
        /// </summary>
        public readonly string Name;
    }
}
