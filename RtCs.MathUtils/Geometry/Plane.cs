using System;
using System.Collections.Generic;

namespace RtCs.MathUtils.Geometry
{
    public struct Plane : ILineIntersectable3D
    {
        public Plane(Vector3 inPoint, Vector3 inNormal)
        {
            Point = inPoint;
            Normal = inNormal;
            return;
        }

        public Vector3 Point
        { get; set; }
        public Vector3 Normal
        { get; set; }

        public Vector3 Project(Vector3 inPoint)
        {
            if (Normal.IsZero) {
                return Point;
            }

            if (!Normal.Length2.AlmostEquals(1.0f)) {
                Normal = Normal.Normalized;
            }

            return inPoint - (Vector3.Dot(inPoint - Point, Normal) * Normal);
        }

        public float DistanceTo(Vector3 inPoint)
            => (Project(inPoint) - inPoint).Length;

        public IEnumerable<LineIntersectionInfo3D> IsIntersectWith(Line3D inLine)
        {
            // refer http://www.sousakuba.com/Programming/gs_plane_line_intersect.html

            if (Vector3.Dot(inLine.Vector, Normal).AlmostZero()) {
                // parallel
                yield break;
            }

            float n_c0 = Vector3.Dot(Normal, inLine.Point0 - Point);
            float n_c1 = Vector3.Dot(Normal, inLine.Point1 - Point);
            float abs_n_c0 = Math.Abs(n_c0);
            float abs_n_c1 = Math.Abs(n_c1);

            if (abs_n_c0.AlmostZero()) {
                // Point0 on plane
                yield return new LineIntersectionInfo3D {
                    HitObject = this,
                    LineParameter = 0.0f,
                    Position = inLine.Point0,
                    Normal = Normal
                };
                yield break;
            }
            if (abs_n_c1.AlmostZero()) {
                // Point1 on Plane
                yield return new LineIntersectionInfo3D {
                    HitObject = this,
                    LineParameter = 1.0f,
                    Position = inLine.Point1,
                    Normal = Normal
                };
                yield break;
            }

            float t = default;
            if ((n_c0 * n_c1) < 0.0) {
                // each of Point0 and Point1 is both side of plane.
                // 1 : t = abs_n_c0 + abs_n_c1 : abs_n_c0
                t = abs_n_c0 / (abs_n_c1 + abs_n_c0);
            } else {
                // Point0 and Point1 is one side of plane
                // 1 : t = abs_n_c0 - abs_n_c1 : abs_n_c0
                t = abs_n_c0 / (abs_n_c1 - abs_n_c0);
            }

            yield return new LineIntersectionInfo3D {
                HitObject = this,
                LineParameter = t,
                Position = inLine.Along(t),
                Normal = Normal
            };
        }
    }
}
