using RtCs.MathUtils;
using System;
using System.Windows.Forms;

namespace GLTestVisualizer.TestView.FrustumTest
{
    public partial class Ctrl_ProjectionFrustumParameterView : GLTestVisualizer.TestView.FrustumTest.Ctrl_ProjectionParameterView
    {
        public Ctrl_ProjectionFrustumParameterView()
        {
            InitializeComponent();
        }

        private double ProjectionTop => (double)(UdOffsetY.Value + (UdSizeY.Value * 0.5m));
        private double ProjectionBottom => (double)(UdOffsetY.Value - (UdSizeY.Value * 0.5m));

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

        public override Matrix4x4 ProjectionMatrix
            => Matrix4x4.MakeFrustum(
                (double)(UdOffsetX.Value - (UdSizeX.Value * 0.5m)),
                (double)(UdOffsetX.Value + (UdSizeX.Value * 0.5m)),
                (double)(UdOffsetY.Value - (UdSizeY.Value * 0.5m)),
                (double)(UdOffsetY.Value + (UdSizeY.Value * 0.5m)),
                (double)UdDepthMin.Value,
                (double)(UdDepthMin.Value + UdDepthLength.Value)
            );
    }
}
