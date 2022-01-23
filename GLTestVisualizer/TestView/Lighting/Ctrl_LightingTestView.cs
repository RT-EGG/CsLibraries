using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;

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
                ElevationAngleDeg = -45.0f,
                Radius = 10.0f
            };
            m_CameraController = new OrbitCameraMouseController(GLControl);
            m_CameraController.Camera = m_Camera;

            m_AxisRenderObject.Transform.LocalScale = new Vector3(5.0f);
            m_AxisRenderObject.CalculateBoundingBox();

            m_QuadMesh.Vertices = new Vector3[] {
                new Vector3(-1.0f,  1.0f, 0.0f),
                new Vector3(-1.0f, -1.0f, 0.0f),
                new Vector3( 1.0f, -1.0f, 0.0f),
                new Vector3( 1.0f,  1.0f, 0.0f)
            };
            m_QuadMeshNormals = m_QuadMesh.AddAttribute(new GLVertexVector3AttributeDescriptor(GLVertexAttribute.AttributeName_Normal));
            m_QuadMeshNormals.Buffer = new Vector3[] {
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f)
            };
            m_QuadMesh.VertexBufferUsageHint = BufferUsageHint.StaticDraw;
            m_QuadMesh.CalculateBoundingBox();

            m_QuadMesh.Topology = EGLMeshTopology.Quads;
            m_QuadMesh.Indices = new int[] {
                0, 1, 2, 3
            };
            m_QuadMesh.IndexBufferUsageHint = BufferUsageHint.StaticDraw;
            m_QuadMesh.Apply();

            m_QuadObject.Transform.LocalRotation = Quaternion.FromEuler((-90.0f).DegToRad(), 0.0f, 0.0f, EEulerRotationOrder.YXZ);
            m_QuadObject.Transform.LocalScale = new Vector3(10.0f, 10.0f, 1.0f);
            m_QuadObject.Renderer.Mesh = m_QuadMesh;
            m_QuadObject.Renderer.Material = m_QuadMaterial;
            m_QuadMaterial.Ambient = new Vector3(1.0f, 1.0f, 1.0f);
            m_QuadMaterial.Diffuse = new Vector3(1.0f, 1.0f, 1.0f);
            m_QuadMaterial.Specular = new Vector3(1.0f, 1.0f, 1.0f);
            m_QuadMaterial.Emission = new Vector3(0.0f);
            m_QuadMaterial.Shininess = 10.0f;

            m_Scene.DisplayList.Register(m_AxisRenderObject);
            m_Scene.DisplayList.Register(m_QuadObject);

            m_Scene.Lights.AmbientLight.Color = new ColorRGB(255, 255, 255);
            m_Scene.Lights.AmbientLight.Intensity = 0.5f;

            m_DirectionalLights.Add(
                    new GLDirectionalLight {
                        Color = new ColorRGB(255, 255, 255),
                        Intensity = 0.25f,
                        Direction = new Vector3(-1.0f, -1.0f, -1.0f).Normalized,
                    }
                );
            m_Scene.Lights.DirectionalLights.AddRange(m_DirectionalLights);

            m_PointLights.Add(
                    new GLPointLight {
                        Color = new ColorRGB(255, 0, 0),
                        Intensity = 1.0f,
                        Range = 10.0f
                    }
                );
            m_PointLights.Add(
                    new GLPointLight {
                        Color = new ColorRGB(0, 255, 0),
                        Intensity = 1.0f,
                        Range = 10.0f
                    }
                );
            m_PointLights.Add(
                    new GLPointLight {
                        Color = new ColorRGB(0, 0, 255),
                        Intensity = 1.0f,
                        Range = 10.0f
                    }
                );
            //m_Scene.Lights.PointLights.AddRange(m_PointLights);

            m_SpotLights.Add(
                    new GLSpotLight {
                        Color = new ColorRGB(255, 255, 0),
                        Intensity = 1.0f,
                        Range = 10.0f,
                        Angle = (45.0f).DegToRad()
                    }
                );
            m_Scene.Lights.SpotLights.AddRange(m_SpotLights);

            ButtonSphereMaterialAmbient.Value = VectorToColor(m_QuadMaterial.Ambient);
            ButtonSphereMaterialDiffuse.Value = VectorToColor(m_QuadMaterial.Diffuse);
            ButtonSphereMaterialSpecular.Value = VectorToColor(m_QuadMaterial.Specular);
            ButtonSphereMaterialEmission.Value = VectorToColor(m_QuadMaterial.Emission);
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

            t += (float)Math.PI * 0.01f;
            void MoveTo(Transform transform, float thete)
            {
                float ct = 2.0f * (float)Math.Cos(thete);
                float st = 2.0f * (float)Math.Sin(thete);
                transform.LocalPosition = new Vector3(st, 2.0f, ct);
            }

            for (int i = 0; i < 3; ++i) {
                MoveTo(m_PointLights[i].Transform, t + ((i * (2.0f / 3.0f)) * (float)Math.PI));
            }

            m_SpotLights[0].Transform.LocalPosition = new Vector3(2.0f * (float)Math.Cos(t), 1.0f, 2.0f * (float)Math.Sin(t));
            m_SpotLights[0].Transform.LookAt(new Vector3(0.0f, 0.0f, 0.0f));
        }

        private float t = 0.0f;

        private void ButtonSphereMaterialAmbient_ValueChanged(object sender, EventArgs e)
            => m_QuadMaterial.Ambient = ColorToVector3((sender as ColorSelectButton).Value);

        private void ButtonSphereMaterialDiffuse_ValueChanged(object sender, EventArgs e)
            => m_QuadMaterial.Diffuse = ColorToVector3((sender as ColorSelectButton).Value);

        private void ButtonSphereMaterialSpecular_ValueChanged(object sender, EventArgs e)
            => m_QuadMaterial.Specular = ColorToVector3((sender as ColorSelectButton).Value);

        private void ButtonSphereMaterialEmission_ValueChanged(object sender, EventArgs e)
            => m_QuadMaterial.Emission = ColorToVector3((sender as ColorSelectButton).Value);

        private Vector3 ColorToVector3(Color inValue)
            => new Vector3(inValue.R / 255.0f, inValue.G / 255.0f, inValue.B / 255.0f);

        private Color VectorToColor(Vector3 inValue)
            => Color.FromArgb(255,
                             (byte)(inValue.x * 255.0f).TruncateToInt(),
                             (byte)(inValue.y * 255.0f).TruncateToInt(),
                             (byte)(inValue.z * 255.0f).TruncateToInt());

        private GLScene m_Scene = new GLScene();
        private OrbitCameraModel m_Camera = new OrbitCameraModel();
        private GLPerspectiveProjection m_Projection = new GLPerspectiveProjection();
        private OrbitCameraMouseController m_CameraController;

        private GLAxisRenderObject m_AxisRenderObject = new GLAxisRenderObject();
        private GLRenderObject m_QuadObject = new GLRenderObject();
        private GLMesh m_QuadMesh = new GLMesh();
        private IGLVertexAttribute<Vector3> m_QuadMeshNormals = null;
        private GLPhongMaterial m_QuadMaterial = new GLPhongMaterial();

        private List<GLDirectionalLight> m_DirectionalLights = new List<GLDirectionalLight>();
        private List<GLPointLight> m_PointLights = new List<GLPointLight>();
        private List<GLSpotLight> m_SpotLights = new List<GLSpotLight>();
    }
}
