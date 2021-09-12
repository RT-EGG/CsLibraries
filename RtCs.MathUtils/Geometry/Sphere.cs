namespace RtCs.MathUtils.Geometry
{
    public struct Sphere
    {
        public Sphere(Vector3 inCenter, double inRadius)
        {
            Center = inCenter;
            Radius = inRadius;
            return;
        }

        public Vector3 Center
        { get; set; }
        public double Radius
        { get; set; }
    }
}
