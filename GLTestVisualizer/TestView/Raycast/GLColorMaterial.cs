using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView.Raycast
{
    class GLColorMaterial : GLMaterial
    {
        public GLColorMaterial()
        {
            base.Shader = GLRenderShaderProgram.Preset.Color;
            return;
        }

        protected override void CommitPropertiesCore(GLRenderParameter inParameter)
        {
            GetProperty<Vector4>("inColor").Value = new Vector4(Color, 1.0f);
            GetProperty<Matrix4x4>("inProjectionMatrix").Value = inParameter.ProjectionMatrix.CurrentMatrix;
            GetProperty<Matrix4x4>("inModelviewMatrix").Value = inParameter.ModelViewMatrix.CurrentMatrix;

            base.CommitPropertiesCore(inParameter);
            return;
        }

        public Vector3 Color
        { get; set; } = new Vector3(1.0f);
        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
