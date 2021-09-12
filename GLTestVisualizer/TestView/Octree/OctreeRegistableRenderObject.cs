using RtCs.MathUtils;
using RtCs.MathUtils.Geometry;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView.Octree
{
    class OctreeRegistableRenderObject : GLRenderObject, IOctreeRegistable
    {
        public Vector3 BoundsMin => BoundingBox.Min;
        public Vector3 BoundsMax => BoundingBox.Max;

        public IOctreeCell AffiliationCell
        { get; set; } = null;
    }
}
