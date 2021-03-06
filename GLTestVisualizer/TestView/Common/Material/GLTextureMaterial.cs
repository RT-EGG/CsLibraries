using OpenTK.Graphics.OpenGL4;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView
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

        protected override void CommitPropertiesCore()
        {
            SetVariableValue("inTexture", TextureReference);

            base.CommitPropertiesCore();
            return;
        }

        public GLTextureReference TextureReference
        { get; } = new GLTextureReference();
        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
