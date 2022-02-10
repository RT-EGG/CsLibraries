using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    public class GLVertexAttributePointer
    {
        public GLVertexAttributePointer(int inLocation, string inAttributeName)
        {
            Location = inLocation;
            AttributeName = inAttributeName;
            return;
        }

        public void BindFormat(GLVertexArrayObject inVAO, int inOffset)
            => BindFormatCore(inVAO, inOffset);

        protected virtual void BindFormatCore(GLVertexArrayObject inVAO, int inOffset)
        {

        }

        public string AttributeName
        { get; } = "";
        public int Location
        { get; } = 0;

        public class Floats : GLVertexAttributePointer
        {
            public Floats(int inLocation, string inAttributeName, int inElementCount, bool inNormalized = false)
                : base(inLocation, inAttributeName) 
            {
                ElementCount = inElementCount;
                Normalized = inNormalized;
            }

            protected override void BindFormatCore(GLVertexArrayObject inVAO, int inOffset)
                => GL.VertexArrayAttribFormat(inVAO.ID, Location, ElementCount, VertexAttribType.Float, Normalized, inOffset);

            private readonly int ElementCount;
            private readonly bool Normalized;
        }

        public class Float1 : Floats
        {
            public Float1(int inLocation, string inAttributeName, bool inNormalized = false) : base(inLocation, inAttributeName, 1, inNormalized)
            { }
        }

        public class Float2 : Floats
        {
            public Float2(int inLocation, string inAttributeName, bool inNormalized = false) : base(inLocation, inAttributeName, 2, inNormalized)
            { }
        }

        public class Float3 : Floats
        {
            public Float3(int inLocation, string inAttributeName, bool inNormalized = false) : base(inLocation, inAttributeName, 3, inNormalized)
            { }
        }

        public class Float4 : Floats
        {
            public Float4(int inLocation, string inAttributeName, bool inNormalized = false) : base(inLocation, inAttributeName, 4, inNormalized)
            { }
        }

        public class Ints : GLVertexAttributePointer
        {
            public Ints(int inLocation, string inAttributeName, int inElementCount)
                : base(inLocation, inAttributeName)
            {
                ElementCount = inElementCount;
            }

            protected override void BindFormatCore(GLVertexArrayObject inVAO, int inOffset)
                => GL.VertexArrayAttribIFormat(inVAO.ID, Location, ElementCount, VertexAttribType.Int, inOffset);

            private readonly int ElementCount;
        }

        public class Int1 : Ints
        {
            public Int1(int inLocation, string inAttributeName) : base(inLocation, inAttributeName, 1) { }
        }

        public class Int2 : Ints
        {
            public Int2(int inLocation, string inAttributeName) : base(inLocation, inAttributeName, 2) { }
        }

        public class Int3 : Ints
        {
            public Int3(int inLocation, string inAttributeName) : base(inLocation, inAttributeName, 3) { }
        }

        public class Int4 : Ints
        {
            public Int4(int inLocation, string inAttributeName) : base(inLocation, inAttributeName, 4) { }
        }
    }
}
