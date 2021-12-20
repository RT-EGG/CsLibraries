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
            m_UvSphere.Transform.LocalPosition = new Vector3(-2.5f, 0.0f, 0.0f);
            m_UvSphere.PolygonMode = EGLRenderPolygonMode.Line;
            m_UvSphere.Renderer.Mesh = GLPrimitiveMesh.CreateSphereUV(5, 5);
            m_UvSphere.Renderer.Material = new GLSphereMaterial();
            m_UvAxix.Transform.Parent = m_UvSphere.Transform;

            m_IcoSphere.Transform.LocalPosition = new Vector3(0.0f, 0.0f, 0.0f);
            m_IcoSphere.PolygonMode = EGLRenderPolygonMode.Line;
            m_IcoSphere.Renderer.Mesh = GLPrimitiveMesh.CreateSphereICO(0);
            m_IcoSphere.Renderer.Material = new GLSphereMaterial();
            m_IcoAxis.Transform.Parent = m_IcoSphere.Transform;

            m_RcSphere.Transform.LocalPosition = new Vector3(2.5f, 0.0f, 0.0f);
            m_RcSphere.PolygonMode = EGLRenderPolygonMode.Line;
            m_RcSphere.Renderer.Mesh = GLPrimitiveMesh.CreateSphereRoundedCube(2);
            m_RcSphere.Renderer.Material = new GLSphereMaterial();
            m_RcAxis.Transform.Parent = m_RcSphere.Transform;
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

        private void GLViewer_OnRenderScene(RtCs.OpenGL.WinForms.GLControl inControl, GLRenderParameter inParameter)
        {
            inParameter.ProjectionMatrix.PushMatrix();
            try {
                inParameter.ProjectionMatrix.LoadMatrix(Matrix4x4.MakeSymmetricalPerspective(45.0f, (float)GLViewer.Width / (float)GLViewer.Height, 0.01f, 100.0f));

                inParameter.ModelViewMatrix.View.PushMatrix();
                inParameter.ModelViewMatrix.Model.PushMatrix();
                try {
                    inParameter.ModelViewMatrix.View.LookAt(new Vector3(0.0f, 1.0f, 4.0f), new Vector3(0.0f), new Vector3(0.0f, 1.0f, 0.0f));
                    inParameter.ModelViewMatrix.Model.LoadIdentity();

                    GL.Enable(EnableCap.DepthTest);
                    GL.Enable(EnableCap.CullFace);
                    GL.LineWidth(1.0f);

                    m_UvAxix.Render(inParameter);
                    m_UvSphere.Render(inParameter);

                    m_IcoAxis.Render(inParameter);
                    m_IcoSphere.Render(inParameter);

                    m_RcAxis.Render(inParameter);
                    m_RcSphere.Render(inParameter);

                } finally {
                    inParameter.ModelViewMatrix.Model.PopMatrix();
                    inParameter.ModelViewMatrix.View.PopMatrix();
                }
            } finally {
                inParameter.ProjectionMatrix.PopMatrix();
            }
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
    }
}
