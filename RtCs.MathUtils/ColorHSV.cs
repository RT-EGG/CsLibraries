using System;

namespace RtCs.MathUtils
{
    public struct ColorHSV : IEquatable<ColorHSV>
    {
        public ColorHSV(double inH, double inS, double inV)
        {
            H = inH;
            S = inS;
            V = inV;
            return;
        }

        public double H
        { get; set; }
        public double S
        { get; set; }
        public double V
        { get; set; }

        public override bool Equals(object obj)
            => obj is ColorHSV hSV && Equals(hSV);

        public bool Equals(ColorHSV other)
            => H.AlmostEquals(other.H) &&
               S.AlmostEquals(other.S) &&
               V.AlmostEquals(other.V);

        public override int GetHashCode()
        {
            int hashCode = -1397884734;
            hashCode = hashCode * -1521134295 + H.GetHashCode();
            hashCode = hashCode * -1521134295 + S.GetHashCode();
            hashCode = hashCode * -1521134295 + V.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(ColorHSV left, ColorHSV right)
            => left.Equals(right);
        public static bool operator !=(ColorHSV left, ColorHSV right)
            => !(left == right);
    }
}
