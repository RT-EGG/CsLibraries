using RtCs.MathUtils;
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

        public virtual Matrix4x4 ProjectionMatrix => Matrix4x4.Identity;

        protected Control AutoAspectReference
        { get; private set; } = null;
        protected double AspectRatio
        {
            get {
                if (AutoAspectReference != null) {
                    return (double)AutoAspectReference.Width / (double)AutoAspectReference.Height;
                }
                throw new NullReferenceException($"AspectRatio is refered when AutoAspectReference is null.");
            }
        }
    }
}
