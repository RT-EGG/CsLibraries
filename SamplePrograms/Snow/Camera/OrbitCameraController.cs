using RtCs.MathUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Snow.Camera
{
    class OrbitCameraController : IDisposable
    {
        public OrbitCameraController(Control inControl)
        {
            Control = inControl;
            Control.MouseDown += Control_MouseDown;
            Control.MouseMove += Control_MouseMove;
            Control.MouseUp += Control_MouseUp;
            Control.MouseWheel += Control_MouseWheel;
            return;
        }

        protected virtual void Dispose(bool inDisposing)
        {
            if (!m_Disposed) {
                Control.MouseDown -= Control_MouseDown;
                Control.MouseMove -= Control_MouseMove;
                Control.MouseUp -= Control_MouseUp;
                Control.MouseWheel -= Control_MouseWheel;
                m_Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            return;
        }

        public OrbitCamera Camera
        { get; set; } = null;

        public float TranslatePerPixel
        { get; set; } = 0.01f;
        public float TranslatePerWheel
        { get; set; } = 0.1f;
        public float RotationPerPixel
        { get; set; } = (1.0f).DegToRad();

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

            Vector3 center = Camera.Center;
            SphericalCoordinate coord = Camera.Coordinate;

            if ((m_MouseButtonDown[MouseButtons.Left] && m_MouseButtonDown[MouseButtons.Right]) || m_MouseButtonDown[MouseButtons.Middle]) {
                // move center x-z plane
                Vector3 z = new Vector3(Matrix4x4.MakeRotateY(coord.AzimuthAngle) * (new Vector4(0.0f, 0.0f, -1.0f, 0.0f)));
                z.y = 0.0f;
                z.Normalize();
                Vector3 x = Vector3.Cross(z, new Vector3(0.0f, 1.0f, 0.0f));

                center += (x * transfer.X * TranslatePerPixel) + (z * -transfer.Y * TranslatePerPixel);

            } else {
                if (m_MouseButtonDown[MouseButtons.Left]) {
                    coord.AzimuthAngle += transfer.X * RotationPerPixel;
                    coord.ElevationAngle -= transfer.Y * RotationPerPixel;
                } else if (m_MouseButtonDown[MouseButtons.Right]) {
                    coord.Radius += transfer.X * TranslatePerPixel;
                    center.y += transfer.Y * TranslatePerPixel;
                }
            }

            Camera.Center = center;
            Camera.Coordinate = coord;

            Cursor.Position = Control.PointToScreen(m_LastMousePoint);
            return;
        }

        private void Control_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Camera == null) {
                return;
            }

            SphericalCoordinate coord = Camera.Coordinate;
            coord.Radius -= (e.Delta / SystemInformation.MouseWheelScrollDelta) * TranslatePerWheel;
            Camera.Coordinate = coord;
            return;
        }

        private readonly Control Control = null;
        private bool m_Disposed;

        private Dictionary<MouseButtons, bool> m_MouseButtonDown = new Dictionary<MouseButtons, bool>();
        private Point m_LastMousePoint = default;

        private readonly MouseButtons[] m_MonitorMouseButtons = new MouseButtons[] {
            MouseButtons.Left,
            MouseButtons.Right,
            MouseButtons.Middle
        };
    }
}
