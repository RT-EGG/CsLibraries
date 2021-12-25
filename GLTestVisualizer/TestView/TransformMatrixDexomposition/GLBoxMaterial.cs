using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView.TransformMatrixDexomposition
{
    class GLBoxMaterial : GLMaterial
    {
        public GLBoxMaterial()
        {
            base.Shader = GLRenderShaderProgram.Preset.VertexColor;
            return;
        }

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
