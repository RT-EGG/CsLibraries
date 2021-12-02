using RtCs.MathUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GLTestVisualizer
{
    class RotationCameraMouseController : IDisposable
    {
        public RotationCameraMouseController(Control inControl)
        {
            Control = inControl;
            Control.MouseDown += Control_MouseDown;
            Control.MouseMove += Control_MouseMove;
            Control.MouseUp += Control_MouseUp;
            return;
        }

        public RotationCameraModel Camera
        { get; set; } = null;

        public double RotationPerPixel
        { get; set; } = (1.0).DegToRad();

        protected virtual void Dispose(bool inDisposing)
        {
            if (!m_Disposed) {
                Control.MouseDown -= Control_MouseDown;
                Control.MouseMove -= Control_MouseMove;
                Control.MouseUp -= Control_MouseUp;
                m_Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            return;
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            if (!m_MonitorMouseButtons.Contains(e.Button)) {
                return;
            }

            m_MouseButtonDown[e.Button] = true;
            m_LastMousePoint = e.Location;
            return;
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            if (!m_MonitorMouseButtons.Contains(e.Button)) {
                return;
            }

            m_MouseButtonDown[e.Button] = false;
            m_LastMousePoint = e.Location;
            return;
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            Point transfer = new Point(e.X - m_LastMousePoint.X, e.Y - m_LastMousePoint.Y);

            if (Camera == null) {
                return;
            }

            // fix up button downing
            foreach (var button in m_MonitorMouseButtons) {
                if ((e.Button & button) == 0) {
                    m_MouseButtonDown[button] = false;
                }
            }
            if (m_MouseButtonDown.All(item => !item.Value)) {
                return;
            }

            if (m_MouseButtonDown[MouseButtons.Left]) {
                Camera.AzimuthAngleDeg -= transfer.X * RotationPerPixel.RadToDeg();
                Camera.ElevationAngleDeg -= transfer.Y * RotationPerPixel.RadToDeg();
            }
            Cursor.Position = Control.PointToScreen(m_LastMousePoint);
            return;
        }

        private readonly Control Control = null;
        private bool m_Disposed;

        private Dictionary<MouseButtons, bool> m_MouseButtonDown = new Dictionary<MouseButtons, bool>();
        private Point m_LastMousePoint = default;

        private readonly MouseButtons[] m_MonitorMouseButtons = new MouseButtons[] {
            MouseButtons.Left,
        };
    }
}
