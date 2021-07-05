using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    public abstract class GLVertexAttributePointer
    {
        public GLVertexAttributePointer(int inIndex)
        {
            Index = inIndex;
            return;
        }

        public abstract EGLVertexAttributeType AttributeType { get; }
        public abstract void Commit(GLMesh inMesh);
        public readonly int Index;

        public class Position : GLVertexAttributePointer
        {
            public Position(int inIndex) : base(inIndex) { }
            public override EGLVertexAttributeType AttributeType => EGLVertexAttributeType.Position;
            public override void Commit(GLMesh inMesh)
                => GL.VertexAttribPointer(Index, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);
        }

        public class Normal : GLVertexAttributePointer
        {
            public Normal(int inIndex) : base(inIndex) { }
            public override EGLVertexAttributeType AttributeType => EGLVertexAttributeType.Normal;
            public override void Commit(GLMesh inMesh)
                => GL.VertexAttribPointer(Index, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);
        }
    }
}
