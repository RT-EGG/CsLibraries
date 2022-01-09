using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace RtCs.MathUtils
{
    public struct ColorRGB : IEquatable<ColorRGB>, IEnumerable<byte>
    {
        public ColorRGB(byte inR, byte inG, byte inB)
        {
            R = inR;
            G = inG;
            B = inB;
            return;
        }

        public ColorRGB(Color inColor)
            : this(inColor.R, inColor.G, inColor.B)
        { }

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public ColorHSV ToHSV()
        {
            ColorHSV result = new ColorHSV();
            byte min = Math.Min(R, Math.Min(G, B));
            byte max = Math.Max(R, Math.Max(G, B));

            result.V = max / 255.0f;
            if (max == 0) {
                result.H = 0.0f;
                result.S = 0.0f;
            } else {
                result.S = ((max - min) / (float)max).Clamp(0.0f, 1.0f);

                if (max == min) {
                    result.H = 0.0f;
                } else {
                    float h;
                    if (max == B) {
                        h = 60.0f * ((R - G) / (float)(max - min)) + 240.0f;
                    } else if (max == G) {
                        h = 60.0f * ((B - R) / (float)(max - min)) + 120.0f;
                    } else {
                        h = 60.0f * ((G - B) / (float)(max - min));
                    }

                    result.H = (h / 360.0f).Modulate(0.0f, 1.0f);
                }
            }

            return result;
        }

        public Color ToColor()
            => Color.FromArgb(255, R, G, B);

        public override bool Equals(object obj)
            => obj is ColorRGB rGB && Equals(rGB);

        public bool Equals(ColorRGB other)
            => R == other.R &&
               G == other.G &&
               B == other.B;

        public override int GetHashCode()
        {
            int hashCode = -1520100960;
            hashCode = hashCode * -1521134295 + R.GetHashCode();
            hashCode = hashCode * -1521134295 + G.GetHashCode();
            hashCode = hashCode * -1521134295 + B.GetHashCode();
            return hashCode;
        }

        public override string ToString()
            => $"({R}, {G}, {B})";
        internal static ColorRGB Parse(string inValue)
        {
            inValue = inValue.Substring(1, inValue.Length - 2);
            string[] items = inValue.Split(',');
            return new ColorRGB(
                    (byte)int.Parse(items[0]),
                    (byte)int.Parse(items[1]),
                    (byte)int.Parse(items[2])
                );
        }

        IEnumerator<byte> IEnumerable<byte>.GetEnumerator()
            => Enumerate().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => Enumerate().GetEnumerator();

        private IEnumerable<byte> Enumerate()
        {
            yield return R;
            yield return G;
            yield return B;
        }

        public static bool operator ==(ColorRGB left, ColorRGB right)
            => left.Equals(right);
        public static bool operator !=(ColorRGB left, ColorRGB right)
            => !(left == right);
    }
}
