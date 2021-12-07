using RtCs;
using RtCs.MathUtils;
using System;
using System.Windows.Forms;

namespace GLTestVisualizer.TestView.TransformMatrixDexomposition
{
    public delegate void Vector3EventHandler(object inSender, Vector3 inValue);
    public partial class Ctrl_DecompositeTransformMatrixView : UserControl
    {
        public event Vector3EventHandler TranslationChanged;
        public event Vector3EventHandler RotationChanged;
        public event Vector3EventHandler ScaleChanged;

        public Ctrl_DecompositeTransformMatrixView()
        {
            InitializeComponent();
        }

        public Vector3 Translation
        {
            get => new Vector3(
                    (float)UdTranslationX.Value,
                    (float)UdTranslationY.Value,
                    (float)UdTranslationZ.Value
                );

            set => WithLockingControlEvent(() => {
                UdTranslationX.Value = (decimal)value.x;
                UdTranslationY.Value = (decimal)value.y;
                UdTranslationZ.Value = (decimal)value.z;
            });
        }

        public Vector3 Rotation
        {
            get => new Vector3(
                    (float)UdRotationX.Value,
                    (float)UdRotationY.Value,
                    (float)UdRotationZ.Value
                );

            set => WithLockingControlEvent(() => {
                UdRotationX.Value = (decimal)value.x;
                UdRotationY.Value = (decimal)value.y;
                UdRotationZ.Value = (decimal)value.z;
            });
        }

        public new Vector3 Scale
        {
            get => new Vector3(
                    (float)UdScaleX.Value,
                    (float)UdScaleY.Value,
                    (float)UdScaleZ.Value
                );

            set => WithLockingControlEvent(() => {
                UdScaleX.Value = (decimal)value.x;
                UdScaleY.Value = (decimal)value.y;
                UdScaleZ.Value = (decimal)value.z;
            });
        }

        private void WithLockingControlEvent(Action inAction)
        {
            UIControled = false;
            try {
                inAction();
            } finally {
                UIControled = true;
            }
            return;
        }
        private bool UIControled = true;

        private void UdTranslation_ValueChanged(object sender, EventArgs e)
            => UIControled.When(() => TranslationChanged?.Invoke(this, Translation));
        private void UdRotation_ValueChanged(object sender, EventArgs e)
            => UIControled.When(() => RotationChanged?.Invoke(this, Rotation));
        private void UdScale_ValueChanged(object sender, EventArgs e)
            => UIControled.When(() => ScaleChanged?.Invoke(this, Scale));
    }
}
