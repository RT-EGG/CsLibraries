using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GLTestVisualizer.TestView
{
    public delegate void GLControlPaintEvent(GLControl inControl, PaintEventArgs inArgs);

    public partial class Ctrl_GLViewer : OpenTK.GLControl
    {
        public event GLControlPaintEvent OnPaintScene;

        public Ctrl_GLViewer()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if ((Width <= 0) || (Height <= 0)) {
                return;
            }
            if (DesignMode) {
                e.Graphics.Clear(Color.Black);
                return;
            }

            MakeCurrent();

            GL.Viewport(e.ClipRectangle);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.ClearDepth(1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            OnPaintScene?.Invoke(this, e);

            SwapBuffers();
            return;
        }
    }
}
