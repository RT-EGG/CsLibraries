using RtCs.MathUtils;
using RtCs.OpenGL;
using System.Linq;

namespace GLTestVisualizer.TestView
{
    class GLAxisRenderObject : GLRenderer
    {
        public GLAxisRenderObject()
        {
            GLMesh mesh = new GLMesh();
            mesh.Vertices = new Vector3[] {
                new Vector3(), new Vector3(1.0f, 0.0f, 0.0f),
                new Vector3(), new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(), new Vector3(0.0f, 0.0f, 1.0f)
            };
            IGLVertexAttribute<Vector4> meshColors = mesh.AddAttribute<Vector4>(GLVertexAttribute.AttributeName_Color,
                                                                                new GLVertexAttributeDescriptor(GLVertexAttribute.AttributeName_Color, 4, sizeof(float)));
            meshColors.Buffer = new Vector4[] {
                new Vector4(1.0f, 0.0f, 0.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(0.0f, 1.0f, 0.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(0.0f, 0.0f, 1.0f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f)
            };
            mesh.Topology = EGLMeshTopology.Lines;
            mesh.Indices = Enumerable.Range(0, 6).ToArray();
            mesh.Apply();

            Mesh = mesh;
            Material = new AxisRenderMaterial();

            return;
        }

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);

            if (inDisposing) {
                Mesh.Dispose();
                Material.Dispose();
            }
        }

        private class AxisRenderMaterial : GLMaterial
        {
            public AxisRenderMaterial()
            {
                base.Shader = GLRenderShaderProgram.Preset.VertexColor;
                return;
            }

            private new GLRenderShaderProgram Shader => base.Shader;
        }
    }
}
