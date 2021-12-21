using RtCs.MathUtils;

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
        {
            inDst[inOffset + 0] = inMat.m00;
            inDst[inOffset + 1] = inMat.m10;
            inDst[inOffset + 2] = inMat.m20;
            inDst[inOffset + 3] = inMat.m30;
            inDst[inOffset + 4] = inMat.m01;
            inDst[inOffset + 5] = inMat.m11;
            inDst[inOffset + 6] = inMat.m21;
            inDst[inOffset + 7] = inMat.m31;
            inDst[inOffset + 8] = inMat.m02;
            inDst[inOffset + 9] = inMat.m12;
            inDst[inOffset + 10] = inMat.m22;
            inDst[inOffset + 11] = inMat.m32;
            inDst[inOffset + 12] = inMat.m03;
            inDst[inOffset + 13] = inMat.m13;
            inDst[inOffset + 14] = inMat.m23;
            inDst[inOffset + 15] = inMat.m33;
        }
    }
}
