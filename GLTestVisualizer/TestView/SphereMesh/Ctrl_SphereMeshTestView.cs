using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.OpenGL;
using System;

namespace GLTestVisualizer.TestView.SphereMesh
{
    public partial class Ctrl_SphereMeshTestView : GLTestVisualizer.TestView.Ctrl_TestView
    {
        public Ctrl_SphereMeshTestView()
        {
            InitializeComponent();
            return;
        }

        public override string SceneName => "SphereMesh";

        public override void Start()
        {
            GLColorMaterial material = new GLColorMaterial() {
                Color = new Vector4(1.0f)
            };

            m_UvSphere.Transform.LocalPosition = new Vector3(-2.5f, 0.0f, 0.0f);
            m_UvSphere.PolygonMode = EGLRenderPolygonMode.Line;
            m_UvSphere.Renderer.Mesh = GLPrimitiveMesh.CreateSphereUV(5, 5);
            m_UvSphere.Renderer.Material = material;
            m_UvSphere.FrustumCullingMode = EGLFrustumCullingMode.AlwaysRender;
            m_UvAxix.Transform.Parent = m_UvSphere.Transform;
            m_UvAxix.FrustumCullingMode = EGLFrustumCullingMode.AlwaysRender;

            m_IcoSphere.Transform.LocalPosition = new Vector3(0.0f, 0.0f, 0.0f);
            m_IcoSphere.PolygonMode = EGLRenderPolygonMode.Line;
            m_IcoSphere.Renderer.Mesh = GLPrimitiveMesh.CreateSphereICO(0);
            m_IcoSphere.Renderer.Material = material;
            m_IcoSphere.FrustumCullingMode = EGLFrustumCullingMode.AlwaysRender;
            m_IcoAxis.Transform.Parent = m_IcoSphere.Transform;
            m_IcoAxis.FrustumCullingMode = EGLFrustumCullingMode.AlwaysRender;

            m_RcSphere.Transform.LocalPosition = new Vector3(2.5f, 0.0f, 0.0f);
            m_RcSphere.PolygonMode = EGLRenderPolygonMode.Line;
            m_RcSphere.Renderer.Mesh = GLPrimitiveMesh.CreateSphereRoundedCube(2);
            m_RcSphere.Renderer.Material = material;
            m_RcSphere.FrustumCullingMode = EGLFrustumCullingMode.AlwaysRender;
            m_RcAxis.Transform.Parent = m_RcSphere.Transform;
            m_RcAxis.FrustumCullingMode = EGLFrustumCullingMode.AlwaysRender;

            m_Scene.DisplayList.Register(m_UvSphere);
            m_Scene.DisplayList.Register(m_UvAxix);
            m_Scene.DisplayList.Register(m_IcoSphere);
            m_Scene.DisplayList.Register(m_IcoAxis);
            m_Scene.DisplayList.Register(m_RcSphere);
            m_Scene.DisplayList.Register(m_RcAxis);

            m_Projection.Near = 0.01f;
            m_Projection.Far = 100.0f;
            m_Camera.Projection = m_Projection;
            m_Camera.RenderTarget = GLViewer;
            m_Camera.Transform.LocalPosition = new Vector3(0.0f, 2.0f, 5.0f);
            m_Camera.Transform.LookAt(new Vector3());
            return;
        }

        public override void Exit()
        {
            base.Exit();

            m_UvSphere.Renderer.Mesh.Dispose();
            m_IcoSphere.Renderer.Mesh.Dispose();
            m_RcSphere.Renderer.Mesh.Dispose();
            return;
        }

        private void GLViewer_OnRenderScene(object inSender, EventArgs inArgs)
        {
            m_Projection.SetAngleAndViewportSize(45.0f, GLViewer.Width, GLViewer.Height);
            m_Scene.Render(m_Camera);
            return;
        }

        private void UdICOSubdivision_ValueChanged(object sender, EventArgs e)
            => m_IcoSphere.Renderer.Mesh = GLPrimitiveMesh.CreateSphereICO((int)UdICOSubdivision.Value);

        private void UdUVSubdivision_ValueChanged(object sender, EventArgs e)
            => m_UvSphere.Renderer.Mesh = GLPrimitiveMesh.CreateSphereUV((int)UdUVSlices.Value, (int)UdUVStacks.Value);

        private void UdRoundedCubeSubdivision_ValueChanged(object sender, EventArgs e)
            => m_RcSphere.Renderer.Mesh = GLPrimitiveMesh.CreateSphereRoundedCube((int)UdRoundedCubeSubdivision.Value);

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            GLViewer.Invalidate();

            var a = (90.0f * (RenderTimer.Interval / 1000.0f));
            var rot = Quaternion.FromEuler(new Vector3(0.0f, a, 0.0f).DegToRad(), EEulerRotationOrder.YXZ);

            m_UvSphere.Transform.LocalRotation *= rot;
            m_IcoSphere.Transform.LocalRotation *= rot;
            m_RcSphere.Transform.LocalRotation *= rot;

            return;
        }

        private GLRenderObject m_UvSphere = new GLRenderObject();
        private GLAxisRenderObject m_UvAxix = new GLAxisRenderObject();

        private GLRenderObject m_IcoSphere = new GLRenderObject();
        private GLAxisRenderObject m_IcoAxis = new GLAxisRenderObject();

        private GLRenderObject m_RcSphere = new GLRenderObject();
        private GLAxisRenderObject m_RcAxis = new GLAxisRenderObject();

        private GLScene m_Scene = new GLScene();
        private GLPerspectiveProjection m_Projection = new GLPerspectiveProjection();
        private GLCamera m_Camera = new GLCamera();
    }
}
