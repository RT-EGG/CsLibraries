using System;
using System.Collections;
using System.Collections.Generic;

namespace RtCs.MathUtils
{
    public struct Matrix4x4 : IMatrix, IEquatable<Matrix4x4>
    {
        public Matrix4x4(params double[] inValues)
            : this((IEnumerable<double>) inValues)
        { }

        public Matrix4x4(IEnumerable<double> inValues)
        {
            var e = inValues.GetEnumerator();
            e.MoveNext();
            m00 = e.Current; e.MoveNext();
            m01 = e.Current; e.MoveNext();
            m02 = e.Current; e.MoveNext();
            m03 = e.Current; e.MoveNext();
            m10 = e.Current; e.MoveNext();
            m11 = e.Current; e.MoveNext();
            m12 = e.Current; e.MoveNext();
            m13 = e.Current; e.MoveNext();
            m20 = e.Current; e.MoveNext();
            m21 = e.Current; e.MoveNext();
            m22 = e.Current; e.MoveNext();
            m23 = e.Current; e.MoveNext();
            m30 = e.Current; e.MoveNext();
            m31 = e.Current; e.MoveNext();
            m32 = e.Current; e.MoveNext();
            m33 = e.Current; e.MoveNext();
            return;
        }

        public double m00;
        public double m01;
        public double m02;
        public double m03;
        public double m10;
        public double m11;
        public double m12;
        public double m13;
        public double m20;
        public double m21;
        public double m22;
        public double m23;
        public double m30;
        public double m31;
        public double m32;
        public double m33;

        public double this[int inIndex]
        {
            get {
                switch (inIndex) {
                    case 0: return m00;
                    case 1: return m01;
                    case 2: return m02;
                    case 3: return m03;
                    case 4: return m10;
                    case 5: return m11;
                    case 6: return m12;
                    case 7: return m13;
                    case 8: return m20;
                    case 9: return m21;
                    case 10: return m22;
                    case 11: return m23;
                    case 12: return m30;
                    case 13: return m31;
                    case 14: return m32;
                    case 15: return m33;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set {
                switch (inIndex) {
                    case 0: m00 = value; break;
                    case 1: m01 = value; break;
                    case 2: m02 = value; break;
                    case 3: m03 = value; break;
                    case 4: m10 = value; break;
                    case 5: m11 = value; break;
                    case 6: m12 = value; break;
                    case 7: m13 = value; break;
                    case 8: m20 = value; break;
                    case 9: m21 = value; break;
                    case 10: m22 = value; break;
                    case 11: m23 = value; break;
                    case 12: m30 = value; break;
                    case 13: m31 = value; break;
                    case 14: m32 = value; break;
                    case 15: m33 = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public double this[int inRow, int inCol]
        {
            get => this[this.RowColToIndex(inRow, inCol)];
            set => this[this.RowColToIndex(inRow, inCol)] = value;
        }

        public Matrix4x4 SetElements(params (int row, int col, double value)[] inCommands)
        {
            foreach (var command in inCommands) {
                this[command.row, command.col] = command.value;
            }
            return this;
        }

        public void Set3x3(int inRowIndex, int inColIndex, Matrix3x3 inSource)
        {
            if (!(inRowIndex.InRange(0, 1) && inColIndex.InRange(0, 1))) {
                throw new ArgumentOutOfRangeException("inRowIndex and inColIndex must be range in 0 to 1.");
            }

            for (int r = 0; r < 3; ++r) {
                for (int c = 0; c < 3; ++c) {
                    this[r, c] = inSource[r + inRowIndex, c + inColIndex];
                }
            }
            return;
        }

        public Matrix3x3 Extract3x3(int inRowIndex, int inColIndex)
        {
            if (!(inRowIndex.InRange(0, 1) && inColIndex.InRange(0, 1))) {
                throw new ArgumentOutOfRangeException("inRowIndex and inColIndex must be range in 0 to 1.");
            }

            Matrix3x3 result = new Matrix3x3();
            for (int r = 0; r < 3; ++r) {
                for (int c = 0; c < 3; ++c) {
                    result[r, c] = this[r + inRowIndex, c + inColIndex];
                }
            }

            return result;
        }

        public void SetRow(int inRowIndex, double in0, double in1, double in2, double in3)
            => SetElements(
                (inRowIndex, 0, in0),
                (inRowIndex, 1, in1),
                (inRowIndex, 2, in2),
                (inRowIndex, 3, in3)
            );

        public void SetRow(int inRowIndex, IEnumerable<double> inValues)
        {
            var enumerator = inValues.GetEnumerator();
            this[inRowIndex, 0] = enumerator.Current; enumerator.MoveNext();
            this[inRowIndex, 1] = enumerator.Current; enumerator.MoveNext();
            this[inRowIndex, 2] = enumerator.Current; enumerator.MoveNext();
            this[inRowIndex, 3] = enumerator.Current; enumerator.MoveNext();
            return;
        }

        public void SetColumn(int inColIndex, double in0, double in1, double in2, double in3)
            => SetElements(
                (0, inColIndex, in0),
                (1, inColIndex, in1),
                (2, inColIndex, in2),
                (3, inColIndex, in3)
            );

        public void SetColumn(int inColIndex, IEnumerable<double> inValues)
        {
            var enumerator = inValues.GetEnumerator();
            this[0, inColIndex] = enumerator.Current; enumerator.MoveNext();
            this[1, inColIndex] = enumerator.Current; enumerator.MoveNext();
            this[2, inColIndex] = enumerator.Current; enumerator.MoveNext();
            this[3, inColIndex] = enumerator.Current; enumerator.MoveNext();
            return;
        }

        public Vector4 GetRow(int inRowIndex)
            => new Vector4(this[inRowIndex, 0], this[inRowIndex, 1], this[inRowIndex, 2], this[inRowIndex, 3]);
        public Vector4 GetColumn(int inColIndex)
            => new Vector4(this[0, inColIndex], this[1, inColIndex], this[2, inColIndex], this[3, inColIndex]);

        public Matrix4x4 Transposed
            => new Matrix4x4(
                    this[0, 0], this[1, 0], this[2, 0], this[3, 0],
                    this[0, 1], this[1, 1], this[2, 1], this[3, 1],
                    this[0, 2], this[1, 2], this[2, 2], this[3, 2],
                    this[0, 3], this[1, 3], this[2, 3], this[3, 3]
                );        

        public Vector3 Translation
            => new Vector3(GetColumn(3));
        public Quaternion Rotation
        {
            get {
                // refered https://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/
                // refered https://qiita.com/aa_debdeb/items/abe90a9bd0b4809813da

                double px =  this[0, 0] - this[1, 1] - this[2, 2] + 1.0;
                double py = -this[0, 0] + this[1, 1] - this[2, 2] + 1.0;
                double pz = -this[0, 0] - this[1, 1] + this[2, 2] + 1.0;
                double pw =  this[0, 0] + this[1, 1] + this[2, 2] + 1.0;

                int s = 0;
                double max = px;
                double v = Math.Sqrt(px) * 0.5;
                if (max < py) {
                    s = 1;
                    v = Math.Sqrt(py) * 0.5;
                    max = py;
                }
                if (max < pz) {
                    s = 2;
                    v = Math.Sqrt(pz) * 0.5;
                    max = pz;
                }
                if (max < pw) {
                    s = 3;
                    v = Math.Sqrt(pw) * 0.5;
                    max = pw;
                }

                double d = 0.25 / v;
                switch (s) {
                    case 0:
                        return new Quaternion(
                                v,
                                (this[1, 0] + this[0, 1]) * d,
                                (this[0, 2] + this[2, 0]) * d,
                                (this[2, 1] - this[1, 2]) * d
                            );

                    case 1:
                        return new Quaternion(
                                (this[1, 0] + this[0, 1]) * d,
                                v,
                                (this[2, 1] + this[1, 2]) * d,
                                (this[0, 2] - this[2, 0]) * d
                            );

                    case 2:
                        return new Quaternion(
                                (this[0, 2] + this[2, 0]) * d,
                                (this[2, 1] + this[1, 2]) * d,
                                v,
                                (this[1, 0] - this[0, 1]) * d
                            );

                    case 3:
                        return new Quaternion(
                                (this[2, 1] - this[1, 2]) * d,
                                (this[0, 2] - this[2, 0]) * d,
                                (this[1, 0] - this[0, 1]) * d,
                                v
                            );
                }

                throw new InvalidProgramException();
            }
        }

        public Vector3 GetEulerRotation(EEulerRotationOrder inOrder)
            => EulerAngles.MatrixToEuler(this, inOrder);
        public void SetEulerRotation(Vector3 inEuler, EEulerRotationOrder inOrder)
            => Set3x3(0, 0, MakeRotateEuelr(inEuler, inOrder).Extract3x3(0, 0));

        int IMatrix.ElemCount => Matrix4x4.ElemCount;
        int IMatrix.RowCount => Matrix4x4.RowCount;
        int IMatrix.ColCount => Matrix4x4.ColCount;
        public const int RowCount = 4;
        public const int ColCount = 4;
        public const int ElemCount = RowCount * ColCount;

        public override bool Equals(object inOther)
            => inOther is Matrix4x4 x && Equals(x);

        public bool Equals(Matrix4x4 other)
        {
            bool Equals(double left, double right) => left.AlmostEquals(right, NumericExtensions.DoubleThreshold);
            return Equals(m00, other.m00) &&
                   Equals(m01, other.m01) &&
                   Equals(m02, other.m02) &&
                   Equals(m03, other.m03) &&
                   Equals(m10, other.m10) &&
                   Equals(m11, other.m11) &&
                   Equals(m12, other.m12) &&
                   Equals(m13, other.m13) &&
                   Equals(m20, other.m20) &&
                   Equals(m21, other.m21) &&
                   Equals(m22, other.m22) &&
                   Equals(m33, other.m23) &&
                   Equals(m30, other.m30) &&
                   Equals(m31, other.m31) &&
                   Equals(m32, other.m32) &&
                   Equals(m33, other.m33);
        }

        public override int GetHashCode()
        {
            int hashCode = 1860550904;
            hashCode = hashCode * -1521134295 + m00.GetHashCode();
            hashCode = hashCode * -1521134295 + m01.GetHashCode();
            hashCode = hashCode * -1521134295 + m02.GetHashCode();
            hashCode = hashCode * -1521134295 + m03.GetHashCode();
            hashCode = hashCode * -1521134295 + m10.GetHashCode();
            hashCode = hashCode * -1521134295 + m11.GetHashCode();
            hashCode = hashCode * -1521134295 + m12.GetHashCode();
            hashCode = hashCode * -1521134295 + m13.GetHashCode();
            hashCode = hashCode * -1521134295 + m20.GetHashCode();
            hashCode = hashCode * -1521134295 + m21.GetHashCode();
            hashCode = hashCode * -1521134295 + m22.GetHashCode();
            hashCode = hashCode * -1521134295 + m23.GetHashCode();
            hashCode = hashCode * -1521134295 + m30.GetHashCode();
            hashCode = hashCode * -1521134295 + m31.GetHashCode();
            hashCode = hashCode * -1521134295 + m32.GetHashCode();
            hashCode = hashCode * -1521134295 + m33.GetHashCode();
            return hashCode;
        }

        public IEnumerator<double> GetEnumerator()
            => this.Enumerate().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public bool Inverse()
        {
            // http://miffysora.wikidot.com/invertmatrix-4x4
            // http://www.iwata-system-support.com/CAE_HomePage/vector/vector18/vector18.html
            // http://physmath.main.jp/src/inverse-cofactor-ex4.html

            double det = Deternimant;
            if (det.AlmostZero()) {
                return false;
            }

            Matrix4x4 tmp = this;
            for (int row = 0; row < 4; ++row) {
                for (int col = 0; col < 4; ++col) {
                    if (((row + col) % 2) == 0) {
                        this[row, col] = tmp.GetSubDeterminant(col, row) / det;
                    } else {
                        this[row, col] = -tmp.GetSubDeterminant(col, row) / det;
                    }
                }
            }

            return true;
        }

        public Matrix4x4 Inversed
        {
            get {
                Matrix4x4 result = this;
                if (!result.Inverse()) {
                    return Matrix4x4.Identity;
                }
                return result;
            }
        }

        public double Deternimant
            => (this[0, 0] * GetSubDeterminant(0, 0))
             - (this[1, 0] * GetSubDeterminant(1, 0))
             + (this[2, 0] * GetSubDeterminant(2, 0))
             - (this[3, 0] * GetSubDeterminant(3, 0));

        private double GetSubDeterminant(int inRow, int inCol)
        {
            Matrix3x3 subMatrix = new Matrix3x3();
            int srcRow, srcCol;

            srcRow = 0;
            for (int dstRow = 0; dstRow < 3; ++dstRow) {
                if (dstRow == inRow)
                    ++srcRow;

                srcCol = 0;
                for (int dstCol = 0; dstCol < 3; ++dstCol) {
                    if (dstCol == inCol)
                        ++srcCol;

                    subMatrix[dstRow, dstCol] = this[srcRow, srcCol];
                    ++srcCol;
                }
                ++srcRow;
            }

            return subMatrix.Determinant;
        }

        public Vector4 Multiply(Vector3 inRight, double inRightW)
            => Multiply(new Vector4(inRight.x, inRight.y, inRight.z, inRightW));
        public Vector4 Multiply(Vector4 inRight)
            => Multiply(this, inRight);

        public static readonly Matrix4x4 Identity = new Matrix4x4(Matrix.Identity(RowCount));
        public static readonly Matrix4x4 Zero = new Matrix4x4(ElemCount.Enumerate(_ => 0.0));

        public static bool operator ==(Matrix4x4 left, Matrix4x4 right)
            => left.Equals(right);
        public static bool operator !=(Matrix4x4 left, Matrix4x4 right)
            => !(left == right);
        public static Matrix4x4 operator -(Matrix4x4 matrix)
            => matrix * -1.0;
        public static Matrix4x4 operator +(Matrix4x4 left, Matrix4x4 right)
            => new Matrix4x4(Matrix.Add(left, right));
        public static Matrix4x4 operator -(Matrix4x4 left, Matrix4x4 right)
            => new Matrix4x4(Matrix.Subtract(left, right));
        public static Matrix4x4 operator *(Matrix4x4 left, double right)
            => new Matrix4x4(Matrix.Multiply(left, right));
        public static Matrix4x4 operator *(double left, Matrix4x4 right)
            => new Matrix4x4(Matrix.Multiply(left, right));
        public static Matrix4x4 operator /(Matrix4x4 left, double right)
            => new Matrix4x4(Matrix.Divide(left, right));
        public static Matrix4x4 operator *(Matrix4x4 left, Matrix4x4 right)
            => new Matrix4x4(Matrix.Multiply(left, right));
        public static Vector4 operator *(Matrix4x4 left, Vector4 right)
            => Multiply(left, right);

        public static Vector4 Multiply(Matrix4x4 inLeft, Vector3 inRight, double inRightW)
            => Multiply(inLeft, new Vector4(inRight.x, inRight.y, inRight.z, inRightW));
        public static Vector4 Multiply(Matrix4x4 inLeft, Vector4 inRight)
            => new Vector4(Matrix.Multiply(inLeft, inRight));

        public static Matrix4x4 MakeTranslate(double inX, double inY, double inZ)
            => new Matrix4x4(
                    1.0, 0.0, 0.0, inX,
                    0.0, 1.0, 0.0, inY,
                    0.0, 0.0, 1.0, inZ,
                    0.0, 0.0, 0.0, 1.0
                );

        public static Matrix4x4 MakeTranslate(Vector3 inXYZ)
            => MakeTranslate(inXYZ.x, inXYZ.y, inXYZ.z);

        public static Matrix4x4 MakeRotateX(double inRadian)
        {
            double c = Math.Cos(inRadian);
            double s = Math.Sin(inRadian);
            return new Matrix4x4(
                    1.0, 0.0, 0.0, 0.0,
                    0.0, c, -s, 0.0,
                    0.0, s, c, 0.0,
                    0.0, 0.0, 0.0, 1.0
                );
        }

        public static Matrix4x4 MakeRotateY(double inRadian)
        {
            double c = Math.Cos(inRadian);
            double s = Math.Sin(inRadian);
            return new Matrix4x4(
                       c, 0.0, s, 0.0,
                    0.0, 1.0, 0.0, 0.0,
                      -s, 0.0, c, 0.0,
                    0.0, 0.0, 0.0, 1.0
                );
        }

        public static Matrix4x4 MakeRotateZ(double inRadian)
        {
            double c = Math.Cos(inRadian);
            double s = Math.Sin(inRadian);
            return new Matrix4x4(
                       c, -s, 0.0, 0.0,
                       s, c, 0.0, 0.0,
                    0.0, 0.0, 1.0, 0.0,
                    0.0, 0.0, 0.0, 1.0
                );
        }

        public static Matrix4x4 MakeRotate(double inRadian, double inAxisX, double inAxisY, double inAxisZ)
        {
            if (inAxisX.AlmostZero() && inAxisY.AlmostZero() && inAxisZ.AlmostZero()) {
                return Identity;
            }

            double c = Math.Cos(inRadian);
            double s = Math.Sin(inRadian);

            return new Matrix4x4(
                    (inAxisX * inAxisX * (1.0 - c)) + c,
                    (inAxisX * inAxisY * (1.0 - c)) - (inAxisZ * s),
                    (inAxisX * inAxisZ * (1.0 - c)) + (inAxisY * s),
                    0.0,
                    (inAxisY * inAxisX * (1.0 - c)) + (inAxisZ * s),
                    (inAxisY * inAxisY * (1.0 - c)) + c,
                    (inAxisY * inAxisZ * (1.0 - c)) - (inAxisX * s),
                    0.0,
                    (inAxisZ * inAxisX * (1.0 - c)) - (inAxisY * s),
                    (inAxisZ * inAxisY * (1.0 - c)) + (inAxisX * s),
                    (inAxisZ * inAxisZ * (1.0 - c)) + c,
                    0.0,
                    0.0, 0.0, 0.0, 1.0
                );
        }

        public static Matrix4x4 MakeRotate(Quaternion inValue)
        {
            // refered http://marupeke296.sakura.ne.jp/DXG_No58_RotQuaternionTrans.html
            ref Quaternion q = ref inValue;
            double xx2 = q.x * q.x * 2.0;
            double xy2 = q.x * q.y * 2.0;
            double xz2 = q.x * q.z * 2.0;
            double xw2 = q.x * q.w * 2.0;
            double yy2 = q.y * q.y * 2.0;
            double yz2 = q.y * q.z * 2.0;
            double yw2 = q.y * q.w * 2.0;
            double zz2 = q.z * q.z * 2.0;
            double zw2 = q.z * q.w * 2.0;
            double ww2 = q.w * q.w * 2.0;
            //return new Matrix4x4(
            //        ww2 + xx2 - 1.0,       xy2 - zw2,       xz2 + yw2, 0.0,
            //              xy2 + zw2, ww2 + yy2 - 1.0,       yz2 - xw2, 0.0,
            //              xz2 - yw2,       yz2 + xw2, ww2 + zz2 - 1.0, 0.0,
            //                    0.0,             0.0,             0.0, 1.0
            //    );
            return new Matrix4x4(
                    1.0 - yy2 - zz2,       xy2 - zw2,       xz2 + yw2, 0.0,
                          xy2 + zw2, 1.0 - xx2 - zz2,       yz2 - xw2, 0.0,
                          xz2 - yw2,       yz2 + xw2, 1.0 - xx2 - yy2, 0.0,
                                0.0,             0.0,             0.0, 1.0
                );
        }

        public static Matrix4x4 MakeRotateEuelr(Vector3 inEuler, EEulerRotationOrder inOrder)
            => EulerAngles.EulerToMatrix(inEuler, inOrder);

        public static Matrix4x4 MakeScale(double inX, double inY, double inZ)
            => new Matrix4x4(
                    inX, 0.0, 0.0, 0.0,
                    0.0, inY, 0.0, 0.0,
                    0.0, 0.0, inZ, 0.0,
                    0.0, 0.0, 0.0, 1.0
                );

        public static Matrix4x4 MakeScale(Vector3 inValue)
            => MakeScale(inValue.x, inValue.y, inValue.z);

        public static Matrix4x4 MakeOrtho(double inLeft, double inRight, double inBottom, double inTop, double inNear, double inFar)
        {
            if (inLeft.AlmostEquals(inRight)) {
                throw new DivideByZeroException($"Argument \"{nameof(inLeft)}\" and \"{nameof(inRight)}\" must be different value.");
            }
            if (inBottom.AlmostEquals(inTop)) {
                throw new DivideByZeroException($"Argument \"{nameof(inBottom)}\" and \"{nameof(inBottom)}\" must be different value.");
            }
            if (inNear.AlmostEquals(inFar)) {
                throw new DivideByZeroException($"Argument \"{nameof(inNear)}\" and \"{nameof(inFar)}\" must be different value.");
            }

            return new Matrix4x4(
                    2.0 / (inRight - inLeft),                      0.0,                     0.0, -(inRight + inLeft) / (inRight - inLeft),
                                         0.0, 2.0 / (inTop - inBottom),                     0.0, -(inTop + inBottom) / (inTop - inBottom),
                                         0.0,                      0.0, -2.0 / (inFar - inNear),     -(inFar + inNear) / (inFar - inNear),
                                         0.0,                      0.0,                     0.0,                                      1.0
                );
        }
        public static Matrix4x4 MakeOrtho(double aWidth, double aHeight, double aNear, double aFar)
            => MakeOrtho(-aWidth * 0.5f, aWidth * 0.5f, -aHeight * 0.5f, aHeight * 0.5f, aNear, aFar);
        public static Matrix4x4 MakeOrtho(double aFovY, double aWidth, double aHeight, double aNear, double aFar)
            => MakeOrtho(aFovY * (aWidth / aHeight), aFovY, aNear, aFar);

        public static Matrix4x4 MakeFrustum(double inLeft, double inRight, double inBottom, double inTop, double inNear, double inFar)
        {
            if (inLeft.AlmostEquals(inRight)) {
                throw new DivideByZeroException($"Argument \"{nameof(inLeft)}\" and \"{nameof(inRight)}\" must be different value.");
            }
            if (inBottom.AlmostEquals(inTop)) {
                throw new DivideByZeroException($"Argument \"{nameof(inBottom)}\" and \"{nameof(inBottom)}\" must be different value.");
            }
            if (inNear.AlmostEquals(inFar)) {
                throw new DivideByZeroException($"Argument \"{nameof(inNear)}\" and \"{nameof(inFar)}\" must be different value.");
            }

            return new Matrix4x4(
                    (2.0 * inNear) / (inRight - inLeft),                                 0.0, (inRight + inLeft) / (inRight - inLeft),                                        0.0,
                                                    0.0, (2.0 * inNear) / (inTop - inBottom), (inTop + inBottom) / (inTop - inBottom),                                        0.0,
                                                    0.0,                                 0.0,    -(inFar + inNear) / (inFar - inNear), -(2.0 * inFar * inNear) / (inFar - inNear),
                                                    0.0,                                 0.0,                                    -1.0,                                        0.0
                );
        }

        public static Matrix4x4 MakeFrustum(double inWidth, double inHeight, double inNear, double inFar)
            => MakeFrustum(-inWidth * 0.5f, inWidth * 0.5f, -inHeight * 0.5f, inHeight * 0.5f, inNear, inFar);

        public static Matrix4x4 MakePerspective(double inFovYinDeg, double inAspect, double inNear, double inFar)
        {
            double fovY_2 = Math.Tan(inFovYinDeg.DegToRad() * 0.5f) * inNear;
            double fovX_2 = fovY_2 * inAspect;

            return MakeFrustum(-fovX_2, fovX_2, -fovY_2, fovY_2, inNear, inFar);
        }

        public static Matrix4x4 MakePerspective(double aFovY, double aWidth, double aHeight, double aNear, double aFar)
            => MakePerspective(aFovY, aWidth / aHeight, aNear, aFar);

        public override string ToString()
        {
            return $"{m00}, {m01}, {m02}, {m03}, {Environment.NewLine}" +
                   $"{m10}, {m11}, {m12}, {m13}, {Environment.NewLine}" +
                   $"{m20}, {m21}, {m22}, {m23}, {Environment.NewLine}" +
                   $"{m30}, {m31}, {m32}, {m33}{Environment.NewLine}";
        }
    }
}
