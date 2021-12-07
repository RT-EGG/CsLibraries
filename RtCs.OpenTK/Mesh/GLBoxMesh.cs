using RtCs.MathUtils;
using RtCs.MathUtils.Geometry;

namespace RtCs.OpenGL
{
    public partial class GLPrimitiveMesh
    {
        public static GLMesh CreateBox()
            => CreateBox(1.0f, 1.0f, 1.0f);

        public static GLMesh CreateBox(float inSizeX, float inSizeY, float inSizeZ)
        {
            GLMesh mesh = new GLMesh();
            mesh.Clear();

            float hx = inSizeX * 0.5f;
            float hy = inSizeY * 0.5f;
            float hz = inSizeZ * 0.5f;

            mesh.Positions = new Vector3[24] {
                // -x
                new Vector3(-hx, -hy, -hz),
                new Vector3(-hx, -hy,  hz),
                new Vector3(-hx,  hy,  hz),
                new Vector3(-hx,  hy, -hz),
                // +x
                new Vector3( hx, -hy,  hz),
                new Vector3( hx, -hy, -hz),
                new Vector3( hx,  hy, -hz),
                new Vector3( hx,  hy,  hz),
                // -y
                new Vector3(-hx, -hy, -hz),
                new Vector3( hx, -hy, -hz),
                new Vector3( hx, -hy,  hz),
                new Vector3(-hx, -hy,  hz),
                // +y
                new Vector3(-hx,  hy, -hz),
                new Vector3(-hx,  hy,  hz),
                new Vector3( hx,  hy,  hz),
                new Vector3( hx,  hy, -hz),
                // -z
                new Vector3(-hx,  hy, -hz),
                new Vector3( hx,  hy, -hz),
                new Vector3( hx, -hy, -hz),
                new Vector3(-hx, -hy, -hz),
                // +z
                new Vector3(-hx,  hy,  hz),
                new Vector3(-hx, -hy,  hz),
                new Vector3( hx, -hy,  hz),
                new Vector3( hx,  hy,  hz)
            };

            mesh.Normals = new Vector3[24] {
                // -x
                new Vector3(-1.0f, 0.0f, 0.0f),
                new Vector3(-1.0f, 0.0f, 0.0f),
                new Vector3(-1.0f, 0.0f, 0.0f),
                new Vector3(-1.0f, 0.0f, 0.0f),
                // +x
                new Vector3(1.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 0.0f, 0.0f),
                // -y
                new Vector3(0.0f, -1.0f, 0.0f),
                new Vector3(0.0f, -1.0f, 0.0f),
                new Vector3(0.0f, -1.0f, 0.0f),
                new Vector3(0.0f, -1.0f, 0.0f),
                // +y
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
                // -z
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                // +z
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f)
            };

            mesh.Topology = EGLMeshTopology.Quads;
            mesh.Indices = new int[24] {
                // -x
                0, 1, 2, 3,
                // +x
                4, 5, 6, 7,
                // -y
                8, 9, 10, 11,
                // +y
                12, 13, 14, 15,
                // -z
                16, 17, 18, 19,
                // +z
                20, 21, 22, 23
            };

            mesh.BoundingBox = new AABB3D {
                MinX = -hx,
                MaxX =  hx,
                MinY = -hy,
                MaxY =  hy,
                MinZ = -hz,
                MaxZ =  hz
            };
            return mesh;
        }

        public static GLMesh CreateBox(Vector3 inSize)
            => CreateBox(inSize.x, inSize.y, inSize.z);
    }
}
