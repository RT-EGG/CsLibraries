using System;

namespace RtCs.MathUtils
{
    public struct Quaternion : IEquatable<Quaternion>
    {
        public Quaternion(float inX, float inY, float inZ, float inW)
        {
            x = inX;
            y = inY;
            z = inZ;
            w = inW;
            return;
        }

        public Quaternion(float inReal, Vector3 inImaginary)
            : this(inImaginary.x, inImaginary.y, inImaginary.z, inReal)
        { }

        public float x;
        public float y;
        public float z;
        public float w;

        public float Real
        {
            get => w;
            set => w = value;
        }
        public Vector3 Imaginary
        {
            get => new Vector3(x, y, z);
            set {
                x = value.x;
                y = value.y;
                z = value.z;
                return;
            }
        }

        public Vector3 ToEuler(EEulerRotationOrder inOrder)
            => EulerAngles.QuaternionToEuler(this, inOrder);
        public static Quaternion FromEuler(Vector3 inEuler, EEulerRotationOrder inOrder)
            => EulerAngles.EulerToQuaternion(inEuler, inOrder);
        public static Quaternion FromEuler(float inEulerX, float inEulerY, float inEulerZ, EEulerRotationOrder inOrder)
            => EulerAngles.EulerToQuaternion(new Vector3(inEulerX, inEulerY, inEulerZ), inOrder);

        public float Norm => Real.Sqr() + Imaginary.Length2;
        public float Abs => (float)Math.Sqrt(Norm);

        public Quaternion Conjugated
            => new Quaternion(Real, -Imaginary);

        public Quaternion Normalized
        {
            get {
                float norm = Norm;
                if (norm.AlmostZero()) {
                    return new Quaternion(0.0f, new Vector3(0.0f));
                }

                float abs = (float)Math.Sqrt(norm);
                return this / abs;
            }
        }

        public void Conjugate()
            => this = Conjugated;
        public void Normalize()
            => this = Normalized;

        public static Quaternion MakeRotation(float aAngleRad, float aAxisX, float aAxisY, float aAxisZ)
            => MakeRotation(aAngleRad, new Vector3(aAxisX, aAxisY, aAxisZ));
        public static Quaternion MakeRotation(float inAngleRad, Vector3 inAxis)
            => new Quaternion((float)Math.Cos(inAngleRad * 0.5f), inAxis.Normalized * (float)Math.Sin(inAngleRad * 0.5f));

        public bool Equals(Quaternion quaternion)
            => x.AlmostEquals(quaternion.x) &&
               y.AlmostEquals(quaternion.y) &&
               z.AlmostEquals(quaternion.z) &&
               w.AlmostEquals(quaternion.w);

        public override bool Equals(object obj)
            => obj is Quaternion quaternion && Equals(quaternion);

        public override int GetHashCode()
        {
            int hashCode = -1743314642;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + z.GetHashCode();
            hashCode = hashCode * -1521134295 + w.GetHashCode();
            return hashCode;
        }

        public static readonly Quaternion Identity = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);

        public static Quaternion operator ~(Quaternion inValue)
            => inValue.Conjugated;
        public static Quaternion operator -(Quaternion inValue)
            => inValue * -1.0f;
        public static Quaternion operator +(Quaternion inLeft, Quaternion inRight)
            => new Quaternion(inLeft.Real + inRight.Real, inLeft.Imaginary + inRight.Imaginary);
        public static Quaternion operator -(Quaternion inLeft, Quaternion inRight)
            => new Quaternion(inLeft.Real - inRight.Real, inLeft.Imaginary - inRight.Imaginary);
        public static Quaternion operator *(Quaternion inLeft, Quaternion inRight)
            => new Quaternion(
                (inLeft.Real * inRight.Real) - Vector.Dot(inLeft.Imaginary, inRight.Imaginary),
                (inLeft.Real * inRight.Imaginary) + (inLeft.Imaginary * inRight.Real) + Vector3.Cross(inLeft.Imaginary, inRight.Imaginary));
        public static Quaternion operator *(Quaternion inLeft, float inRight)
            => new Quaternion(inLeft.Real * inRight, inLeft.Imaginary * inRight);
        public static Quaternion operator *(float inLeft, Quaternion inRight)
            => new Quaternion(inLeft * inRight.Real, inLeft * inRight.Imaginary);
        public static Quaternion operator /(Quaternion inLeft, float inRight)
            => new Quaternion(inLeft.Real / inRight, inLeft.Imaginary / inRight);
        public static Vector3 operator *(Quaternion inLeft, Vector3 inRight)
            => (inLeft * (new Quaternion(0.0f, inRight)) * ~inLeft).Imaginary;

        public static float Dot(Quaternion inLeft, Quaternion inRight)
            => (inLeft.Real * inRight.Real) + Vector3.Dot(inLeft.Imaginary, inRight.Imaginary);

        public static Quaternion LinearInterpolate(Quaternion inValue1, Quaternion inValue2, float inRatio)
            => ((inValue1 * (1.0f - inRatio)) + (inValue2 * inRatio)).Normalized;

        public static Quaternion SphereInterpolate(Quaternion inValue1, Quaternion inValue2, float aRatio)
        {
            if (inValue1.Equals(inValue2) || inValue1.Equals(-inValue2)) {
                return inValue1;
            }

            if (Dot(inValue1, inValue2) < 0.0f) {
                inValue2 *= -1.0f;
            }

            inValue1.Normalize();
            inValue2.Normalize();

            float prod = Dot(inValue1, inValue2).Clamp(-1.0f, 1.0f);

            float thete = (float)Math.Acos(prod);
            float sThete = (float)Math.Sin(prod);

            if (sThete.AlmostZero()) {
                return LinearInterpolate(inValue1, inValue2, aRatio);
            } else {
                return (((float)Math.Sin(thete * (1.0f - aRatio)) / sThete) * inValue1) + (((float)Math.Sin(thete * aRatio) / sThete) * inValue2);
            }
        }

        public override string ToString()
            => $"({x}, {y}, {z}), {w}";
    }
}
