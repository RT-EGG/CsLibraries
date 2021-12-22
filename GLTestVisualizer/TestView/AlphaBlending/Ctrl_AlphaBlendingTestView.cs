using RtCs.MathUtils;
using RtCs.OpenGL;
using System;
using System.Collections.Generic;

namespace GLTestVisualizer.TestView.AlphaBlending
{
    public partial class Ctrl_AlphaBlendingTestView : GLTestVisualizer.TestView.Ctrl_TestView
    {
        public Ctrl_AlphaBlendingTestView()
        {
            InitializeComponent();
        }

        public override void Start()
        {
            base.Start();

            m_CameraController = new OrbitCameraMouseController(glView);
            m_CameraController.Camera = m_Camera;
            m_Camera.Center = new Vector3(1.0f, 1.0f, 1.0f);
            m_Camera.Coordinate = new SphericalCoordinate {
                AzimuthAngleDeg = 0.0f,
                ElevationAngleDeg = -15.0f,
                Radius = 5.0f
            };

            void CreateNewCube(Vector3 inCenter, Vector3 inRGB)
            {
                GLRenderObject newObject = new GLRenderObject();
                newObject.Transform.LocalPosition = inCenter;
                newObject.Renderer.Mesh = m_CubeMesh;
                newObject.Renderer.Material = new GLCubeMaterial(new Vector4(inRGB, 0.3f));
                newObject.FrustumCullingMode = EGLFrustumCullingMode.AlwaysRender;
                newObject.RenderFaceMode = EGLRenderFaceMode.FrontAndBack;

                m_CubeObjects.Add(newObject);
            }
            CreateNewCube(new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.4f, 0.4f, 0.4f));
            CreateNewCube(new Vector3(1.6f, 0.4f, 0.4f), new Vector3(0.9f, 0.0f, 0.0f));
            CreateNewCube(new Vector3(0.4f, 1.6f, 0.4f), new Vector3(0.0f, 0.9f, 0.0f));
            CreateNewCube(new Vector3(0.4f, 0.4f, 1.6f), new Vector3(0.0f, 0.0f, 0.9f));
            CreateNewCube(new Vector3(1.6f, 1.6f, 0.4f), new Vector3(0.9f, 0.9f, 0.0f));
            CreateNewCube(new Vector3(0.4f, 1.6f, 1.6f), new Vector3(0.0f, 0.9f, 0.9f));
            CreateNewCube(new Vector3(1.6f, 0.4f, 1.6f), new Vector3(0.9f, 0.0f, 0.9f));
            CreateNewCube(new Vector3(1.6f, 1.6f, 1.6f), new Vector3(0.9f, 0.9f, 0.9f));

            m_Scene.DisplayList.Register(m_CubeObjects);
            return;
        }

        public override void Exit()
        {
            base.Exit();

            m_CameraController.Dispose();
            return;
        }

        private void glView_OnRenderScene(RtCs.OpenGL.WinForms.GLControl inControl, RtCs.OpenGL.GLRenderParameter inParameter)
        {
            m_Camera.ProjectionMatrix = Matrix4x4.MakeSymmetricalPerspective(45.0f, inControl.Width, inControl.Height, 0.01f, 100.0f);
            m_Camera.Render(inParameter, m_Scene);
            return;
        }

        private void timer1_Tick(object sender, EventArgs e)
            => glView.Invalidate();

        private OrbitCameraModel m_Camera = new OrbitCameraModel();
        private OrbitCameraMouseController m_CameraController = null;

        private GLScene m_Scene = new GLScene();
        private GLMesh m_CubeMesh = GLPrimitiveMesh.CreateBox();
        private List<GLRenderObject> m_CubeObjects = new List<GLRenderObject>();
    }
}
