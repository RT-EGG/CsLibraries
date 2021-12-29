using RtCs.OpenGL;

namespace GLTestVisualizer.TestView
{
    class GLVertexColorMaterial : GLMaterial
    {
        public GLVertexColorMaterial()
        {
            base.Shader = GLRenderShaderProgram.Preset.VertexColor;
            return;
        }

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
