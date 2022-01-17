using RtCs.MathUtils;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    public partial class GLPrimitiveMesh
    {
        /// <summary>
        /// Create line mesh.
        /// </summary>
        /// <param name="inLines">List of pair of position which construct lines.</param>
        /// <returns>Created mesh object.</returns>
        /// <remarks>
        /// Returned mesh has setup only Vertices.
        /// </remarks>
        public static GLMesh CreateLines(params (Vector3 Point0, Vector3 Point1)[] inLines)
            => CreateLines((IEnumerable<(Vector3 Point0, Vector3 Point1)>)inLines);

        /// <summary>
        /// Create line mesh.
        /// </summary>
        /// <param name="inLines">List of pair of position which construct lines.</param>
        /// <returns>Created mesh object.</returns>
        /// <remarks>
        /// Returned mesh has setup only Vertices.
        /// </remarks>
        public static GLMesh CreateLines(IEnumerable<(Vector3 Point0, Vector3 Point1)> inLines)
        {
            GLMesh mesh = new GLMesh();
            mesh.Clear();

            List<Vector3> points = new List<Vector3>(inLines.Count() * 2);
            foreach (var (point0, point1) in inLines) {
                points.Add(point0);
                points.Add(point1);
            }
            mesh.Vertices = points.ToArray();
            mesh.Topology = EGLMeshTopology.Lines;
            mesh.Indices = Enumerable.Range(0, mesh.Vertices.Length).ToArray();

            mesh.CalculateBoundingBox();
            mesh.Apply();
            return mesh;
        }
    }
}
