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
            mesh.Colors = new Vector4[] {
                new Vector4(1.0f, 0.0f, 0.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(0.0f, 1.0f, 0.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(0.0f, 0.0f, 1.0f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f)
            };
            mesh.Topology = EGLMeshTopology.Lines;
            mesh.Indices = Enumerable.Range(0, 6).ToArray();

            Renderer.Mesh = mesh;
            Renderer.Material = new Material();
            return;
        }

        private class Material : GLMaterial
        {
            public Material()
            {
                base.Shader = GLRenderShaderProgram.Preset.VertexColor;
                return;
            }

            protected override void CommitPropertiesCore(GLRenderingStatus inRenderingStatus)
            {
                GetProperty<Matrix4x4>("inProjectionMatrix").Value = inRenderingStatus.ProjectionMatrix.CurrentMatrix;
                GetProperty<Matrix4x4>("inModelviewMatrix").Value = inRenderingStatus.ModelViewMatrix.CurrentMatrix;

                base.CommitPropertiesCore(inRenderingStatus);
                return;
            }

            private new GLRenderShaderProgram Shader => base.Shader;
        }
    }
}
