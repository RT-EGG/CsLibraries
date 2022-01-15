using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace RtCs.OpenGL.WinForms
{
    public partial class GLControl : OpenTK.WinForms.GLControl
    {
        [Category("Rendering")]
        public event EventHandler OnRenderScene;

        public GLControl()
        {
            InitializeComponent();
            return;
        }

        public new void MakeCurrent()
        {
            //if (GraphicsContext == null) {
            //    GraphicsContext = this.Context;
            //}
            //GraphicsContext = this.Context;
            base.MakeCurrent();
            //if (GraphicsContext != this.Context) {
            //    GraphicsContext.MakeCurrent();
            //}

            GLMainThreadTaskQueue.CurrentContext = Context;
            return;
        }

        public void MakeNonCurrent()
        {
            Context.MakeNoneCurrent();
            GLMainThreadTaskQueue.CurrentContext = null;
            return;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) {
                OnPaintProc = DesignModelPaint;
            } else {
                Profile = ContextProfile.Compatability;
                OnPaintProc = RuntimePaint;
            }
            return;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if ((Width <= 0) || (Height <= 0)) {
                return;
            }
            OnPaintProc?.Invoke(e);
            return;
        }

        private Action<PaintEventArgs> OnPaintProc;
        private void DesignModelPaint(PaintEventArgs e)
            => e.Graphics.Clear(BackColor);
        private void RuntimePaint(PaintEventArgs e)
        {
            MakeCurrent();
            GLMainThreadTaskQueue.Process();

            //GL.Viewport(e.ClipRectangle);
            GL.Viewport(this.ClientRectangle);

            GL.ClearColor(BackColor);
            GL.ClearDepth(1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            OnRenderScene?.Invoke(this, EventArgs.Empty);

            SwapBuffers();
            MakeNonCurrent();
            return;
        }

        public new bool DesignMode => GetDesignMode(this);
        private bool GetDesignMode(Control inControl)
        {
            if (inControl == null) {
                return false;
            }

            bool mode = inControl.Site != null && inControl.Site.DesignMode;
            return mode | GetDesignMode(inControl.Parent);
        }

        //private static IGraphicsContext GraphicsContext
        //{ get; set; } = null;
    }
}
