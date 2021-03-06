using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    public partial class GLPrimitiveMesh
    {
        /// <summary>
        /// Create grid mesh by lines.
        /// </summary>
        /// <param name="inDimensions">Length of each axis.</param>
        /// <param name="inDivisionX">Number of division along x-axis.</param>
        /// <param name="inDivisionY">Number of division along y-axis.</param>
        /// <param name="inDivisionZ">Number of division along z-axis.</param>
        /// <returns>Created mesh object.</returns>
        /// <remarks>
        /// Returned mesh has setup only Vertices.
        /// </remarks>
        public static GLMesh CreateGrid(Vector3 inDimensions, int inDivisionX, int inDivisionY, int inDivisionZ)
            => CreateGrid(new Vector3(0.0f), inDimensions, inDivisionX, inDivisionY, inDivisionZ);

        /// <summary>
        /// Create grid mesh by lines.
        /// </summary>
        /// <param name="inOffset">The origin of mesh.</param>
        /// <param name="inDimensions">Length of each axis.</param>
        /// <param name="inDivisionX">Number of division along x-axis.</param>
        /// <param name="inDivisionY">Number of division along y-axis.</param>
        /// <param name="inDivisionZ">Number of division along z-axis.</param>
        /// <returns>Created mesh object.</returns>
        /// <remarks>
        /// Returned mesh has setup only Vertices.
        /// </remarks>
        public static GLMesh CreateGrid(Vector3 inOffset, Vector3 inDimensions, int inDivisionX, int inDivisionY, int inDivisionZ)
        {
            float xStep = inDimensions.x / inDivisionX;
            float yStep = inDimensions.y / inDivisionY;
            float zStep = inDimensions.z / inDivisionZ;

            GLMesh result = new GLMesh();
            List<Vector3> vertices = new List<Vector3>(
                    2 * (
                    ((inDivisionX + 1) * (inDivisionY + 1)) +
                    ((inDivisionY + 1) * (inDivisionZ + 1)) +
                    ((inDivisionX + 1) * (inDivisionZ + 1))
                    )
                );
            for (int x = 0; x <= inDivisionX; ++x) {
                for (int y = 0; y <= inDivisionY; ++y) {
                    Vector3 p0 = new Vector3(
                        x * xStep,
                        y * yStep,
                        0.0f
                    ) + inOffset;
                    Vector3 p1 = new Vector3(
                        x * xStep,
                        y * yStep,
                        inDimensions.z
                    ) + inOffset;

                    vertices.Add(p0, p1);
                }
            }
            for (int x = 0; x <= inDivisionX; ++x) {
                for (int z = 0; z <= inDivisionZ; ++z) {
                    Vector3 p0 = new Vector3(
                        x * xStep,
                        0.0f,
                        z * zStep
                    ) + inOffset;
                    Vector3 p1 = new Vector3(
                        x * xStep,
                        inDimensions.y,
                        z * zStep
                    ) + inOffset;

                    vertices.Add(p0, p1);
                }
            }
            for (int y = 0; y <= inDivisionY; ++y) {
                for (int z = 0; z <= inDivisionZ; ++z) {
                    Vector3 p0 = new Vector3(
                        0.0f,
                        y * yStep,
                        z * zStep
                    ) + inOffset;
                    Vector3 p1 = new Vector3(
                        inDimensions.x,
                        y * yStep,
                        z * zStep
                    ) + inOffset;

                    vertices.Add(p0, p1);
                }
            }

            result.VertexBufferUsageHint = BufferUsageHint.StaticDraw;
            result.Topology = EGLMeshTopology.Lines;
            result.Vertices = vertices.ToArray();
            result.Indices = Enumerable.Range(0, vertices.Count).ToArray();

            result.Apply();
            return result;
        }
    }
}
