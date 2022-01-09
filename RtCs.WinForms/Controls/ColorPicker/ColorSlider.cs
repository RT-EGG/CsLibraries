using RtCs.MathUtils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace RtCs.WinForms.Controls
{
    partial class ColorSlider : UserControl
    {
        public ColorSlider()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }

        [Category("Action")]
        public event EventHandler ValueChanged;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if ((e.ClipRectangle.Width <= 0) || (e.ClipRectangle.Height <= 0)) {
                return;
            }

            Rectangle barRect = e.ClipRectangle;
            if (Direction == EColorSliderDirection.Vertical) {
                barRect.Width -= ValueLineOverhangLength;
            } else /*Direction == EColorSliderDirection.Horizontal*/ {
                barRect.Height -= ValueLineOverhangLength;
                barRect.Y += ValueLineOverhangLength;
            }

            PaintBar(e.Graphics, barRect);
            DrawCurrentValueLine(e.Graphics, e.ClipRectangle, Value);            
            return;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Point location = e.Location;
            if (!ClientRectangle.Contains(location)) {
                return;
            }

            m_MouseDowning = true;

            SetValueByLocalLocation(location);
            return;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            m_MouseDowning = false;
            return;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!m_MouseDowning) {
                return;
            }
            if (!e.Button.HasFlag(MouseButtons.Left)) {
                m_MouseDowning = false;
                return;
            }

            SetValueByLocalLocation(e.Location);
            return;
        }

        private void PaintBar(Graphics inGraphics, Rectangle inRectangle)
        {
            if ((inRectangle.Width <= 0) || (inRectangle.Height <= 0)) {
                return;
            }

            if ((m_Colors == null) || (m_Colors.Length < 1)) {
                Color fillColor = m_Colors.IsNullOrEmpty() ? Color.Black : m_Colors.First();
                Brush b = new SolidBrush(fillColor);
                inGraphics.FillRectangle(b, inRectangle);
                return;
            }

            ColorBlend blend = new ColorBlend();
            blend.Positions = m_Colors.Length.Range().Select(i => i / (float)(m_Colors.Length - 1)).ToArray();
            blend.Colors = m_Colors.ToArray();

            float angle = (Direction == EColorSliderDirection.Vertical) ? 90.0f : 0.0f;
            LinearGradientBrush brush = new LinearGradientBrush(inRectangle, Color.Black, Color.Black, angle);
            brush.InterpolationColors = blend;

            inGraphics.FillRectangle(brush, inRectangle);
            return;
        }

        private void DrawCurrentValueLine(Graphics inGraphics, Rectangle inRectangle, float inValue)
        {
            Pen linePen = new Pen(ValueLineColor);
            linePen.Width = ValueLineWidth;

            if (Direction == EColorSliderDirection.Vertical) {
                int current = (inValue * inRectangle.Height).TruncateToInt();
                inGraphics.DrawLine(linePen, 0, current, inRectangle.Right, current);
            } else /*Direction == EColorSliderDirection.Horizontal*/ {
                int current = (inValue * inRectangle.Width).TruncateToInt();
                inGraphics.DrawLine(linePen, current, 0, current, inRectangle.Bottom);
            }
            
            return;
        }

        [Category("Appearance")]
        public Color[] Colors
        {
            get => m_Colors;
            set {
                if (m_Colors != value) {
                    m_Colors = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        public EColorSliderDirection Direction
        {
            get => m_Direction;
            set {
                if (m_Direction != value) {
                    m_Direction = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        public Color ValueLineColor
        {
            get => m_ValueLineColor;
            set {
                if (m_ValueLineColor != value) {
                    m_ValueLineColor = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        public int ValueLineOverhangLength
        {
            get => m_ValueLineOverhangLength;
            set {
                if (m_ValueLineOverhangLength != value) {
                    m_ValueLineOverhangLength = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        public int ValueLineWidth
        {
            get => m_ValueLineWidth;
            set {
                if (m_ValueLineWidth != value) {
                    m_ValueLineWidth = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        public float Value
        {
            get => m_Value;
            set {
                value = value.Clamp(0.0f, 1.0f);
                if (!m_Value.AlmostEquals(value)) {
                    m_Value = value;
                    Invalidate();

                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void SetValueByLocalLocation(Point inPoint)
        {
            if (Direction == EColorSliderDirection.Vertical) {
                int current = (inPoint.Y - ClientRectangle.Top).Clamp(0, ClientRectangle.Height);
                Value = current / (float)ClientRectangle.Height;
            } else /*Direction == EColorSliderDirection.Horizontal*/ {
                int current = (inPoint.X - ClientRectangle.Left).Clamp(0, ClientRectangle.Width);
                Value = current / (float)ClientRectangle.Width;
            }            
            return;
        }

        private Color[] m_Colors = new Color[] {
                            Color.Black,
                            Color.White
                        };
        private EColorSliderDirection m_Direction = EColorSliderDirection.Vertical;
        private Color m_ValueLineColor = Color.Black;
        private int m_ValueLineOverhangLength = 8;
        private int m_ValueLineWidth = 3;
        private float m_Value = 0.0f;

        private bool m_MouseDowning = false;
    }

    public enum EColorSliderDirection
    {
        Vertical,
        Horizontal
    }

    static class ColorSliderDirectionConverter
    {
        public static LinearGradientMode ToLinearGradientMode(this EColorSliderDirection inValue)
        {
            switch (inValue) {
                case EColorSliderDirection.Vertical: return LinearGradientMode.Vertical;
                case EColorSliderDirection.Horizontal: return LinearGradientMode.Horizontal;
            }
            throw new InvalidEnumValueException<EColorSliderDirection>(inValue);
        }
    }
}
