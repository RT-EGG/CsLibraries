using OpenTK.Graphics.OpenGL4;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView.Texture
{
    class GLTextureMaterial : GLMaterial
    {
        public GLTextureMaterial()
        {
            base.Shader = GLRenderShaderProgram.Preset.Texture;

            RenderLevel = EGLRenderLevel.Transparent;
            BlendParameters = new GLBlendParameters(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            return;
        }

        public override void CommitProperties(GLRenderingStatus inRenderingStatus)
        {
            SetPropertyValue("inTexture", TextureReference);
            SetPropertyValue("inProjectionMatrix", inRenderingStatus.ProjectionMatrix.CurrentMatrix);
            SetPropertyValue("inModelviewMatrix", inRenderingStatus.ModelViewMatrix.CurrentMatrix);

            base.CommitProperties(inRenderingStatus);
            return;
        }

        public GLTextureReference TextureReference
        { get; } = new GLTextureReference();
        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
