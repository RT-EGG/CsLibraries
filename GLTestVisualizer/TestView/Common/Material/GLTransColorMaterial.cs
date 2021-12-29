using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView
{
    class GLTransColorMaterial : GLMaterial
    {
        public GLTransColorMaterial()
        {
            base.Shader = new GLRenderShaderProgram.Color();

            RenderLevel = EGLRenderLevel.Transparent;
            BlendParameters = new GLBlendParameters {
                SourceFactor = BlendingFactor.SrcAlpha,
                DestinationFactor = BlendingFactor.OneMinusSrcAlpha
            };
            return;
        }

        protected override void CommitPropertiesCore()
        {
            SetVariableValue("inColor", Color);

            base.CommitPropertiesCore();
            return;
        }

        public Vector4 Color
        { get; set; } = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
