using OpenTK.Graphics.OpenGL4;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView.Text
{
    class GLTextMaterial : GLMaterial
    {
        public GLTextMaterial()
        {
            base.Shader = GLRenderShaderProgram.Preset.Texture;

            RenderLevel = EGLRenderLevel.Transparent;
            BlendParameters = new GLBlendParameters(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            return;
        }

        protected override void CommitPropertiesCore(GLRenderParameter inParameter)
        {
            SetPropertyValue("inTexture", TextureReference);
            SetPropertyValue("inProjectionMatrix", inParameter.ProjectionMatrix.CurrentMatrix);
            SetPropertyValue("inModelviewMatrix", inParameter.ModelViewMatrix.CurrentMatrix);

            base.CommitPropertiesCore(inParameter);
            return;
        }

        public GLTextureReference TextureReference
        { get; } = new GLTextureReference();
        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
