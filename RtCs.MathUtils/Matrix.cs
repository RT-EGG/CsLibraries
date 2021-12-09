using System.Collections.Generic;
using System.Linq;

namespace RtCs.MathUtils
{
    public interface IMatrix : IEnumerable<float>, IReadOnlyList<float>
    {
        new float this[int inIndex] { get; set; }
        float this[int inRow, int inCol] { get; set; }
        int ElemCount { get; }
        int RowCount { get; }
        int ColCount { get; }
    }

    public static class Matrix
    {
        //internal static IEnumerable<float> Identity(int inSize)
        //    => inSize.Range()
        //        .Select(r => inSize.Range()
        //        .Select(c => (r == c) ? 1.0f : 0.0f)).Flatten();

        //internal static IEnumerable<float> Add(IMatrix inLeft, IMatrix inRight)
        //    => inLeft.ElemCount.Range().Select(i => inLeft[i] + inRight[i]);
        //internal static IEnumerable<float> Subtract(IMatrix inLeft, IMatrix inRight)
        //    => inLeft.ElemCount.Range().Select(i => inLeft[i] - inRight[i]);
        //internal static IEnumerable<float> Multiply(IMatrix inLeft, float inRight)
        //    => inLeft.ElemCount.Range().Select(i => inLeft[i] * inRight);
        //internal static IEnumerable<float> Multiply(float inLeft, IMatrix inRight)
        //    => inRight.ElemCount.Range().Select(i => inLeft * inRight[i]);
        //internal static IEnumerable<float> Divide(IMatrix inLeft, float inRight)
        //    => inLeft.ElemCount.Range().Select(i => inLeft[i] / inRight);

        //internal static IEnumerable<float> Multiply(IMatrix inLeft, IMatrix inRight)
        //    => inLeft.RowCount.Range()
        //        .Select(r => inLeft.ColCount.Range()
        //        .Select(c => inLeft.ColCount.Range()
        //        .Sum(i => inLeft[r, i] * inRight[i, c])))
        //        .Flatten();

        internal static void MultiplyMatrix<Result, T1, T2>(ref Result refResult, T1 inLeft, T2 inRight) where Result : IMatrix where T1 : IMatrix where T2 : IMatrix
        {
            int rowCount = refResult.RowCount;
            int colCount = refResult.ColCount;
            for (int r = 0; r < rowCount; ++r) {
                for (int c = 0; c < colCount; ++c) {
                    float sum = 0.0f;
                    for (int i = 0; i < colCount; ++i) {
                        sum += inLeft[r, i] * inRight[i, c];
                    }
                    refResult[r, c] = sum;
                }
            }
            return;
        }

        internal static void MultiplyVector<Result, T1, T2>(ref Result refResult, T1 inLeft, T2 inRight) where Result : IVector where T1 : IMatrix where T2 : IVector
        {
            int count = refResult.Count;
            for (int i = 0; i < count; ++i) {
                float sum = 0.0f;
                for (int j = 0; j < count; ++j) {
                    sum += inLeft[i, j] * inRight[j];
                }
                refResult[i] = sum;
            }
            return;
        }

        //internal static IEnumerable<float> Multiply(IMatrix inLeft, IVector inRight)
        //    => inLeft.RowCount.Range().Select(r => inLeft.ColCount.Range().Sum(c => inLeft[r, c] * inRight[c]));

    }

    internal static class MatrixExtensions
    {
        internal static IEnumerable<float> Enumerate<T>(this T inMatrix) where T : IMatrix
            => Enumerable.Range(0, inMatrix.ElemCount).Select(i => inMatrix[i]);
        internal static int RowColToIndex<T>(this T inMatrix, int inRowIndex, int inColIndex) where T : IMatrix
            => (inRowIndex * inMatrix.ColCount) + inColIndex;
    }
}
