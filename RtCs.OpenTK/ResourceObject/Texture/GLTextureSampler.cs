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

    /// <summary>
    /// OpenGL texture sampler object.
    /// </summary>
    /// <remarks>
    /// To understand sampler object, see OpenGL official [wiki](https://www.khronos.org/opengl/wiki/Sampler_Object).
    /// </remarks>
    public class GLTextureSampler : GLResourceIdObject
    {
        public GLTextureSampler()
            => Apply();

        /// <summary>
        /// Get or set MinFilter.
        /// </summary>
        /// <remarks>
        /// The change is not applied internally until call Apply().
        /// </remarks>
        public EGLTextureMinFilter MinFilter
        { get; set; } = EGLTextureMinFilter.NearestMipmapLinear;
        /// <summary>
        /// Get or set MagFilter.
        /// </summary>
        /// <remarks>
        /// The change is not applied internally until call Apply().
        /// </remarks>
        public EGLTextureMagFilter MagFilter
        { get; set; } = EGLTextureMagFilter.Linear;
        /// <summary>
        /// Get or set MinLOD.
        /// </summary>
        /// <remarks>
        /// The change is not applied internally until call Apply().
        /// </remarks>
        public float MinLod
        { get; set; } = 1000.0f;
        /// <summary>
        /// Get or set MaxLOD.
        /// </summary>
        /// <remarks>
        /// The change is not applied internally until call Apply().
        /// </remarks>
        public float MaxLod
        { get; set; } = 1000.0f;
        /// <summary>
        /// Get or set WrapS.
        /// </summary>
        /// <remarks>
        /// The change is not applied internally until call Apply().
        /// </remarks>
        public EGLTextureWrapMode WrapS
        { get; set; } = EGLTextureWrapMode.Repeat;
        /// <summary>
        /// Get or set WrapT.
        /// </summary>
        /// <remarks>
        /// The change is not applied internally until call Apply().
        /// </remarks>
        public EGLTextureWrapMode WrapT
        { get; set; } = EGLTextureWrapMode.Repeat;
        /// <summary>
        /// Get or set WrapR.
        /// </summary>
        /// <remarks>
        /// The change is not applied internally until call Apply().
        /// </remarks>
        public EGLTextureWrapMode WrapR
        { get; set; } = EGLTextureWrapMode.Repeat;
        /// <summary>
        /// Get or set BorderColor.
        /// </summary>
        /// <remarks>
        /// The change is not applied internally until call Apply().
        /// </remarks>
        public ColorRGBA BorderColor
        { get; set; } = new ColorRGBA(0, 0, 0, 0);
        /// <summary>
        /// Get or set CompareMode.
        /// </summary>
        /// <remarks>
        /// The change is not applied internally until call Apply().
        /// </remarks>
        public EGLTextureCompareMode CompareMode
        { get; set; } = EGLTextureCompareMode.None;
        /// <summary>
        /// Get or set CompareFunc.
        /// </summary>
        /// <remarks>
        /// The change is not applied internally until call Apply().
        /// </remarks>
        public EGLCompareFunc CompareFunc
        { get; set; } = EGLCompareFunc.LEqual;

        /// <summary>
        /// Apply all changes.
        /// </summary>
        public void Apply()
        {
            if (!m_ApplyRegistered) {
                m_ApplyRegistered = true;
                GLMainThreadTask.CreateNew(_ => ApplyChanged());
            }
            return;
        }

        protected override void CreateResourceCore()
        {
            base.CreateResourceCore();
            ID = GL.GenSampler();            
            return;
        }

        protected override void DestroyResourceCore()
        {
            base.DestroyResourceCore();
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
