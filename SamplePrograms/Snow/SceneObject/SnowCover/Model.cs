using Reactive.Bindings;
using RtCs;
using RtCs.MathUtils;
using RtCs.MathUtils.Geometry;
using RtCs.OpenGL;
using RtCs.OpenGL.WinForms;
using Snow.View;
using System;
using System.IO;

namespace Snow.SceneObject.SnowCover
{
    class Model : GLObject, ISnowCoverVisibilityConfigurationModel
    {
        public Model()
        {
            m_HeightMap.Apply();
            m_HeightMapSampler.MinFilter = EGLTextureMinFilter.Linear;
            m_HeightMapSampler.MagFilter = EGLTextureMagFilter.Linear;
            m_HeightMapSampler.WrapS = EGLTextureWrapMode.ClampToEdge;
            m_HeightMapSampler.WrapT = EGLTextureWrapMode.ClampToEdge;
            m_HeightMapSampler.Apply();
            m_Material.HeightMap.Texture = m_HeightMap;
            m_Material.HeightMap.Sampler = m_HeightMapSampler;

            string surfaceImagePath = ".\\resources\\SnowSurface.jpg";
            if (File.Exists(surfaceImagePath)) {
                new GLTextureImageImporter().ImportFromFile(surfaceImagePath, m_SurfaceTexture);
            }

            m_SurfaceTextureSampler.MinFilter = EGLTextureMinFilter.Linear;
            m_SurfaceTextureSampler.MagFilter = EGLTextureMagFilter.Linear;
            m_SurfaceTextureSampler.WrapS = EGLTextureWrapMode.MirroredRepeat;
            m_SurfaceTextureSampler.WrapT = EGLTextureWrapMode.MirroredRepeat;
            m_SurfaceTextureSampler.Apply();
            m_Material.SurfaceTexture.Texture = m_SurfaceTexture;
            m_Material.SurfaceTexture.Sampler = m_SurfaceTextureSampler;

            m_RenderObject.FrustumCullingMode = EGLFrustumCullingMode.AlwaysRender;

            m_RenderObject.Renderer.Mesh = m_Mesh;
            m_RenderObject.Renderer.Material = m_Material;

            ChannelVisibilityR.Subscribe(v => m_Material.ChannelVisibility = new Container3<bool>(ChannelVisibilityR.Value, ChannelVisibilityG.Value, ChannelVisibilityB.Value));
            ChannelVisibilityG.Subscribe(v => m_Material.ChannelVisibility = new Container3<bool>(ChannelVisibilityR.Value, ChannelVisibilityG.Value, ChannelVisibilityB.Value));
            ChannelVisibilityB.Subscribe(v => m_Material.ChannelVisibility = new Container3<bool>(ChannelVisibilityR.Value, ChannelVisibilityG.Value, ChannelVisibilityB.Value));
            RenderPolygonMode.Subscribe(v => m_RenderObject.PolygonMode = v);
            InnerLevel.Subscribe(v => m_Material.InnerLevel = v);
            OuterLevel.Subscribe(v => m_Material.OuterLevel = v);
            HeightScale.Subscribe(v => m_Material.MaxHeight = v);
            RenderMode.Subscribe(v => m_Material.RenderMode = v);

            Randomize((float)(new Random().NextDouble()));
        }

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);
            if (inDisposing) {
                m_Mesh.Dispose();
                m_Material.Dispose();
                m_HeightMap.Dispose();
                m_HeightMapSampler.Dispose();
            }
        }

        public void Randomize(float inSeed)
            => m_HeightMap.Randomize(512, 512, inSeed);

        public Transform Transform => m_RenderObject.Transform;

        public HeightMap HeightMap => m_HeightMap;

        public AABB3D LandBounds
        {
            get {
                Vector3 scale = Transform.LocalScale;

                AABB3D result = new AABB3D();
                result.Center = Transform.WorldPosition;
                result.Size = new Vector3(1.0f * scale.x, 0.0f, 1.0f * scale.y);
                return result;
            }
        }

        public IReactiveProperty<bool> ChannelVisibilityR
        { get; } = new ReactiveProperty<bool>(true);
        public IReactiveProperty<bool> ChannelVisibilityG
        { get; } = new ReactiveProperty<bool>(true);
        public IReactiveProperty<bool> ChannelVisibilityB
        { get; } = new ReactiveProperty<bool>(true);
        public IReactiveProperty<EGLRenderPolygonMode> RenderPolygonMode
        { get; } = new ReactiveProperty<EGLRenderPolygonMode>(EGLRenderPolygonMode.Face);
        public IReactiveProperty<float> InnerLevel
        { get; } = new ReactiveProperty<float>(64.0f);
        public IReactiveProperty<float> OuterLevel
        { get; } = new ReactiveProperty<float>(64.0f);
        public IReactiveProperty<float> HeightScale
        { get; } = new ReactiveProperty<float>(1.0f);
        public IReactiveProperty<RenderMode> RenderMode
        { get; } = new ReactiveProperty<RenderMode>(SnowCover.RenderMode.Surface);

        public void RegisterToDisplayList(GLDisplayList inList)
            => inList.Register(m_RenderObject);
        public void UnregisterFromDisplayList(GLDisplayList inList)
            => inList.Unregister(m_RenderObject);

        private GLRenderObject m_RenderObject = new GLRenderObject();

        private Mesh m_Mesh = new Mesh();
        private Material m_Material = new Material();
        private HeightMap m_HeightMap = new HeightMap();
        private GLTextureSampler m_HeightMapSampler = new GLTextureSampler();
        private GLColorTexture2D m_SurfaceTexture = new GLColorTexture2D(0, 0);
        private GLTextureSampler m_SurfaceTextureSampler = new GLTextureSampler();
    }
}
