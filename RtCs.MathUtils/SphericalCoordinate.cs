using System;

namespace RtCs.MathUtils
{
    public struct SphericalCoordinate : IEquatable<SphericalCoordinate>
    {
        public float AzimuthAngle
        { get; set; }
        public float ElevationAngle
        { get; set; }
        public float Radius
        { get; set; }

        public float AzimuthAngleDeg
        {
            get => AzimuthAngle.RadToDeg();
            set => AzimuthAngle = value.DegToRad();
        }
        public float ElevationAngleDeg
        {
            get => ElevationAngle.RadToDeg();
            set => ElevationAngle = value.DegToRad();
        }

        public Vector3 Rectangular
            => new Vector3(
               Matrix4x4.MakeRotateY(AzimuthAngle)
             * Matrix4x4.MakeRotateX(ElevationAngle)
             * (new Vector4(0.0f, 0.0f, Radius, 1.0f))
            );

        public override bool Equals(object obj)
            => obj is SphericalCoordinate coordinate && Equals(coordinate);

        public bool Equals(SphericalCoordinate other)
            => AzimuthAngle.AlmostEquals(other.AzimuthAngle) &&
               ElevationAngle.AlmostEquals(other.ElevationAngle) &&
               Radius.AlmostEquals(other.Radius);

        public override int GetHashCode()
        {
            int hashCode = 681108072;
            hashCode = hashCode * -1521134295 + AzimuthAngle.GetHashCode();
            hashCode = hashCode * -1521134295 + ElevationAngle.GetHashCode();
            hashCode = hashCode * -1521134295 + Radius.GetHashCode();
            return hashCode;
        }

        public override string ToString()
            => $"a: {AzimuthAngleDeg} deg; e: {ElevationAngleDeg} deg; r: {Radius};";

        public static bool operator ==(SphericalCoordinate left, SphericalCoordinate right)
            => left.Equals(right);
        public static bool operator !=(SphericalCoordinate left, SphericalCoordinate right)
            => !(left == right);
    }
}
