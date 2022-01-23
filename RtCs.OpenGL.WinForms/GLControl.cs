using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace RtCs.OpenGL.WinForms
{
    public partial class GLControl : OpenTK.GLControl, IGLRenderTarget
    {
        [Category("Appearance")]
        public event EventHandler RenderScene;

        public GLControl()
        { 
            InitializeComponent();
            return;
        }        

        public new void MakeCurrent()
        {
            if (GraphicsContext == null) {
                GraphicsContext = this.Context;
            }
            base.MakeCurrent();
            if (GraphicsContext != this.Context) {
                GraphicsContext.MakeCurrent(this.WindowInfo);
            }

            if (GLProperties == null) {
                GLProperties = new GLProperties();
                GLProperties.Collect();
            }
            return;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) {
                OnPaintProc = DesignModelPaint;
            } else {
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

            GL.Viewport(e.ClipRectangle);

            GL.ClearColor(BackColor);
            GL.ClearDepth(1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            RenderScene?.Invoke(this, EventArgs.Empty);

            SwapBuffers();
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

        private static GLProperties GLProperties
        { get; set; } = null;
        private static IGraphicsContext GraphicsContext
        { get; set; } = null;
    }
}
