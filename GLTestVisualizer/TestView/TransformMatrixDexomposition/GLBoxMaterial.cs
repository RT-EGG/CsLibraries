using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView.TransformMatrixDexomposition
{
    class GLBoxMaterial : GLMaterial
    {
        public GLBoxMaterial()
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
