using RtCs.MathUtils;

namespace GLTestVisualizer
{
    class OrbitCameraModel : CameraModel
    {
        public float ElevationAnbleTopLimitDeg
        { get; set; } = 89.0f;
        public float ElevationAnbleBottomLimitDeg
        { get; set; } = -89.0f;
        public float DistanceLimitMin
        { get; set; } = 0.1f;
        public float DistanceLimitMax
        { get; set; } = 100.0f;

        public Vector3 Center
        {
            get => m_Center;
            set {
                m_Center = value;
                Transform.LocalPosition = Center + Coordinate.GetRectangularCoordinate();
                Transform.LocalRotation = Quaternion.FromEuler(m_Coordinate.ElevationAngle, m_Coordinate.AzimuthAngle, 0.0f, EEulerRotationOrder.YXZ);
                return;
            }
        }
        public SphericalCoordinate Coordinate
        {
            get => m_Coordinate;
            set {
                m_Coordinate = value;
                m_Coordinate.ElevationAngleDeg = m_Coordinate.ElevationAngleDeg.Clamp(-ElevationAnbleTopLimitDeg, -ElevationAnbleBottomLimitDeg);
                m_Coordinate.Radius = m_Coordinate.Radius.Clamp(DistanceLimitMin, DistanceLimitMax);
                Transform.LocalPosition = Center + Coordinate.GetRectangularCoordinate();
                Transform.LocalRotation = Quaternion.FromEuler(m_Coordinate.ElevationAngle, m_Coordinate.AzimuthAngle, 0.0f, EEulerRotationOrder.YXZ);
                return;
            }
        }

        private Vector3 m_Center = default;
        private SphericalCoordinate m_Coordinate = default;
    }
}
