using RtCs.MathUtils;
using System.Linq;

namespace RtCs.OpenGL
{
    public class GLRenderObject
    {
        public void Render(GLRenderingStatus inRenderingStatus)
        {
            inRenderingStatus.ModelViewMatrix.Model.PushMatrix();
            try {
                inRenderingStatus.ModelViewMatrix.Model.MultiMatrix(Transform.WorldMatrix);
                Renderer.Render(inRenderingStatus);

            } finally {
                inRenderingStatus.ModelViewMatrix.Model.PopMatrix();
            }
            return;
        }

        public void CalculateBoundingBox()
        {
            var vertices = Renderer?.Mesh?.Positions;
            if (vertices.IsNullOrEmpty()) {
                BoundingBox = new AABB3D();
                return;
            }

            var matrix = Transform.WorldMatrix;
            BoundingBox = AABB3D.InclusionBoundary(vertices.Select(v => new Vector3(matrix * new Vector4(v, 1.0))));
            return;
        }

        public AABB3D BoundingBox
        { get; set; } = new AABB3D();

        public Transform Transform
        { get; } = new Transform();
        public GLRenderer Renderer
        { get; } = new GLRenderer();
    }
}
