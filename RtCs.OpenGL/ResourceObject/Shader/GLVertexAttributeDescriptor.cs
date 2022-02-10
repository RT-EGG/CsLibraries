using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;

namespace RtCs.OpenGL
{
    interface IGLVertexAttributeDescriptor
    {
        string Name { get; }
        int Size { get; }
        int Stride { get; }
    }

    public class GLVertexAttributeDescriptor : IGLVertexAttributeDescriptor
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inName">Initializer of Name.</param>
        public GLVertexAttributeDescriptor(string inName, int inElementCount, int inTypeSize)
        {
            Name = inName;
            ElementCount = inElementCount;
            m_Size = inTypeSize;
        }

        /// <summary>
        /// The arbitary name which indeitify the attribute.
        /// </summary>
        public string Name { get; }
        public int ElementCount { get; }
        /// <summary>
        /// The size of T.
        /// </summary>
        public int Size => m_Size;
        /// <summary>
        /// 
        /// </summary>
        //public VertexAttribType AttributeType { get; }

        public int Stride => ElementCount * Size;


        private readonly int m_Size = 0;
    }
}
