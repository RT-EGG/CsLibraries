﻿using RtCs.MathUtils;

namespace GLTestVisualizer
{
    class OrbitCameraModel : CameraModel
    {
        public double ElevationAnbleTopLimitDeg
        { get; set; } = 89.0;
        public double ElevationAnbleBottomLimitDeg
        { get; set; } = -89.0;
        public double DistanceLimitMin
        { get; set; } = 0.1;
        public double DistanceLimitMax
        { get; set; } = 100.0;

        public override Matrix4x4 ViewMatrix
        {
            get {
                if (m_Changed) {
                    Transform.LookAt(Center + Coordinate.GetRectangularCoordinate(), Center, new Vector3(0.0, 1.0, 0.0));
                    m_Changed = false;
                }
                return base.ViewMatrix;
            }
        }

        public Vector3 Center
        {
            get => m_Center;
            set {
                m_Center = value;
                m_Changed = true;
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
                m_Changed = true;
                return;
            }
        }

        private Vector3 m_Center = default;
        private SphericalCoordinate m_Coordinate = default;
        private bool m_Changed = true;
    }
}