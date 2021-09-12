using System;
using System.Collections.Generic;

namespace RtCs.MathUtils.Geometry
{
    public struct AABB3D : IEquatable<AABB3D>
    {
        public static AABB3D InclusionBoundary(IEnumerable<Vector3> inPoints)
        {
            Vector3 min = new Vector3(double.MaxValue);
            Vector3 max = new Vector3(double.MinValue);
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

        public double CenterX
        {
            get => (MinX + MaxX) * 0.5;
            set {
                double e = ExtentX;
                MinX = value - e;
                MaxX = value + e;
            }
        }

        public double CenterY
        {
            get => (MinY + MaxY) * 0.5;
            set {
                double e = ExtentY;
                MinY = value - e;
                MaxY = value + e;
            }
        }

        public double CenterZ
        {
            get => (MinZ + MaxZ) * 0.5;
            set {
                double e = ExtentZ;
                MinZ = value - e;
                MaxZ = value + e;
            }
        }

        public double SizeX
        {
            get => MaxX - MinX;
            set => ExtentX = value * 0.5;
        }

        public double SizeY
        {
            get => MaxY - MinY;
            set => ExtentY = value * 0.5;
        }

        public double SizeZ
        {
            get => MaxZ - MinZ;
            set => ExtentZ = value * 0.5;
        }

        public double ExtentX
        {
            get => SizeX * 0.5;
            set {
                double c = CenterX;
                MinX = c - value;
                MaxX = c + value;
            }
        }

        public double ExtentY
        {
            get => SizeY * 0.5;
            set {
                double c = CenterY;
                MinY = c - value;
                MaxY = c + value;
            }
        }

        public double ExtentZ
        {
            get => SizeZ * 0.5;
            set {
                double c = CenterZ;
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

        public double MinX
        {
            get => m_BoundaryX.Min;
            set => m_BoundaryX.Min = value;
        }
        public double MaxX
        {
            get => m_BoundaryX.Max;
            set => m_BoundaryX.Max = value;
        }
        public double MinY
        {
            get => m_BoundaryY.Min;
            set => m_BoundaryY.Min = value;
        }
        public double MaxY
        {
            get => m_BoundaryY.Max;
            set => m_BoundaryY.Max = value;
        }
        public double MinZ
        {
            get => m_BoundaryZ.Min;
            set => m_BoundaryZ.Min = value;
        }
        public double MaxZ
        {
            get => m_BoundaryZ.Max;
            set => m_BoundaryZ.Max = value;
        }

        public double Volume
            => SizeX * SizeY * SizeZ;

        public double InnerRadius
            => 0.5 * Math.Min(SizeX, Math.Min(SizeY, SizeZ));
        public double OuterRadius
            => 0.5 * Math.Sqrt(SizeX.Sqr() + SizeY.Sqr() + SizeZ.Sqr());

        public void Include(double inX, double inY, double inZ)
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

        public bool Contains(double inX, double inY, double inZ)
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
