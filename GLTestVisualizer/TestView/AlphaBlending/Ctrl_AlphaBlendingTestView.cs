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
            m_Camera.Center = new Vector3(1.0, 1.0, 1.0);
            m_Camera.Coordinate = new SphericalCoordinate {
                AzimuthAngleDeg = 0.0,
                ElevationAngleDeg = -15.0,
                Radius = 5.0
            };

            void CreateNewCube(Vector3 inCenter, Vector3 inRGB)
            {
                GLRenderObject newObject = new GLRenderObject();
                newObject.Transform.LocalPosition = inCenter;
                newObject.Renderer.Mesh = m_CubeMesh;
                newObject.Renderer.Material = new GLCubeMaterial(new Vector4(inRGB, 0.3));
                newObject.FrustumCullingMode = EGLFrustumCullingMode.AlwaysRender;
                newObject.RenderFaceMode = EGLRenderFaceMode.FrontAndBack;

                m_CubeObjects.Add(newObject);
            }
            CreateNewCube(new Vector3(0.4, 0.4, 0.4), new Vector3(0.4, 0.4, 0.4));
            CreateNewCube(new Vector3(1.6, 0.4, 0.4), new Vector3(0.9, 0.0, 0.0));
            CreateNewCube(new Vector3(0.4, 1.6, 0.4), new Vector3(0.0, 0.9, 0.0));
            CreateNewCube(new Vector3(0.4, 0.4, 1.6), new Vector3(0.0, 0.0, 0.9));
            CreateNewCube(new Vector3(1.6, 1.6, 0.4), new Vector3(0.9, 0.9, 0.0));
            CreateNewCube(new Vector3(0.4, 1.6, 1.6), new Vector3(0.0, 0.9, 0.9));
            CreateNewCube(new Vector3(1.6, 0.4, 1.6), new Vector3(0.9, 0.0, 0.9));
            CreateNewCube(new Vector3(1.6, 1.6, 1.6), new Vector3(0.9, 0.9, 0.9));

            m_Scene.DisplayList = m_CubeObjects;
            return;
        }

        public override void Exit()
        {
            base.Exit();

            m_CameraController.Dispose();
            return;
        }

        private void glView_OnRenderScene(RtCs.OpenGL.GLControl inControl, RtCs.OpenGL.GLRenderingStatus inStatus)
        {
            m_Camera.ProjectionMatrix = Matrix4x4.MakePerspective(45.0, inControl.Width, inControl.Height, 0.01, 100.0);
            m_Camera.Render(inStatus, m_Scene);
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
