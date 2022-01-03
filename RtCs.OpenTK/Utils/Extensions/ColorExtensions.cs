using RtCs.MathUtils;
using RtCs.OpenGL.Utils;
using System.Runtime.InteropServices;

namespace RtCs.OpenGL
{
    public static class ColorExtensions
    {
        public static void CopyToArray(this ColorRGB inValue, float[] inDst, int inIndex)
            => CopyToArray(inValue, inDst, ref inIndex);

        public static void CopyToArray(this ColorRGB inValue, float[] inDst, ref int inIndex)
        {
            inDst[inIndex++] = inValue.R / 255.0f;
            inDst[inIndex++] = inValue.G / 255.0f;
            inDst[inIndex++] = inValue.B / 255.0f;
            return;
        }

        public static void CopyToArray(this ColorRGB inValue, byte[] inDst, int inIndex)
            => CopyToArray(inValue, inDst, ref inIndex);

        public static void CopyToArray(this ColorRGB inValue, byte[] inDst, ref int inIndex)
        {
            float[] items = new float[3];
            inValue.CopyToArray(items, 0);

            ByteConverter.WriteTo(items, inDst, ref inIndex);
            return;
        }

        public static void CopyToArray(this ColorRGBA inValue, float[] inDst, int inIndex)
            => CopyToArray(inValue, inDst, ref inIndex);

        public static void CopyToArray(this ColorRGBA inValue, float[] inDst, ref int inIndex)
        {
            inDst[inIndex++] = inValue.R / 255.0f;
            inDst[inIndex++] = inValue.G / 255.0f;
            inDst[inIndex++] = inValue.B / 255.0f;
            inDst[inIndex++] = inValue.A / 255.0f;
            return;
        }

        public static void CopyToArray(this ColorRGBA inValue, byte[] inDst, int inIndex)
            => CopyToArray(inValue, inDst, ref inIndex);

        public static void CopyToArray(this ColorRGBA inValue, byte[] inDst, ref int inIndex)
        {
            float[] items = new float[4];
            inValue.CopyToArray(items, 0);

            ByteConverter.WriteTo(items, inDst, ref inIndex);
            return;
        }
    }
}
