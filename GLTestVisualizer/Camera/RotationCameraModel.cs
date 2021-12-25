using RtCs.MathUtils;

namespace GLTestVisualizer
{
    class RotationCameraModel : CameraModel
    {
        public float ElevationAnbleTopLimitDeg
        { get; set; } = 89.0f;
        public float ElevationAnbleBottomLimitDeg
        { get; set; } = -89.0f;

        public float AzimuthAngleDeg
        {
            get => m_AzimuthAngle.RadToDeg();
            set {
                m_AzimuthAngle = value.DegToRad();
                Transform.LocalRotation = Quaternion.MakeRotation(m_AzimuthAngle, new Vector3(0.0f, 1.0f, 0.0f))
                                        * Quaternion.MakeRotation(m_ElevationAngle, new Vector3(1.0f, 0.0f, 0.0f));
            }
        }

        public float ElevationAngleDeg
        {
            get => m_ElevationAngle.RadToDeg();
            set {
                m_ElevationAngle = value.Clamp(-ElevationAnbleTopLimitDeg, -ElevationAnbleBottomLimitDeg).DegToRad();
                Transform.LocalRotation = Quaternion.MakeRotation(m_AzimuthAngle, new Vector3(0.0f, 1.0f, 0.0f))
                                        * Quaternion.MakeRotation(m_ElevationAngle, new Vector3(1.0f, 0.0f, 0.0f));
            }
        }

        private float m_AzimuthAngle;
        private float m_ElevationAngle;
    }
}
