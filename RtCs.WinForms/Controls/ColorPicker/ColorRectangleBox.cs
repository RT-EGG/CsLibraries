using RtCs.MathUtils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RtCs.WinForms.Controls
{
    partial class ColorPickerBox : UserControl
    {
        public ColorPickerBox()
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

            PaintBox(e.Graphics, e.ClipRectangle);
            PaintMarker(e.Graphics, e.ClipRectangle, m_ValueX, m_ValueY);
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

        private void PaintBox(Graphics inGraphics, Rectangle inRectangle)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(inRectangle);

            PathGradientBrush brush = new PathGradientBrush(path);
            brush.SurroundColors = new Color[] {
                LeftTopColor, RightTopColor, LeftBottomColor, RightBottomColor
            };

            Color top = LeftTopColor.Interpolate(RightTopColor);
            Color bottom = LeftBottomColor.Interpolate(RightBottomColor);
            brush.CenterColor = top.Interpolate(bottom);

            inGraphics.FillRectangle(brush, inRectangle);
            return;
        }

        private void PaintMarker(Graphics inGraphics, Rectangle inRectangle, float inValueX, float inValueY)
        {
            Point center = new Point(
                    (inRectangle.Width * inValueX).TruncateToInt(),
                    (inRectangle.Height * inValueY).TruncateToInt()
                );

            Pen linePen = new Pen(MarkerColor);
            linePen.Width = 3;
            const int LineLength = 6;
            const int Offset = 5;
            inGraphics.DrawLine(linePen, new Point(center.X - (Offset + LineLength), center.Y), new Point(center.X - Offset, center.Y));
            inGraphics.DrawLine(linePen, new Point(center.X + (Offset + LineLength), center.Y), new Point(center.X + Offset, center.Y));
            inGraphics.DrawLine(linePen, new Point(center.X, center.Y - (Offset + LineLength)), new Point(center.X, center.Y - Offset));
            inGraphics.DrawLine(linePen, new Point(center.X, center.Y + (Offset + LineLength)), new Point(center.X, center.Y + Offset));
            return;
        }

        [Category("Appearance")]
        public float ValueX
        {
            get => m_ValueX;
            set {
                value = value.Clamp(0.0f, 1.0f);
                if (!m_ValueX.AlmostEquals(value)) {
                    m_ValueX = value;
                    Invalidate();

                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [Category("Appearance")]
        public float ValueY
        {
            get => m_ValueY;
            set {
                value = value.Clamp(0.0f, 1.0f);
                if (!m_ValueY.AlmostEquals(value)) {
                    m_ValueY = value;
                    Invalidate();

                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [Category("Appearance")]
        public Color LeftTopColor
        {
            get => m_LeftTopColor;
            set {
                if (m_LeftTopColor != value) {
                    m_LeftTopColor = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        public Color LeftBottomColor
        {
            get => m_LeftBottomColor;
            set {
                if (m_LeftBottomColor != value) {
                    m_LeftBottomColor = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        public Color RightTopColor
        {
            get => m_RightTopColor;
            set {
                if (m_RightTopColor != value) {
                    m_RightTopColor = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        public Color RightBottomColor
        {
            get => m_RightBottomColor;
            set {
                if (m_RightBottomColor != value) {
                    m_RightBottomColor = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        public Color MarkerColor
        {
            get => m_MarkerColor;
            set {
                if (m_MarkerColor != value) {
                    m_MarkerColor = value;
                    Invalidate();
                }
            }
        }

        private void SetValueByLocalLocation(Point inPoint)
        {
            int x = (inPoint.X - ClientRectangle.Left).Clamp(0, ClientRectangle.Width);
            int y = (inPoint.Y - ClientRectangle.Top).Clamp(0, ClientRectangle.Height);
            ValueX = x / (float)ClientRectangle.Width;
            ValueY = y / (float)ClientRectangle.Height;
            return;
        }

        private float m_ValueX = 0.0f;
        private float m_ValueY = 0.0f;
        private Color m_LeftTopColor = Color.FromArgb(255, 255, 255, 255);
        private Color m_LeftBottomColor = Color.FromArgb(255, 0, 0, 0);
        private Color m_RightTopColor = Color.FromArgb(255, 255, 0, 0);
        private Color m_RightBottomColor = Color.FromArgb(255, 0, 0, 0);
        private Color m_MarkerColor = Color.Black;

        private bool m_MouseDowning = false;
    }
}
