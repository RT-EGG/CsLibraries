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
    public abstract class GLVertexAttributePointer
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inIndex">Initial value of Index.</param>
        public GLVertexAttributePointer(int inIndex)
        {
            Index = inIndex;
            return;
        }

        /// <summary>
        /// Type of vertex attribute.
        /// </summary>
        public abstract EGLVertexAttributeType AttributeType { get; }
        /// <summary>
        /// Layout location of vertex attribute in shader program.
        /// </summary>
        public readonly int Index;

        public class Position : GLVertexAttributePointer
        {
            public Position(int inIndex) : base(inIndex) { }
            public override EGLVertexAttributeType AttributeType => EGLVertexAttributeType.Position;
        }

        public class Normal : GLVertexAttributePointer
        {
            public Normal(int inIndex) : base(inIndex) { }
            public override EGLVertexAttributeType AttributeType => EGLVertexAttributeType.Normal;
        }

        public class Color : GLVertexAttributePointer
        {
            public Color(int inIndex) : base(inIndex) { }
            public override EGLVertexAttributeType AttributeType => EGLVertexAttributeType.Color;
        }

        public class TexCoord : GLVertexAttributePointer
        {
            public TexCoord(int inIndex) : base(inIndex) { }
            public override EGLVertexAttributeType AttributeType => EGLVertexAttributeType.TexCoord;
        }
    }
}
