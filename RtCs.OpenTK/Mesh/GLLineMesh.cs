using RtCs.MathUtils;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    public partial class GLPrimitiveMesh
    {
        public static GLMesh CreateLines(params (Vector3 Point0, Vector3 Point1)[] inLines)
            => CreateLines((IEnumerable<(Vector3 Point0, Vector3 Point1)>)inLines);

        public static GLMesh CreateLines(IEnumerable<(Vector3 Point0, Vector3 Point1)> inLines)
        {
            GLMesh mesh = new GLMesh();
            mesh.Clear();

            List<Vector3> points = new List<Vector3>(inLines.Count() * 2);
            foreach (var (point0, point1) in inLines) {
                points.Add(point0);
                points.Add(point1);
            }
            mesh.Positions = points.ToArray();
            mesh.Topology = EGLMeshTopology.Lines;
            mesh.Indices = Enumerable.Range(0, mesh.Positions.Length).ToArray();

            mesh.CalculateBoundingBox();
            return mesh;
        }
    }
}
