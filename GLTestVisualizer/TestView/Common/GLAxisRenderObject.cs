using RtCs.MathUtils;
using RtCs.OpenGL;
using System.Linq;

namespace GLTestVisualizer.TestView
{
    class GLAxisRenderObject : GLRenderObject
    {
        public GLAxisRenderObject()
        {
            GLMesh mesh = new GLMesh();
            mesh.Vertices = new Vector3[] {
                new Vector3(), new Vector3(1.0f, 0.0f, 0.0f),
                new Vector3(), new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(), new Vector3(0.0f, 0.0f, 1.0f)
            };
            IGLVertexAttribute<Vector4> meshColors = mesh.AddAttribute(new GLVertexVector4AttributeDescriptor(GLVertexAttribute.AttributeName_Color));
            meshColors.Buffer = new Vector4[] {
                new Vector4(1.0f, 0.0f, 0.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(0.0f, 1.0f, 0.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(0.0f, 0.0f, 1.0f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f)
            };
            mesh.Topology = EGLMeshTopology.Lines;
            mesh.Indices = Enumerable.Range(0, 6).ToArray();
            mesh.Apply();

            Renderer.Mesh = mesh;
            Renderer.Material = new Material();

            CalculateBoundingBox();
            this.PolygonMode = EGLRenderPolygonMode.Line;
            return;
        }

        private class Material : GLMaterial
        {
            public Material()
            {
                base.Shader = GLRenderShaderProgram.Preset.VertexColor;
                return;
            }

            private new GLRenderShaderProgram Shader => base.Shader;
        }
    }
}
