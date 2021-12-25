using RtCs.MathUtils;
using RtCs.OpenGL;
using OpenTK.Graphics.OpenGL4;

namespace GLTestVisualizer.TestView.FrustumTest
{
    public class GLViewFrustumRendererObject : GLRenderObject
    {
        public GLViewFrustumRendererObject()
        {
            Renderer.Mesh = new Mesh();
            Renderer.Material = new Material();
            PolygonMode = EGLRenderPolygonMode.Line;
            return;
        }

        public Matrix4x4 ProjectionMatrix
        {
            get => m_ProjectionMatrix;
            set {
                m_ProjectionMatrix = value;
                (Renderer.Mesh as Mesh).UpdateMeshVertices(m_ProjectionMatrix);
            }
        }
        private Matrix4x4 m_ProjectionMatrix = Matrix4x4.Identity;

        protected override void DisposeObject(bool inDisposing)
        {
            Renderer.Mesh.Dispose();
            base.DisposeObject(inDisposing);

            return;
        }

        private class Material : GLMaterial
        {
            public Material()
            {
                base.Shader = GLRenderShaderProgram.Preset.Color;
                return;
            }

            protected override void CommitPropertiesCore()
            {
                GetVariable<Vector4>("inColor").Value = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

                base.CommitPropertiesCore();
                return;
            }
        }

        private class Mesh : GLMesh
        {
            public Mesh()
            {
                Vertices = new Vector3[8] {
                    new Vector3(-1.0f, -1.0f, -1.0f),
                    new Vector3(-1.0f, -1.0f,  1.0f),
                    new Vector3(-1.0f,  1.0f, -1.0f),
                    new Vector3(-1.0f,  1.0f,  1.0f),
                    new Vector3( 1.0f, -1.0f, -1.0f),
                    new Vector3( 1.0f, -1.0f,  1.0f),
                    new Vector3( 1.0f,  1.0f, -1.0f),
                    new Vector3( 1.0f,  1.0f,  1.0f)
                };
                Indices = new int[] {
                    0, 1, 1, 3, 3, 2, 2, 0, // -x
                    5, 4, 4, 6, 6, 7, 7, 5, // +x
                    0, 4, 1, 5, 2, 6, 3, 7
                };
                this.Topology = EGLMeshTopology.Lines;
                this.VertexBufferUsageHint = BufferUsageHint.DynamicDraw;
                return;
            }

            public void UpdateMeshVertices(Matrix4x4 inProjectionMatrix)
            {
                inProjectionMatrix.Inverse();

                Vector3[] positions = new Vector3[8] {
                    new Vector3(-1.0f, -1.0f, -1.0f),
                    new Vector3(-1.0f, -1.0f,  1.0f),
                    new Vector3(-1.0f,  1.0f, -1.0f),
                    new Vector3(-1.0f,  1.0f,  1.0f),
                    new Vector3( 1.0f, -1.0f, -1.0f),
                    new Vector3( 1.0f, -1.0f,  1.0f),
                    new Vector3( 1.0f,  1.0f, -1.0f),
                    new Vector3( 1.0f,  1.0f,  1.0f)
                };
                for (int i = 0; i < positions.Length; ++i) {
                    Vector4 v = inProjectionMatrix.Multiply(positions[i], 1.0f);
                    v = v / v.w;

                    positions[i] = new Vector3(v);
                }

                Vertices = positions;
                this.Apply();
                return;
            }
        }
    }
}
