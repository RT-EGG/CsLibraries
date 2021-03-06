using RtCs;
using RtCs.MathUtils;
using RtCs.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace GLTestVisualizer.TestView.FrustumTest
{
    public partial class Ctrl_FrustumTestTestView : GLTestVisualizer.TestView.Ctrl_TestView
    {
        private enum ProjectionType
        {
            Ortho,
            Frustum
        }

        public Ctrl_FrustumTestTestView()
        {
            InitializeComponent();
            return;
        }

        public override void Start()
        {
            base.Start();

            ComboProjectionType.Items.Clear();
            ComboProjectionType.Items.Add(new ProjectionTypeItem {
                Type = ProjectionType.Ortho,
                Text = "Ortho",
                ViewGenerator = () => new Ctrl_ProjectionOrthoParameterView()
            });
            ComboProjectionType.Items.Add(new ProjectionTypeItem {
                Type = ProjectionType.Frustum,
                Text = "Frustum",
                ViewGenerator = () => new Ctrl_ProjectionFrustumParameterView()
            });
            ComboProjectionType.SelectedIndex = 0;
            ComboProjectionType_SelectionChangeCommitted(ComboProjectionType, EventArgs.Empty);

            m_FrustumRenderObject.Transform.Parent = m_FPSCamera.Transform;

            m_FPSCameraController = new RotationCameraMouseController(GLFirstPersonView);
            m_FPSCameraController.Camera = m_FPSCamera;
            m_FPSCamera.RenderTarget = GLFirstPersonView;

            m_TPSCameraController = new OrbitCameraMouseController(GLThirdPersonView);
            m_TPSCameraController.Camera = m_TPSCamera;
            m_TPSCamera.Coordinate = new SphericalCoordinate {
                AzimuthAngleDeg = 0.0f,
                ElevationAngleDeg = -15.0f,
                Radius = 50.0f
            };
            m_TPSProjection.Near = 0.01f;
            m_TPSProjection.Far = 1000.0f;
            m_TPSCamera.Projection = m_TPSProjection;
            m_TPSCamera.RenderTarget = GLThirdPersonView;

            timer1.Enabled = true;
            RandomizeSpheres();
            return;
        }

        public override void Exit()
        {
            base.Exit();

            timer1.Enabled = false;

            m_FrustumRenderObject.Dispose();
            m_FPSCameraController.Dispose();
            m_TPSCameraController.Dispose();
            return;
        }

        private void RandomizeSpheres()
        {
            m_SphereObjects.DisposeItems();
            m_SphereObjects.Clear();
            m_CubeObjects.DisposeItems();
            m_CubeObjects.Clear();

            Random randomizer = new Random();
            int count = (randomizer.Next() % 30) + 1;
            for (int i = 0; i < count; ++i) {
                GLRenderObject newSphere = new GLRenderObject();
                newSphere.Transform.LocalPosition = new SphericalCoordinate {
                    AzimuthAngleDeg = (float)(randomizer.NextDouble() * 360.0),
                    ElevationAngleDeg = (float)((randomizer.NextDouble() * 360.0) - 180.0),
                    Radius = (float)((randomizer.NextDouble() * 35.0) + 5.0)
                }.GetRectangularCoordinate();
                newSphere.Renderer.Mesh = m_SphereMesh;
                newSphere.Renderer.Material = m_OutFrustumMaterial;
                newSphere.CalculateBoundingBox();
                m_SphereObjects.Add(newSphere);
            }

            count = (randomizer.Next() % 30) + 1;
            for (int i = 0; i < count; ++i) {
                GLRenderObject newCube = new GLRenderObject();
                newCube.Transform.LocalPosition = new SphericalCoordinate {
                    AzimuthAngleDeg = (float)(randomizer.NextDouble() * 360.0),
                    ElevationAngleDeg = (float)((randomizer.NextDouble() * 360.0) - 180.0),
                    Radius = (float)((randomizer.NextDouble() * 35.0) + 5.0)
                }.GetRectangularCoordinate();
                newCube.Transform.LocalScale = new Vector3(
                    (float)(1.0 + (randomizer.NextDouble() * 0.5)),
                    (float)(1.0 + (randomizer.NextDouble() * 0.5)),
                    (float)(1.0 + (randomizer.NextDouble() * 0.5))
                );
                newCube.Renderer.Mesh = m_CubeMesh;
                newCube.Renderer.Material = m_OutFrustumMaterial;
                newCube.CalculateBoundingBox();
                m_CubeObjects.Add(newCube);
            }

            m_Scene.DisplayList.Clear();
            m_Scene.DisplayList.Register(m_FrustumRenderObject);
            m_Scene.DisplayList.Register(m_SphereObjects);
            m_Scene.DisplayList.Register(m_CubeObjects);
            return;
        }

        private void CheckInFrustum()
        {
            if (m_CurrentParameterView == null) {
                return;
            }

            GLViewFrustum frustum = new GLViewFrustum(m_FPSCamera.ViewMatrix, m_CurrentParameterView.Projection.ProjectionMatrix);
            foreach (var sphere in m_SphereObjects) {
                Vector3 p = new Vector3(sphere.Transform.WorldMatrix.Multiply(new Vector4(0.0f, 0.0f, 0.0f, 1.0f)));
                if (frustum.IsIntersectSphere(p, 1.0f)) {
                    sphere.Renderer.Material = m_InFrustumMaterial;
                } else {
                    sphere.Renderer.Material = m_OutFrustumMaterial;
                }
            }
            foreach (var cube in m_CubeObjects) {
                if (frustum.IsIntersectAABB(cube.BoundingBox)) {
                    cube.Renderer.Material = m_InFrustumMaterial;
                } else {
                    cube.Renderer.Material = m_OutFrustumMaterial;
                }
            }
            return;
        }

        private void GLFirstPersonView_OnRenderScene(object inSender, EventArgs inArgs)
        {
            if (m_CurrentParameterView == null) {
                return;
            }

            //m_Origin.Visible = false;
            m_FPSCamera.Projection = m_CurrentParameterView.Projection;
            m_Scene.Render(m_FPSCamera);
            return;
        }

        private void GLThirdPersonView_OnRenderScene(object inSender, EventArgs inArgs)
        {
            //m_Origin.Visible = true;
            m_TPSProjection.SetAngleAndViewportSize(45.0f, GLThirdPersonView.Width, GLThirdPersonView.Height);
            m_Scene.Render(m_TPSCamera);
            return;
        }

        private void ComboProjectionType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_CurrentParameterView != null) {
                m_CurrentParameterView.Parent = null;
                m_CurrentParameterView = null;
            }

            if (ComboProjectionType.SelectedItem == null) {
                return;
            }

            Debug.Assert(ComboProjectionType.SelectedItem is ProjectionTypeItem);
            var item = ComboProjectionType.SelectedItem as ProjectionTypeItem;

            m_CurrentParameterView = item.ViewGenerator();
            if (m_CurrentParameterView != null) {
                m_CurrentParameterView.Parent = PanelParameterView;
                m_CurrentParameterView.Dock = DockStyle.Fill;

                m_CurrentParameterView.SetAutoAspectReference(CheckBoxAutoAspect.Checked ? GLThirdPersonView : null);
            }
        }

        private void CheckBoxAutoAspect_CheckedChanged(object sender, EventArgs e)
            => m_CurrentParameterView.SetAutoAspectReference(CheckBoxAutoAspect.Checked ? GLThirdPersonView : null); 

        private void ButtonRandomizer_Click(object sender, EventArgs e)
             => RandomizeSpheres();

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_CurrentParameterView != null) {
                m_FrustumRenderObject.ProjectionMatrix = m_CurrentParameterView.Projection.ProjectionMatrix;
            }

            CheckInFrustum();
            GLFirstPersonView.Invalidate();
            GLThirdPersonView.Invalidate();
            return;
        }

        private class ProjectionTypeItem
        {
            public ProjectionType Type;
            public string Text;
            public Func<Ctrl_ProjectionParameterView> ViewGenerator;

            public override string ToString() => Text;
        }

        private Ctrl_ProjectionParameterView m_CurrentParameterView = null;

        private GLScene m_Scene = new GLScene();
        private GLViewFrustumRendererObject m_FrustumRenderObject = new GLViewFrustumRendererObject();
        private RotationCameraModel m_FPSCamera = new RotationCameraModel();
        private RotationCameraMouseController m_FPSCameraController = null;
        private GLPerspectiveProjection m_TPSProjection = new GLPerspectiveProjection();
        private OrbitCameraModel m_TPSCamera = new OrbitCameraModel();
        private OrbitCameraMouseController m_TPSCameraController = null;

        private GLMesh m_CubeMesh = GLPrimitiveMesh.CreateBox();
        private GLMesh m_SphereMesh = GLPrimitiveMesh.CreateSphereICO(3);
        private GLSphereMaterial m_InFrustumMaterial = new GLSphereMaterial{ IsInFrustum = true };
        private GLSphereMaterial m_OutFrustumMaterial = new GLSphereMaterial{ IsInFrustum = false };
        private List<GLRenderObject> m_CubeObjects = new List<GLRenderObject>();
        private List<GLRenderObject> m_SphereObjects = new List<GLRenderObject>();        
    }
}
