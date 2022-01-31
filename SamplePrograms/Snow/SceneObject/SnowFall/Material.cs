using OpenTK.Graphics.OpenGL4;
using RtCs.OpenGL;

namespace Snow.SceneObject.SnowFall
{
    class Material : GLMaterial
    {
        public Material()
        {
            RenderLevel = EGLRenderLevel.Transparent;
            BlendParameters = new GLBlendParameters {
                SourceFactor = BlendingFactor.SrcAlpha,
                DestinationFactor = BlendingFactor.OneMinusSrcAlpha
            };
            base.Shader = new Shader();
            return;
        }

        protected override void CommitPropertiesCore()
        {
            base.CommitPropertiesCore();
        }

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
