using RtCs.MathUtils;
using RtCs.MathUtils.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    public partial class GLPrimitiveMesh
    {
        public static GLMesh CreateLines(params (Vector3 LineStart, Vector3 LineEnd)[] inPoints)
            => CreateLines(inPoints as IReadOnlyCollection<(Vector3 LineStart, Vector3 LineEnd)>);

        public static GLMesh CreateLines(IReadOnlyCollection<(Vector3 LineStart, Vector3 LineEnd)> inPoints)
        {
            GLMesh mesh = new GLMesh();
            mesh.Clear();

            List<Vector3> positions = new List<Vector3>(inPoints.Count * 2);
            foreach (var (start, end) in inPoints) {
                positions.Add(start, end);
            }
            mesh.Positions = positions.ToArray();

            mesh.Topology = EGLMeshTopology.Lines;
            mesh.Indices = Enumerable.Range(0, positions.Count).ToArray();

            mesh.BoundingBox = AABB3D.InclusionBoundary(positions);
            return mesh;
        }
    }
}
