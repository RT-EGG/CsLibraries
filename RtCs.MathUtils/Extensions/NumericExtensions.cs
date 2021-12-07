using System;
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

        public static int Pow(this int inValue, int inExp)
            => Enumerable.Repeat(inValue, inExp).Aggregate(1, (a, b) => a * b);
        public static float Pow(this float inValue, int inExp)
            => Enumerable.Repeat(inValue, inExp).Aggregate(1.0f, (a, b) => a * b);
        public static double Pow(this double inValue, int inExp)
            => Enumerable.Repeat(inValue, inExp).Aggregate(1.0, (a, b) => a * b);

        public static bool InRange(this int inValue, int inMin, int inMax)
            => (inMin <= inValue) && (inValue <= inMax);
        public static bool InRange(this float inValue, float inMin, float inMax)
            => (inMin <= inValue) && (inValue <= inMax);
        public static bool InRange(this double inValue, double inMin, double inMax)
            => (inMin <= inValue) && (inValue <= inMax);

        public const double Rad2Deg = 180.0 / Math.PI;
        public const float Rad2DegF = 180.0f / (float)Math.PI;
        public const double Deg2Rad = Math.PI / 180.0;
        public const float Deg2RadF = (float)Math.PI / 180.0f;

        public static double RadToDeg(this double inRadians) => inRadians * Rad2Deg;
        public static float RadToDeg(this float inRadians) => inRadians * Rad2DegF;
        public static double DegToRad(this double inDegrees) => inDegrees * Deg2Rad;
        public static float DegToRad(this float inDegrees) => inDegrees * Deg2RadF;

        public static int TruncateToInt(this float inValue) => (int)Math.Truncate(inValue);
        public static int TruncateToInt(this double inValue) => (int)Math.Truncate(inValue);
    }

    public static class Numerics
    {
        public static T Min<T>(params T[] inItems) where T : IComparable<T>
            => inItems.Min();
        public static T Max<T>(params T[] inItems) where T : IComparable<T>
            => inItems.Max();

        public static double Asin(double inValue) => Math.Asin(inValue.Clamp(-1.0, 1.0));
        public static float Asin(float inValue) => (float)Math.Asin(inValue.Clamp(-1.0f, 1.0f));
        public static double Acos(double inValue) => Math.Acos(inValue.Clamp(-1.0, 1.0));
        public static float Acos(float inValue) => (float)Math.Acos(inValue.Clamp(-1.0f, 1.0f));
    }
}
