using RtCs.MathUtils;

namespace GLTestVisualizer
{
    public static class SphericalCoordinateExtensions
    {
        public static Vector3 GetRectangularCoordinate(this SphericalCoordinate inValue)
            => new Vector3(
                Matrix4x4.MakeRotateY(inValue.AzimuthAngle)
              * Matrix4x4.MakeRotateX(inValue.ElevationAngle)
              * (new Vector4(0.0f, 0.0f, inValue.Radius, 1.0f))
            );
    }
}
