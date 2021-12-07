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

        public override void CommitProperties(GLRenderingStatus inRenderingStatus)
        {
            GetProperty<Vector4>("inColor").Value = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            GetProperty<Matrix4x4>("inProjectionMatrix").Value = inRenderingStatus.ProjectionMatrix.CurrentMatrix;
            GetProperty<Matrix4x4>("inModelviewMatrix").Value = inRenderingStatus.ModelViewMatrix.CurrentMatrix;

            base.CommitProperties(inRenderingStatus);
            return;
        }

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
