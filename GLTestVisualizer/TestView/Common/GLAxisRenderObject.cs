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
            mesh.Positions = new Vector3[] {
                new Vector3(), new Vector3(1.0, 0.0, 0.0),
                new Vector3(), new Vector3(0.0, 1.0, 0.0),
                new Vector3(), new Vector3(0.0, 0.0, 1.0)
            };
            mesh.Colors = new Vector4[] {
                new Vector4(1.0, 0.0, 0.0, 1.0), new Vector4(1.0, 0.0, 0.0, 1.0),
                new Vector4(0.0, 1.0, 0.0, 1.0), new Vector4(0.0, 1.0, 0.0, 1.0),
                new Vector4(0.0, 0.0, 1.0, 1.0), new Vector4(0.0, 0.0, 1.0, 1.0)
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

            public override void CommitProperties(GLRenderingStatus inRenderingStatus)
            {
                GetProperty<Matrix4x4>("inProjectionMatrix").Value = inRenderingStatus.ProjectionMatrix.CurrentMatrix;
                GetProperty<Matrix4x4>("inModelviewMatrix").Value = inRenderingStatus.ModelViewMatrix.CurrentMatrix;

                base.CommitProperties(inRenderingStatus);
                return;
            }

            private new GLRenderShaderProgram Shader => base.Shader;
        }
    }
}
