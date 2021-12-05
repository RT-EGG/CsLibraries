using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.MathUtils
{
    public struct Vector2 : IVector, IEquatable<Vector2>
    {
        public Vector2(IEnumerable<double> inValues)
        {
            var e = inValues.GetEnumerator();
            e.MoveNext();
            x = e.Current; e.MoveNext();
            y = e.Current; e.MoveNext();
            return;
        }

        public Vector2(double inValue)
            : this(inValue, inValue)
        { }

        public Vector2(double inX, double inY)
        {
            x = inX;
            y = inY;
            return;
        }

        public double x;
        public double y;

        public double this[int inIndex]
        {
            get {
                switch (inIndex) {
                    case 0: return x;
                    case 1: return y;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set {
                switch (inIndex) {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public int Dimension => 2;
        int IReadOnlyCollection<double>.Count => Dimension;

        public double Length => Vector.Length(this);
        public double Length2 => Vector.Length2(this);
        public bool IsZero => Vector.IsZero(this);

        public void Normalize()
            => this = Normalized;
        public Vector2 Normalized
        {
            get {
                double len = Length2;
                if (len.AlmostZero()) {
                    return new Vector2(0.0);
                }
                return this / (double)Math.Sqrt(len);
            }
        }

        public override bool Equals(object inObj)
            => inObj is Vector2 vector && Equals(vector);

        public bool Equals(Vector2 inOther)
            => x.AlmostEquals(inOther.x) &&
               y.AlmostEquals(inOther.y);

        public override int GetHashCode()
        {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        public IEnumerator<double> GetEnumerator()
            => this.Enumerate().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public static bool operator ==(Vector2 inLeft, Vector2 inRight)
            => inLeft.Equals(inRight);
        public static bool operator !=(Vector2 inLeft, Vector2 inRight)
            => !(inLeft == inRight);
        public static Vector2 operator -(Vector2 inValue)
            => new Vector2(inValue.Select(v => -v));
        public static Vector2 operator +(Vector2 inLeft, Vector2 inRight)
            => new Vector2(Vector.Add(inLeft, inRight));
        public static Vector2 operator -(Vector2 inLeft, Vector2 inRight)
            => new Vector2(Vector.Subtract(inLeft, inRight));
        public static Vector2 operator *(Vector2 inLeft, double inRight)
            => new Vector2(Vector.Multiply(inLeft, inRight));
        public static Vector2 operator *(double inLeft, Vector2 inRight)
            => new Vector2(Vector.Multiply(inLeft, inRight));
        public static Vector2 operator /(Vector2 inLeft, double inRight)
            => new Vector2(Vector.Divide(inLeft, inRight));

        public static double Dot(Vector2 inLeft, Vector2 inRight)
            => Vector.Dot(inLeft, inRight);
        public static double Cross(Vector2 inLeft, Vector2 inRight)
            => (inLeft.x * inRight.y) - (inLeft.y * inRight.x);
    }
}
