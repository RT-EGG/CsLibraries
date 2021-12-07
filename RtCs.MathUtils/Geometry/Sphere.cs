namespace RtCs.MathUtils.Geometry
{
    public struct Sphere
    {
        public Sphere(Vector3 inCenter, float inRadius)
        {
            Center = inCenter;
            Radius = inRadius;
            return;
        }

        public Vector3 Center
        { get; set; }
        public float Radius
        { get; set; }
    }
}
