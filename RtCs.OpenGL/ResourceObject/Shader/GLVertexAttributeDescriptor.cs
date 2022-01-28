using System;
using System.Runtime.InteropServices;

namespace RtCs.OpenGL
{
    interface IGLVertexAttributeDescriptor
    {
        string Name { get; }
        int Size { get; }
    }

    public class GLVertexAttributeDescriptor<T> : IGLVertexAttributeDescriptor where T : unmanaged
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inName">Initializer of Name.</param>
        public GLVertexAttributeDescriptor(string inName)
        {
            Name = inName;
            m_Size = Marshal.SizeOf(typeof(T));
        }

        /// <summary>
        /// The arbitary name which indeitify the attribute.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The size of T.
        /// </summary>
        public int Size => m_Size;

        /// <summary>
        /// Copy data to array for vertex buffer.
        /// </summary>
        /// <param name="inSource">The vertex attribute data.</param>
        /// <param name="inDestination">The vertex buffer data.</param>
        /// <param name="inOffset">The position that should be written start.</param>
        /// <returns>The length of written buffers to destination.</returns>
        public virtual int CopyToBuffer(T[] inSource, byte[] inDestination, int inOffset)
        {
            int length = inSource.Length * Size;
            Span<byte> source = MemoryMarshal.Cast<T, byte>(inSource.AsSpan());
            source.TryCopyTo(new Span<byte>(inDestination, inOffset, length));
            
            return length;
        }

        private readonly int m_Size = 0;
    }
}
