using System;
using System.Collections.Generic;

namespace RtCs.MathUtils.Geometry
{
    public struct AABB3D : IEquatable<AABB3D>, ILineIntersectable3D
    {
        public static AABB3D InclusionBoundary(IEnumerable<Vector3> inPoints)
        {
            Vector3 min = new Vector3(float.MaxValue);
            Vector3 max = new Vector3(float.MinValue);
            foreach (var point in inPoints) {
                min.x = Math.Min(min.x, point.x);
                min.y = Math.Min(min.y, point.y);
                min.z = Math.Min(min.z, point.z);
                max.x = Math.Max(max.x, point.x);
                max.y = Math.Max(max.y, point.y);
                max.z = Math.Max(max.z, point.z);
            }
            AABB3D result = default;
            result.Min = min;
            result.Max = max;
            return result;
        }

        public Vector3 Center
        {
            get => new Vector3(CenterX, CenterY, CenterZ);
            set {
                CenterX = value.x;
                CenterY = value.y;
                CenterZ = value.z;
            }
        }

        public Vector3 Size
        {
            get => new Vector3(SizeX, SizeY, SizeZ);
            set {
                SizeX = value.x;
                SizeY = value.y;
                SizeZ = value.z;
            }
        }
            
        public Vector3 Extent
        {
            get => new Vector3(ExtentX, ExtentY, ExtentZ);
            set {
                ExtentX = value.x;
                ExtentY = value.y;
                ExtentZ = value.z;
            }
        }

        public float CenterX
        {
            get => (MinX + MaxX) * 0.5f;
            set {
                float e = ExtentX;
                MinX = value - e;
                MaxX = value + e;
            }
        }

        public float CenterY
        {
            get => (MinY + MaxY) * 0.5f;
            set {
                float e = ExtentY;
                MinY = value - e;
                MaxY = value + e;
            }
        }

        public float CenterZ
        {
            get => (MinZ + MaxZ) * 0.5f;
            set {
                float e = ExtentZ;
                MinZ = value - e;
                MaxZ = value + e;
            }
        }

        public float SizeX
        {
            get => MaxX - MinX;
            set => ExtentX = value * 0.5f;
        }

        public float SizeY
        {
            get => MaxY - MinY;
            set => ExtentY = value * 0.5f;
        }

        public float SizeZ
        {
            get => MaxZ - MinZ;
            set => ExtentZ = value * 0.5f;
        }

        public float ExtentX
        {
            get => SizeX * 0.5f;
            set {
                float c = CenterX;
                MinX = c - value;
                MaxX = c + value;
            }
        }

        public float ExtentY
        {
            get => SizeY * 0.5f;
            set {
                float c = CenterY;
                MinY = c - value;
                MaxY = c + value;
            }
        }

        public float ExtentZ
        {
            get => SizeZ * 0.5f;
            set {
                float c = CenterZ;
                MinZ = c - value;
                MaxZ = c + value;
            }
        }

        public Vector3 Min
        {
            get => new Vector3(MinX, MinY, MinZ);
            set {
                MinX = value.x;
                MinY = value.y;
                MinZ = value.z;
            }
        }

        public Vector3 Max
        {
            get => new Vector3(MaxX, MaxY, MaxZ);
            set {
                MaxX = value.x;
                MaxY = value.y;
                MaxZ = value.z;
            }
        }

        public float MinX
        {
            get => m_BoundaryX.Min;
            set => m_BoundaryX.Min = value;
        }
        public float MaxX
        {
            get => m_BoundaryX.Max;
            set => m_BoundaryX.Max = value;
        }
        public float MinY
        {
            get => m_BoundaryY.Min;
            set => m_BoundaryY.Min = value;
        }
        public float MaxY
        {
            get => m_BoundaryY.Max;
            set => m_BoundaryY.Max = value;
        }
        public float MinZ
        {
            get => m_BoundaryZ.Min;
            set => m_BoundaryZ.Min = value;
        }
        public float MaxZ
        {
            get => m_BoundaryZ.Max;
            set => m_BoundaryZ.Max = value;
        }

        public float Volume
            => SizeX * SizeY * SizeZ;

        public float InnerRadius
            => (float)(0.5 * Math.Min(SizeX, Math.Min(SizeY, SizeZ)));
        public float OuterRadius
            => (float)(0.5 * Math.Sqrt(SizeX.Sqr() + SizeY.Sqr() + SizeZ.Sqr()));

        public void Include(float inX, float inY, float inZ)
        {
            MinX = Math.Min(MinX, inX);
            MaxX = Math.Max(MaxX, inX);
            MinY = Math.Min(MinY, inY);
            MaxY = Math.Max(MaxY, inY);
            MinZ = Math.Min(MinZ, inZ);
            MaxZ = Math.Max(MaxZ, inZ);
            return;
        }

        public void Include(Vector3 inPoint)
            => Include(inPoint.x, inPoint.y, inPoint.z);

        public bool Contains(float inX, float inY, float inZ)
            => m_BoundaryX.Contains(inX)
            && m_BoundaryY.Contains(inY)
            && m_BoundaryZ.Contains(inZ);

        public bool Contains(Vector3 inPoint)
            => Contains(inPoint.x, inPoint.y, inPoint.z);

        public Vector3 Clamp(Vector3 inValue)
            => new Vector3(
                    inValue.x.Clamp(MinX, MaxX),
                    inValue.y.Clamp(MinY, MaxY),
                    inValue.z.Clamp(MinZ, MaxZ)
                );

        public AABB3D Union(AABB3D inOther)
            => new AABB3D {
                MinX = Math.Min(MinX, inOther.MinX),
                MaxX = Math.Max(MaxX, inOther.MaxX),
                MinY = Math.Min(MinY, inOther.MinY),
                MaxY = Math.Max(MaxY, inOther.MaxY),
                MinZ = Math.Min(MinZ, inOther.MinZ),
                MaxZ = Math.Max(MaxZ, inOther.MaxZ)
            };

        public AABB3D Intersection(AABB3D inOther)
        {
            AABB3D result = default;
            result.MinX = Math.Max(MinX, inOther.MinX);
            result.MaxX = Math.Max(MaxX, inOther.MaxX);
            result.MinY = Math.Max(MinY, inOther.MinY);
            result.MaxY = Math.Max(MaxY, inOther.MaxY);
            result.MinZ = Math.Max(MinZ, inOther.MinZ);
            result.MaxZ = Math.Max(MaxZ, inOther.MaxZ);

            if (!result.IsValid) {
                result = default;
            }
            return result;
        }

        public bool IsValid
            => (MinX < MaxX) && (MinY < MaxY) && (MinZ < MaxZ);

        public override bool Equals(object obj)
            => obj is AABB3D d && Equals(d);

        public bool Equals(AABB3D other)
            => m_BoundaryX.Equals(other.m_BoundaryX) 
            && m_BoundaryY.Equals(other.m_BoundaryY)
            && m_BoundaryZ.Equals(other.m_BoundaryZ);

        public override int GetHashCode()
        {
            int hashCode = 1482604910;
            hashCode = hashCode * -1521134295 + m_BoundaryX.GetHashCode();
            hashCode = hashCode * -1521134295 + m_BoundaryY.GetHashCode();
            hashCode = hashCode * -1521134295 + m_BoundaryZ.GetHashCode();
            return hashCode;
        }

        public IEnumerable<LineIntersectionInfo3D> IsIntersectWith(Line3D inLine)
        {
            // refer http://marupeke296.com/COL_3D_No18_LineAndAABB.html

            Vector3 GetNormal(int inIndex)
            {
                switch (inIndex) {
                    case 0: return new Vector3(1.0f, 0.0f, 0.0f);
                    case 1: return new Vector3(0.0f, 1.0f, 0.0f);
                    case 2: return new Vector3(0.0f, 0.0f, 1.0f);
                }
                throw new ArgumentException(nameof(inIndex));
            }

            Vector3 p = inLine.Point0;
            Vector3 d = inLine.Direction;
            Vector3 min = Min;
            Vector3 max = Max;

            float near = float.MinValue;
            float far = float.MaxValue;
            Vector3 nearNormal = default;
            Vector3 farNormal = default;
            for (int i = 0; i < 3; ++i) {
                if (!d[i].AlmostZero()) {
                    if ((p[i] < min[i]) || (max[i] < p[i])) {
                        yield break;
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
                        nearNormal = GetNormal(i);
                        if (t0 < t1)
                            nearNormal *= -1.0f;
                    }
                    if (t1 < far) {
                        far = t1;
                        farNormal = GetNormal(i);
                        if (t0 > t1)
                            farNormal *= -1.0f;
                    }
                    if (near >= far) {
                        yield break;
                    }
                }
            }

            yield return new LineIntersectionInfo3D {
                HitObject = this,
                LineParameter = near,
                Position = inLine.Along(near),
                Normal = nearNormal
            };
            yield return new LineIntersectionInfo3D {
                HitObject = this,
                LineParameter = far,
                Position = inLine.Along(far),
                Normal = farNormal
            };
        }

        public static bool operator ==(AABB3D left, AABB3D right)
            => left.Equals(right);
        public static bool operator !=(AABB3D left, AABB3D right)
            => !(left == right);

        public static AABB3D operator &(AABB3D left, AABB3D right)
            => left.Intersection(right);
        public static AABB3D operator |(AABB3D left, AABB3D right)
            => left.Union(right);

        private Range1D m_BoundaryX;
        private Range1D m_BoundaryY;
        private Range1D m_BoundaryZ;
    }
}
