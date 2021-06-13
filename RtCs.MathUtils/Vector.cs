using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.MathUtils
{
    public interface IVector : IEnumerable<double>
    {
        double this[int inIndex] { get; set; }
        int Dimension { get; }
    }

    internal static class Vector
    {
        internal static IEnumerable<double> Enumerate(this IVector inVector)
            => Enumerable.Range(0, inVector.Dimension).Select(i => inVector[i]);

        public static double Length2(IVector inVector)
            => Enumerable.Range(0, inVector.Dimension).Sum(i => inVector[i].Sqr());
        public static double Length(IVector inVector)
            => Math.Sqrt(Length2(inVector));
        public static bool IsZero(IVector inVector)
            => Enumerable.Range(0, inVector.Dimension).All(i => inVector[i].AlmostZero());

        public static IEnumerable<double> Add(IVector inLeft, IVector inRight)
            => inLeft.Dimension.Range().Select(i => inLeft[i] + inRight[i]);
        public static IEnumerable<double> Subtract(IVector inLeft, IVector inRight)
            => inLeft.Dimension.Range().Select(i => inLeft[i] - inRight[i]);
        public static IEnumerable<double> Multiply(IVector inLeft, double inRight)
            => inLeft.Dimension.Range().Select(i => inLeft[i] * inRight);
        public static IEnumerable<double> Multiply(double inLeft, IVector inRight)
            => inRight.Dimension.Range().Select(i => inLeft * inRight[i]);
        public static IEnumerable<double> Divide(IVector inLeft, double inRight)
            => inLeft.Dimension.Range().Select(i => inLeft[i] / inRight);

        public static double Dot(IVector inLeft, IVector inRight)
            => inLeft.Dimension.Range().Sum(i => inLeft[i] * inRight[i]);
    }
}
