using RtCs.MathUtils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RtCs.WinForms.Controls.ColorPicker
{
    public partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            InitializeComponent();

            HSlider.Colors = new Color[] {
                Color.FromArgb(255, 255, 0, 0),
                Color.FromArgb(255, 255, 255, 0),
                Color.FromArgb(255, 0, 255, 255),
                Color.FromArgb(255, 0, 0, 255),
                Color.FromArgb(255, 255, 0, 255),
                Color.FromArgb(255, 255, 0, 0),
            };

            Value = Color.FromArgb(255, 0, 0, 0);
            return;
        }

        [Category("Action")]
        public event EventHandler ValueChanged;

        public Color Value
        {
            get => m_Value.ToRGB().ToColor();
            set {
                m_Value = new ColorRGB(value).ToHSV();
                m_Alpha = value.A;

                HSlider.ValueChanged -= HSlider_ValueChanged;
                SVPicker.ValueChanged -= SVPicker_ValueChanged;
                ColorInput.ValueChanged -= ColorInput_ValueChanged;
                try {
                    HSlider.Value = m_Value.H;
                    SVPicker.ValueX = m_Value.S;
                    SVPicker.ValueY = 1.0f - m_Value.V;
                    ColorInput.Value = value;

                    SampleColorPanel.BackColor = value;

                } finally {
                    HSlider.ValueChanged += HSlider_ValueChanged;
                    SVPicker.ValueChanged += SVPicker_ValueChanged;
                    ColorInput.ValueChanged += ColorInput_ValueChanged;
                }

                ColorHSV hMax = new ColorHSV(m_Value.H, 1.0f, 1.0f);
                SVPicker.RightTopColor = hMax.ToRGB().ToColor();

                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void HSlider_ValueChanged(object sender, EventArgs e)
        {
            m_Value.H = (sender as ColorSlider).Value;
            Value = m_Value.ToRGB().ToColor();
        }

        private void SVPicker_ValueChanged(object sender, EventArgs e)
        {
            m_Value.S = (sender as ColorPickerBox).ValueX;
            m_Value.V = 1.0f - (sender as ColorPickerBox).ValueY;
            Value = m_Value.ToRGB().ToColor();
        }

        private void ColorInput_ValueChanged(object sender, EventArgs e)
        {
            Value = (sender as ColorInput).Value;
        }

        private ColorHSV m_Value;
        private byte m_Alpha = 255;
    }
}
