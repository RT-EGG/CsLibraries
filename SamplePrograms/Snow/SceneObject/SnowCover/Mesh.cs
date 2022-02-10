using RtCs.MathUtils;
using RtCs.OpenGL;

namespace Snow.SceneObject.SnowCover
{
    class Mesh : GLMesh
    {
        public Mesh()
        {
            Vertices = new Vector3[] {
                new Vector3(-0.5f,  0.5f, 0.0f),
                new Vector3(-0.5f, -0.5f, 0.0f),
                new Vector3( 0.5f, -0.5f, 0.0f),
                new Vector3( 0.5f,  0.5f, 0.0f)
            };

            IGLVertexAttribute<Vector3> normals = AddAttribute<Vector3>(GLVertexAttribute.AttributeName_Normal,
                                                                        new GLVertexAttributeDescriptor(GLVertexAttribute.AttributeName_Normal, 3, sizeof(float)));
            normals.Buffer = new Vector3[] {
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f)
            };
            IGLVertexAttribute<Vector2> texCoords = AddAttribute<Vector2>(GLVertexAttribute.AttributeName_Normal,
                                                                          new GLVertexAttributeDescriptor(GLVertexAttribute.AttributeName_TexCoord, 2, sizeof(float)));
            texCoords.Buffer = new Vector2[] {
                new Vector2(0.0f, 1.0f),
                new Vector2(0.0f, 0.0f),
                new Vector2(1.0f, 0.0f),
                new Vector2(1.0f, 1.0f)
            };
            
            CalculateBoundingBox();

            Topology = EGLMeshTopology.PatchedQuads;
            Indices = new int[] {
                0, 1, 2, 3
            };

            Apply();            
        }
    }
}
