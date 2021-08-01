namespace RtCs.MathUtils.Geometry
{
    public struct Line3D
    {
        public Line3D(Vector3 inPoint0, Vector3 inPoint1)
        {
            Point0 = inPoint0;
            Point1 = inPoint1;
            return;
        }

        public Vector3 Point0
        { get; set; }
        public Vector3 Point1
        { get; set; }

        public Vector3 Along(double inRatio)
            => Point0 + ((Point1 - Point0) * inRatio);

        public Vector3 Vector => Point1 - Point0;
        public Vector3 Direction => Vector.Normalized;        
        public double Length => Vector.Length;

        public Vector3 Project(Vector3 inPoint)
            => Project(inPoint, out double _);

        public Vector3 Project(Vector3 inPoint, out double outParameter)
        {
            if (Vector.IsZero) {
                outParameter = 0.0;
                return Point0;
            }
            outParameter = Vector3.Dot(Direction, inPoint - Point0) / Length;
            return Point0 + (outParameter * Vector);
        }

        public double DistanceTo(Vector3 inPoint)
        {
            Project(inPoint, out double t);
            return (inPoint - Along(t.Clamp(0.0, 1.0))).Length;
        }
    }
}
