using System;
using System.Collections;
using System.Collections.Generic;

namespace RtCs.MathUtils
{
    public struct ColorRGBA : IEquatable<ColorRGBA>, IEnumerable<byte>
    {
        public ColorRGBA(byte inR, byte inG, byte inB, byte inA = 255)
        {
            R = inR;
            G = inG;
            B = inB;
            A = inA;
            return;
        }

        public ColorRGBA(ColorRGB inRGB, byte inA = 255)
            : this(inRGB.R, inRGB.G, inRGB.B, inA)
        { }

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public ColorRGB RGB
        {
            get => new ColorRGB(R, G, B);
            set {
                R = value.R;
                G = value.G;
                B = value.B;
                return;
            }
        }

        public static implicit operator ColorRGB(ColorRGBA inSource) => new ColorRGB(inSource.R, inSource.G, inSource.B);

        public override bool Equals(object obj)
            => obj is ColorRGBA rGBA && Equals(rGBA);

        public bool Equals(ColorRGBA other)
            => R == other.R &&
               G == other.G &&
               B == other.B &&
               A == other.A;

        public override int GetHashCode()
        {
            int hashCode = 1960784236;
            hashCode = hashCode * -1521134295 + R.GetHashCode();
            hashCode = hashCode * -1521134295 + G.GetHashCode();
            hashCode = hashCode * -1521134295 + B.GetHashCode();
            hashCode = hashCode * -1521134295 + A.GetHashCode();
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
            yield return A;
        }

        public static bool operator ==(ColorRGBA left, ColorRGBA right)
            => left.Equals(right);
        public static bool operator !=(ColorRGBA left, ColorRGBA right)
            => !(left == right);
    }
}
