using RtCs.MathUtils;
using RtCs.OpenGL;
using Snow.Camera;
using System;
using System.Windows.Forms;

namespace Snow
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            m_Projection.Near = 0.01f;
            m_Projection.Far = 100.0f;
            m_Camera.Projection = m_Projection;
            m_Camera.RenderTarget = glControl;
            m_Camera.Center = new Vector3(0.0f, 0.2f, 0.0f);
            m_Camera.Coordinate = new SphericalCoordinate {
                AzimuthAngleDeg = 0.0f,
                ElevationAngleDeg = -30.0f,
                Radius = 1.0f
            };
            m_CameraController = new OrbitCameraController(glControl);
            m_CameraController.Camera = m_Camera;

            m_SimulationModel = new SimulationModel(m_Scene);
            SimulationView.Model = m_SimulationModel;

            SnowCoverVisibilityConfigurationView.Model = m_Scene.SnowCover;
        }

        private void glControl_RenderScene(object sender, EventArgs e)
        {
            m_Projection.SetAngleAndViewportSize(45.0f, glControl.Width, glControl.Height);

            m_Scene.Render(m_Camera);
        }

        private void SimulationTimer_Tick(object sender, EventArgs e)
        {
            m_SimulationModel.TimeStep(SimulationTimer.Interval * 0.001f);
            glControl.Invalidate();
        }

        private Scene m_Scene = new Scene();
        private SimulationModel m_SimulationModel = null;

        private GLPerspectiveProjection m_Projection = new GLPerspectiveProjection();
        private OrbitCamera m_Camera = new OrbitCamera();
        private OrbitCameraController m_CameraController = null;
    }
}
