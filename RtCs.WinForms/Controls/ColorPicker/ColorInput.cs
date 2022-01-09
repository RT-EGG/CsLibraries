using RtCs.MathUtils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RtCs.WinForms.Controls.ColorPicker
{
    partial class ColorInput : UserControl
    {
        public ColorInput()
        {
            InitializeComponent();
            
            RadioSelectModeChanged(RadioRgbSelectMode, EventArgs.Empty);
        }

        [Category("Action")]
        public event EventHandler ValueChanged;

        [Category("Behavior")]
        public bool AlphaControlVisible
        {
            get => AlphaLabel.Visible && AlphaSlider.Visible && AlphaUpDown.Visible;
            set {
                AlphaLabel.Visible = value;
                AlphaSlider.Visible = value;
                AlphaUpDown.Visible = value;
            }
        }

        private void RadioSelectModeChanged(object sender, EventArgs e)
        {
            if (m_ValueSetter != null) {
                m_ValueSetter.FinalizeComponents();
            }

            if (RadioRgbSelectMode.Checked) {
                m_ValueSetter = new ColorRgbSetter(this);
            } else if (RadioHsvSelectMode.Checked) {
                m_ValueSetter = new ColorHsvSetter(this);
            } else {
                throw new InvalidProgramException();
            }
            m_ValueSetter.ValueChanged += ValueSetterValueChanged;
            m_ValueSetter.InitializeComponents();
            m_ValueSetter.Value = Value;
            return;
        }

        [Category("Appearance")]
        public Color Value
        {
            get => m_Value;
            set {
                if (m_Value != value) {
                    m_Value = value;
                    if (!m_IsChangedFromUI) {
                        m_ValueSetter.Value = m_Value;
                    }

                    Invalidate();

                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void ValueSetterValueChanged(object sender, EventArgs e)
        {
            m_IsChangedFromUI = true;
            try {
                Value = m_ValueSetter.Value;
            } finally {
                m_IsChangedFromUI = false;
            }
        }

        private Color m_Value = Color.Transparent;
        private ColorSetter m_ValueSetter;
        private bool m_IsChangedFromUI = false;

        private abstract class ColorSetter
        {
            public ColorSetter(ColorInput inControl)
            {
                Control = inControl;
                ElementLabels = new Label[] {
                    Control.ElementLabel0, Control.ElementLabel1, Control.ElementLabel2, Control.AlphaLabel
                };
                ElementSliders = new ColorSlider[] {
                    Control.ElementSlider0, Control.ElementSlider1, Control.ElementSlider2, Control.AlphaSlider
                };
                ElementUpDowns = new NumericUpDown[] {
                    Control.ElementUpDown0, Control.ElementUpDown1, Control.ElementUpDown2, Control.AlphaUpDown
                };
                return;
            }

            public virtual void InitializeComponents()
            {
                ElementSliders.ForEach(c => c.ValueChanged += ElementSlider_ValueChanged);
                ElementUpDowns.ForEach(c => c.ValueChanged += ElementUpDown_ValueChanged);
            }

            public void FinalizeComponents()
            {
                ElementSliders.ForEach(c => c.ValueChanged -= ElementSlider_ValueChanged);
                ElementUpDowns.ForEach(c => c.ValueChanged -= ElementUpDown_ValueChanged);
            }

            public EventHandler ValueChanged;
            protected void CallValueChanged()
                => ValueChanged?.Invoke(this, EventArgs.Empty);

            protected abstract void SetValue(Color inValue);
            protected abstract Color GetValue();

            public Color Value 
            {
                get => GetValue();
                set => SetValue(value);
            }

            protected void DoWithoutValueChangedEvent(Action inAciton)
            {
                ElementSliders.ForEach(c => c.ValueChanged -= ElementSlider_ValueChanged);
                ElementUpDowns.ForEach(c => c.ValueChanged -= ElementUpDown_ValueChanged);
                try {
                    inAciton();

                } finally {
                    ElementSliders.ForEach(c => c.ValueChanged += ElementSlider_ValueChanged);
                    ElementUpDowns.ForEach(c => c.ValueChanged += ElementUpDown_ValueChanged);
                }
            }

            private void ElementSlider_ValueChanged(object sender, EventArgs e)
                => ElementSlider_ValueChangedCore(sender, e);

            private void ElementUpDown_ValueChanged(object sender, EventArgs e)
                => ElementUpDown_ValueChangedCore(sender, e);

            protected abstract void ElementSlider_ValueChangedCore(object sender, EventArgs e);
            protected abstract void ElementUpDown_ValueChangedCore(object sender, EventArgs e);

            private readonly ColorInput Control;
            protected readonly Label[] ElementLabels;
            protected readonly ColorSlider[] ElementSliders;
            protected readonly NumericUpDown[] ElementUpDowns;
        }

        private sealed class ColorRgbSetter : ColorSetter
        {
            public ColorRgbSetter(ColorInput inControl)
                : base (inControl)
            { }

            public override void InitializeComponents()
            {
                base.InitializeComponents();

                DoWithoutValueChangedEvent(() => {
                    ElementLabels[0].Text = "R :";
                    ElementLabels[1].Text = "G :";
                    ElementLabels[2].Text = "B :";
                    ElementUpDowns[0].Maximum = 255;
                    ElementUpDowns[1].Maximum = 255;
                    ElementUpDowns[2].Maximum = 255;
                    ElementSliders[0].Colors = new Color[] { Color.Black, Color.Red };
                    ElementSliders[1].Colors = new Color[] { Color.Black, Color.Green };
                    ElementSliders[2].Colors = new Color[] { Color.Black, Color.Blue };
                });

                UpdateSliderColor();
            }

            protected override void SetValue(Color inValue)
            {
                DoWithoutValueChangedEvent(() => {
                    ElementSliders[0].Value = inValue.R / 255.0f;
                    ElementSliders[1].Value = inValue.G / 255.0f;
                    ElementSliders[2].Value = inValue.B / 255.0f;
                    ElementSliders[3].Value = inValue.A / 255.0f;
                    ElementUpDowns[0].Value = inValue.R;
                    ElementUpDowns[1].Value = inValue.G;
                    ElementUpDowns[2].Value = inValue.B;
                    ElementUpDowns[3].Value = inValue.A;
                });

                m_Value = new ColorRGBA(inValue);
                UpdateSliderColor();
            }

            protected override Color GetValue()
                => m_Value.ToColor();

            protected override void ElementSlider_ValueChangedCore(object sender, EventArgs e)
            {
                m_Value.R = (byte)(ElementSliders[0].Value * 255.0f).Clamp(0.0f, 255.0f);
                m_Value.G = (byte)(ElementSliders[1].Value * 255.0f).Clamp(0.0f, 255.0f);
                m_Value.B = (byte)(ElementSliders[2].Value * 255.0f).Clamp(0.0f, 255.0f);
                m_Value.A = (byte)(ElementSliders[3].Value * 255.0f).Clamp(0.0f, 255.0f);

                DoWithoutValueChangedEvent(() => {
                    ElementUpDowns[0].Value = m_Value.R;
                    ElementUpDowns[1].Value = m_Value.G;
                    ElementUpDowns[2].Value = m_Value.B;
                    ElementUpDowns[3].Value = m_Value.A;
                });

                UpdateSliderColor();
                CallValueChanged();
                return;
            }

            protected override void ElementUpDown_ValueChangedCore(object sender, EventArgs e)
            {
                m_Value.R = (byte)ElementUpDowns[0].Value;
                m_Value.G = (byte)ElementUpDowns[1].Value;
                m_Value.B = (byte)ElementUpDowns[2].Value;
                m_Value.A = (byte)ElementUpDowns[3].Value;

                DoWithoutValueChangedEvent(() => {
                    ElementSliders[0].Value = m_Value.R / 255.0f;
                    ElementSliders[1].Value = m_Value.G / 255.0f;
                    ElementSliders[2].Value = m_Value.B / 255.0f;
                    ElementSliders[3].Value = m_Value.A / 255.0f;
                });

                UpdateSliderColor();
                CallValueChanged();
                return;
            }

            private void UpdateSliderColor()
            {
                ElementSliders[3].Colors = new Color[] { Color.Transparent, m_Value.ToColor() };
                return;
            }

            private ColorRGBA m_Value;
        }

        private sealed class ColorHsvSetter : ColorSetter
        {
            public ColorHsvSetter(ColorInput inControl)
                : base(inControl)
            { }

            public override void InitializeComponents()
            {
                base.InitializeComponents();

                DoWithoutValueChangedEvent(() => {
                    ElementLabels[0].Text = "H :";
                    ElementLabels[1].Text = "S :";
                    ElementLabels[2].Text = "V :";
                    ElementUpDowns[0].Maximum = 360;
                    ElementUpDowns[1].Maximum = 100;
                    ElementUpDowns[2].Maximum = 100;
                    ElementSliders[0].Colors = HueSliderColors;
                });
                UpdateSliderColor();
            }

            protected override void SetValue(Color inValue)
            {
                DoWithoutValueChangedEvent(() => {
                    ColorHSV hsv = new ColorRGB(inValue).ToHSV();
                    ElementSliders[0].Value = hsv.H;
                    ElementSliders[1].Value = hsv.S;
                    ElementSliders[2].Value = hsv.V;
                    ElementSliders[3].Value = inValue.A / 255.0f;
                    ElementUpDowns[0].Value = (decimal)hsv.H * ElementUpDowns[0].Maximum;
                    ElementUpDowns[1].Value = (decimal)hsv.S * ElementUpDowns[1].Maximum;
                    ElementUpDowns[2].Value = (decimal)hsv.V * ElementUpDowns[2].Maximum;
                    ElementUpDowns[3].Value = (decimal)inValue.A;
                });

                m_Value = new ColorRGB(inValue).ToHSV();
                m_Alpha = inValue.A;

                UpdateSliderColor();
            }

            protected override Color GetValue()
            {
                ColorRGB rgb = m_Value.ToRGB();
                return Color.FromArgb(m_Alpha, rgb.R, rgb.G, rgb.B);
            }

            protected override void ElementSlider_ValueChangedCore(object sender, EventArgs e)
            {
                m_Value.H = ElementSliders[0].Value;
                m_Value.S = ElementSliders[1].Value;
                m_Value.V = ElementSliders[2].Value;
                m_Alpha = (byte)(ElementSliders[3].Value * 255.0f).Clamp(0.0f, 255.0f);

                DoWithoutValueChangedEvent(() => {
                    ElementUpDowns[0].Value = (decimal)(m_Value.H) * ElementUpDowns[0].Maximum;
                    ElementUpDowns[1].Value = (decimal)(m_Value.S) * ElementUpDowns[1].Maximum;
                    ElementUpDowns[2].Value = (decimal)(m_Value.V) * ElementUpDowns[2].Maximum;
                    ElementUpDowns[3].Value = m_Alpha;
                });

                UpdateSliderColor();
                CallValueChanged();
                return;
            }

            protected override void ElementUpDown_ValueChangedCore(object sender, EventArgs e)
            {
                m_Value.H = (float)(ElementUpDowns[0].Value / ElementUpDowns[0].Maximum);
                m_Value.S = (float)(ElementUpDowns[1].Value / ElementUpDowns[1].Maximum);
                m_Value.V = (float)(ElementUpDowns[2].Value / ElementUpDowns[2].Maximum);
                m_Alpha = (byte)ElementUpDowns[3].Value;

                DoWithoutValueChangedEvent(() => {
                    ElementSliders[0].Value = m_Value.H;
                    ElementSliders[1].Value = m_Value.S;
                    ElementSliders[2].Value = m_Value.V;
                    ElementSliders[3].Value = m_Alpha / 255.0f;
                });

                UpdateSliderColor();
                CallValueChanged();
                return;
            }

            private void UpdateSliderColor()
            {
                ColorHSV sMin = new ColorHSV(m_Value.H, 0.0f, 1.0f);
                ColorHSV sMax = new ColorHSV(m_Value.H, 1.0f, 1.0f);
                ElementSliders[1].Colors = new Color[] { sMin.ToRGB().ToColor(), sMax.ToRGB().ToColor() };

                ColorHSV vMin = new ColorHSV(m_Value.H, 1.0f, 0.0f);
                ColorHSV vMax = new ColorHSV(m_Value.H, 1.0f, 1.0f);
                ElementSliders[2].Colors = new Color[] { vMin.ToRGB().ToColor(), vMax.ToRGB().ToColor() };

                ColorHSV aMax = new ColorHSV(m_Value.H, 1.0f, 1.0f);
                ElementSliders[3].Colors = new Color[] { Color.Transparent, aMax.ToRGB().ToColor() };
                return;
            }

            private ColorHSV m_Value;
            private byte m_Alpha;
            private readonly Color[] HueSliderColors = new Color[] {
                Color.FromArgb(255, 255, 0, 0),
                Color.FromArgb(255, 255, 255, 0),
                Color.FromArgb(255, 0, 255, 0),
                Color.FromArgb(255, 0, 255, 255),
                Color.FromArgb(255, 0, 0, 255),
                Color.FromArgb(255, 255, 0, 255),
                Color.FromArgb(255, 255, 0, 0),
            };
        }
    }
}
