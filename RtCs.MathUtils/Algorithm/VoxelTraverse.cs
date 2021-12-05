using RtCs.MathUtils.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.MathUtils.Algorithm
{
    public class VoxelTraverse
    {
        public IEnumerable<Container3<int>> Traverse(Line3D inRay)
        {
            // refered
            // http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.42.3443&rep=rep1&type=pdf

            if (!IsCrossedRayToBounds(inRay, out var t0, out var t1)) {
                // not cross with Bounds of grid.
                yield break;
            }

            Vector3 point0 = inRay.Along(t0);
            Vector3 point1 = inRay.Along(t1);

            Container3<int> startCellIndex = new Container3<int>(
                ((point0.x - BoundsMin.x + 1.0e-6) / CellDimension).TruncateToInt(),
                ((point0.y - BoundsMin.y + 1.0e-6) / CellDimension).TruncateToInt(),
                ((point0.z - BoundsMin.z + 1.0e-6) / CellDimension).TruncateToInt()
            );
            Container3<int> endCellIndex = new Container3<int>(
                ((point1.x - BoundsMin.x - 1.0e-6) / CellDimension).TruncateToInt(),
                ((point1.y - BoundsMin.y - 1.0e-6) / CellDimension).TruncateToInt(),
                ((point1.z - BoundsMin.z - 1.0e-6) / CellDimension).TruncateToInt()
            );
            Container3<int> currentCellIndex = startCellIndex;

            var direction = inRay.Vector;
            Container3<int> step = new Container3<int>(
                Math.Sign(direction.x),
                Math.Sign(direction.y),
                Math.Sign(direction.z)
            );

            Vector3 delta = new Vector3(
                step.Item0 == 0 ? t1 : Math.Abs(CellDimension / direction.x),
                step.Item1 == 0 ? t1 : Math.Abs(CellDimension / direction.y),
                step.Item2 == 0 ? t1 : Math.Abs(CellDimension / direction.z)
            );

            Vector3 max = new Vector3();
            for (int i = 0; i < 3; ++i) {
                switch (step[i]) {
                    case 0:
                        max[i] = double.PositiveInfinity;
                        break;
                    case 1:
                        max[i] = (CellDimension - ((point0[i] - BoundsMin[i]) % CellDimension)) / Math.Abs(direction[i]);
                        break;
                    case -1:
                        max[i] = ((point0[i] - BoundsMin[i]) % CellDimension) / Math.Abs(direction[i]);
                        break;
                }
            }

            yield return currentCellIndex;
            while ((currentCellIndex != endCellIndex) && max.Any(v => v.InRange(0.0, 1.0))) {
                int minIndex = max.MinIndex(t => Math.Abs(t));
                currentCellIndex[minIndex] += step[minIndex];
                max[minIndex] += delta[minIndex];

                yield return currentCellIndex;
            }
            yield break;
        }

        private bool IsCrossedRayToBounds(Line3D inRay, out double outT0, out double outT1)
        {
            // refered
            // https://github.com/cgyurgyik/fast-voxel-traversal-algorithm/blob/master/Ray.h
            // http://marupeke296.com/COL_3D_No18_LineAndAABB.html
            // http://marupeke296.com/COL_3D_No4_LineToPlanePolygon.html

            outT0 = 0.0;
            outT1 = 1.0;

            if (!(CalcLineParametersForBounds(inRay.Point0.x, inRay.Point1.x, BoundsMin.x, BoundsMax.x, out var x0, out var x1)
               && CalcLineParametersForBounds(inRay.Point0.y, inRay.Point1.y, BoundsMin.y, BoundsMax.y, out var y0, out var y1)
               && CalcLineParametersForBounds(inRay.Point0.z, inRay.Point1.z, BoundsMin.z, BoundsMax.z, out var z0, out var z1))) {
                return false;
            }

            outT0 = Numerics.Max(x0, y0, z0);
            outT1 = Numerics.Min(x1, y1, z1);
            return true;
        }

        private bool CalcLineParametersForBounds(double inPoint0, double inPoint1, double inBoundsMin, double inBoundsMax, out double outT0, out double outT1)
        {
            outT0 = 0.0;
            outT1 = 1.0;

            double direction = inPoint1 - inPoint0;
            if (direction.AlmostZero()) {
                if (inPoint0.InRange(inBoundsMin, inBoundsMax)) {
                    return true;
                } else {
                    return false; 
                }
            } else {
                if (!inPoint0.InRange(inBoundsMin, inBoundsMax)) {
                    if (direction > 0.0) {
                        // 1 : outT0 = inPoint1 - inPoint0 : inBoundsMin - inPoint0
                        outT0 = (inBoundsMin - inPoint0) / (inPoint1 - inPoint0);
                    } else { // direction < 0.0
                        // 1 : outT0 = inPoint1 - inPoint0 : inPoint0 - inBoundsMax
                        outT0 = (inPoint0 - inBoundsMax) / (inPoint0 - inPoint1);
                    }
                }
                if (!inPoint1.InRange(inBoundsMin, inBoundsMax)) {
                    if (direction > 0.0) {
                        // 1 : outT1 = inPoint1 - inPoint0 : inBoundsMax - inPoint0
                        outT1 = (inBoundsMax - inPoint0) / (inPoint1 - inPoint0);
                    } else { // direction < 0.0
                        // 1 : outT1 = inPoint1 - inPoint0 : inPoint0 - inBoundsMin
                        outT1 = (inPoint0 - inBoundsMin) / (inPoint0 - inPoint1);
                    }
                }
            }
            return outT0.InRange(0.0, 1.0) && outT1.InRange(0.0, 1.0);
        }

        public Vector3 BoundsMin
        { get; set; } = new Vector3(0.0, 0.0, 0.0);
        public Vector3 BoundsMax
            => BoundsMin + (new Vector3(CellDimension * CellCount));
        public double CellDimension
        { get; set; } = 1.0;
        public int CellCount
        { get; set; } = 5;
    }
}
