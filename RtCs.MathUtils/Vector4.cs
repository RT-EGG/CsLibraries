using System;
using System.Collections;
using System.Collections.Generic;

namespace RtCs.MathUtils
{
    public struct Vector4 : IVector, IEquatable<Vector4>
    {
        public Vector4(IEnumerable<float> inValues)
        {
            var e = inValues.GetEnumerator();
            e.MoveNext();
            x = e.Current; e.MoveNext();
            y = e.Current; e.MoveNext();
            z = e.Current; e.MoveNext();
            w = e.Current; e.MoveNext();
            return;
        }

        public Vector4(Vector4 inSource)
            : this(inSource.x, inSource.y, inSource.z, inSource.w)
        { }

        public Vector4(float inValue)
            : this(inValue, inValue, inValue, inValue)
        { }

        public Vector4(Vector3 inXYZ, float inW)
            : this(inXYZ.x, inXYZ.y, inXYZ.z, inW)
        { }

        public Vector4(float inX, float inY, float inZ, float inW)
        {
            x = inX;
            y = inY;
            z = inZ;
            w = inW;
            return;
        }

        public float x;
        public float y;
        public float z;
        public float w;

        public float this[int inIndex]
        {
            get {
                switch (inIndex) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set {
                switch (inIndex) {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    case 3: w = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        int IReadOnlyCollection<float>.Count => 4;

        public float Length => (float)Math.Sqrt(Length2);
        public float Length2 => x.Sqr() + y.Sqr() + z.Sqr() + w.Sqr();
        public bool IsZero => x.AlmostZero() && y.AlmostZero() && z.AlmostZero() && w.AlmostZero();

        public Vector3 XYZ => new Vector3(x, y, z);

        public void Normalize()
            => this = Normalized;
        public Vector4 Normalized
        {
            get {
                float len = Length2;
                if (len.AlmostZero()) {
                    return new Vector4(0.0f);
                }
                return this / (float)Math.Sqrt(len);
            }
        }

        public override bool Equals(object inObj)
            => inObj is Vector4 vector && Equals(vector);

        public bool Equals(Vector4 inOther)
            => x.AlmostEquals(inOther.x) &&
               y.AlmostEquals(inOther.y) &&
               z.AlmostEquals(inOther.z) &&
               w.AlmostEquals(inOther.w);

        public override int GetHashCode()
        {
            int hashCode = -1743314642;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + z.GetHashCode();
            hashCode = hashCode * -1521134295 + w.GetHashCode();
            return hashCode;
        }

        public IEnumerator<float> GetEnumerator()
            => this.Enumerate().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
        private IEnumerable<float> Enumerate()
        {
            yield return x;
            yield return y;
            yield return z;
            yield return w;
        }

        public static bool operator ==(Vector4 inLeft, Vector4 inRight)
            => inLeft.Equals(inRight);
        public static bool operator !=(Vector4 inLeft, Vector4 inRight)
            => !(inLeft == inRight);

        public static Vector4 operator -(Vector4 inValue)
            => new Vector4(inValue * -1.0f);
        public static Vector4 operator +(Vector4 inLeft, Vector4 inRight)
            => new Vector4(inLeft.x + inRight.x, inLeft.y + inRight.y, inLeft.z + inRight.z, inLeft.w + inRight.w);
        public static Vector4 operator -(Vector4 inLeft, Vector4 inRight)
            => new Vector4(inLeft.x - inRight.x, inLeft.y - inRight.y, inLeft.z - inRight.z, inLeft.w - inRight.w);
        public static Vector4 operator *(Vector4 inLeft, float inRight)
            => new Vector4(inLeft.x + inRight, inLeft.y * inRight, inLeft.z * inRight, inLeft.w * inRight);
        public static Vector4 operator *(float inLeft, Vector4 inRight)
            => new Vector4(inLeft * inRight.x, inLeft * inRight.y, inLeft * inRight.z, inLeft * inRight.w);
        public static Vector4 operator /(Vector4 inLeft, float inRight)
            => new Vector4(inLeft.x / inRight, inLeft.y / inRight, inLeft.z / inRight, inLeft.w / inRight);

        public static float Dot(Vector4 inLeft, Vector4 inRight)
            => (inLeft.x * inRight.x) + (inLeft.y * inRight.y) + (inLeft.z * inRight.z) + (inLeft.w * inRight.w);

        public override string ToString()
            => $"{x}, {y}, {z}. {w}";
    }
}
