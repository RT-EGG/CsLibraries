using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        public int Dimension => 4;
        int IReadOnlyCollection<float>.Count => Dimension;

        public float Length => Vector.Length(this);
        public float Length2 => Vector.Length2(this);
        public bool IsZero => Vector.IsZero(this);

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

        public static bool operator ==(Vector4 inLeft, Vector4 inRight)
            => inLeft.Equals(inRight);
        public static bool operator !=(Vector4 inLeft, Vector4 inRight)
            => !(inLeft == inRight);

        public static Vector4 operator -(Vector4 inValue)
            => new Vector4(inValue.Select(v => -v));
        public static Vector4 operator +(Vector4 inLeft, Vector4 inRight)
            => new Vector4(Vector.Add(inLeft, inRight));
        public static Vector4 operator -(Vector4 inLeft, Vector4 inRight)
            => new Vector4(Vector.Subtract(inLeft, inRight));
        public static Vector4 operator *(Vector4 inLeft, float inRight)
            => new Vector4(Vector.Multiply(inLeft, inRight));
        public static Vector4 operator *(float inLeft, Vector4 inRight)
            => new Vector4(Vector.Multiply(inLeft, inRight));
        public static Vector4 operator /(Vector4 inLeft, float inRight)
            => new Vector4(Vector.Divide(inLeft, inRight));

        public static float Dot(Vector4 inLeft, Vector4 inRight)
            => Vector.Dot(inLeft, inRight);

        public override string ToString()
            => $"{x}, {y}, {z}. {w}";

        public static implicit operator Vector3(Vector4 inValue)
            => new Vector3(inValue);
    }
}
