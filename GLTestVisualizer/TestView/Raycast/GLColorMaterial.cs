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

        public override void CommitProperties(GLRenderingStatus inRenderingStatus)
        {
            GetProperty<Vector4>("inColor").Value = new Vector4(Color, 1.0);
            GetProperty<Matrix4x4>("inProjectionMatrix").Value = inRenderingStatus.ProjectionMatrix.CurrentMatrix;
            GetProperty<Matrix4x4>("inModelviewMatrix").Value = inRenderingStatus.ModelViewMatrix.CurrentMatrix;

            base.CommitProperties(inRenderingStatus);
            return;
        }

        public Vector3 Color
        { get; set; } = new Vector3(1.0);
        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
