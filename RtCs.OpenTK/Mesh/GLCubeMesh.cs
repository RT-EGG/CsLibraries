using RtCs.MathUtils;

namespace RtCs.OpenGL
{
    public partial class GLPrimitiveMesh
    {
        public static GLMesh CreateCube()
            => CreateCube(1.0, 1.0, 1.0);

        public static GLMesh CreateCube(double inSizeX, double inSizeY, double inSizeZ)
        {
            GLMesh mesh = new GLMesh();
            mesh.Clear();

            double hx = inSizeX * 0.5;
            double hy = inSizeY * 0.5;
            double hz = inSizeZ * 0.5;

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
                new Vector3(-1.0, 0.0, 0.0),
                new Vector3(-1.0, 0.0, 0.0),
                new Vector3(-1.0, 0.0, 0.0),
                new Vector3(-1.0, 0.0, 0.0),
                // +x
                new Vector3(1.0, 0.0, 0.0),
                new Vector3(1.0, 0.0, 0.0),
                new Vector3(1.0, 0.0, 0.0),
                new Vector3(1.0, 0.0, 0.0),
                // -y
                new Vector3(0.0, -1.0, 0.0),
                new Vector3(0.0, -1.0, 0.0),
                new Vector3(0.0, -1.0, 0.0),
                new Vector3(0.0, -1.0, 0.0),
                // +y
                new Vector3(0.0, 1.0, 0.0),
                new Vector3(0.0, 1.0, 0.0),
                new Vector3(0.0, 1.0, 0.0),
                new Vector3(0.0, 1.0, 0.0),
                // -z
                new Vector3(0.0, 0.0, -1.0),
                new Vector3(0.0, 0.0, -1.0),
                new Vector3(0.0, 0.0, -1.0),
                new Vector3(0.0, 0.0, -1.0),
                // +z
                new Vector3(0.0, 0.0, 1.0),
                new Vector3(0.0, 0.0, 1.0),
                new Vector3(0.0, 0.0, 1.0),
                new Vector3(0.0, 0.0, 1.0)
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

        public static GLMesh CreateCube(Vector3 inSize)
            => CreateCube(inSize.x, inSize.y, inSize.z);
    }
}
