using System;
using System.Drawing;
using System.Windows.Forms;

namespace GLTestVisualizer.TestView.Lighting
{
    public partial class ColorPickerPopupDialog : Form
    {
        public ColorPickerPopupDialog()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
        }

        public event EventHandler ValueChanged;

        public Color Value
        {
            get => ColorPicker.Value;
            set => ColorPicker.Value = value;
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            Close();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Escape) {
                Close();
            }
        }

        private void ColorPicker_ValueChanged(object sender, EventArgs e)
            => ValueChanged?.Invoke(sender, e);
    }
}
