using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.MathUtils
{
    public static class NumericExtensions
    {
        public const float FloatThreshold = 1.0e-5f;
        public const double DoubleThreshold = 1.0e-10;

        public static bool AlmostZero(this float inValue, float inThreshould = FloatThreshold)
            => Math.Abs(inValue) <= inThreshould;
        public static bool AlmostZero(this double inValue, double inThreshould = DoubleThreshold)
            => Math.Abs(inValue) <= inThreshould;

        public static bool AlmostEquals(this float inValue, float inValue2, float inThreshould = FloatThreshold)
            => (inValue - inValue2).AlmostZero(inThreshould);
        public static bool AlmostEquals(this double inValue, double inValue2, double inThreshould = DoubleThreshold)
            => (inValue - inValue2).AlmostZero(inThreshould);

        public static float Sqr(this float inValue) => inValue * inValue;
        public static double Sqr(this double inValue) => inValue * inValue;
        public static float Clamp(this float inValue, float inMin, float inMax)=> Math.Min(inMax, Math.Max(inMin, inValue));
        public static double Clamp(this double inValue, double inMin, double inMax) => Math.Min(inMax, Math.Max(inMin, inValue));

        public static IEnumerable<int> Range(this int inCount)
            => Enumerable.Range(0, inCount);
        public static IEnumerable<T> Enumerate<T>(this int inCount, Func<int, T> inInitializer)
            => inCount.Range().Select(i => inInitializer(i));
        public static bool InRange(this int inValue, int inMin, int inMax)
            => (inMin <= inValue) && (inValue <= inMax);

        public const double Rad2Deg = 180.0 / Math.PI;
        public const double Deg2Rad = Math.PI / 180.0;

        public static double RadToDeg(this double inRadians) => inRadians * Rad2Deg;
        public static double DegToRad(this double inDegrees) => inDegrees * Deg2Rad;

        public static double Asin(double inValue)
            => Math.Asin(inValue.Clamp(-1.0, 1.0));
        public static double Acos(double inValue)
            => Math.Acos(inValue.Clamp(-1.0, 1.0));
    }
}
