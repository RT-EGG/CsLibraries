using System.Collections.Generic;

namespace RtCs.MathUtils.Geometry
{
    public class Triangle3D : ILineIntersectable3D
    {
        public Vector3 Vertex0
        { get; set; } = new Vector3();
        public Vector3 Vertex1
        { get; set; } = new Vector3();
        public Vector3 Vertex2
        { get; set; } = new Vector3();

        public Vector3? Normal
        {
            get {
                Vector3 v10 = (Vertex1 - Vertex0).Normalized;
                Vector3 v20 = (Vertex2 - Vertex0).Normalized;
                if (v10.IsZero || v20.IsZero) {
                    return null;
                }

                return Vector3.Cross(v10, v20).Normalized;
            }
        }

        public IEnumerable<LineIntersectionInfo3D> IsIntersectWith(Line3D inLine)
        {
            // refered https://qiita.com/edo_m18/items/2bd885b13bd74803a368

            double Det(Vector3 v1, Vector3 v2, Vector3 v3)
                => new Matrix3x3(
                    v1.x, v1.y, v1.z,
                    v2.x, v2.y, v2.z,
                    v3.x, v3.y, v3.z
                ).Determinant;

            var invRay = -inLine.Vector;
            var edge1 = Vertex1 - Vertex0;
            var edge2 = Vertex2 - Vertex0;

            var det = Det(invRay, edge1, edge2);

            if (det.AlmostZero()) {
                // is parallel
                yield break;
            }

            var d = inLine.Point0 - Vertex0;
            var u = Det(d, edge2, invRay) / det;
            if (u.InRange(0.0, 1.0)) {
                var v = Det(edge1, d, invRay) / det;
                if (v.InRange(0.0, 1.0) && ((u + v) <= 1.0)) {
                    var t = Det(edge1, edge2, d) / det;

                    yield return new LineIntersectionInfo3D {
                        HitObject = this,
                        Position = inLine.Along(t),
                        Normal = Normal,
                        LineParameter = t
                    };
                }
            }

            yield break;
        }
    }
}
