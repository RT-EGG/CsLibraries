namespace RtCs.OpenGL
{ 
    interface IGLVertexAttribute
    {
        IGLVertexAttributeDescriptor Description { get; }
        int Offset { get;}
        int BufferSize { get; }

        int CopyToBuffer(byte[] inDestination, int inOffset);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">The type of vertex attribute buffer.</typeparam>
    public interface IGLVertexAttribute<T> where T : unmanaged
    {
        GLVertexAttributeDescriptor<T> Descriptor { get; }
        T[] Buffer { get; set; }
    }

    class GLVertexAttribute<T> : IGLVertexAttribute, IGLVertexAttribute<T> where T : unmanaged
    {
        public GLVertexAttribute(GLVertexAttributeDescriptor<T> inDescriptor)
        {
            Descriptor = inDescriptor;
        }

        public GLVertexAttributeDescriptor<T> Descriptor
        { get; } = null;
        public T[] Buffer
        { get; set; } = null;
        public int Offset
        { get; private set; } = 0;

        public int CopyToBuffer(byte[] inDestination, int inOffset)
        { 
            Offset = inOffset;
            if (Buffer != null) {
                return Descriptor.CopyToBuffer(Buffer, inDestination, inOffset);
            } else {
                return 0;
            }
        }

        public int BufferSize => Descriptor.Size * ((Buffer == null) ? 0 : Buffer.Length);
        IGLVertexAttributeDescriptor IGLVertexAttribute.Description => Descriptor;
    }

    public class GLVertexAttribute
    {
        public const string AttributeName_Vertex = "Vertex";
        public const string AttributeName_Normal = "Normal";
        public const string AttributeName_TexCoord = "TexCoord";
        public const string AttributeName_Color = "Color";
    }
}
