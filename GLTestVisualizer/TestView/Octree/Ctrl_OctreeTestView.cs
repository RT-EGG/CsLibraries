using RtCs.OpenGL;
using RtCs.MathUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace GLTestVisualizer.TestView.Octree
{
    public partial class Ctrl_OctreeTestView : GLTestVisualizer.TestView.Ctrl_TestView
    {
        public Ctrl_OctreeTestView()
        {
            InitializeComponent();
        }

        public override void Start()
        {
            base.Start();

            m_Scene.DisplayList = DisplayList;

            m_OctreeObject.LocalPosition = m_Octree.Offset;
            m_OctreeObject.SetupForOctree(m_Octree);

            m_SphereObject.Transform.LocalScale = new Vector3(0.5);
            m_SphereObject.Renderer.Mesh = m_SphereMesh;
            m_SphereObject.Renderer.Material = m_SphereMaterial;
            m_SphereBoundsObject.Transform.Parent = m_SphereObject.Transform;
            m_SphereBoundsObject.Transform.LocalScale = new Vector3(2.0);
            m_SphereBoundsObject.Renderer.Mesh = m_SphereBoundsMesh;
            m_SphereBoundsObject.Renderer.Material = m_SphereMaterial;
            m_SphereBoundsObject.PolygonMode = EGLRenderPolygonMode.Line;

            m_CameraController = new OrbitCameraMouseController(glView);
            m_CameraController.Camera = m_Camera;
            m_Camera.Coordinate = new SphericalCoordinate {
                AzimuthAngleDeg = 0.0,
                ElevationAngleDeg = 0.0,
                Radius = 20.0
            };

            ValidationTimer.Enabled = true;
            CheckBoxAutoTime.Checked = true;
            return;
        }

        public override void Exit()
        {
            base.Exit();

            ValidationTimer.Enabled = false;
            m_CameraController.Dispose();
            return;
        }

        private void glView_OnRenderScene(RtCs.OpenGL.Controls.GLControl inControl, GLRenderingStatus inStatus)
        {
            m_Camera.ProjectionMatrix = Matrix4x4.MakePerspective(45.0, (double)glView.Width / (double)glView.Height, 0.01, 100.0);
            m_Camera.Render(inStatus, m_Scene);
            return;
        }

        private void ValidationTimer_Tick(object sender, EventArgs e)
        {
            double t = (double)TrackBarTime.Value / (double)(TrackBarTime.Maximum - TrackBarTime.Minimum) * Math.PI * 2.0;
            m_SphereObject.Transform.LocalPosition = new Vector3(
                    Math.Sin(t * 1.0) * OctreeSize * 0.5 * 0.8,
                    Math.Cos(t * 3.0) * OctreeSize * 0.5 * 0.5,
                    Math.Sin(t * 2.0 + Math.PI) * OctreeSize * 0.5 * 0.6
                );
            m_SphereObject.CalculateBoundingBox();

            m_Octree.Clear();
            m_Octree.Register(m_SphereObject);
            m_OctreeObject.UpdateVisibility();

            glView.Invalidate();
            return;
        }

        private void AutoTrackTimer_Tick(object sender, EventArgs e)
        {
            int newValue = TrackBarTime.Value + AutoTrackTimer.Interval;
            if (newValue > TrackBarTime.Maximum) {
                newValue = (newValue - TrackBarTime.Minimum) % (TrackBarTime.Maximum - TrackBarTime.Minimum);
                newValue += TrackBarTime.Minimum;
            }
            TrackBarTime.Value = newValue;
            return;
        }

        private void CheckBoxAutoTime_CheckedChanged(object sender, EventArgs e)
            => AutoTrackTimer.Enabled = CheckBoxAutoTime.Checked;

        private IEnumerable<GLRenderObject> DisplayList
        {
            get {
                yield return m_SphereObject;
                yield return m_SphereBoundsObject;
                foreach (var a in m_OctreeObject) {
                    yield return a;
                }
            }
        }

        private const double OctreeSize = 8.0;
        private RtCs.MathUtils.Geometry.Octree m_Octree = new RtCs.MathUtils.Geometry.Octree(3, OctreeSize, new Vector3(-OctreeSize * 0.5));
        private OctreeRenderObject m_OctreeObject = new OctreeRenderObject();
        private OctreeRegistableRenderObject m_SphereObject = new OctreeRegistableRenderObject();
        private GLMesh m_SphereMesh = GLPrimitiveMesh.CreateSphereUV(8, 5);
        private GLRenderObject m_SphereBoundsObject = new GLRenderObject();
        private GLMesh m_SphereBoundsMesh = GLPrimitiveMesh.CreateBox();
        private GLSolidColorMaterial m_SphereMaterial = new GLSolidColorMaterial();

        private OrbitCameraModel m_Camera = new OrbitCameraModel();
        private OrbitCameraMouseController m_CameraController = null;

        private GLScene m_Scene = new GLScene();
    }
}
