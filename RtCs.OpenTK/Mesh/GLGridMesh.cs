using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtCs.OpenGL
{
    public partial class GLPrimitiveMesh
    {
        public static GLMesh CreateGrid(Vector3 inDimensions, int inDivisionX, int inDivisionY, int inDivisionZ)
            => CreateGrid(new Vector3(0.0), inDimensions, inDivisionX, inDivisionY, inDivisionZ);

        public static GLMesh CreateGrid(Vector3 inOffset, Vector3 inDimensions, int inDivisionX, int inDivisionY, int inDivisionZ)
        {
            double xStep = inDimensions.x / inDivisionX;
            double yStep = inDimensions.y / inDivisionY;
            double zStep = inDimensions.z / inDivisionZ;

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
                        0.0
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
                        0.0,
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
                        0.0,
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

            result.BufferUsageHint = BufferUsageHint.StaticDraw;
            result.Topology = EGLMeshTopology.Lines;
            result.Positions = vertices.ToArray();
            result.Indices = Enumerable.Range(0, vertices.Count).ToArray();

            return result;
        }
    }
}
