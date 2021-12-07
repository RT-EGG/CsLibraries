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

        public Vector3 Along(float inRatio)
            => Point0 + ((Point1 - Point0) * inRatio);

        public Vector3 Vector => Point1 - Point0;
        public Vector3 Direction => Vector.Normalized;        
        public float Length => Vector.Length;

        public Vector3 Project(Vector3 inPoint)
            => Project(inPoint, out float _);

        public Vector3 Project(Vector3 inPoint, out float outParameter)
        {
            if (Vector.IsZero) {
                outParameter = 0.0f;
                return Point0;
            }
            outParameter = Vector3.Dot(Direction, inPoint - Point0) / Length;
            return Point0 + (outParameter * Vector);
        }

        public float DistanceTo(Vector3 inPoint)
        {
            Project(inPoint, out float t);
            return (inPoint - Along(t.Clamp(0.0f, 1.0f))).Length;
        }

        public bool IsIntersect(Plane inPlane, out float outT, out Vector3 outPosition)
        {
            // refer http://www.sousakuba.com/Programming/gs_plane_line_intersect.html

            if (Vector3.Dot(Vector, inPlane.Normal).AlmostZero()) {
                outT = default;
                outPosition = default;
                return false;
            }

            float n_c0 = Vector3.Dot(inPlane.Normal, Point0 - inPlane.Point);
            float n_c1 = Vector3.Dot(inPlane.Normal, Point1 - inPlane.Point);
            float abs_n_c0 = Math.Abs(n_c0);
            float abs_n_c1 = Math.Abs(n_c1);

            if (abs_n_c0.AlmostZero()) {
                // Point0 on plane
                outT = 0.0f;
                outPosition = Point0;
                return true;
            }
            if (abs_n_c1.AlmostZero()) {
                // Point1 on Plane
                outT = 1.0f;
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
            return outT.InRange(0.0f, 1.0f);
        }

        public bool IsIntersect(AABB3D inAABB)
            => IsIntersect(inAABB, out float _, out float _);

        public bool IsIntersect(AABB3D inAABB, out float outParamNear, out float outParamFar)
        {
            // refer http://marupeke296.com/COL_3D_No18_LineAndAABB.html

            outParamNear = float.NaN;
            outParamFar = float.NaN;

            Vector3 p = Point0;
            Vector3 d = Direction;
            Vector3 min = inAABB.Min;
            Vector3 max = inAABB.Max;

            float near = float.MinValue;
            float far = float.MaxValue;
            for (int i = 0; i < 3; ++i) {
                if (!d[i].AlmostZero()) {
                    if ((p[i] < min[i]) || (max[i] < p[i])) {
                        return false;
                    }

                    float odd = 1.0f / d[i];
                    float t0 = (min[i] - p[i]) * odd;
                    float t1 = (max[i] - p[i]) * odd;
                    if (t0 > t1) {
                        float tmp = t0;
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
        public float LineParameter
        { get; set; } = float.NaN;
        public Vector3? Normal
        { get; set; } = null;
    }

    public interface ILineIntersectable3D
    {
        IEnumerable<LineIntersectionInfo3D> IsIntersectWith(Line3D inLine);
    }
}
