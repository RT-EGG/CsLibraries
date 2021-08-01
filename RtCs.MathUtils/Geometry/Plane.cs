namespace RtCs.MathUtils.Geometry
{
    public struct Plane
    {
        public Plane(Vector3 inPoint, Vector3 inNormal)
        {
            Point = inPoint;
            Normal = inNormal;
            return;
        }

        public Vector3 Point
        { get; set; }
        public Vector3 Normal
        { get; set; }

        public Vector3 Project(Vector3 inPoint)
        {
            if (Normal.IsZero) {
                return Point;
            }

            if (!Normal.Length2.AlmostEquals(1.0)) {
                Normal = Normal.Normalized;
            }

            return inPoint - (Vector3.Dot(inPoint - Point, Normal) * Normal);
        }

        public double DistanceTo(Vector3 inPoint)
            => (Project(inPoint) - inPoint).Length;
    }
}
