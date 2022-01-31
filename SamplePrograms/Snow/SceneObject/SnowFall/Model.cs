using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.OpenGL;

namespace Snow.SceneObject.SnowFall
{
    class Model
    {
        public Model()
        {
            m_RenderObject.BeforeRender += (sender, args) => {
                GL.Enable(EnableCap.PointSprite);
                GL.Enable(EnableCap.VertexProgramPointSize);
            };
            m_RenderObject.AfterRender += (sender, args) => {
                GL.Disable(EnableCap.PointSprite);
                GL.Disable(EnableCap.VertexProgramPointSize);
            };

            m_RenderObject.FrustumCullingMode = EGLFrustumCullingMode.AlwaysRender;
            m_RenderObject.Renderer.Mesh = m_Mesh;
            m_RenderObject.Renderer.Material = m_Material;
        }

        public void TimeStep(float inTimeSpan, SnowCover.Model inSnowCover)
        {
            m_Mesh.TimeStep(inTimeSpan, inSnowCover);
        }

        public Transform Transform => m_RenderObject.Transform;

        public void RegisterToDisplayList(GLDisplayList inList)
            => inList.Register(m_RenderObject);
        public void UnregisterFromDisplayList(GLDisplayList inList)
            => inList.Unregister(m_RenderObject);

        private GLRenderObject m_RenderObject = new GLRenderObject();

        private Mesh m_Mesh = new Mesh();
        private Material m_Material = new Material();
    }
}
