using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView.SphereMesh
{
    class GLSphereMaterial : GLMaterial
    {
        public GLSphereMaterial()
        {
            base.Shader = GLRenderShaderProgram.Preset.Color;
            return;
        }

        protected override void CommitPropertiesCore(GLRenderingStatus inRenderingStatus)
        {
            GetProperty<Vector4>("inColor").Value = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            GetProperty<Matrix4x4>("inProjectionMatrix").Value = inRenderingStatus.ProjectionMatrix.CurrentMatrix;
            GetProperty<Matrix4x4>("inModelviewMatrix").Value = inRenderingStatus.ModelViewMatrix.CurrentMatrix;

            base.CommitPropertiesCore(inRenderingStatus);
            return;
        }

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
