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

        protected override void CommitPropertiesCore(GLRenderParameter inParameter)
        {
            GetProperty<Vector4>("inColor").Value = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            GetProperty<Matrix4x4>("inProjectionMatrix").Value = inParameter.ProjectionMatrix.CurrentMatrix;
            GetProperty<Matrix4x4>("inModelviewMatrix").Value = inParameter.ModelViewMatrix.CurrentMatrix;

            base.CommitPropertiesCore(inParameter);
            return;
        }

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
