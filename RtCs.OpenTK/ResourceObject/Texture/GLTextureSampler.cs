using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

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
        {            
            m_MinFilter.Subscribe(_ => DoChanged());
            m_MagFilter.Subscribe(_ => DoChanged());
            m_MinLod.Subscribe(_ => DoChanged());
            m_MaxLod.Subscribe(_ => DoChanged());
            m_WrapS.Subscribe(_ => DoChanged());
            m_WrapT.Subscribe(_ => DoChanged());
            m_WrapR.Subscribe(_ => DoChanged());
            m_BorderColor.Subscribe(_ => DoChanged());
            m_CompareMode.Subscribe(_ => DoChanged());
            m_CompareFunc.Subscribe(_ => DoChanged());

            m_AdaptationList.Add((m_MinFilter, () => GL.SamplerParameter(ID, SamplerParameterName.TextureMinFilter, (int)MinFilter)));
            m_AdaptationList.Add((m_MagFilter, () => GL.SamplerParameter(ID, SamplerParameterName.TextureMagFilter, (int)MagFilter)));
            m_AdaptationList.Add((m_MinLod, () => GL.SamplerParameter(ID, SamplerParameterName.TextureMinLod, MinLod)));
            m_AdaptationList.Add((m_MaxLod, () => GL.SamplerParameter(ID, SamplerParameterName.TextureMaxLod, MaxLod)));
            m_AdaptationList.Add((m_WrapT, () => GL.SamplerParameter(ID, SamplerParameterName.TextureWrapT, (int)WrapT)));
            m_AdaptationList.Add((m_WrapS, () => GL.SamplerParameter(ID, SamplerParameterName.TextureWrapS, (int)WrapS)));
            m_AdaptationList.Add((m_WrapR, () => GL.SamplerParameter(ID, SamplerParameterName.TextureWrapR, (int)WrapR)));
            m_AdaptationList.Add((m_BorderColor, () => GL.SamplerParameter(ID, SamplerParameterName.TextureBorderColor, BorderColor.Cast<float>().ToArray())));
            m_AdaptationList.Add((m_CompareMode, () => GL.SamplerParameter(ID, SamplerParameterName.TextureCompareMode, (int)CompareMode)));
            m_AdaptationList.Add((m_CompareFunc, () => GL.SamplerParameter(ID, SamplerParameterName.TextureCompareFunc, (int)CompareFunc)));
            return;
        }

        public EGLTextureMinFilter MinFilter
        { get => m_MinFilter.Value; set => m_MinFilter.Value = value; }
        public EGLTextureMagFilter MagFilter
        { get => m_MagFilter.Value; set => m_MagFilter.Value = value; }
        public float MinLod
        { get => m_MinLod.Value; set => m_MinLod.Value = value; }
        public float MaxLod
        { get => m_MaxLod.Value; set => m_MaxLod.Value = value; }
        public EGLTextureWrapMode WrapS
        { get => m_WrapS.Value; set => m_WrapS.Value = value ; }
        public EGLTextureWrapMode WrapT
        { get => m_WrapT.Value; set => m_WrapT.Value = value; }
        public EGLTextureWrapMode WrapR
        { get => m_WrapR.Value; set => m_WrapR.Value = value; }
        public Vector4 BorderColor
        { get => m_BorderColor.Value; set => m_BorderColor.Value = value; }
        public EGLTextureCompareMode CompareMode
        { get => m_CompareMode.Value; set => m_CompareMode.Value = value; }
        public EGLCompareFunc CompareFunc
        { get => m_CompareFunc.Value; set => m_CompareFunc.Value = value; }

        public void Apply()
        {
            foreach (var (value, adaptation) in m_AdaptationList) {
                if (value.CheckAndTurnOff()) {
                    adaptation();
                }
            }            
            m_ValueChanged = false;
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

        private void DoChanged()
        {
            if (!m_ValueChanged) {
                new GLMainThreadTask(_ => Apply());
            }
            m_ValueChanged = true;
            return;
        }

        private ModificationRecordValue<EGLTextureMinFilter> m_MinFilter = new ModificationRecordValue<EGLTextureMinFilter>(EGLTextureMinFilter.NearestMipmapLinear);
        private ModificationRecordValue<EGLTextureMagFilter> m_MagFilter = new ModificationRecordValue<EGLTextureMagFilter>(EGLTextureMagFilter.Linear);
        private ModificationRecordValue<float> m_MinLod = new ModificationRecordValue<float>(-1000);
        private ModificationRecordValue<float> m_MaxLod = new ModificationRecordValue<float>(1000);
        private ModificationRecordValue<EGLTextureWrapMode> m_WrapS = new ModificationRecordValue<EGLTextureWrapMode>(EGLTextureWrapMode.Repeat);
        private ModificationRecordValue<EGLTextureWrapMode> m_WrapT = new ModificationRecordValue<EGLTextureWrapMode>(EGLTextureWrapMode.Repeat);
        private ModificationRecordValue<EGLTextureWrapMode> m_WrapR = new ModificationRecordValue<EGLTextureWrapMode>(EGLTextureWrapMode.Repeat);
        private ModificationRecordValue<Vector4> m_BorderColor = new ModificationRecordValue<Vector4>(new Vector4(0.0));
        private ModificationRecordValue<EGLTextureCompareMode> m_CompareMode = new ModificationRecordValue<EGLTextureCompareMode>(EGLTextureCompareMode.None);
        private ModificationRecordValue<EGLCompareFunc> m_CompareFunc = new ModificationRecordValue<EGLCompareFunc>(EGLCompareFunc.Always);
        private bool m_ValueChanged = false;

        private List<(IModificationRecordValue Value, Action Adaptation)> m_AdaptationList = new List<(IModificationRecordValue Value, Action Adaptation)>();
    }
}
