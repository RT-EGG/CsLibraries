﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace RtCs.MathUtils
{
    public struct Matrix3x3 : IMatrix, IEquatable<Matrix3x3>
    {
        public Matrix3x3(IEnumerable<double> inValues)
        {
            var e = inValues.GetEnumerator();
            e.MoveNext();
            m00 = e.Current; e.MoveNext();
            m01 = e.Current; e.MoveNext();
            m02 = e.Current; e.MoveNext();
            m10 = e.Current; e.MoveNext();
            m11 = e.Current; e.MoveNext();
            m12 = e.Current; e.MoveNext();
            m20 = e.Current; e.MoveNext();
            m21 = e.Current; e.MoveNext();
            m22 = e.Current; e.MoveNext();
            return;
        }

        public double m00;
        public double m01;
        public double m02;
        public double m10;
        public double m11;
        public double m12;
        public double m20;
        public double m21;
        public double m22;

        public double this[int inIndex]
        {
            get {
                switch (inIndex) {
                    case 0: return m00;
                    case 1: return m01;
                    case 2: return m02;
                    case 3: return m00;
                    case 4: return m01;
                    case 5: return m02;
                    case 6: return m00;
                    case 7: return m01;
                    case 8: return m02;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set {
                switch (inIndex) {
                    case 0: m00 = value; break;
                    case 1: m01 = value; break;
                    case 2: m02 = value; break;
                    case 3: m10 = value; break;
                    case 4: m11 = value; break;
                    case 5: m12 = value; break;
                    case 6: m20 = value; break;
                    case 7: m21 = value; break;
                    case 8: m22 = value; break;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public double this[int inRow, int inCol]
        {
            get => this[this.RowColToIndex(inRow, inCol)];
            set => this[this.RowColToIndex(inRow, inCol)] = value;
        }

        public Matrix3x3 SetElements(params (int row, int col, double value)[] inCommands)
            => SetElements((IEnumerable<(int row, int col, double value)>)inCommands);

        public Matrix3x3 SetElements(IEnumerable<(int row, int col, double value)> inCommands)
        {
            foreach (var command in inCommands) {
                this[command.row, command.col] = command.value;
            }
            return this;
        }

        public void SetRow(int inRowIndex, double in0, double in1, double in2)
            => SetElements(
                (inRowIndex, 0, in0),
                (inRowIndex, 1, in1),
                (inRowIndex, 2, in2)
            );

        public void SetRow(int inRowIndex, IEnumerable<double> inValues)
        {
            var enumerator = inValues.GetEnumerator();
            this[inRowIndex, 0] = enumerator.Current; enumerator.MoveNext();
            this[inRowIndex, 1] = enumerator.Current; enumerator.MoveNext();
            this[inRowIndex, 2] = enumerator.Current; enumerator.MoveNext();
            return;
        }

        public void SetColumn(int inColIndex, double in0, double in1, double in2)
            => SetElements(
                (0, inColIndex, in0),
                (1, inColIndex, in1),
                (2, inColIndex, in2)
            );

        public void SetColumn(int inColIndex, IEnumerable<double> inValues)
        {
            var enumerator = inValues.GetEnumerator();
            this[0, inColIndex] = enumerator.Current; enumerator.MoveNext();
            this[1, inColIndex] = enumerator.Current; enumerator.MoveNext();
            this[2, inColIndex] = enumerator.Current; enumerator.MoveNext();
            return;
        }

        public Vector3 GetRow(int inRowIndex)
            => new Vector3(this[inRowIndex, 0], this[inRowIndex, 1], this[inRowIndex, 2]);
        public Vector3 GetColumn(int inColIndex)
            => new Vector3(this[0, inColIndex], this[1, inColIndex], this[2, inColIndex]);

        int IMatrix.ElemCount => Matrix3x3.ElemCount;
        int IMatrix.RowCount => Matrix3x3.RowCount;
        int IMatrix.ColCount => Matrix3x3.ColCount;
        public const int RowCount = 3;
        public const int ColCount = 3;
        public const int ElemCount = RowCount * ColCount;

        public override bool Equals(object inOther)
            => inOther is Matrix3x3 x && Equals(x);

        public bool Equals(Matrix3x3 inOther)
        {
            bool Equals(double left, double right) => left.AlmostEquals(right, NumericExtensions.DoubleThreshold);
            return Equals(m00, inOther.m00) &&
                   Equals(m01, inOther.m01) &&
                   Equals(m02, inOther.m02) &&
                   Equals(m10, inOther.m10) &&
                   Equals(m11, inOther.m11) &&
                   Equals(m12, inOther.m12) &&
                   Equals(m20, inOther.m20) &&
                   Equals(m21, inOther.m21) &&
                   Equals(m22, inOther.m22);
        }

        public override int GetHashCode()
        {
            int hashCode = 1544463542;
            hashCode = hashCode * -1521134295 + m00.GetHashCode();
            hashCode = hashCode * -1521134295 + m01.GetHashCode();
            hashCode = hashCode * -1521134295 + m02.GetHashCode();
            hashCode = hashCode * -1521134295 + m10.GetHashCode();
            hashCode = hashCode * -1521134295 + m11.GetHashCode();
            hashCode = hashCode * -1521134295 + m12.GetHashCode();
            hashCode = hashCode * -1521134295 + m20.GetHashCode();
            hashCode = hashCode * -1521134295 + m21.GetHashCode();
            hashCode = hashCode * -1521134295 + m22.GetHashCode();
            return hashCode;
        }

        public IEnumerator<double> GetEnumerator()
            => this.Enumerate().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public double Determinant =>
                         (this[0, 0] * this[1, 1] * this[2, 2])
                       + (this[0, 1] * this[1, 2] * this[2, 0])
                       + (this[0, 2] * this[1, 0] * this[2, 1])
                       - (this[0, 0] * this[1, 2] * this[2, 1])
                       - (this[0, 2] * this[1, 1] * this[2, 0])
                       - (this[0, 1] * this[1, 0] * this[2, 2]);

        public bool Inverse()
        {
            double det = Determinant;
            if (det.AlmostZero()) {
                return false;
            }

            Matrix3x3 buffer = new Matrix3x3(this);
            this[0, 0] = ((buffer[1, 1] * buffer[2, 2]) - (buffer[1, 2] * buffer[2, 1])) / det;
            this[0, 1] = ((buffer[0, 2] * buffer[2, 1]) - (buffer[0, 1] * buffer[2, 2])) / det;
            this[0, 2] = ((buffer[0, 1] * buffer[1, 2]) - (buffer[0, 2] * buffer[1, 1])) / det;
            this[1, 0] = ((buffer[1, 2] * buffer[2, 0]) - (buffer[1, 0] * buffer[2, 2])) / det;
            this[1, 1] = ((buffer[0, 0] * buffer[2, 2]) - (buffer[0, 2] * buffer[2, 0])) / det;
            this[1, 2] = ((buffer[0, 2] * buffer[1, 0]) - (buffer[0, 0] * buffer[1, 2])) / det;
            this[2, 0] = ((buffer[1, 0] * buffer[2, 1]) - (buffer[1, 1] * buffer[2, 0])) / det;
            this[2, 1] = ((buffer[0, 1] * buffer[2, 0]) - (buffer[0, 0] * buffer[2, 1])) / det;
            this[2, 2] = ((buffer[0, 0] * buffer[1, 1]) - (buffer[0, 1] * buffer[1, 0])) / det;
            return true;
        }

        public Vector3 Multiply(Vector2 inRight, double inRightH)
            => Multiply(new Vector3(inRight.x, inRight.y, inRightH));
        public Vector3 Multiply(Vector3 inRight)
            => Multiply(this, inRight);

        public static readonly Matrix3x3 Identity = new Matrix3x3(Matrix.Identity(RowCount));
        public static readonly Matrix3x3 Zero = new Matrix3x3(ElemCount.Enumerate(_ => 0.0));

        public static bool operator ==(Matrix3x3 left, Matrix3x3 right)
            => left.Equals(right);
        public static bool operator !=(Matrix3x3 left, Matrix3x3 right)
            => !(left == right);
        public static Matrix3x3 operator -(Matrix3x3 matrix)
            => matrix * -1.0f;
        public static Matrix3x3 operator +(Matrix3x3 left, Matrix3x3 right)
            => new Matrix3x3(Matrix.Add(left, right));
        public static Matrix3x3 operator -(Matrix3x3 left, Matrix3x3 right)
            => new Matrix3x3(Matrix.Subtract(left, right));
        public static Matrix3x3 operator *(Matrix3x3 left, double right)
            => new Matrix3x3(Matrix.Multiply(left, right));
        public static Matrix3x3 operator *(double left, Matrix3x3 right)
            => new Matrix3x3(Matrix.Multiply(left, right));
        public static Matrix3x3 operator /(Matrix3x3 left, double right)
            => new Matrix3x3(Matrix.Divide(left, right));
        public static Matrix3x3 operator *(Matrix3x3 left, Matrix3x3 right)
            => new Matrix3x3(Matrix.Multiply(left, right));

        public static Vector3 Multiply(Matrix3x3 inLeft, Vector2 inRight, double inRightH)
            => Multiply(inLeft, new Vector3(inRight.x, inRight.y, inRightH));
        public static Vector3 Multiply(Matrix3x3 inLeft, Vector3 inRight)
            => new Vector3(Matrix.Multiply(inLeft, inRight));

        public static Matrix3x3 MakeTranslate(double inX, double inY)
            => new Matrix3x3(EnumerableExtensions.AsEnumerable(
                    1.0f, 0.0f, inX,
                    0.0f, 1.0f, inY,
                    0.0f, 0.0f, 1.0f
                ));
        public static Matrix3x3 MakeTranslate(Vector2 inValue)
            => MakeTranslate(inValue.x, inValue.y);

        public static Matrix3x3 MakeRotate(double inRadian)
        {
            var result = Identity;
            double s = Math.Sin(inRadian);
            double c = Math.Cos(inRadian);
            result.SetElements(
                (0, 0, c),
                (0, 1, -s),
                (1, 0, s),
                (1, 1, c)
            );
            return result;
        }

        public static Matrix3x3 MakeScale(double inX, double inY)
            => new Matrix3x3(EnumerableExtensions.AsEnumerable(
                     inX, 0.0f, 0.0f,
                    0.0f, inY, 0.0f,
                    0.0f, 0.0f, 1.0f
                ));
        public static Matrix3x3 MakeScale(Vector2 inValue)
            => MakeScale(inValue.x, inValue.y);
    }
}
