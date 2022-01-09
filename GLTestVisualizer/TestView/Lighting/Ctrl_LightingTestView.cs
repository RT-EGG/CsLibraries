using RtCs.MathUtils;
using RtCs.OpenGL;
using System;
using System.Collections.Generic;

namespace GLTestVisualizer.TestView.Lighting
{
    public partial class Ctrl_LightingTestView : GLTestVisualizer.TestView.Ctrl_TestView
    {
        public Ctrl_LightingTestView()
        {
            InitializeComponent();
        }

        public override void Start()
        {
            base.Start();

            m_Projection.Near = 0.01f;
            m_Projection.Far = 100.0f;
            m_Camera.Projection = m_Projection;
            m_Camera.Coordinate = new SphericalCoordinate {
                AzimuthAngleDeg = 0.0f,
                ElevationAngleDeg = 0.0f,
                Radius = 5.0f
            };
            m_CameraController = new OrbitCameraMouseController(GLControl);
            m_CameraController.Camera = m_Camera;

            m_AxisRenderObject.Transform.LocalScale = new Vector3(5.0f);
            m_AxisRenderObject.CalculateBoundingBox();

            m_SphereObject.Renderer.Mesh = m_SphereMesh;
            m_SphereObject.Renderer.Material = m_SphereMaterial;
            m_SphereMaterial.Ambient = new Vector3(0.5f, 0.5f, 0.0f);
            m_SphereMaterial.Diffuse = new Vector3(0.75f, 0.5f, 0.0f);
            m_SphereMaterial.Specular = new Vector3(1.0f, 1.0f, 1.0f);
            m_SphereMaterial.Emission = new Vector3(0.0f);
            m_SphereMaterial.Shininess = 10.0f;

            m_Scene.DisplayList.Register(m_AxisRenderObject);
            m_Scene.DisplayList.Register(m_SphereObject);

            m_Scene.Lights.AmbientLight.Color = new ColorRGB(255, 255, 255);
            m_Scene.Lights.AmbientLight.Intensity = 0.5f;
            m_DirectionalLights.Add(
                    new GLDirectionalLight {
                        Color = new ColorRGB(255, 255, 255),
                        Intensity = 0.5f,
                        Direction = new Vector3(-1.0f, -1.0f, 0.0f).Normalized,
                    }
                );
            m_DirectionalLights.Add(
                    new GLDirectionalLight {
                        Color = new ColorRGB(0, 255, 0),
                        Intensity = 1.0f,
                        Direction = new Vector3(1.0f, -1.0f, 0.0f).Normalized
                    }
                );
            m_Scene.Lights.DirectionalLights.AddRange(m_DirectionalLights);
            return;
        }

        private void GLControl_OnRenderScene(object sender, EventArgs e)
        {
            m_Projection.SetAngleAndViewportSize(45.0f, GLControl.Width, GLControl.Height);
            m_Scene.Render(m_Camera);
            return;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            GLControl.Invalidate();
        }

        private GLScene m_Scene = new GLScene();
        private OrbitCameraModel m_Camera = new OrbitCameraModel();
        private GLPerspectiveProjection m_Projection = new GLPerspectiveProjection();
        private OrbitCameraMouseController m_CameraController;

        private GLAxisRenderObject m_AxisRenderObject = new GLAxisRenderObject();
        private GLRenderObject m_SphereObject = new GLRenderObject();
        private GLMesh m_SphereMesh = GLPrimitiveMesh.CreateSphereICO(3);
        private GLPhongMaterial m_SphereMaterial = new GLPhongMaterial();

        private List<GLDirectionalLight> m_DirectionalLights = new List<GLDirectionalLight>();
    }
}
