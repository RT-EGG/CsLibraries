using System;

namespace RtCs.MathUtils
{
    public struct ColorHSV : IEquatable<ColorHSV>
    {
        public ColorHSV(float inH, float inS, float inV)
        {
            H = inH;
            S = inS;
            V = inV;
            return;
        }

        public float H { get; set; }
        public float S { get; set; }
        public float V { get; set; }

        public ColorRGB ToRGB()
        {
            byte v = (byte)(V.Clamp(0.0f, 1.0f) * 255);
            if (S.AlmostZero()) {
                return new ColorRGB(v, v, v);
            }
            float hf = H * 6.0f;
            int hi = hf.TruncateToInt();

            float fr = hf - hi;
            byte m = (byte)((V * (1.0f - S)).Clamp(0.0f, 1.0f) * 255);
            byte n = (byte)((V * (1.0f - (S * fr))).Clamp(0.0f, 1.0f) * 255);
            byte p = (byte)((V * (1.0f - (S * (1.0f - fr)))).Clamp(0.0f, 1.0f) * 255);

            switch (hi % 6) {
                case 0: return new ColorRGB(v, p, m);
                case 1: return new ColorRGB(n, v, m);
                case 2: return new ColorRGB(m, v, p);
                case 3: return new ColorRGB(m, n, v);
                case 4: return new ColorRGB(p, m, v);
                case 5: return new ColorRGB(v, m, n);
            }
            throw new InvalidProgramException();
        }

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
