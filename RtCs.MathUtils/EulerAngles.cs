using System;

namespace RtCs.MathUtils
{
    public enum EEulerRotationOrder
    {
        XYZ,
        XZY,
        YXZ,
        YZX,
        ZXY,
        ZYX
    }

    // refered https://qiita.com/aa_debdeb/items/abe90a9bd0b4809813da

    static class EulerAngles
    {
        //public static Quaternion EulerToQuaternion(Vector3 inEuler, EulerRotationOrder inOrder)
        //    => EulerToMatrix(inEuler, inOrder).Rotation;

        //public static Vector3 QuaternionToEuler(Quaternion inQuaternion, EulerRotationOrder inOrder)
        //    => MatrixToEuler(Matrix4x4.MakeRotate(inQuaternion), inOrder);

        public static Quaternion EulerToQuaternion(Vector3 inEuler, EEulerRotationOrder inOrder)
        {
            double cx = Math.Cos(inEuler.x * 0.5); double sx = Math.Sin(inEuler.x * 0.5);
            double cy = Math.Cos(inEuler.y * 0.5); double sy = Math.Sin(inEuler.y * 0.5);
            double cz = Math.Cos(inEuler.z * 0.5); double sz = Math.Sin(inEuler.z * 0.5);
            switch (inOrder) {
                case EEulerRotationOrder.XYZ:
                    return new Quaternion(
                            (float)( (cx * sy * sz) + (sx * cy * cz)),
                            (float)(-(sx * cy * sz) + (cx * sy * cz)),
                            (float)( (cx * cy * sz) + (sx * sy * cz)),
                            (float)(-(sx * sy * sz) + (cx * cy * cz))
                        );

                case EEulerRotationOrder.XZY:
                    return new Quaternion(
                            (float)(-(cx * sy * sz) + (sx * cy * cz)),
                            (float)( (cx * sy * cz) - (sx * cy * sz)),
                            (float)( (sx * sy * cz) + (cx * cy * sz)),
                            (float)( (sx * sy * sz) + (cx * cy * cz))
                        );

                case EEulerRotationOrder.YXZ:
                    return new Quaternion(
                            (float)( (cx * sy * sz) + (sx * cy * cz)),
                            (float)(-(sx * cy * sz) + (cx * sy * cz)),
                            (float)( (cx * cy * sz) - (sx * sy * cz)),
                            (float)( (sx * sy * sz) + (cx * cy * cz))
                        );

                case EEulerRotationOrder.YZX:
                    return new Quaternion(
                            (float)( (sx * cy * cz) + (cx * sy * sz)),
                            (float)( (sx * cy * sz) + (cx * sy * cz)),
                            (float)(-(sx * sy * cz) + (cx * cy * sz)),
                            (float)(-(sx * sy * sz) + (cx * cy * cz))
                        );

                case EEulerRotationOrder.ZXY:
                    return new Quaternion(
                            (float)(-(cx * sy * sz) + (sx * cy * cz)),
                            (float)( (cx * sy * cz) + (sx * cy * sz)),
                            (float)( (sx * sy * cz) + (cx * cy * sz)),
                            (float)(-(sx * sy * sz) + (cx * cy * cz))
                        );

                case EEulerRotationOrder.ZYX:
                    return new Quaternion(
                            (float)( (sx * cy * cz) - (cx * sy * sz)),
                            (float)( (sx * cy * sz) + (cx * sy * cz)),
                            (float)(-(sx * sy * cz) + (cx * cy * sz)),
                            (float)( (sx * sy * sz) + (cx * cy * cz))
                        );
            }
            throw new InvalidEnumValueException<EEulerRotationOrder>(inOrder);
        }

        public static Vector3 QuaternionToEuler(Quaternion inQuaternion, EEulerRotationOrder inOrder)
        {
            const double n = 1.0 - 1.0e-6;
            ref Quaternion q = ref inQuaternion;
            double sx, sy, sz;
            bool unlocked;
            switch (inOrder) {
                case EEulerRotationOrder.XYZ:
                    sy = (2.0 * q.x * q.z) + (2.0 * q.y * q.w);
                    unlocked = Math.Abs(sy) < n;
                    return new Vector3(
                            (float)(unlocked ? Math.Atan2(-((2.0 * q.y * q.z) - (2.0 * q.x * q.w)), (2.0 * q.w * q.w) + (2.0 * q.z * q.z) - 1.0)
                                             : Math.Atan2(  (2.0 * q.y * q.z) + (2.0 * q.x * q.w),  (2.0 * q.w * q.w) + (2.0 * q.y * q.y) - 1.0)),
                            (float)Numerics.Asin(sy),
                            (float)(unlocked ? Math.Atan2(-((2.0 * q.x * q.y) - (2.0 * q.z * q.w)), (2.0 * q.w * q.w) + (2.0 * q.x * q.x) - 1.0) : 0.0)
                        );

                case EEulerRotationOrder.XZY:
                    sz = -((2.0 * q.x * q.y) - (2.0 * q.z * q.w));
                    unlocked = Math.Abs(sz) < n;
                    return new Vector3(
                            (float)(unlocked ? Math.Atan2(  (2.0 * q.y * q.z) + (2.0 * q.x * q.w),  (2.0 * q.w * q.w) + (2.0 * q.y * q.y) - 1.0)
                                             : Math.Atan2(-((2.0 * q.y * q.z) - (2.0 * q.x * q.w)), (2.0 * q.w * q.w) + (2.0 * q.z * q.z) - 1.0)),
                            (float)(unlocked ? Math.Atan2(  (2.0 * q.x * q.z) + (2.0 * q.y * q.w),  (2.0 * q.w * q.w) + (2.0 * q.x * q.x) - 1.0) : 0.0),
                            (float)Numerics.Asin(sz)
                        );

                case EEulerRotationOrder.YXZ:
                    sx = -((2.0 * q.y * q.z) - (2.0 * q.x * q.w));
                    unlocked = Math.Abs(sx) < n;
                    return new Vector3(
                            (float)Numerics.Asin(sx),
                            (float)(unlocked ? Math.Atan2(  (2.0 * q.x * q.z) + (2.0 * q.y * q.w),  (2.0 * q.w * q.w) + (2.0 * q.z * q.z) - 1.0)
                                             : Math.Atan2(-((2.0 * q.x * q.z) - (2.0 * q.y * q.w)), (2.0 * q.w * q.w) + (2.0 * q.x * q.x) - 1.0)),
                            (float)(unlocked ? Math.Atan2(  (2.0 * q.x * q.y) + (2.0 * q.z * q.w),  (2.0 * q.w * q.w) + (2.0 * q.y * q.y) - 1.0) : 0.0)
                        );

                case EEulerRotationOrder.YZX:
                    sz = (2.0 * q.x * q.y) + (2.0 * q.z * q.w);
                    unlocked = Math.Abs(sz) < n;
                    return new Vector3(
                            (float)(unlocked ? Math.Atan2(-((2.0 * q.y * q.z) - (2.0 * q.x * q.w)), (2.0 * q.w * q.w) + (2.0 * q.y * q.y) - 1.0) : 0.0),
                            (float)(unlocked ? Math.Atan2(-((2.0 * q.x * q.z) - (2.0 * q.y * q.w)), (2.0 * q.w * q.w) + (2.0 * q.x * q.x) - 1.0)
                                             : Math.Atan2(  (2.0 * q.x * q.z) + (2.0 * q.y * q.w),  (2.0 * q.w * q.w) + (2.0 * q.z * q.z) - 1.0)),
                            (float)Numerics.Asin(sz)
                        );

                case EEulerRotationOrder.ZXY:
                    sx = (2.0 * q.y * q.z) + (2.0 * q.x * q.w);
                    unlocked = Math.Abs(sx) < n;
                    return new Vector3(
                            (float)Numerics.Asin(sx),
                            (float)(unlocked ? Math.Atan2(-((2.0 * q.x * q.z) - (2.0 * q.y * q.w)), (2.0 * q.w * q.w) + (2.0 * q.z * q.z) - 1.0) : 0.0),
                            (float)(unlocked ? Math.Atan2(-((2.0 * q.x * q.y) - (2.0 * q.z * q.w)), (2.0 * q.w * q.w) + (2.0 * q.y * q.y) - 1.0)
                                             : Math.Atan2(  (2.0 * q.x * q.y) + (2.0 * q.z * q.w),  (2.0 * q.w * q.w) + (2.0 * q.x * q.x) - 1.0))
                        );

                case EEulerRotationOrder.ZYX:
                    sy = -((2.0 * q.x * q.z) - (2.0 * q.y * q.w));
                    unlocked = Math.Abs(sy) < n;
                    return new Vector3(
                            (float)(unlocked ? Math.Atan2((2.0 * q.y * q.z) + (2.0 * q.x * q.w), (2.0 * q.w * q.w) + (2.0 * q.z * q.z) - 1.0) : 0.0),
                            (float)Numerics.Asin(sy),
                            (float)(unlocked ? Math.Atan2(  (2.0 * q.x * q.y) + (2.0 * q.z * q.w),  (2.0 * q.w * q.w) + (2.0 * q.x * q.x) - 1.0)
                                             : Math.Atan2(-((2.0 * q.x * q.y) - (2.0 * q.z * q.w)), (2.0 * q.w * q.w) + (2.0 * q.y * q.y) - 1.0))
                        );
            }
            throw new InvalidEnumValueException<EEulerRotationOrder>(inOrder);
        }

        public static Matrix4x4 EulerToMatrix(Vector3 inEuler, EEulerRotationOrder inOrder)
        {
            float cx = (float)Math.Cos(inEuler.x); float sx = (float)Math.Sin(inEuler.x);
            float cy = (float)Math.Cos(inEuler.y); float sy = (float)Math.Sin(inEuler.y);
            float cz = (float)Math.Cos(inEuler.z); float sz = (float)Math.Sin(inEuler.z);

            switch (inOrder) {
                case EEulerRotationOrder.XYZ:
                    return new Matrix4x4(
                                             (cy * cz),                  -(cy * sz),       sy, 0.0f,
                            (sx * sy * cz) + (cx * sz), -(sx * sy * sz) + (cx * cz), -sx * cy, 0.0f,
                            (cx * sy * cz) + (sx * sz),  (cx * sy * sz) + (sx * cz),  cx * cy, 0.0f,
                                                  0.0f,                        0.0f,     0.0f, 1.0f
                        );

                case EEulerRotationOrder.XZY:
                    return new Matrix4x4(
                                             (cy * cz),       -sz,                  (sy * cz), 0.0f,
                            (cx * cy * sz) + (sx * sy), (cx * cz), (cx * sy * sz) - (sx * cy), 0.0f,
                            (sx * cy * sz) - (cx * sy), (sx * cz), (sx * sy * sz) + (cx * cy), 0.0f,
                                                  0.0f,      0.0f,                       0.0f, 1.0f
                        );

                case EEulerRotationOrder.YXZ:
                    return new Matrix4x4(
                            (sx * sy * sz) + (cy * cz), (sx * sy * cz) - (cy * sz), (cx * sy), 0.0f,
                                             (cx * sz),                  (cx * cz),       -sx, 0.0f,
                            (sx * cy * sz) - (sy * cz), (sx * cy * cz) + (sy * sz), (cx * cy), 0.0f,
                                                  0.0f,                       0.0f,      0.0f, 1.0f
                        );

                case EEulerRotationOrder.YZX:
                    return new Matrix4x4(
                             (cy * cz), -(cx * cy * sz) + (sx * sy),  (sx * cy * sz) + (cx * sy), 0.0f,
                                    sz,                   (cx * cz),                  -(sx * cz), 0.0f,
                            -(sy * cz),  (cx * sy * sz) + (sx * cy), -(sx * sy * sz) + (cx * cy), 0.0f,
                                  0.0f,                        0.0f,                        0.0f, 1.0f
                        );

                case EEulerRotationOrder.ZXY:
                    return new Matrix4x4(
                            -(sx * sy * sz) + (cy * cz), -(cx * sz),  (sx * cy * sz) + (sy * cz), 0.0f,
                             (sx * sy * cz) + (cy * sz),  (cx * cz), -(sx * cy * cz) + (sy * sz), 0.0f,
                                             -(cx * sy),         sx,                   (cx * cy), 0.0f,
                                                   0.0f,       0.0f,                        0.0f, 1.0f
                        );

                case EEulerRotationOrder.ZYX:
                    return new Matrix4x4(
                            (cy * cz), (sx * sy * cz) - (cx * sz), (cx * sy * cz) + (sx * sz), 0.0f,
                            (cy * sz), (sx * sy * sz) + (cx * sz), (cx * sy * sz) - (sx * cz), 0.0f,
                                  -sy,                  (sx * cy),                  (cx * cy), 0.0f,
                                 0.0f,                       0.0f,                       0.0f, 1.0f
                        );
            }
            throw new InvalidEnumValueException<EEulerRotationOrder>(inOrder);
        }

        public static Vector3 MatrixToEuler(Matrix4x4 inRotation, EEulerRotationOrder inOrder)
        {
            const double n = 1.0 - 1.0e-6;
            double sx, sy, sz;
            bool unlocked;
            switch (inOrder) {
                case EEulerRotationOrder.XYZ:
                    sy = inRotation[2, 0];
                    unlocked = Math.Abs(sy) < n;
                    return new Vector3(
                            (float)(unlocked ? Math.Atan2(-inRotation[2, 1], inRotation[2, 2]) : Math.Atan2(inRotation[1, 2], inRotation[1, 1])),
                            (float)Numerics.Asin(sy),
                            (float)(unlocked ? Math.Atan2(-inRotation[1, 0], inRotation[0, 0]) : 0.0)
                        );

                case EEulerRotationOrder.XZY:
                    sz = -inRotation[1, 0];
                    unlocked = Math.Abs(sz) < n;
                    return new Vector3(
                            (float)(unlocked ? Math.Atan2(inRotation[1, 2], inRotation[1, 1]) : Math.Atan2(inRotation[2, 1], inRotation[2, 2])),
                            (float)(unlocked ? Math.Atan2(inRotation[2, 0], inRotation[0, 0]) : 0.0),
                            (float)Numerics.Asin(sz)
                        );

                case EEulerRotationOrder.YXZ:
                    sx = -inRotation[2, 1];
                    unlocked = Math.Abs(sx) < n;
                    return new Vector3(
                            (float)Numerics.Asin(sx),
                            (float)(unlocked ? Math.Atan2(inRotation[2, 0], inRotation[2, 2]) : Math.Atan2(-inRotation[0, 2], inRotation[0, 0])),
                            (float)(unlocked ? Math.Atan2(inRotation[0, 1], inRotation[1, 1]) : 0.0)
                        );

                case EEulerRotationOrder.YZX:
                    sz = inRotation[0, 1];
                    unlocked = Math.Abs(sz) < n;
                    return new Vector3(
                            (float)(unlocked ? Math.Atan2(-inRotation[2, 1], inRotation[1, 1]) : 0.0),
                            (float)(unlocked ? Math.Atan2(-inRotation[0, 2], inRotation[0, 0]) : Math.Atan2(inRotation[2, 0], inRotation[2, 2])),
                            (float)Numerics.Asin(sz)
                        );

                case EEulerRotationOrder.ZXY:
                    sx = inRotation[1, 2];
                    unlocked = Math.Abs(sx) < n;
                    return new Vector3(
                            (float)Numerics.Asin(sx),
                            (float)(unlocked ? Math.Atan2(-inRotation[0, 2], inRotation[2, 2]) : 0.0),
                            (float)(unlocked ? Math.Atan2(-inRotation[1, 0], inRotation[1, 1]) : Math.Atan2(inRotation[0, 1], inRotation[0, 0]))
                        );

                case EEulerRotationOrder.ZYX:
                    sy = -inRotation[0, 2];
                    unlocked = Math.Abs(sy) < n;
                    return new Vector3(
                            (float)(unlocked ? Math.Atan2(inRotation[1, 2], inRotation[2, 2]) : 0.0),
                            (float)Numerics.Asin(sy),
                            (float)(unlocked ? Math.Atan2(inRotation[0, 1], inRotation[0, 0]) : Math.Atan2(-inRotation[1, 0], inRotation[1, 1]))
                        );
            }
            throw new InvalidEnumValueException<EEulerRotationOrder>(inOrder);
        }
    }
}
