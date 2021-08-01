using RtCs.MathUtils;

namespace GLTestVisualizer
{
    class RotationCameraModel : CameraModel
    {
        public double ElevationAnbleTopLimitDeg
        { get; set; } = 89.0;
        public double ElevationAnbleBottomLimitDeg
        { get; set; } = -89.0;

        public override Matrix4x4 ViewMatrix
        {
            get {
                if (m_Changed) {
                    Transform.LocalRotation = Quaternion.MakeRotation(m_AzimuthAngle, new Vector3(0.0, 1.0, 0.0))
                                            * Quaternion.MakeRotation(m_ElevationAngle, new Vector3(1.0, 0.0, 0.0));
                    m_Changed = false;
                }

                return base.ViewMatrix;
            }
        }

        public double AzimuthAngleDeg
        {
            get => m_AzimuthAngle.RadToDeg();
            set {
                m_AzimuthAngle = value.DegToRad();
                m_Changed = true;
            }
        }

        public double ElevationAngleDeg
        {
            get => m_ElevationAngle.RadToDeg();
            set {
                m_ElevationAngle = value.Clamp(-ElevationAnbleTopLimitDeg, -ElevationAnbleBottomLimitDeg).DegToRad();
                m_Changed = true;
            }
        }

        private double m_AzimuthAngle;
        private double m_ElevationAngle;
        private bool m_Changed = true;
    }
}
