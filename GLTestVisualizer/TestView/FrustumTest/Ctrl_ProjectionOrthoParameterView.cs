using RtCs.MathUtils;
using RtCs.OpenGL;
using System;
using System.Windows.Forms;

namespace GLTestVisualizer.TestView.FrustumTest
{
    public sealed partial class Ctrl_ProjectionOrthoParameterView : Ctrl_ProjectionParameterView
    {
        public Ctrl_ProjectionOrthoParameterView()
        {
            InitializeComponent();
            return;
        }

        private float ProjectionTop => (float)(UdOffsetY.Value + (UdSizeY.Value * 0.5m));
        private float ProjectionBottom => (float)(UdOffsetY.Value - (UdSizeY.Value * 0.5m));

        public override void SetAutoAspectReference(Control inControl)
        {
            base.SetAutoAspectReference(inControl);

            UdSizeX.Enabled = inControl == null;
            if (!UdSizeX.Enabled) {
                UdSizeX.Value = (decimal)((ProjectionTop - ProjectionBottom) * AspectRatio);
            }
            return;
        }

        protected override void AutoAspectReference_Resize(object sender, EventArgs e)
        {
            base.AutoAspectReference_Resize(sender, e);
            if (!UdSizeX.Enabled) {
                UdSizeX.Value = (decimal)((ProjectionTop - ProjectionBottom) * AspectRatio);
            }
            return;
        }

        private void UdSizeY_Changed(object inSender, EventArgs e)
        {
            if (!UdSizeX.Enabled) {
                UdSizeX.Value = (decimal)((ProjectionTop - ProjectionBottom) * AspectRatio);
            }
            return;
        }

        public override GLProjection Projection
        { 
            get {
                m_Projection.Left = (float)(UdOffsetX.Value - (UdSizeX.Value * 0.5m));
                m_Projection.Right = (float)(UdOffsetX.Value + (UdSizeX.Value * 0.5m));
                m_Projection.Bottom = (float)(UdOffsetY.Value - (UdSizeY.Value * 0.5m));
                m_Projection.Top = (float)(UdOffsetY.Value + (UdSizeY.Value * 0.5m));
                m_Projection.Near = (float)UdDepthMin.Value;
                m_Projection.Far = (float)(UdDepthMin.Value + UdDepthLength.Value);
                return m_Projection;
            } 
        }

        private GLOrthoProjection m_Projection = new GLOrthoProjection();
    }
}
