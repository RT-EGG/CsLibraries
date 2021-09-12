using GLTestVisualizer.TestView;
using GLTestVisualizer.TestView.AlphaBlending;
using GLTestVisualizer.TestView.FrustumTest;
using GLTestVisualizer.TestView.Octree;
using GLTestVisualizer.TestView.SphereMesh;
using GLTestVisualizer.TestView.TransformMatrixDexomposition;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace GLTestVisualizer
{
    enum TestContentType
    {
        TransformMatrixDecomposition,
        SphereMesh,
        FrustumTest,
        AlphaBlending,
        Octree
    }

    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            return;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ComboTestContent.Items.Clear();
            ComboTestContent.Items.Add(new TestContentItem {
                Type = TestContentType.TransformMatrixDecomposition,
                Text = "Transform Matrix Decomposition",
                ViewGenerator = () => new Ctrl_TransformMatrixDecompositionTestView()
            });
            ComboTestContent.Items.Add(new TestContentItem {
                Type = TestContentType.SphereMesh,
                Text = "Sphere Mesh",
                ViewGenerator = () => new Ctrl_SphereMeshTestView()
            });
            ComboTestContent.Items.Add(new TestContentItem {
                Type = TestContentType.FrustumTest,
                Text = "Frustum Test",
                ViewGenerator = () => new Ctrl_FrustumTestTestView()
            });
            ComboTestContent.Items.Add(new TestContentItem {
                Type = TestContentType.AlphaBlending,
                Text = "Alpha Blending",
                ViewGenerator = () => new Ctrl_AlphaBlendingTestView()
            });
            ComboTestContent.Items.Add(new TestContentItem {
                Type = TestContentType.Octree,
                Text = "Octree",
                ViewGenerator = () => new Ctrl_OctreeTestView()
            });

            ComboTestContent.SelectedIndex = 0;
            ComboTestContent_SelectionChangeCommitted(ComboTestContent, EventArgs.Empty);
            return;
        }

        private void ComboTestContent_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_CurrentViewer != null) {
                m_CurrentViewer.Exit();
                m_CurrentViewer.Parent = null;
                m_CurrentViewer = null;
            }

            if (ComboTestContent.SelectedItem == null) {
                return;
            }

            Debug.Assert(ComboTestContent.SelectedItem is TestContentItem);
            var item = ComboTestContent.SelectedItem as TestContentItem;

            m_CurrentViewer = item.ViewGenerator();
            if (m_CurrentViewer != null) {
                m_CurrentViewer.Parent = PanelContentView;
                m_CurrentViewer.Dock = DockStyle.Fill;

                m_CurrentViewer.Start();
            }
            return;
        }

        private Ctrl_TestView m_CurrentViewer = null;

        private class TestContentItem
        {
            public TestContentType Type;
            public string Text;
            public Func<Ctrl_TestView> ViewGenerator;

            public override string ToString() => Text;
        }
    }
}
