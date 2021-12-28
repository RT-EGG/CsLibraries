using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView.Octree
{
    class GLSolidColorMaterial : GLMaterial
    {
        public GLSolidColorMaterial()
        {
            base.Shader = GLRenderShaderProgram.Preset.Color;
            return;
        }

        protected override void CommitPropertiesCore()
        {
            SetVariableValue("inColor", new Vector4(1.0f));

            base.CommitPropertiesCore();
            return;
        }

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
