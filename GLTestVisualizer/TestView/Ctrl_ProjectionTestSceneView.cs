using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GLTestVisualizer.TestView
{
    public partial class Ctrl_ProjectionTestSceneView : GLTestVisualizer.TestView.Ctrl_TestView
    {
        public Ctrl_ProjectionTestSceneView()
        {
            InitializeComponent();
        }

        public override string SceneName => "Projection";

        private void GL1stPersonView_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void OnViewMouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
