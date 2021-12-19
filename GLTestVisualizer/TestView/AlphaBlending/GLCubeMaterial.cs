using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView.AlphaBlending
{
    class GLCubeMaterial : GLMaterial
    {
        public GLCubeMaterial(Vector4 inColor)
        {
            base.Shader = new GLRenderShaderProgram.Color();
            Color = inColor;

            RenderLevel = EGLRenderLevel.Transparent;
            BlendParameters = new GLBlendParameters {
                SourceFactor = BlendingFactor.SrcAlpha,
                DestinationFactor = BlendingFactor.OneMinusSrcAlpha
            };
            return;
        }

        protected override void CommitPropertiesCore(GLRenderingStatus inRenderingStatus)
        {
            GetProperty<Vector4>("inColor").Value = Color;
            GetProperty<Matrix4x4>("inProjectionMatrix").Value = inRenderingStatus.ProjectionMatrix.CurrentMatrix;
            GetProperty<Matrix4x4>("inModelviewMatrix").Value = inRenderingStatus.ModelViewMatrix.CurrentMatrix;

            base.CommitPropertiesCore(inRenderingStatus);
            return;
        }

        public Vector4 Color
        { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
