using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView
{
    class GLColorMaterial : GLMaterial
    {
        public GLColorMaterial()
        {
            base.Shader = GLRenderShaderProgram.Preset.Color;
            return;
        }

        protected override void CommitPropertiesCore()
        {
            SetVariableValue("inColor", Color);

            base.CommitPropertiesCore();
            return;
        }

        public Vector4 Color
        { get; set; } = new Vector4(1.0f);
        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
