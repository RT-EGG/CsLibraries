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

        protected override void CommitPropertiesCore()
        {
            SetVariableValue("inColor", new Vector4(Color, 1.0f));

            base.CommitPropertiesCore();
            return;
        }

        public Vector3 Color
        { get; set; } = new Vector3(1.0f);
        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
