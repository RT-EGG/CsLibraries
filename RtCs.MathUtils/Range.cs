using System;

namespace RtCs.MathUtils
{
    public struct Range1D : IEquatable<Range1D>
    {
        public double Min
        { get; set; }
        public double Max
        { get; set; }

        public double Length => Math.Abs(Max - Min);

        public bool Contains(double inValue)
            => (Min.CompareTo(inValue) >= 0) && (Max.CompareTo(inValue) <= 0);

        public void Include(double inValue)
        {
            Min = Math.Min(inValue, Min);
            Max = Math.Max(inValue, Max);
            return;
        }

        public override bool Equals(object obj)
            => obj is Range1D d && Equals(d);

        public bool Equals(Range1D other)
            => Min.AlmostEquals(other.Min)
            && Max.AlmostEquals(other.Max);

        public override int GetHashCode()
        {
            int hashCode = 1537547080;
            hashCode = hashCode * -1521134295 + Min.GetHashCode();
            hashCode = hashCode * -1521134295 + Max.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Range1D left, Range1D right)
            => left.Equals(right);
        public static bool operator !=(Range1D left, Range1D right)
            => !(left == right);
    }
}
