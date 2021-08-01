using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView.FrustumTest
{
    class GLSphereMaterial : GLMaterial
    {
        public GLSphereMaterial()
        {
            base.Shader = GLRenderShaderProgram.Preset.Color;
            return;
        }

        public override void CommitProperties(GLRenderingStatus inRenderingStatus)
        {
            GetProperty<Vector4>("inColor").Value = IsInFrustum ? new Vector4(1.0, 0.0, 0.0, 1.0) : new Vector4(0.0, 0.0, 1.0, 1.0);
            GetProperty<Matrix4x4>("inProjectionMatrix").Value = inRenderingStatus.ProjectionMatrix.CurrentMatrix;
            GetProperty<Matrix4x4>("inModelviewMatrix").Value = inRenderingStatus.ModelViewMatrix.CurrentMatrix;

            base.CommitProperties(inRenderingStatus);
            return;
        }

        public bool IsInFrustum
        { get; set; } = false;

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
