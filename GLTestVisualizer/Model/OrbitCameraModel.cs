using System;
using RtCs.MathUtils;

namespace GLTestVisualizer
{
    class OrbitCameraModel : ICameraModel
    {
        public double ElevationAnbleTopLimitDeg
        { get; set; } = 89.0;
        public double ElevationAnbleBottomLimitDeg
        { get; set; } = -89.0;
        public double DistanceLimitMin
        { get; set; } = 0.1;
        public double DistanceLimitMax
        { get; set; } = 100.0;

        public Matrix4x4 ViewMatrix
            => Matrix4x4.MakeLookAt(Center + Coordinate.GetRectangularCoordinate(), Center, new Vector3(0.0, 1.0, 0.0));

        public Vector3 Center
        { get; set; } = default;
        public SphericalCoordinate Coordinate
        { 
            get => m_Coordinate;
            set {
                m_Coordinate = value;
                m_Coordinate.ElevationAngleDeg = m_Coordinate.ElevationAngleDeg.Clamp(-ElevationAnbleTopLimitDeg, -ElevationAnbleBottomLimitDeg);
                m_Coordinate.Radius = m_Coordinate.Radius.Clamp(DistanceLimitMin, DistanceLimitMax);
                return;
            }
        }
        private SphericalCoordinate m_Coordinate = default;
    }
}
