using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView.SphereMesh
{
    class GLSphereMaterial : GLMaterial
    {
        public GLSphereMaterial()
        {
            base.Shader = GLRenderShaderProgram.Preset.Color;
            return;
        }

        protected override void CommitPropertiesCore()
        {
            GetVariable<Vector4>("inColor").Value = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

            base.CommitPropertiesCore();
            return;
        }

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
