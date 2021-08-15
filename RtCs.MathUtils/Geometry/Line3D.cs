using System;
using System.Collections.Generic;

namespace RtCs.MathUtils.Geometry
{
    public struct Line3D
    {
        public Line3D(Vector3 inPoint0, Vector3 inPoint1)
        {
            Point0 = inPoint0;
            Point1 = inPoint1;
            return;
        }

        public Vector3 Point0
        { get; set; }
        public Vector3 Point1
        { get; set; }

        public Vector3 Along(double inRatio)
            => Point0 + ((Point1 - Point0) * inRatio);

        public Vector3 Vector => Point1 - Point0;
        public Vector3 Direction => Vector.Normalized;        
        public double Length => Vector.Length;

        public Vector3 Project(Vector3 inPoint)
            => Project(inPoint, out double _);

        public Vector3 Project(Vector3 inPoint, out double outParameter)
        {
            if (Vector.IsZero) {
                outParameter = 0.0;
                return Point0;
            }
            outParameter = Vector3.Dot(Direction, inPoint - Point0) / Length;
            return Point0 + (outParameter * Vector);
        }

        public double DistanceTo(Vector3 inPoint)
        {
            Project(inPoint, out double t);
            return (inPoint - Along(t.Clamp(0.0, 1.0))).Length;
        }

        public bool IsIntersect(Plane inPlane, out double outT, out Vector3 outPosition)
        {
            // refer http://www.sousakuba.com/Programming/gs_plane_line_intersect.html

            if (Vector3.Dot(Vector, inPlane.Normal).AlmostZero()) {
                outT = default;
                outPosition = default;
                return false;
            }

            double n_c0 = Vector3.Dot(inPlane.Normal, Point0 - inPlane.Point);
            double n_c1 = Vector3.Dot(inPlane.Normal, Point1 - inPlane.Point);
            double abs_n_c0 = Math.Abs(n_c0);
            double abs_n_c1 = Math.Abs(n_c1);

            if (abs_n_c0.AlmostZero()) {
                // Point0 on plane
                outT = 0.0;
                outPosition = Point0;
                return true;
            }
            if (abs_n_c1.AlmostZero()) {
                // Point1 on Plane
                outT = 1.0;
                outPosition = Point1;
                return true;
            }

            if ((n_c0 * n_c1) < 0.0) {
                // each of Point0 and Point1 is both side of plane.
                // 1 : t = abs_n_c0 + abs_n_c1 : abs_n_c0
                outT = abs_n_c0 / (abs_n_c1 + abs_n_c0);
            } else {
                // Point0 and Point1 is one side of plane
                // 1 : t = abs_n_c0 - abs_n_c1 : abs_n_c0
                outT = abs_n_c0 / (abs_n_c1 - abs_n_c0);
            }

            outPosition = Point0 + (Vector * outT);
            return outT.InRange(0.0, 1.0);
        }

        public bool IsIntersect(AABB3D inAABB)
            => IsIntersect(inAABB, out double _, out double _);

        public bool IsIntersect(AABB3D inAABB, out double outParamNear, out double outParamFar)
        {
            // refer http://marupeke296.com/COL_3D_No18_LineAndAABB.html

            outParamNear = double.NaN;
            outParamFar = double.NaN;

            Vector3 p = Point0;
            Vector3 d = Direction;
            Vector3 min = inAABB.Min;
            Vector3 max = inAABB.Max;

            double near = double.MinValue;
            double far = double.MaxValue;
            for (int i = 0; i < 3; ++i) {
                if (!d[i].AlmostZero()) {
                    if ((p[i] < min[i]) || (max[i] < p[i])) {
                        return false;
                    }

                    double odd = 1.0 / d[i];
                    double t0 = (min[i] - p[i]) * odd;
                    double t1 = (max[i] - p[i]) * odd;
                    if (t0 > t1) {
                        double tmp = t0;
                        t0 = t1;
                        t1 = tmp;
                    }

                    if (t0 > near) {
                        near = t0;
                    }
                    if (t1 < far) {
                        far = t1;
                    }
                    if (near >= far) {
                        return false;
                    }
                }
            }

            outParamNear = near;
            outParamFar = far;
            return true;
        }
    }

    //public enum ELineIntersectionQueryDetails
    //{
    //    OnlyHitting = 0,
    //    Position = 0x0001,
    //    LinePara
    //}

    public class LineIntersectionInfo3D
    {
        public ILineIntersectable3D HitObject
        { get; set; } = null;
        public Vector3 Position
        { get; set; } = new Vector3();
        public double LineParameter
        { get; set; } = double.NaN;
        public Vector3? Normal
        { get; set; } = null;
    }

    public interface ILineIntersectable3D
    {
        IEnumerable<LineIntersectionInfo3D> IsIntersectWith(Line3D inLine);
    }
}
