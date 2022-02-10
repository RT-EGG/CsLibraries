using System;
using System.Runtime.InteropServices;

namespace RtCs.OpenGL
{
    interface IGLVertexAttribute
    {
        string Name { get; }
        IGLVertexAttributeDescriptor[] Descriptors { get; }
        int Offset { get; set; }
        int BufferSize { get; }
        int Stride { get; }

        int CopyToBuffer(byte[] inDestination, int inOffset);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">The type of vertex attribute buffer.</typeparam>
    public interface IGLVertexAttribute<T> where T : unmanaged
    {
        GLVertexAttributeDescriptor[] Descriptors { get; }
        T[] Buffer { get; set; }
    }

    class GLVertexAttribute<T> : IGLVertexAttribute, IGLVertexAttribute<T> where T : unmanaged
    {
        public GLVertexAttribute(string inName, GLVertexAttributeDescriptor inDescriptor)
            : this (inName, new GLVertexAttributeDescriptor[] { inDescriptor })
        {
        }

        public GLVertexAttribute(string inName, GLVertexAttributeDescriptor[] inDescriptors)
        {
            Name = inName;

            Descriptors = inDescriptors;
            m_SizeOfT = Marshal.SizeOf(typeof(T));
            return;
        }

        public string Name
        { get; }

        public GLVertexAttributeDescriptor[] Descriptors
        { get; } = new GLVertexAttributeDescriptor[0];
        public T[] Buffer
        { get; set; } = null;
        public int Offset
        { get; set; } = 0;
        public int Stride => m_SizeOfT;

        public int CopyToBuffer(byte[] inDestination, int inOffset)
        { 
            Offset = inOffset;
            if (Buffer != null) {
                int length = Buffer.Length * m_SizeOfT;
                Span<byte> source = MemoryMarshal.Cast<T, byte>(Buffer.AsSpan());
                source.TryCopyTo(new Span<byte>(inDestination, inOffset, length));

                return length;

            } else {
                return 0;
            }
        }

        public int BufferSize => m_SizeOfT * (Buffer.IsNullOrEmpty() ? 0 : Buffer.Length);
        IGLVertexAttributeDescriptor[] IGLVertexAttribute.Descriptors => Descriptors;

        private int m_SizeOfT = 0;
    }

    public class GLVertexAttribute
    {
        public const string AttributeName_Vertex = "Vertex";
        public const string AttributeName_Normal = "Normal";
        public const string AttributeName_TexCoord = "TexCoord";
        public const string AttributeName_Color = "Color";
    }
}
