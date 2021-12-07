using RtCs.MathUtils.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.MathUtils
{
    public class RayCaster
    {
        public IEnumerable<LineIntersectionInfo3D> Test(Line3D inRay, IEnumerable<ILineIntersectable3D> inTestObjects)
        {
            var result = inTestObjects.Select(o => o.IsIntersectWith(inRay)).Flatten();
            if (ClampToRayRange) {
                result = result.Where(o => o.LineParameter.InRange(0.0, 1.0));
            }

            if (CullBackFace) {
                Vector3 rayDirection = inRay.Direction;
                bool FaceToRay(Vector3 inNormal)
                {
                    if (inNormal.IsZero) {
                        return false;
                    }
                    inNormal = inNormal.Normalized;
                    return Vector.Dot(rayDirection, inNormal) < 0.0f;
                }

                result = result.Where(o => o.Normal.HasValue && FaceToRay(o.Normal.Value));
            }

            if (SortByDistance) {
                List<LineIntersectionInfo3D> sorted = new List<LineIntersectionInfo3D>(result);
                sorted.Sort((l, r) => Math.Sign(l.LineParameter - r.LineParameter));
                result = sorted;
            }
            return result;
        }

        public bool SortByDistance
        { get; set; } = false;
        public bool CullBackFace
        { get; set; } = false;
        public bool ClampToRayRange
        { get; set; } = true;
    }
}
