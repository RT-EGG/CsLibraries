using System.Runtime.InteropServices;

namespace RtCs.OpenGL.Utils
{
    static class ByteConverter
    {
        public static void WriteTo<T>(T inSrc, byte[] inDst, ref int inDstIndex) where T : struct
            => WriteTo(new T[] { inSrc }, inDst, ref inDstIndex);

        public static void WriteTo<T>(T[] inSrc, byte[] inDst, ref int inDstIndex) where T : struct
        {
            var itemsAsByte = MemoryMarshal.AsBytes<T>(inSrc);
            for (int i = 0; i < itemsAsByte.Length; ++i) {
                inDst[inDstIndex++] = itemsAsByte[i];
            }
            return;
        }
    }
}
