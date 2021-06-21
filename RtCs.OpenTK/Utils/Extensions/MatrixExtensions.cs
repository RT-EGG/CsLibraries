using RtCs.MathUtils;

namespace RtCs.OpenGL
{
    public static class MatrixExtensions
    {
        public static float[] ToGLArrayF(this Matrix3x3 inMat)
            => new float[Matrix3x3.ElemCount] {
                (float)inMat[0, 0],
                (float)inMat[1, 0],
                (float)inMat[2, 0],
                (float)inMat[0, 1],
                (float)inMat[1, 1],
                (float)inMat[2, 1],
                (float)inMat[0, 2],
                (float)inMat[1, 2],
                (float)inMat[2, 2]
            };

        public static double[] ToGLArray(this Matrix3x3 inMat)
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

        public static float[] ToGLArrayF(this Matrix4x4 inMat)
            => new float[Matrix4x4.ElemCount] {
                (float)inMat[0, 0],
                (float)inMat[1, 0],
                (float)inMat[2, 0],
                (float)inMat[3, 0],
                (float)inMat[0, 1],
                (float)inMat[1, 1],
                (float)inMat[2, 1],
                (float)inMat[3, 1],
                (float)inMat[0, 2],
                (float)inMat[1, 2],
                (float)inMat[2, 2],
                (float)inMat[3, 2],
                (float)inMat[0, 3],
                (float)inMat[1, 3],
                (float)inMat[2, 3],
                (float)inMat[3, 3]
            };

        public static double[] ToGLArray(this Matrix4x4 inMat)
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
    }
}
