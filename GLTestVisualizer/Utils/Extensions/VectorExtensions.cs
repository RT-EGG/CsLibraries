using RtCs.MathUtils;

namespace GLTestVisualizer
{
    public static class VectorExtensions
    {
        public static Vector3 DegToRad(this Vector3 inDegrees)
            => new Vector3(
                    inDegrees.x.DegToRad(),
                    inDegrees.y.DegToRad(),
                    inDegrees.z.DegToRad()
                );

        public static Vector3 RadToDeg(this Vector3 inDegrees)
            => new Vector3(
                    inDegrees.x.RadToDeg(),
                    inDegrees.y.RadToDeg(),
                    inDegrees.z.RadToDeg()
                );
    }
}
