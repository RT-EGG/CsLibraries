using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System;
using System.Runtime.InteropServices;

namespace RtCs.OpenGL
{
    interface IGLVertexAttributeDescriptor
    {
        string Name { get; }
        //int ElementCount { get; }
        int Size { get; }
        //int Stride { get; }
    }

    public abstract class GLVertexAttributeDescriptor<T> : IGLVertexAttributeDescriptor where T : unmanaged
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
        ///// <summary>
        ///// The count of variable for one attribute. e.g. 3-dimensional vertex position, ElementCount is 3.
        ///// </summary>
        //public int ElementCount
        //{ get; }
        /// <summary>
        /// The size of T.
        /// </summary>
        public int Size => m_Size;
        ///// <summary>
        ///// The total size of attribute for one vertex.
        ///// </summary>
        //public int Stride => ElementCount * Size;

        /// <summary>
        /// Copy data to array for vertex buffer.
        /// </summary>
        /// <param name="inSource">The vertex attribute data.</param>
        /// <param name="inDestination">The vertex buffer data.</param>
        /// <param name="inOffset">The position that should be written start.</param>
        /// <returns>The length of written buffers to destination.</returns>
        public int CopyToBuffer(T[] inSource, byte[] inDestination, int inOffset)
        {
            int length = inSource.Length * Size;
            Span<byte> source = MemoryMarshal.Cast<T, byte>(inSource.AsSpan());
            source.TryCopyTo(new Span<byte>(inDestination, inOffset, length));
            
            return length;
        }

        /// <summary>
        /// Notify vertex buffer binding to GPU. Normaly, call GL.VertexAttributePointer.
        /// </summary>
        /// <param name="inPointer">The information that shader required.</param>
        /// <param name="inOffset">The offset position in vertex buffer.</param>
        public abstract void BindPointer(GLVertexAttributePointer inPointer, int inOffset);

        private readonly int m_Size = 0;
    }

    public class GLVertexIntAttributeDescriptor : GLVertexAttributeDescriptor<int>
    {
        public GLVertexIntAttributeDescriptor(string inName)
            : base(inName)
        { }

        public override void BindPointer(GLVertexAttributePointer inPointer, int inOffset)
            => GL.VertexAttribIPointer(inPointer.Index, 1, VertexAttribIntegerType.Int, sizeof(int), (IntPtr)inOffset);
    }

    public class GLVertexFloatAttributeDescriptor : GLVertexAttributeDescriptor<float>
    {
        public GLVertexFloatAttributeDescriptor(string inName)
            : base(inName)
        { }

        public override void BindPointer(GLVertexAttributePointer inPointer, int inOffset)
            => GL.VertexAttribPointer(inPointer.Index, 1, VertexAttribPointerType.Float, false, sizeof(float), inOffset);
    }

    public class GLVertexVector2AttributeDescriptor : GLVertexAttributeDescriptor<Vector2>
    {
        public GLVertexVector2AttributeDescriptor(string inName)
            : base(inName)
        { }

        public override void BindPointer(GLVertexAttributePointer inPointer, int inOffset)
            => GL.VertexAttribPointer(inPointer.Index, 2, VertexAttribPointerType.Float, false, sizeof(float) * 2, inOffset);
    }

    public class GLVertexVector3AttributeDescriptor : GLVertexAttributeDescriptor<Vector3>
    {
        public GLVertexVector3AttributeDescriptor(string inName)
            : base(inName)
        { }

        public override void BindPointer(GLVertexAttributePointer inPointer, int inOffset)
            => GL.VertexAttribPointer(inPointer.Index, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, inOffset);
    }

    public class GLVertexVector4AttributeDescriptor : GLVertexAttributeDescriptor<Vector4>
    {
        public GLVertexVector4AttributeDescriptor(string inName)
            : base(inName)
        { }

        public override void BindPointer(GLVertexAttributePointer inPointer, int inOffset)
            => GL.VertexAttribPointer(inPointer.Index, 4, VertexAttribPointerType.Float, false, sizeof(float) * 4, inOffset);
    }
}
