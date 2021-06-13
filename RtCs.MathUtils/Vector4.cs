using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.MathUtils
{
    public struct Vector4 : IVector, IEquatable<Vector4>
    {
        public Vector4(IEnumerable<double> inValues)
        {
            var e = inValues.GetEnumerator();
            e.MoveNext();
            x = e.Current; e.MoveNext();
            y = e.Current; e.MoveNext();
            z = e.Current; e.MoveNext();
            w = e.Current; e.MoveNext();
            return;
        }

        public Vector4(double inValue)
            : this(inValue, inValue, inValue, inValue)
        { }

        public Vector4(Vector3 inXYZ, double inW)
            : this(inXYZ.x, inXYZ.y, inXYZ.z, inW)
        { }

        public Vector4(double inX, double inY, double inZ, double inW)
        {
            x = inX;
            y = inY;
            z = inZ;
            w = inW;
            return;
        }

        public double x;
        public double y;
        public double z;
        public double w;

        public double this[int inIndex]
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

        public double Length => Vector.Length(this);
        public double Length2 => Vector.Length2(this);
        public bool IsZero => Vector.IsZero(this);

        public void Normalize()
            => this = Normalized;
        public Vector4 Normalized
        {
            get {
                double len = Length2;
                if (len.AlmostZero()) {
                    return new Vector4(0.0f);
                }
                return this / Math.Sqrt(len);
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

        public IEnumerator<double> GetEnumerator()
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
        public static Vector4 operator *(Vector4 inLeft, double inRight)
            => new Vector4(Vector.Multiply(inLeft, inRight));
        public static Vector4 operator *(double inLeft, Vector4 inRight)
            => new Vector4(Vector.Multiply(inLeft, inRight));
        public static Vector4 operator /(Vector4 inLeft, double inRight)
            => new Vector4(Vector.Divide(inLeft, inRight));

        public static double Dot(Vector4 inLeft, Vector4 inRight)
            => Vector.Dot(inLeft, inRight);
    }
}
