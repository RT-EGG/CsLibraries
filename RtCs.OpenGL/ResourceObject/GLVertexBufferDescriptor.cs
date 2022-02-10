namespace RtCs.OpenGL
{
    class GLVertexBufferDescriptor
    {
        public GLVertexBufferDescriptor(int inOffset, IGLVertexAttributeDescriptor inAttributeDescriptor)
        {
            Offset = inOffset;
            AttributeDescriptor = inAttributeDescriptor;
        }

        public readonly int Offset;
        public readonly IGLVertexAttributeDescriptor AttributeDescriptor;
    }
}
