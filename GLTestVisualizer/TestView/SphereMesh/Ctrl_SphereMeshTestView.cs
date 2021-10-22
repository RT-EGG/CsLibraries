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
            m_UvSphere.Transform.LocalPosition = new Vector3(-2.5, 0.0, 0.0);
            m_UvSphere.PolygonMode = EGLRenderPolygonMode.Line;
            m_UvSphere.Renderer.Mesh = GLPrimitiveMesh.CreateSphereUV(5, 5);
            m_UvSphere.Renderer.Material = new GLSphereMaterial();
            m_UvAxix.Transform.Parent = m_UvSphere.Transform;

            m_IcoSphere.Transform.LocalPosition = new Vector3(0.0, 0.0, 0.0);
            m_IcoSphere.PolygonMode = EGLRenderPolygonMode.Line;
            m_IcoSphere.Renderer.Mesh = GLPrimitiveMesh.CreateSphereICO(0);
            m_IcoSphere.Renderer.Material = new GLSphereMaterial();
            m_IcoAxis.Transform.Parent = m_IcoSphere.Transform;

            m_RcSphere.Transform.LocalPosition = new Vector3(2.5, 0.0, 0.0);
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

        private void GLViewer_OnRenderScene(RtCs.OpenGL.WinForms.GLControl inControl, GLRenderingStatus inStatus)
        {
            inStatus.ProjectionMatrix.PushMatrix();
            try {
                inStatus.ProjectionMatrix.LoadMatrix(Matrix4x4.MakePerspective(45.0, (double)GLViewer.Width / (double)GLViewer.Height, 0.01, 100.0));
                //inStatus.ProjectionMatrix.LoadMatrix(Matrix4x4.MakeOrtho(3.0, inControl.Width, inControl.Height, 0.01, 100.0));

                inStatus.ModelViewMatrix.View.PushMatrix();
                inStatus.ModelViewMatrix.Model.PushMatrix();
                try {
                    inStatus.ModelViewMatrix.View.LookAt(new Vector3(0.0, 1.0, 4.0), new Vector3(0.0), new Vector3(0.0, 1.0, 0.0));
                    inStatus.ModelViewMatrix.Model.LoadIdentity();

                    GL.Enable(EnableCap.DepthTest);
                    GL.Enable(EnableCap.CullFace);
                    GL.LineWidth(1.0f);

                    m_UvAxix.Render(inStatus);
                    m_UvSphere.Render(inStatus);

                    m_IcoAxis.Render(inStatus);
                    m_IcoSphere.Render(inStatus);

                    m_RcAxis.Render(inStatus);
                    m_RcSphere.Render(inStatus);

                } finally {
                    inStatus.ModelViewMatrix.Model.PopMatrix();
                    inStatus.ModelViewMatrix.View.PopMatrix();
                }
            } finally {
                inStatus.ProjectionMatrix.PopMatrix();
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

            var a = (90.0 * (RenderTimer.Interval / 1000.0));
            var rot = Quaternion.FromEuler(new Vector3(0.0, a, 0.0).DegToRad(), EEulerRotationOrder.YXZ);

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
