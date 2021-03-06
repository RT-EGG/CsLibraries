using RtCs.MathUtils;
using RtCs.OpenGL;
using System;
using System.Windows.Forms;

namespace GLTestVisualizer.TestView.FrustumTest
{
    public partial class Ctrl_ProjectionParameterView : UserControl
    {
        public Ctrl_ProjectionParameterView()
        {
            InitializeComponent();
            return;
        }

        public virtual void SetAutoAspectReference(Control inControl)
        {
            if (AutoAspectReference != null) {
                AutoAspectReference.Resize -= AutoAspectReference_Resize;
            }

            AutoAspectReference = inControl;
            if (AutoAspectReference != null) {
                AutoAspectReference.Resize += AutoAspectReference_Resize;
            }
            return;
        }

        protected virtual void AutoAspectReference_Resize(object sender, EventArgs e)
        { }

        public virtual GLProjection Projection => null;

        protected Control AutoAspectReference
        { get; private set; } = null;
        protected float AspectRatio
        {
            get {
                if (AutoAspectReference != null) {
                    return (float)AutoAspectReference.Width / (float)AutoAspectReference.Height;
                }
                throw new NullReferenceException($"AspectRatio is refered when AutoAspectReference is null.");
            }
        }
    }
}
