using GLTestVisualizer.TestView;
using GLTestVisualizer.TestView.TransformMatrixDexomposition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GLTestVisualizer
{
    enum TestContentType
    {
        TransformMatrixDecomposition
    }

    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ComboTestContent.Items.Clear();
            ComboTestContent.Items.Add(new TestContentItem {
                Type = TestContentType.TransformMatrixDecomposition,
                Text = "Transform Matrix Decomposition",
                ViewGenerator = () => new Ctrl_TransformMatrixDecompositionTestView()
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
