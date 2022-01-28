using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public interface IGLVertexAttributeList
    {
        bool TryGetBufferPointerOffset(string inName, out int outOffset);
        //bool TryGetValue<T>(string inName, out IGLVertexAttribute<T> outAttribute) where T : unmanaged;
    }

    class GLVertexAttributeList : List<IGLVertexAttribute>, IGLVertexAttributeList
    {
        public bool TryGetBufferPointerOffset(string inName, out int outOffset)
        {
            if (this.TryGetFirst(out var att, a => a.Description.Name == inName)) {
                outOffset = att.Offset;
                return true;
            }
            outOffset = -1;
            return false;
        }

        public bool TryGetValue<T>(string inName, out IGLVertexAttribute<T> outAttribute) where T : unmanaged
        {
            foreach (IGLVertexAttribute att in this) {
                if ((att.Description.Name == inName) && (att is IGLVertexAttribute<T>)) {
                    outAttribute = att as IGLVertexAttribute<T>;
                    return true;
                }
            }
            outAttribute = null;
            return false;
        }
    }
}
