using RtCs.MathUtils;

namespace RtCs.OpenGL
{
    public class GLViewMatrixStack : GLModelMatrixStack
    {
        public void LookAt(Vector3 inCenter, Vector3 inTargetPoint, Vector3 inUpDirection)
            => LoadMatrix(MakeLookAtViewMatrix(inCenter, inTargetPoint, inUpDirection));

        public static Matrix4x4 MakeLookAtViewMatrix(Vector3 inCenter, Vector3 inTargetPoint, Vector3 inUpDirection)
        {
            if ((inCenter == inTargetPoint) || inUpDirection.IsZero) {
                return Matrix4x4.MakeTranslate(inCenter);
            }

            Vector3 z = (inCenter - inTargetPoint).Normalized;
            Vector3 x = Vector3.Cross(inUpDirection, z).Normalized;
            Vector3 y = Vector3.Cross(z, x).Normalized;

            Vector3 t = new Vector3(
                    -Vector3.Dot(x, inCenter),
                    -Vector3.Dot(y, inCenter),
                    -Vector3.Dot(z, inCenter)
                );

            // ViewMatrix to ModelMatrix
            return new Matrix4x4(
                    x.x, x.y, x.z, t.x,
                    y.x, y.y, y.z, t.y,
                    z.x, z.y, z.z, t.z,
                    0.0, 0.0, 0.0, 1.0
                );
        }
    }
}
