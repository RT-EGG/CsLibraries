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
    }
}
