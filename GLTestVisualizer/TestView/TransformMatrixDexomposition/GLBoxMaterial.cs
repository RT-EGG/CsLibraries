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

        protected override void CommitPropertiesCore(GLRenderParameter inParamter)
        {
            GetProperty<Matrix4x4>("inProjectionMatrix").Value = inParamter.ProjectionMatrix.CurrentMatrix;
            GetProperty<Matrix4x4>("inModelviewMatrix").Value = inParamter.ModelViewMatrix.CurrentMatrix;

            base.CommitPropertiesCore(inParamter);
            return;
        }

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
