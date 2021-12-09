using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System.Linq;

namespace RtCs.OpenGL
{
    public enum EGLTextureMinFilter
    {
        Nearest = All.Nearest,
        Linear = All.Linear,
        NearestMipmapNearest = All.NearestMipmapNearest,
        LinearMipmapNearest = All.LinearMipmapNearest,
        NearestMipmapLinear = All.NearestMipmapLinear,
        LinearMipmapLinear = All.LinearMipmapLinear
    }

    public enum EGLTextureMagFilter
    {
        Nearest = All.Nearest,
        Linear = All.Linear
    }

    public enum EGLTextureWrapMode
    {
        ClampToEdge = All.ClampToEdge,
        ClampToBorder = All.ClampToBorder,
        Repeat = All.Repeat,
        MirroredRepeat = All.MirroredRepeat,
        MirrorClampToEdge = All.MirrorClampToEdge,
    }

    public enum EGLTextureCompareMode
    {
        None = All.None,
        CompareRefToTexture = All.CompareRefToTexture
    }

    public class GLTextureSampler : GLResourceIdObject
    {
        public GLTextureSampler()
            => Apply();

        public EGLTextureMinFilter MinFilter
        { get; set; } = EGLTextureMinFilter.NearestMipmapLinear;
        public EGLTextureMagFilter MagFilter
        { get; set; } = EGLTextureMagFilter.Linear;
        public float MinLod
        { get; set; } = 1000.0f;
        public float MaxLod
        { get; set; } = 1000.0f;
        public EGLTextureWrapMode WrapS
        { get; set; } = EGLTextureWrapMode.Repeat;
        public EGLTextureWrapMode WrapT
        { get; set; } = EGLTextureWrapMode.Repeat;
        public EGLTextureWrapMode WrapR
        { get; set; } = EGLTextureWrapMode.Repeat;
        public ColorRGBA BorderColor
        { get; set; } = new ColorRGBA(0, 0, 0, 0);
        public EGLTextureCompareMode CompareMode
        { get; set; } = EGLTextureCompareMode.None;
        public EGLCompareFunc CompareFunc
        { get; set; } = EGLCompareFunc.LEqual;

        public void Apply()
        {
            if (!m_ApplyRegistered) {
                m_ApplyRegistered = true;
                new GLMainThreadTask(_ => ApplyChanged()).Enqueue();
            }
            return;
        }

        protected override void InternalCreateResource()
        {
            base.InternalCreateResource();
            ID = GL.GenSampler();            
            return;
        }

        protected override void InternalDestroyResource()
        {
            base.InternalDestroyResource();
            GL.DeleteSampler(ID);
            ID = 0;
            return;
        }

        private void ApplyChanged()
        {
            GL.SamplerParameter(ID, SamplerParameterName.TextureMinFilter, (int)MinFilter);
            GL.SamplerParameter(ID, SamplerParameterName.TextureMagFilter, (int)MagFilter);
            GL.SamplerParameter(ID, SamplerParameterName.TextureMinLod, MinLod);
            GL.SamplerParameter(ID, SamplerParameterName.TextureMaxLod, MaxLod);
            GL.SamplerParameter(ID, SamplerParameterName.TextureWrapS, (int)WrapS);
            GL.SamplerParameter(ID, SamplerParameterName.TextureWrapT, (int)WrapT);
            GL.SamplerParameter(ID, SamplerParameterName.TextureWrapR, (int)WrapR);
            GL.SamplerParameter(ID, SamplerParameterName.TextureBorderColor, BorderColor.Select<byte, float>(b => (float)b / 255.0f).ToArray());
            GL.SamplerParameter(ID, SamplerParameterName.TextureCompareMode, (int)CompareMode);
            GL.SamplerParameter(ID, SamplerParameterName.TextureCompareFunc, (int)CompareFunc);
            m_ApplyRegistered = false;
            return;
        }

        private bool m_ApplyRegistered = false;
    }
}
