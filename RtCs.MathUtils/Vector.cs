using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.MathUtils
{
    public interface IVector : IReadOnlyList<float>, IEnumerable<float>
    {
        new float this[int inIndex] { get; set; }
        int Dimension { get; }
    }

    internal static class Vector
    {
        internal static IEnumerable<float> Enumerate(this IVector inVector)
            => Enumerable.Range(0, inVector.Dimension).Select(i => inVector[i]);

        public static float Length2(IVector inVector)
            => Enumerable.Range(0, inVector.Dimension).Sum(i => inVector[i].Sqr());
        public static float Length(IVector inVector)
            => (float)Math.Sqrt(Length2(inVector));
        public static bool IsZero(IVector inVector)
            => Enumerable.Range(0, inVector.Dimension).All(i => inVector[i].AlmostZero());

        public static IEnumerable<float> Add(IVector inLeft, IVector inRight)
            => inLeft.Dimension.Range().Select(i => inLeft[i] + inRight[i]);
        public static IEnumerable<float> Subtract(IVector inLeft, IVector inRight)
            => inLeft.Dimension.Range().Select(i => inLeft[i] - inRight[i]);
        public static IEnumerable<float> Multiply(IVector inLeft, float inRight)
            => inLeft.Dimension.Range().Select(i => inLeft[i] * inRight);
        public static IEnumerable<float> Multiply(float inLeft, IVector inRight)
            => inRight.Dimension.Range().Select(i => inLeft * inRight[i]);
        public static IEnumerable<float> Divide(IVector inLeft, float inRight)
            => inLeft.Dimension.Range().Select(i => inLeft[i] / inRight);

        public static float Dot(IVector inLeft, IVector inRight)
            => inLeft.Dimension.Range().Sum(i => inLeft[i] * inRight[i]);
    }
}
