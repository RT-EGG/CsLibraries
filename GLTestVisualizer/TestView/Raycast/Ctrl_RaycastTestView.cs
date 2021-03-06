using GLTestVisualizer.TestView.Octree;
using RtCs;
using RtCs.MathUtils;
using RtCs.MathUtils.Geometry;
using RtCs.OpenGL;
using RtCs.OpenGL.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GLTestVisualizer.TestView.Raycast
{
    public partial class Ctrl_RaycastTestView : GLTestVisualizer.TestView.Ctrl_TestView
    {
        public Ctrl_RaycastTestView()
        {
            InitializeComponent();
            return;
        }

        public override void Start()
        {
            base.Start();

            m_OctreeRenderObject.SetupForOctree(m_Octree);
            m_OctreeRenderObject.LocalPosition = new Vector3(m_Octree.Offset);
            m_OctreeRenderObject.ForEach(cell => cell.CalculateBoundingBox());

            m_TPSCameraController = new OrbitCameraMouseController(GLTPSView);
            m_TPSCameraController.Camera = m_TPSCamera;
            m_TPSCamera.Coordinate = new SphericalCoordinate {
                AzimuthAngleDeg = 0.0f,
                ElevationAngleDeg = -15.0f,
                Radius = 50.0f
            };
            m_TPSProjection.Near = 0.01f;
            m_TPSProjection.Far = 1000.0f;
            m_TPSCamera.Projection = m_TPSProjection;
            m_TPSCamera.RenderTarget = GLTPSView;

            m_FPSCameraController = new FreeFlyCameraKeyMouseController(GLFPSView);
            m_FPSCameraController.Camera = m_FPSCamera;
            m_FPSCameraController.TransferPerFrame = 3.0f * (InvalidateTimer.Interval * 0.001f);
            m_FPSProjection.Near = 0.01f;
            m_FPSProjection.Far = 1000.0f;
            m_FPSCamera.Projection = m_FPSProjection;
            m_FPSCamera.RenderTarget = GLFPSView;

            m_FPSCameraVisualizer.Transform.Parent = m_FPSCamera.Transform;
            m_FPSCameraVisualizer.Renderer.Mesh = m_SphereMesh;
            m_FPSCameraVisualizer.Renderer.Material = new GLColorMaterial {
                Color = new Vector4(0.2f, 0.2f, 0.2f, 1.0f)
            };
            m_FPSCameraRayVisualizer.Transform.Parent = m_FPSCameraVisualizer.Transform;
            m_FPSCameraRayVisualizer.Transform.LocalScale = new Vector3(0.0f, 0.0f, 10.0f);
            m_FPSCameraRayVisualizer.Renderer.Mesh = m_FPSCameraRayMesh;
            m_FPSCameraRayVisualizer.Renderer.Material = new GLColorMaterial {
                Color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f)
            };

            RandomizeObjects();
            InvalidateTimer.Enabled = true;
            return;
        }

        public override void Exit()
        {
            base.Exit();

            InvalidateTimer.Enabled = false;
            return;
        }

        public void RandomizeObjects()
        {
            m_Octree.Clear();
            m_RaycastableObjects.DisposeItems();
            m_RaycastableObjects.Clear();

            Random randomizer = new Random();
            for (int i = 0; i < 20; ++i) {
                OctreeRegistableRenderObject newObject = new OctreeRegistableRenderObject();
                newObject.Transform.LocalPosition = new SphericalCoordinate {
                    AzimuthAngleDeg = (float)(randomizer.NextDouble() * 360.0),
                    ElevationAngleDeg = (float)((randomizer.NextDouble() * 360.0) - 180.0),
                    Radius = (float)((randomizer.NextDouble() * ((m_Octree.Dimensions * 0.5) - 1.0)) + 1.0)
                }.GetRectangularCoordinate();

                newObject.Renderer.Mesh = ((randomizer.Next() % 2) == 0) ? m_SphereMesh : m_CubeMesh;
                newObject.Renderer.Material = new GLColorMaterial {
                    Color = new Vector4(RenderColors[randomizer.Next() % RenderColors.Length], 1.0f)
                };
                newObject.CalculateBoundingBox();
                m_RaycastableObjects.Add(newObject);
                m_Octree.Register(newObject);
            }
            return;
        }

        private void GLFPSView_OnRenderScene(object inSender, EventArgs inArgs)
        {
            Vector3 rayPoint0 = m_FPSCameraRayVisualizer.Transform.WorldPosition;
            Vector3 rayPoint1 = rayPoint0 + (m_FPSCameraRayVisualizer.Transform.WorldRotation * new Vector3(0.0f, 0.0f, -1.0f) * (float)UdRayLength.Value);

            m_HitPointMarkerObjects.ForEach(o => m_RenderObjectPool.Enqueue(o));
            m_HitPointMarkerObjects.Clear();
            foreach (var intersect in RayCast(new Line3D(rayPoint0, rayPoint1))) {
                var marker = RequestHitPointMarkerObject();
                marker.Transform.LocalPosition = intersect.Position;
                marker.Transform.LocalScale = new Vector3(0.1f);
                marker.CalculateBoundingBox();

                m_HitPointMarkerObjects.Add(marker);
            }

            m_Scene.DisplayList.Clear();
            m_Scene.DisplayList.Register(m_RaycastableObjects.Cast<GLRenderObject>().Concat(m_HitPointMarkerObjects));

            m_FPSProjection.SetAngleAndViewportSize(45.0f, GLFPSView.Width, GLTPSView.Height);
            m_Scene.Render(m_FPSCamera);
        }

        private void GLTPSView_OnRenderScene(object inSender, EventArgs inArgs)
        {
            m_FPSCameraRayVisualizer.Transform.LocalScale = new Vector3(1.0f, 1.0f, (float)UdRayLength.Value);

            m_Scene.DisplayList.Clear();
            m_Scene.DisplayList.Register(m_RaycastableObjects);
            m_Scene.DisplayList.Register(m_HitPointMarkerObjects);
            m_Scene.DisplayList.Register(m_FPSCameraVisualizer);
            m_Scene.DisplayList.Register(m_FPSCameraRayVisualizer);
            if (CheckShowOctreeGrid.Checked) {
                m_Scene.DisplayList.Register(m_OctreeRenderObject);
            }

            m_TPSProjection.SetAngleAndViewportSize(45.0f, GLTPSView.Width, GLTPSView.Height);
            m_Scene.Render(m_TPSCamera);
            return;
        }

        private void InvalidateTimer_Tick(object sender, EventArgs e)
        {
            GLFPSView.Invalidate();
            GLTPSView.Invalidate();
            return;
        }

        private void ButtonResetFPSCamera_Click(object sender, EventArgs e)
            => ResetFPScamera();

        private void ResetFPScamera()
        {
            m_FPSCamera.Transform.LocalPosition = new Vector3(0.0f);
            m_FPSCamera.Transform.LocalRotation = Quaternion.Identity;
            return;
        }

        private IEnumerable<LineIntersectionInfo3D> RayCast(Line3D inLine)
        {
            m_Octree.ForEach(cell => m_OctreeRenderObject.SetCellVisibility(cell, false));

            var cells = new List<IOctreeCell>(m_Octree.TraverseOnRay(inLine));
            cells.ForEach(cell => m_OctreeRenderObject.SetCellVisibility(cell, true));

            // add parent cells of cells in last layer.
            List<IOctreeCell> addedParent = new List<IOctreeCell>();
            void recursiveAddParent(IOctreeCell inCell)
            {
                if (inCell.Parent != null) {
                    recursiveAddParent(inCell.Parent);
                }
                if (!addedParent.Contains(inCell)) {
                    addedParent.Add(inCell);
                }
                return;
            }
            cells.ForEach(cell => recursiveAddParent(cell.Parent));
            cells.AddRange(addedParent);

            var objects = new List<OctreeRegistableRenderObject>(m_RaycastableObjects.Count);
            foreach (var cell in cells) {
                foreach (var obj in cell.Objects.Where(o => o is OctreeRegistableRenderObject).Select(o => o as OctreeRegistableRenderObject)) {
                    objects.Add(obj);
                }
            }

            return (new RayCaster {
                CullBackFace = true,
                SortByDistance = true
            }).Test(inLine, objects);
        }

        private GLRenderObject RequestHitPointMarkerObject()
        {
            if (!m_RenderObjectPool.IsEmpty()) {
                return m_RenderObjectPool.Dequeue();
            }
            GLRenderObject newObject = new GLRenderObject();
            newObject.Renderer.Mesh = m_SphereMesh;
            newObject.Renderer.Material = new GLColorMaterial() { Color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f) };
            return newObject;
        }

        private const float OctreeSize = 20.0f;
        private RtCs.MathUtils.Geometry.Octree m_Octree = new RtCs.MathUtils.Geometry.Octree(2, OctreeSize, new Vector3(-OctreeSize * 0.5f));
        private OctreeRenderObject m_OctreeRenderObject = new OctreeRenderObject();

        private List<OctreeRegistableRenderObject> m_RaycastableObjects = new List<OctreeRegistableRenderObject>();
        private GLMesh m_CubeMesh = GLPrimitiveMesh.CreateBox();
        private GLMesh m_SphereMesh = GLPrimitiveMesh.CreateSphereICO(3);

        private List<GLRenderObject> m_HitPointMarkerObjects = new List<GLRenderObject>();
        private Queue<GLRenderObject> m_RenderObjectPool = new Queue<GLRenderObject>();

        private GLScene m_Scene = new GLScene();
        private GLPerspectiveProjection m_TPSProjection = new GLPerspectiveProjection();
        private OrbitCameraModel m_TPSCamera = new OrbitCameraModel();
        private OrbitCameraMouseController m_TPSCameraController = null;
        private GLPerspectiveProjection m_FPSProjection = new GLPerspectiveProjection();
        private FreeFlyCameraModel m_FPSCamera = new FreeFlyCameraModel();
        private FreeFlyCameraKeyMouseController m_FPSCameraController = null;
        private GLRenderObject m_FPSCameraVisualizer = new GLRenderObject();
        private GLRenderObject m_FPSCameraRayVisualizer = new GLRenderObject();
        private GLMesh m_FPSCameraRayMesh = GLPrimitiveMesh.CreateLines((new Vector3(0.0f), new Vector3(0.0f, 0.0f, -1.0f)));

        private readonly Vector3[] RenderColors = new Vector3[] {
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 0.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 1.0f),
            new Vector3(1.0f, 0.0f, 1.0f),
        };
    }
}
