using RtCs.MathUtils;

namespace GLTestVisualizer
{
    class RotationCameraModel : CameraModel
    {
        public float ElevationAnbleTopLimitDeg
        { get; set; } = 89.0f;
        public float ElevationAnbleBottomLimitDeg
        { get; set; } = -89.0f;

        public override Matrix4x4 ViewMatrix
        {
            get {
                if (m_Changed) {
                    Transform.LocalRotation = Quaternion.MakeRotation(m_AzimuthAngle, new Vector3(0.0f, 1.0f, 0.0f))
                                            * Quaternion.MakeRotation(m_ElevationAngle, new Vector3(1.0f, 0.0f, 0.0f));
                    m_Changed = false;
                }

                return base.ViewMatrix;
            }
        }

        public float AzimuthAngleDeg
        {
            get => m_AzimuthAngle.RadToDeg();
            set {
                m_AzimuthAngle = value.DegToRad();
                m_Changed = true;
            }
        }

        public float ElevationAngleDeg
        {
            get => m_ElevationAngle.RadToDeg();
            set {
                m_ElevationAngle = value.Clamp(-ElevationAnbleTopLimitDeg, -ElevationAnbleBottomLimitDeg).DegToRad();
                m_Changed = true;
            }
        }

        private float m_AzimuthAngle;
        private float m_ElevationAngle;
        private bool m_Changed = true;
    }
}
