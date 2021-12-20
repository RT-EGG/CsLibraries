using System;
using System.Collections;
using System.Collections.Generic;

namespace RtCs.MathUtils
{
    public struct Matrix4x4 : IMatrix, IEquatable<Matrix4x4>
    {
        //public Matrix4x4(params float[] inValues)
        //    : this((IEnumerable<float>) inValues)
        //{ }

        public Matrix4x4(IEnumerable<float> inValues)
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

        public Matrix4x4(float in00, float in01, float in02, float in03, float in10, float in11, float in12, float in13, 
                         float in20, float in21, float in22, float in23, float in30, float in31, float in32, float in33)
        {
            m00 = in00;
            m01 = in01;
            m02 = in02;
            m03 = in03;
            m10 = in10;
            m11 = in11;
            m12 = in12;
            m13 = in13;
            m20 = in20;
            m21 = in21;
            m22 = in22;
            m23 = in23;
            m30 = in30;
            m31 = in31;
            m32 = in32;
            m33 = in33;
        }

        public float m00;
        public float m01;
        public float m02;
        public float m03;
        public float m10;
        public float m11;
        public float m12;
        public float m13;
        public float m20;
        public float m21;
        public float m22;
        public float m23;
        public float m30;
        public float m31;
        public float m32;
        public float m33;

        public float this[int inIndex]
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

        public float this[int inRow, int inCol]
        {
            get => this[this.RowColToIndex(inRow, inCol)];
            set => this[this.RowColToIndex(inRow, inCol)] = value;
        }

        public Matrix4x4 SetElements(params (int row, int col, float value)[] inCommands)
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

        public void SetRow(int inRowIndex, float in0, float in1, float in2, float in3)
            => SetElements(
                (inRowIndex, 0, in0),
                (inRowIndex, 1, in1),
                (inRowIndex, 2, in2),
                (inRowIndex, 3, in3)
            );

        public void SetRow(int inRowIndex, IEnumerable<float> inValues)
        {
            var enumerator = inValues.GetEnumerator();
            this[inRowIndex, 0] = enumerator.Current; enumerator.MoveNext();
            this[inRowIndex, 1] = enumerator.Current; enumerator.MoveNext();
            this[inRowIndex, 2] = enumerator.Current; enumerator.MoveNext();
            this[inRowIndex, 3] = enumerator.Current; enumerator.MoveNext();
            return;
        }

        public void SetColumn(int inColIndex, float in0, float in1, float in2, float in3)
            => SetElements(
                (0, inColIndex, in0),
                (1, inColIndex, in1),
                (2, inColIndex, in2),
                (3, inColIndex, in3)
            );

        public void SetColumn(int inColIndex, IEnumerable<float> inValues)
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

                float px =  this[0, 0] - this[1, 1] - this[2, 2] + 1.0f;
                float py = -this[0, 0] + this[1, 1] - this[2, 2] + 1.0f;
                float pz = -this[0, 0] - this[1, 1] + this[2, 2] + 1.0f;
                float pw =  this[0, 0] + this[1, 1] + this[2, 2] + 1.0f;

                int s = 0;
                float max = px;
                float v = (float)Math.Sqrt(px) * 0.5f;
                if (max < py) {
                    s = 1;
                    v = (float)Math.Sqrt(py) * 0.5f;
                    max = py;
                }
                if (max < pz) {
                    s = 2;
                    v = (float)Math.Sqrt(pz) * 0.5f;
                    max = pz;
                }
                if (max < pw) {
                    s = 3;
                    v = (float)Math.Sqrt(pw) * 0.5f;
                    max = pw;
                }

                float d = 0.25f / v;
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

        int IMatrix.ElemCount => ElemCount;
        int IMatrix.RowCount => RowCount;
        int IMatrix.ColCount => ColCount;
        int IReadOnlyCollection<float>.Count => ElemCount;
        public const int ElemCount = 16;
        public const int RowCount = 4;
        public const int ColCount = 4;

        public override bool Equals(object inOther)
            => inOther is Matrix4x4 x && Equals(x);

        public bool Equals(Matrix4x4 other)
        {
            bool Equals(float left, float right) => left.AlmostEquals(right, NumericExtensions.FloatThreshold);
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

        public IEnumerator<float> GetEnumerator()
            => this.Enumerate().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public bool Inverse()
        {
            // http://miffysora.wikidot.com/invertmatrix-4x4
            // http://www.iwata-system-support.com/CAE_HomePage/vector/vector18/vector18.html
            // http://physmath.main.jp/src/inverse-cofactor-ex4.html

            float det = Deternimant;
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

        public float Deternimant
            => (this[0, 0] * GetSubDeterminant(0, 0))
             - (this[1, 0] * GetSubDeterminant(1, 0))
             + (this[2, 0] * GetSubDeterminant(2, 0))
             - (this[3, 0] * GetSubDeterminant(3, 0));

        private float GetSubDeterminant(int inRow, int inCol)
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

        public Vector4 Multiply(Vector3 inRight, float inRightW)
            => Multiply(new Vector4(inRight.x, inRight.y, inRight.z, inRightW));
        public Vector4 Multiply(Vector4 inRight)
            => Multiply(this, inRight);

        public static readonly Matrix4x4 Identity = new Matrix4x4(
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
            );
        public static readonly Matrix4x4 Zero = new Matrix4x4(
                0.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f
            );

        public static bool operator ==(Matrix4x4 left, Matrix4x4 right)
            => left.Equals(right);
        public static bool operator !=(Matrix4x4 left, Matrix4x4 right)
            => !(left == right);
        public static Matrix4x4 operator -(Matrix4x4 matrix)
            => matrix * -1.0f;
        public static Matrix4x4 operator +(Matrix4x4 left, Matrix4x4 right)
            => new Matrix4x4(
                    left.m00 + right.m00, left.m01 + right.m01, left.m02 + right.m02, left.m03 + right.m03,
                    left.m10 + right.m10, left.m11 + right.m11, left.m12 + right.m12, left.m13 + right.m13,
                    left.m20 + right.m20, left.m21 + right.m21, left.m22 + right.m22, left.m23 + right.m23,
                    left.m30 + right.m30, left.m31 + right.m31, left.m32 + right.m32, left.m33 + right.m33
                );
        public static Matrix4x4 operator -(Matrix4x4 left, Matrix4x4 right)
            => new Matrix4x4(
                    left.m00 - right.m00, left.m01 - right.m01, left.m02 - right.m02, left.m03 - right.m03,
                    left.m10 - right.m10, left.m11 - right.m11, left.m12 - right.m12, left.m13 - right.m13,
                    left.m20 - right.m20, left.m21 - right.m21, left.m22 - right.m22, left.m23 - right.m23,
                    left.m30 - right.m30, left.m31 - right.m31, left.m32 - right.m32, left.m33 - right.m33
                );
        public static Matrix4x4 operator *(Matrix4x4 left, float right)
            => new Matrix4x4(
                    left.m00 * right, left.m01 * right, left.m02 * right, left.m03 * right,
                    left.m10 * right, left.m11 * right, left.m12 * right, left.m13 * right,
                    left.m20 * right, left.m21 * right, left.m22 * right, left.m23 * right,
                    left.m30 * right, left.m31 * right, left.m32 * right, left.m33 * right
                );
        public static Matrix4x4 operator *(float left, Matrix4x4 right)
            => new Matrix4x4(
                    left * right.m00, left * right.m01, left * right.m02, left * right.m03,
                    left * right.m10, left * right.m11, left * right.m12, left * right.m13,
                    left * right.m20, left * right.m21, left * right.m22, left * right.m23,
                    left * right.m30, left * right.m31, left * right.m32, left * right.m33
                );
        public static Matrix4x4 operator /(Matrix4x4 left, float right)
            => new Matrix4x4(
                    left.m00 / right, left.m01 / right, left.m02 / right, left.m03 / right,
                    left.m10 / right, left.m11 / right, left.m12 / right, left.m13 / right,
                    left.m20 / right, left.m21 / right, left.m22 / right, left.m23 / right,
                    left.m30 / right, left.m31 / right, left.m32 / right, left.m33 / right
                );
        public static Matrix4x4 operator *(Matrix4x4 left, Matrix4x4 right)
        {
            Matrix4x4 result = default;
            Matrix.MultiplyMatrix(ref result, left, right);
            return result;
        }
        public static Vector4 operator *(Matrix4x4 left, Vector4 right)
            => Multiply(left, right);

        public static Vector4 Multiply(Matrix4x4 inLeft, Vector3 inRight, float inRightW)
            => Multiply(inLeft, new Vector4(inRight.x, inRight.y, inRight.z, inRightW));
        public static Vector4 Multiply(Matrix4x4 inLeft, Vector4 inRight)
        {
            Vector4 result = default;
            Matrix.MultiplyVector(ref result, inLeft, inRight);
            return result;
        }

        public static Matrix4x4 MakeTranslate(float inX, float inY, float inZ)
            => new Matrix4x4(
                    1.0f, 0.0f, 0.0f, inX,
                    0.0f, 1.0f, 0.0f, inY,
                    0.0f, 0.0f, 1.0f, inZ,
                    0.0f, 0.0f, 0.0f, 1.0f
                );

        public static Matrix4x4 MakeTranslate(Vector3 inXYZ)
            => MakeTranslate(inXYZ.x, inXYZ.y, inXYZ.z);

        public static Matrix4x4 MakeRotateX(float inRadian)
        {
            float c = (float)Math.Cos(inRadian);
            float s = (float)Math.Sin(inRadian);
            return new Matrix4x4(
                    1.0f, 0.0f, 0.0f, 0.0f,
                    0.0f,    c,   -s, 0.0f,
                    0.0f,    s,    c, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        public static Matrix4x4 MakeRotateY(float inRadian)
        {
            float c = (float)Math.Cos(inRadian);
            float s = (float)Math.Sin(inRadian);
            return new Matrix4x4(
                       c, 0.0f,    s, 0.0f,
                    0.0f, 1.0f, 0.0f, 0.0f,
                      -s, 0.0f,    c, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        public static Matrix4x4 MakeRotateZ(float inRadian)
        {
            float c = (float)Math.Cos(inRadian);
            float s = (float)Math.Sin(inRadian);
            return new Matrix4x4(
                       c,   -s, 0.0f, 0.0f,
                       s,    c, 0.0f, 0.0f,
                    0.0f, 0.0f, 1.0f, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        public static Matrix4x4 MakeRotate(float inRadian, float inAxisX, float inAxisY, float inAxisZ)
        {
            if (inAxisX.AlmostZero() && inAxisY.AlmostZero() && inAxisZ.AlmostZero()) {
                return Identity;
            }

            float c = (float)Math.Cos(inRadian);
            float s = (float)Math.Sin(inRadian);

            return new Matrix4x4(
                    (inAxisX * inAxisX * (1.0f - c)) + c,
                    (inAxisX * inAxisY * (1.0f - c)) - (inAxisZ * s),
                    (inAxisX * inAxisZ * (1.0f - c)) + (inAxisY * s),
                    0.0f,
                    (inAxisY * inAxisX * (1.0f - c)) + (inAxisZ * s),
                    (inAxisY * inAxisY * (1.0f - c)) + c,
                    (inAxisY * inAxisZ * (1.0f - c)) - (inAxisX * s),
                    0.0f,
                    (inAxisZ * inAxisX * (1.0f - c)) - (inAxisY * s),
                    (inAxisZ * inAxisY * (1.0f - c)) + (inAxisX * s),
                    (inAxisZ * inAxisZ * (1.0f - c)) + c,
                    0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        public static Matrix4x4 MakeRotate(Quaternion inValue)
        {
            // refered http://marupeke296.sakura.ne.jp/DXG_No58_RotQuaternionTrans.html
            ref Quaternion q = ref inValue;
            float xx2 = q.x * q.x * 2.0f;
            float xy2 = q.x * q.y * 2.0f;
            float xz2 = q.x * q.z * 2.0f;
            float xw2 = q.x * q.w * 2.0f;
            float yy2 = q.y * q.y * 2.0f;
            float yz2 = q.y * q.z * 2.0f;
            float yw2 = q.y * q.w * 2.0f;
            float zz2 = q.z * q.z * 2.0f;
            float zw2 = q.z * q.w * 2.0f;
            float ww2 = q.w * q.w * 2.0f;
            return new Matrix4x4(
                    1.0f - yy2 - zz2,        xy2 - zw2,        xz2 + yw2, 0.0f,
                           xy2 + zw2, 1.0f - xx2 - zz2,        yz2 - xw2, 0.0f,
                           xz2 - yw2,        yz2 + xw2, 1.0f - xx2 - yy2, 0.0f,
                                0.0f,             0.0f,             0.0f, 1.0f
                );
        }

        public static Matrix4x4 MakeRotateEuelr(Vector3 inEuler, EEulerRotationOrder inOrder)
            => EulerAngles.EulerToMatrix(inEuler, inOrder);

        public static Matrix4x4 MakeScale(float inX, float inY, float inZ)
            => new Matrix4x4(
                     inX, 0.0f, 0.0f, 0.0f,
                    0.0f,  inY, 0.0f, 0.0f,
                    0.0f, 0.0f,  inZ, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                );

        public static Matrix4x4 MakeScale(Vector3 inValue)
            => MakeScale(inValue.x, inValue.y, inValue.z);

        public static Matrix4x4 MakeOrtho(float inLeft, float inRight, float inBottom, float inTop, float inNear, float inFar)
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
                    2.0f / (inRight - inLeft),                      0.0f,                     0.0f, -(inRight + inLeft) / (inRight - inLeft),
                                         0.0f, 2.0f / (inTop - inBottom),                     0.0f, -(inTop + inBottom) / (inTop - inBottom),
                                         0.0f,                      0.0f, -2.0f / (inFar - inNear),     -(inFar + inNear) / (inFar - inNear),
                                         0.0f,                      0.0f,                     0.0f,                                     1.0f
                );
        }
        public static Matrix4x4 MakeOrtho(float aWidth, float aHeight, float aNear, float aFar)
            => MakeOrtho(-aWidth * 0.5f, aWidth * 0.5f, -aHeight * 0.5f, aHeight * 0.5f, aNear, aFar);
        public static Matrix4x4 MakeOrtho(float aFovY, float aWidth, float aHeight, float aNear, float aFar)
            => MakeOrtho(aFovY * (aWidth / aHeight), aFovY, aNear, aFar);

        public static Matrix4x4 MakePerspective(float inLeft, float inRight, float inBottom, float inTop, float inNear, float inFar)
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
                    (2.0f * inNear) / (inRight - inLeft),                                 0.0f, (inRight + inLeft) / (inRight - inLeft),                                        0.0f,
                                                    0.0f, (2.0f * inNear) / (inTop - inBottom), (inTop + inBottom) / (inTop - inBottom),                                        0.0f,
                                                    0.0f,                                 0.0f,    -(inFar + inNear) / (inFar - inNear), -(2.0f * inFar * inNear) / (inFar - inNear),
                                                    0.0f,                                 0.0f,                                   -1.0f,                                        0.0f
                );
        }

        public static Matrix4x4 MakeSymmetricalPerspective(float inFovYinDeg, float inAspect, float inNear, float inFar)
        {
            float fovY_2 = (float)Math.Tan(inFovYinDeg.DegToRad() * 0.5f) * inNear;
            float fovX_2 = fovY_2 * inAspect;

            return MakePerspective(-fovX_2, fovX_2, -fovY_2, fovY_2, inNear, inFar);
        }

        public static Matrix4x4 MakeSymmetricalPerspective(float aFovY, float aWidth, float aHeight, float aNear, float aFar)
            => MakeSymmetricalPerspective(aFovY, aWidth / aHeight, aNear, aFar);

        public override string ToString()
        {
            return $"{m00}, {m01}, {m02}, {m03}, {Environment.NewLine}" +
                   $"{m10}, {m11}, {m12}, {m13}, {Environment.NewLine}" +
                   $"{m20}, {m21}, {m22}, {m23}, {Environment.NewLine}" +
                   $"{m30}, {m31}, {m32}, {m33}{Environment.NewLine}";
        }
    }
}
