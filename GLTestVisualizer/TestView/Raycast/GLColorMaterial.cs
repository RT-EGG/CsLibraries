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

        protected override void CommitPropertiesCore(GLRenderingStatus inRenderingStatus)
        {
            GetProperty<Vector4>("inColor").Value = new Vector4(Color, 1.0f);
            GetProperty<Matrix4x4>("inProjectionMatrix").Value = inRenderingStatus.ProjectionMatrix.CurrentMatrix;
            GetProperty<Matrix4x4>("inModelviewMatrix").Value = inRenderingStatus.ModelViewMatrix.CurrentMatrix;

            base.CommitPropertiesCore(inRenderingStatus);
            return;
        }

        public Vector3 Color
        { get; set; } = new Vector3(1.0f);
        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
