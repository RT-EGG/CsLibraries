using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView.FrustumTest
{
    class GLSphereMaterial : GLMaterial
    {
        public GLSphereMaterial()
        {
            base.Shader = GLRenderShaderProgram.Preset.Color;
            return;
        }

        protected override void CommitPropertiesCore(GLRenderParameter inParameter)
        {
            GetProperty<Vector4>("inColor").Value = IsInFrustum ? new Vector4(1.0f, 0.0f, 0.0f, 1.0f) : new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
            GetProperty<Matrix4x4>("inProjectionMatrix").Value = inParameter.ProjectionMatrix.CurrentMatrix;
            GetProperty<Matrix4x4>("inModelviewMatrix").Value = inParameter.ModelViewMatrix.CurrentMatrix;

            base.CommitPropertiesCore(inParameter);
            return;
        }

        public bool IsInFrustum
        { get; set; } = false;

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
