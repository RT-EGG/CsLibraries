using RtCs.MathUtils;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    public static class MatrixExtensions
    {
        public static float[] ToGLFloatArray(this Matrix3x3 inMat)
            => new float[Matrix3x3.ElemCount] {
                inMat[0, 0],
                inMat[1, 0],
                inMat[2, 0],
                inMat[0, 1],
                inMat[1, 1],
                inMat[2, 1],
                inMat[0, 2],
                inMat[1, 2],
                inMat[2, 2]
            };

        public static double[] ToGLDoubleArray(this Matrix3x3 inMat)
            => new double[Matrix3x3.ElemCount] {
                inMat[0, 0],
                inMat[1, 0],
                inMat[2, 0],
                inMat[0, 1],
                inMat[1, 1],
                inMat[2, 1],
                inMat[0, 2],
                inMat[1, 2],
                inMat[2, 2]
            };

        public static void CopyToGLArray(this Matrix3x3 inMat, float[] inDst, int inOffset)
            => CopyToGLArray(inMat, inDst, ref inOffset);

        public static void CopyToGLArray(this Matrix3x3 inMat, float[] inDst, ref int inOffset)
        {
            inDst[inOffset++] = inMat.m00;
            inDst[inOffset++] = inMat.m10;
            inDst[inOffset++] = inMat.m20;
            inDst[inOffset++] = inMat.m01;
            inDst[inOffset++] = inMat.m11;
            inDst[inOffset++] = inMat.m21;
            inDst[inOffset++] = inMat.m02;
            inDst[inOffset++] = inMat.m12;
            inDst[inOffset++] = inMat.m22;
        }

        public static float[] ToGLFloatArray(this Matrix4x4 inMat)
            => new float[Matrix4x4.ElemCount] {
                inMat[0, 0],
                inMat[1, 0],
                inMat[2, 0],
                inMat[3, 0],
                inMat[0, 1],
                inMat[1, 1],
                inMat[2, 1],
                inMat[3, 1],
                inMat[0, 2],
                inMat[1, 2],
                inMat[2, 2],
                inMat[3, 2],
                inMat[0, 3],
                inMat[1, 3],
                inMat[2, 3],
                inMat[3, 3]
            };

        public static double[] ToGLDoubleArray(this Matrix4x4 inMat)
            => new double[Matrix4x4.ElemCount] {
                inMat[0, 0],
                inMat[1, 0],
                inMat[2, 0],
                inMat[3, 0],
                inMat[0, 1],
                inMat[1, 1],
                inMat[2, 1],
                inMat[3, 1],
                inMat[0, 2],
                inMat[1, 2],
                inMat[2, 2],
                inMat[3, 2],
                inMat[0, 3],
                inMat[1, 3],
                inMat[2, 3],
                inMat[3, 3]
            };

        public static void CopyToGLArray(this Matrix4x4 inMat, float[] inDst, int inOffset)
            => CopyToGLArray(inMat, inDst, ref inOffset);

        public static void CopyToGLArray(this Matrix4x4 inMat, float[] inDst, ref int inOffset)
        {
            inDst[inOffset++] = inMat.m00;
            inDst[inOffset++] = inMat.m10;
            inDst[inOffset++] = inMat.m20;
            inDst[inOffset++] = inMat.m30;
            inDst[inOffset++] = inMat.m01;
            inDst[inOffset++] = inMat.m11;
            inDst[inOffset++] = inMat.m21;
            inDst[inOffset++] = inMat.m31;
            inDst[inOffset++] = inMat.m02;
            inDst[inOffset++] = inMat.m12;
            inDst[inOffset++] = inMat.m22;
            inDst[inOffset++] = inMat.m32;
            inDst[inOffset++] = inMat.m03;
            inDst[inOffset++] = inMat.m13;
            inDst[inOffset++] = inMat.m23;
            inDst[inOffset++] = inMat.m33;
        }
    }
}
