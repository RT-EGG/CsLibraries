using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    public enum EGLVertexAttributeType
    {
        Position,
        Normal,
        TexCoord,
        Color
    }

    public interface IGLVertexAttribute
    {
        EGLVertexAttributeType Type { get; }
        int Size { get; }
        int Stride { get; }
        int Offset { get; }
    }

    abstract class GLVertexAttribute : IGLVertexAttribute
    {
        public abstract EGLVertexAttributeType Type { get; }
        public abstract int Size { get; }
        public int Stride => sizeof(float) * Size;
        public int Offset => sizeof(float) * DataOffset;

        public float[] Data { get; set; } = null;
        public int DataOffset { get; set; } = 0;

        public class Position : GLVertexAttribute
        {
            public override EGLVertexAttributeType Type => EGLVertexAttributeType.Position;
            public override int Size => 3;
        }

        public class Normal : GLVertexAttribute
        {
            public override EGLVertexAttributeType Type => EGLVertexAttributeType.Normal;
            public override int Size => 3;
        }

        public class TexCoord : GLVertexAttribute
        {
            public override EGLVertexAttributeType Type => EGLVertexAttributeType.TexCoord;
            public override int Size => 2;
        }

        public class Color : GLVertexAttribute
        {
            public override EGLVertexAttributeType Type => EGLVertexAttributeType.Color;
            public override int Size => 4;
        }
    }

    public class GLVertexAttributeList : IEnumerable<IGLVertexAttribute>
    {
        public IGLVertexAttribute this[EGLVertexAttributeType inType]
        {
            get => m_Items[(int)inType];
            set => m_Items[(int)inType] = value;
        }

        public bool Contains(EGLVertexAttributeType inType)
            => this[inType] != null;

        public void Clear()
        {
            foreach (var type in Enum.GetValues(typeof(EGLVertexAttributeType)).Cast<EGLVertexAttributeType>()) {
                this[type] = null;
            }
            return;
        }

        public IEnumerator<IGLVertexAttribute> GetEnumerator()
            => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private IGLVertexAttribute[] m_Items = new IGLVertexAttribute[(int)Enum.GetValues(typeof(EGLVertexAttributeType)).Cast<EGLVertexAttributeType>().Max() + 1];

        private class Enumerator : IEnumerator<IGLVertexAttribute>
        {
            public Enumerator(GLVertexAttributeList inReference)
            {
                m_Current = null;
                m_Reference = inReference;

                Reset();
                return;
            }

            public IGLVertexAttribute Current => m_Current;
            object IEnumerator.Current => m_Current;

            public bool MoveNext()
            {
                while (m_TypeEnumerator.MoveNext()) {
                    if (m_Reference[(EGLVertexAttributeType)m_TypeEnumerator.Current] != null) {
                        m_Current = m_Reference[(EGLVertexAttributeType)m_TypeEnumerator.Current];
                        return true;
                    }
                }
                return false;
            }

            public void Reset()
                => m_TypeEnumerator = Enum.GetValues(typeof(EGLVertexAttributeType)).GetEnumerator();

            public void Dispose()
            {
            }

            private readonly GLVertexAttributeList m_Reference = null;
            private IGLVertexAttribute m_Current;
            private IEnumerator m_TypeEnumerator = null;
        }
    }
}
