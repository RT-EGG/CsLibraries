using System;
using System.Collections;
using System.Collections.Generic;

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

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

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
