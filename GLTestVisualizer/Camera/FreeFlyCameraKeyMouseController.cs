using RtCs.MathUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GLTestVisualizer
{
    class FreeFlyCameraKeyMouseController
    {
        public FreeFlyCameraKeyMouseController(Control inControl)
        {
            Control = inControl;
            Control.MouseDown += Control_MouseDown;
            Control.MouseMove += Control_MouseMove;
            Control.MouseUp += Control_MouseUp;
            Control.KeyDown += Control_KeyDown;
            Control.KeyUp += Control_KeyUp;
            Control.Invalidated += Control_Invalidated;
            Control.LostFocus += Control_LostFocus;
            return;
        }

        public FreeFlyCameraModel Camera
        { get; set; } = null;

        public float RotationPerPixelDeg
        { get; set; } = 1.0f;
        public float TransferPerFrame
        { get; set; } = 0.5f * (1.0f / 60.0f);

        protected virtual void Dispose(bool inDisposing)
        {
            if (!m_Disposed) {
                Control.MouseDown -= Control_MouseDown;
                Control.MouseMove -= Control_MouseMove;
                Control.MouseUp -= Control_MouseUp;
                Control.KeyDown -= Control_KeyDown;
                Control.KeyUp -= Control_KeyUp;
                Control.Invalidated -= Control_Invalidated;
                Control.LostFocus -= Control_LostFocus;
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
                Camera.TurnLeft(transfer.X * RotationPerPixelDeg);
                Camera.TurnUp(transfer.Y * RotationPerPixelDeg);
            }
            Cursor.Position = Control.PointToScreen(m_LastMousePoint);
            return;
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_ControlKeyAssign.TryGetValue(e.KeyCode, out var control)) {
                if (m_ControlKeyState.ContainsKey(control)) {
                    m_ControlKeyState[control] = true;
                }
            }
            return;
        }

        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            if (m_ControlKeyAssign.TryGetValue(e.KeyCode, out var control)) {
                if (m_ControlKeyState.ContainsKey(control)) {
                    m_ControlKeyState[control] = false;
                }
            }
        }

        private void Control_Invalidated(object sender, InvalidateEventArgs e)
        {
            Vector3 transfer = new Vector3(0.0f);
            foreach (var control in m_ControlKeyState.Where(item => item.Value).Select(item => item.Key)) {
                if (m_KeyControlTransfer.TryGetValue(control, out var t)) {
                    transfer += t;
                }
            }
            if (!transfer.IsZero) {
                transfer = transfer.Normalized * TransferPerFrame;
                if (m_ControlKeyState[EKeyControls.Boost]) {
                    transfer *= 3.0f;
                }
                Camera.LocalMove(transfer);
            }
            return;
        }

        private void Control_LostFocus(object sender, EventArgs e)
        {
            foreach (var key in m_ControlKeyState.Keys.ToArray()) {
                m_ControlKeyState[key] = false;
            }
            return;
        }

        private readonly Control Control = null;
        private bool m_Disposed;

        private Dictionary<MouseButtons, bool> m_MouseButtonDown = new Dictionary<MouseButtons, bool>();
        private Point m_LastMousePoint = default;
        private readonly MouseButtons[] m_MonitorMouseButtons = new MouseButtons[] {
            MouseButtons.Left,
        };

        private enum EKeyControls
        {
            MoveForward,
            MoveBack,
            MoveLeft,
            MoveRight,
            MoveUp,
            MoveDown,
            Boost
        }
        private Dictionary<Keys, EKeyControls> m_ControlKeyAssign = new Dictionary<Keys, EKeyControls>() {
            { Keys.W, EKeyControls.MoveForward },
            { Keys.S, EKeyControls.MoveBack },
            { Keys.A, EKeyControls.MoveLeft },
            { Keys.D, EKeyControls.MoveRight },
            { Keys.E, EKeyControls.MoveUp },
            { Keys.Q, EKeyControls.MoveDown },
            { Keys.ShiftKey, EKeyControls.Boost }
        };
        private Dictionary<EKeyControls, bool> m_ControlKeyState = new Dictionary<EKeyControls, bool>() {
            { EKeyControls.MoveForward, false },
            { EKeyControls.MoveBack, false },
            { EKeyControls.MoveLeft, false },
            { EKeyControls.MoveRight, false },
            { EKeyControls.MoveUp, false },
            { EKeyControls.MoveDown, false },
            { EKeyControls.Boost, false }
        };
        private Dictionary<EKeyControls, Vector3> m_KeyControlTransfer = new Dictionary<EKeyControls, Vector3>() {
            { EKeyControls.MoveForward, new Vector3(0.0f, 0.0f, -1.0f) },
            { EKeyControls.MoveBack, new Vector3(0.0f, 0.0f, 1.0f) },
            { EKeyControls.MoveLeft, new Vector3(-1.0f, 0.0f, 0.0f) },
            { EKeyControls.MoveRight, new Vector3(1.0f, 0.0f, 0.0f) },
            { EKeyControls.MoveUp, new Vector3(0.0f, 1.0f, 0.0f) },
            { EKeyControls.MoveDown, new Vector3(0.0f, -1.0f, 0.0f)},
        };
    }
}
