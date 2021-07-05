using RtCs.MathUtils;

namespace RtCs.OpenGL
{
    public static class VectorExtensions
    {
        public static double[] ToDoubleArray(this Vector3[] inVectors)
        {
            double[] result = new double[inVectors.Length * 3];
            for (int i = 0; i < inVectors.Length; ++i) {
                result[(i * 3) + 0] = inVectors[i].x;
                result[(i * 3) + 1] = inVectors[i].y;
                result[(i * 3) + 2] = inVectors[i].z;
            }
            return result;
        }

        public static float[] ToFloatArray(this Vector3[] inVectors)
        {
            float[] result = new float[inVectors.Length * 3];
            for (int i = 0; i < inVectors.Length; ++i) {
                result[(i * 3) + 0] = (float)inVectors[i].x;
                result[(i * 3) + 1] = (float)inVectors[i].y;
                result[(i * 3) + 2] = (float)inVectors[i].z;
            }
            return result;
        }

        public static double[] ToDoubleArray(this Vector2[] inVectors)
        {
            double[] result = new double[inVectors.Length * 2];
            for (int i = 0; i < inVectors.Length; ++i) {
                result[(i * 2) + 0] = inVectors[i].x;
                result[(i * 2) + 1] = inVectors[i].y;
            }
            return result;
        }

        public static float[] ToFloatArray(this Vector2[] inVectors)
        {
            float[] result = new float[inVectors.Length * 2];
            for (int i = 0; i < inVectors.Length; ++i) {
                result[(i * 2) + 0] = (float)inVectors[i].x;
                result[(i * 2) + 1] = (float)inVectors[i].y;
            }
            return result;
        }
    }
}
