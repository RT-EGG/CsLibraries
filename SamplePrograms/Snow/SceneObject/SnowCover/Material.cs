using RtCs;
using RtCs.MathUtils;
using RtCs.OpenGL;
using RtCs.OpenGL.WinForms;
using System.IO;

namespace Snow.SceneObject.SnowCover
{
    enum RenderMode
    {
        Surface,
        Normal,
        Offset,
    }

    class Material : GLMaterial
    {
        public Material()
        {
            base.Shader = new Shader();
            return;
        }

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);
            if (inDisposing) {
                base.Shader.Dispose();
            }
        }

        protected override void CommitPropertiesCore()
        {
            SetVariableValue("inAmbient", Ambient);
            SetVariableValue("inDiffuse", Diffuse);
            SetVariableValue("inInner", InnerLevel);
            SetVariableValue("inOuter", OuterLevel);
            SetVariableValue("inHeightMap", HeightMap);
            SetVariableValue("inMinHeight", MinHeight);
            SetVariableValue("inMaxHeight", MaxHeight);
            SetVariableValue("inChannelVisibility", ChannelVisibility);
            SetVariableValue("inRenderMode", (int)RenderMode);
            SetVariableValue("inSurfaceTexture", SurfaceTexture);

            base.CommitPropertiesCore();
            return;
        }

        public Vector3 Ambient
        { get; set; } = new Vector3(0.5f, 0.5f, 0.6f);
        public Vector3 Diffuse
        { get; set; } = new Vector3(0.5f, 0.5f, 0.6f);
        public float InnerLevel
        { get; set; } = 64.0f;
        public float OuterLevel
        { get; set; } = 64.0f;
        public float MinHeight
        { get; set; } = 0.0f;
        public float MaxHeight
        { get; set; } = 2.0f;
        public Container3<bool> ChannelVisibility
        { get; set; } = new Container3<bool>(true, true, true);
        public GLTextureReference HeightMap
        { get; } = new GLTextureReference();
        public GLTextureReference SurfaceTexture
        { get; } = new GLTextureReference();
        public RenderMode RenderMode
        { get; set; } = RenderMode.Surface;
        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
