using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.MathUtils
{
    public struct Vector3 : IVector, IEquatable<Vector3>
    {
        public Vector3(IEnumerable<float> inValues)
        {
            var e = inValues.GetEnumerator();
            e.MoveNext();
            x = e.Current; e.MoveNext();
            y = e.Current; e.MoveNext();
            z = e.Current; e.MoveNext();
            return;
        }

        public Vector3(float inValue)
            : this(inValue, inValue, inValue)
        { }

        public Vector3(float inX, float inY, float inZ)
        {
            x = inX;
            y = inY;
            z = inZ;
            return;
        }

        public float x;
        public float y;
        public float z;

        public float this[int inIndex]
        {
            get {
                switch (inIndex) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set {
                switch (inIndex) {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public int Dimension => 3;
        int IReadOnlyCollection<float>.Count => Dimension;

        public float Length => Vector.Length(this);
        public float Length2 => Vector.Length2(this);
        public bool IsZero => Vector.IsZero(this);

        public void Normalize()
            => this = Normalized;
        public Vector3 Normalized
        {
            get {
                float len = Length2;
                if (len.AlmostZero()) {
                    return new Vector3(0.0f);
                }
                if (len.AlmostEquals(1.0f)) {
                    return this;
                }
                return this / (float)Math.Sqrt(len);
            }
        }

        public override bool Equals(object inObj)
            => inObj is Vector3 vector && Equals(vector);

        public bool Equals(Vector3 inOther)
            => x.AlmostEquals(inOther.x) &&
               y.AlmostEquals(inOther.y) &&
               z.AlmostEquals(inOther.z);

        public override int GetHashCode()
        {
            int hashCode = 373119288;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + z.GetHashCode();
            return hashCode;
        }

        public IEnumerator<float> GetEnumerator()
            => this.Enumerate().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public static bool operator ==(Vector3 inLeft, Vector3 inRight)
            => inLeft.Equals(inRight);
        public static bool operator !=(Vector3 inLeft, Vector3 inRight)
            => !(inLeft == inRight);
        public static Vector3 operator -(Vector3 inValue)
           => new Vector3(inValue.Select(v => -v));
        public static Vector3 operator +(Vector3 inLeft, Vector3 inRight)
            => new Vector3(Vector.Add(inLeft, inRight));
        public static Vector3 operator -(Vector3 inLeft, Vector3 inRight)
            => new Vector3(Vector.Subtract(inLeft, inRight));
        public static Vector3 operator *(Vector3 inLeft, float inRight)
            => new Vector3(Vector.Multiply(inLeft, inRight));
        public static Vector3 operator *(float inLeft, Vector3 inRight)
            => new Vector3(Vector.Multiply(inLeft, inRight));
        public static Vector3 operator /(Vector3 inLeft, float inRight)
            => new Vector3(Vector.Divide(inLeft, inRight));

        public static float Dot(Vector3 inLeft, Vector3 inRight)
            => Vector.Dot(inLeft, inRight);
        public static Vector3 Cross(Vector3 inLeft, Vector3 inRight)
            => new Vector3(
                (inLeft.y * inRight.z) - (inLeft.z * inRight.y),
                (inLeft.z * inRight.x) - (inLeft.x * inRight.z),
                (inLeft.x * inRight.y) - (inLeft.y * inRight.x)
            );

        public override string ToString()
            => $"{x}, {y}, {z}";
    }
}
