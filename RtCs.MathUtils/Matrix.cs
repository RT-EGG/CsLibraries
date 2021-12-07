using System.Collections.Generic;
using System.Linq;

namespace RtCs.MathUtils
{
    public interface IMatrix : IEnumerable<float>
    {
        float this[int inIndex] { get; set; }
        float this[int inRow, int inCol] { get; set; }
        int ElemCount { get; }
        int RowCount { get; }
        int ColCount { get; }
    }

    public static class Matrix
    {
        internal static IEnumerable<float> Identity(int inSize)
            => inSize.Range()
                .Select(r => inSize.Range()
                .Select(c => (r == c) ? 1.0f : 0.0f)).Flatten();

        internal static IEnumerable<float> Add(IMatrix inLeft, IMatrix inRight)
            => inLeft.ElemCount.Range().Select(i => inLeft[i] + inRight[i]);
        internal static IEnumerable<float> Subtract(IMatrix inLeft, IMatrix inRight)
            => inLeft.ElemCount.Range().Select(i => inLeft[i] - inRight[i]);
        internal static IEnumerable<float> Multiply(IMatrix inLeft, float inRight)
            => inLeft.ElemCount.Range().Select(i => inLeft[i] * inRight);
        internal static IEnumerable<float> Multiply(float inLeft, IMatrix inRight)
            => inRight.ElemCount.Range().Select(i => inLeft * inRight[i]);
        internal static IEnumerable<float> Divide(IMatrix inLeft, float inRight)
            => inLeft.ElemCount.Range().Select(i => inLeft[i] / inRight);

        internal static IEnumerable<float> Multiply(IMatrix inLeft, IMatrix inRight)
            => inLeft.RowCount.Range()
                .Select(r => inLeft.ColCount.Range()
                .Select(c => inLeft.ColCount.Range()
                .Sum(i => inLeft[r, i] * inRight[i, c])))
                .Flatten();

        internal static IEnumerable<float> Multiply(IMatrix inLeft, IVector inRight)
            => inLeft.RowCount.Range().Select(r => inLeft.ColCount.Range().Sum(c => inLeft[r, c] * inRight[c]));

    }

    internal static class MatrixExtensions
    {
        internal static IEnumerable<float> Enumerate(this IMatrix inMatrix)
            => Enumerable.Range(0, inMatrix.ElemCount).Select(i => inMatrix[i]);
        internal static int RowColToIndex(this IMatrix inMatrix, int inRowIndex, int inColIndex)
            => (inRowIndex * inMatrix.ColCount) + inColIndex;
    }
}
