using System;

namespace RtCs.MathUtils
{
    public enum EulerRotationOrder
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

        public static Quaternion EulerToQuaternion(Vector3 inEuler, EulerRotationOrder inOrder)
        {
            double cx = Math.Cos(inEuler.x * 0.5); double sx = Math.Sin(inEuler.x * 0.5);
            double cy = Math.Cos(inEuler.y * 0.5); double sy = Math.Sin(inEuler.y * 0.5);
            double cz = Math.Cos(inEuler.z * 0.5); double sz = Math.Sin(inEuler.z * 0.5);
            switch (inOrder) {
                case EulerRotationOrder.XYZ:
                    return new Quaternion(
                             (cx * sy * sz) + (sx * cy * cz),
                            -(sx * cy * sz) + (cx * sy * cz),
                             (cx * cy * sz) + (sx * sy * cz),
                            -(sx * sy * sz) + (cx * cy * cz)
                        );

                case EulerRotationOrder.XZY:
                    return new Quaternion(
                            -(cx * sy * sz) + (sx * cy * cz),
                             (cx * sy * cz) - (sx * cy * sz),
                             (sx * sy * cz) + (cx * cy * sz),
                             (sx * sy * sz) + (cx * cy * cz)
                        );

                case EulerRotationOrder.YXZ:
                    return new Quaternion(
                             (cx * sy * sz) + (sx * cy * cz),
                            -(sx * cy * sz) + (cx * sy * cz),
                             (cx * cy * sz) - (sx * sy * cz),
                             (sx * sy * sz) + (cx * cy * cz)
                        );

                case EulerRotationOrder.YZX:
                    return new Quaternion(
                             (sx * cy * cz) + (cx * sy * sz),
                             (sx * cy * sz) + (cx * sy * cz),
                            -(sx * sy * cz) + (cx * cy * sz),
                            -(sx * sy * sz) + (cx * cy * cz)
                        );

                case EulerRotationOrder.ZXY:
                    return new Quaternion(
                            -(cx * sy * sz) + (sx * cy * cz),
                             (cx * sy * cz) + (sx * cy * sz),
                             (sx * sy * cz) + (cx * cy * sz),
                            -(sx * sy * sz) + (cx * cy * cz)
                        );

                case EulerRotationOrder.ZYX:
                    return new Quaternion(
                             (sx * cy * cz) - (cx * sy * sz),
                             (sx * cy * sz) + (cx * sy * cz),
                            -(sx * sy * cz) + (cx * cy * sz),
                             (sx * sy * sz) + (cx * cy * cz)
                        );
            }
            throw new InvalidEnumValueException<EulerRotationOrder>(inOrder);
        }

        public static Vector3 QuaternionToEuler(Quaternion inQuaternion, EulerRotationOrder inOrder)
        {
            const double n = 1.0 - 1.0e-6;
            ref Quaternion q = ref inQuaternion;
            double sx, sy, sz;
            bool unlocked;
            switch (inOrder) {
                case EulerRotationOrder.XYZ:
                    sy = (2.0 * q.x * q.z) + (2.0 * q.y * q.w);
                    unlocked = Math.Abs(sy) < n;
                    return new Vector3(
                            unlocked ? Math.Atan2(-((2.0 * q.y * q.z) - (2.0 * q.x * q.w)), (2.0 * q.w * q.w) + (2.0 * q.z * q.z) - 1.0)
                                     : Math.Atan2(  (2.0 * q.y * q.z) + (2.0 * q.x * q.w),  (2.0 * q.w * q.w) + (2.0 * q.y * q.y) - 1.0),
                            NumericExtensions.Asin(sy),
                            unlocked ? Math.Atan2(-((2.0 * q.x * q.y) - (2.0 * q.z * q.w)), (2.0 * q.w * q.w) + (2.0 * q.x * q.x) - 1.0) : 0.0
                        );

                case EulerRotationOrder.XZY:
                    sz = -((2.0 * q.x * q.y) - (2.0 * q.z * q.w));
                    unlocked = Math.Abs(sz) < n;
                    return new Vector3(
                            unlocked ? Math.Atan2(  (2.0 * q.y * q.z) + (2.0 * q.x * q.w),  (2.0 * q.w * q.w) + (2.0 * q.y * q.y) - 1.0)
                                     : Math.Atan2(-((2.0 * q.y * q.z) - (2.0 * q.x * q.w)), (2.0 * q.w * q.w) + (2.0 * q.z * q.z) - 1.0),
                            unlocked ? Math.Atan2(  (2.0 * q.x * q.z) + (2.0 * q.y * q.w),  (2.0 * q.w * q.w) + (2.0 * q.x * q.x) - 1.0) : 0.0,
                            NumericExtensions.Asin(sz)
                        );

                case EulerRotationOrder.YXZ:
                    sx = -((2.0 * q.y * q.z) - (2.0 * q.x * q.w));
                    unlocked = Math.Abs(sx) < n;
                    return new Vector3(
                        NumericExtensions.Asin(sx),
                             unlocked ? Math.Atan2(  (2.0 * q.x * q.z) + (2.0 * q.y * q.w),  (2.0 * q.w * q.w) + (2.0 * q.z * q.z) - 1.0)
                                      : Math.Atan2(-((2.0 * q.x * q.z) - (2.0 * q.y * q.w)), (2.0 * q.w * q.w) + (2.0 * q.x * q.x) - 1.0),
                             unlocked ? Math.Atan2(  (2.0 * q.x * q.y) + (2.0 * q.z * q.w),  (2.0 * q.w * q.w) + (2.0 * q.y * q.y) - 1.0) : 0.0
                        );

                case EulerRotationOrder.YZX:
                    sz = (2.0 * q.x * q.y) + (2.0 * q.z * q.w);
                    unlocked = Math.Abs(sz) < n;
                    return new Vector3(                            
                             unlocked ? Math.Atan2(-((2.0 * q.y * q.z) - (2.0 * q.x * q.w)), (2.0 * q.w * q.w) + (2.0 * q.y * q.y) - 1.0) : 0.0,
                             unlocked ? Math.Atan2(-((2.0 * q.x * q.z) - (2.0 * q.y * q.w)), (2.0 * q.w * q.w) + (2.0 * q.x * q.x) - 1.0)
                                      : Math.Atan2(  (2.0 * q.x * q.z) + (2.0 * q.y * q.w),  (2.0 * q.w * q.w) + (2.0 * q.z * q.z) - 1.0),
                             NumericExtensions.Asin(sz)
                        );

                case EulerRotationOrder.ZXY:
                    sx = (2.0 * q.y * q.z) + (2.0 * q.x * q.w);
                    unlocked = Math.Abs(sx) < n;
                    return new Vector3(
                             NumericExtensions.Asin(sx),
                             unlocked ? Math.Atan2(-((2.0 * q.x * q.z) - (2.0 * q.y * q.w)), (2.0 * q.w * q.w) + (2.0 * q.z * q.z) - 1.0) : 0.0,
                             unlocked ? Math.Atan2(-((2.0 * q.x * q.y) - (2.0 * q.z * q.w)), (2.0 * q.w * q.w) + (2.0 * q.y * q.y) - 1.0)
                                      : Math.Atan2(  (2.0 * q.x * q.y) + (2.0 * q.z * q.w),  (2.0 * q.w * q.w) + (2.0 * q.x * q.x) - 1.0)
                        );

                case EulerRotationOrder.ZYX:
                    sy = -((2.0 * q.x * q.z) - (2.0 * q.y * q.w));
                    unlocked = Math.Abs(sy) < n;
                    return new Vector3(
                             unlocked ? Math.Atan2((2.0 * q.y * q.z) + (2.0 * q.x * q.w), (2.0 * q.w * q.w) + (2.0 * q.z * q.z) - 1.0) : 0.0,
                             NumericExtensions.Asin(sy),
                             unlocked ? Math.Atan2(  (2.0 * q.x * q.y) + (2.0 * q.z * q.w),  (2.0 * q.w * q.w) + (2.0 * q.x * q.x) - 1.0)
                                      : Math.Atan2(-((2.0 * q.x * q.y) - (2.0 * q.z * q.w)), (2.0 * q.w * q.w) + (2.0 * q.y * q.y) - 1.0)
                        );
            }
            throw new InvalidEnumValueException<EulerRotationOrder>(inOrder);
        }

        public static Matrix4x4 EulerToMatrix(Vector3 inEuler, EulerRotationOrder inOrder)
        {
            double cx = Math.Cos(inEuler.x); double sx = Math.Sin(inEuler.x);
            double cy = Math.Cos(inEuler.y); double sy = Math.Sin(inEuler.y);
            double cz = Math.Cos(inEuler.z); double sz = Math.Sin(inEuler.z);

            switch (inOrder) {
                case EulerRotationOrder.XYZ:
                    return new Matrix4x4(EnumerableExtensions.AsEnumerable(
                                             (cy * cz),                  -(cy * sz),       sy,
                            (sx * sy * cz) + (cx * sz), -(sx * sy * sz) + (cx * cz), -sx * cy,
                            (cx * sy * cz) + (sx * sz),  (cx * sy * sz) + (sx * cz),  cx * cy
                        ));

                case EulerRotationOrder.XZY:
                    return new Matrix4x4(EnumerableExtensions.AsEnumerable(
                                             (cy * cz),       -sz,                  (sy * cz),
                            (cx * cy * sz) + (sx * sy), (cx * cz), (cx * sy * sz) - (sx * cy),
                            (sx * cy * sz) - (cx * sy), (sx * cz), (sx * sy * sz) + (cx * cy)
                        ));

                case EulerRotationOrder.YXZ:
                    return new Matrix4x4(EnumerableExtensions.AsEnumerable(
                            (sx * sy * sz) + (cy * cz), (sx * sy * cz) - (cy * sz), (cx * sy),
                                             (cx * sz),                  (cx * cz),       -sx,
                            (sx * cy * sz) - (sy * cz), (sx * cy * cz) + (sy * sz), (cx * cy)
                        ));

                case EulerRotationOrder.YZX:
                    return new Matrix4x4(EnumerableExtensions.AsEnumerable(
                             (cy * cz), -(cx * cy * sz) + (sx * sy),  (sx * cy * sz) + (cx * sy),
                                    sz,                   (cx * cz),                  -(sx * cz),
                            -(sy * cz),  (cx * sy * sz) + (sx * cy), -(sx * sy * sz) + (cx * cy)
                        ));

                case EulerRotationOrder.ZXY:
                    return new Matrix4x4(EnumerableExtensions.AsEnumerable(
                            -(sx * sy * sz) + (cy * cz), -(cx * sz),  (sx * cy * sz) + (sy * cz),
                             (sx * sy * cz) + (cy * sz),  (cx * cz), -(sx * cy * cz) + (sy * sz),
                                             -(cx * sy),         sx,                   (cx * cy)
                        ));

                case EulerRotationOrder.ZYX:
                    return new Matrix4x4(EnumerableExtensions.AsEnumerable(
                            (cy * cz), (sx * sy * cz) - (cx * sz), (cx * sy * cz) + (sx * sz),
                            (cy * sz), (sx * sy * sz) + (cx * sz), (cx * sy * sz) - (sx * cz),
                                  -sy,                  (sx * cy),                  (cx * cy)
                        ));
            }
            throw new InvalidEnumValueException<EulerRotationOrder>(inOrder);
        }

        public static Vector3 MatrixToEuler(Matrix4x4 inRotation, EulerRotationOrder inOrder)
        {
            const double n = 1.0 - 1.0e-6;
            double sx, sy, sz;
            bool unlocked;
            switch (inOrder) {
                case EulerRotationOrder.XYZ:
                    sy = inRotation[2, 0];
                    unlocked = Math.Abs(sy) < n;
                    return new Vector3(
                            unlocked ? Math.Atan2(-inRotation[2, 1], inRotation[2, 2]) : Math.Atan2(inRotation[1, 2], inRotation[1, 1]),
                            NumericExtensions.Asin(sy),
                            unlocked ? Math.Atan2(-inRotation[1, 0], inRotation[0, 0]) : 0.0
                        );

                case EulerRotationOrder.XZY:
                    sz = -inRotation[1, 0];
                    unlocked = Math.Abs(sz) < n;
                    return new Vector3(
                            unlocked ? Math.Atan2(inRotation[1, 2], inRotation[1, 1]) : Math.Atan2(inRotation[2, 1], inRotation[2, 2]),
                            unlocked ? Math.Atan2(inRotation[2, 0], inRotation[0, 0]) : 0.0,
                            NumericExtensions.Asin(sz)
                        );

                case EulerRotationOrder.YXZ:
                    sx = -inRotation[2, 1];
                    unlocked = Math.Abs(sx) < n;
                    return new Vector3(
                            NumericExtensions.Asin(sx),
                            unlocked ? Math.Atan2(inRotation[2, 0], inRotation[2, 2]) : Math.Atan2(-inRotation[0, 2], inRotation[0, 0]),
                            unlocked ? Math.Atan2(inRotation[0, 1], inRotation[1, 1]) : 0.0
                        );

                case EulerRotationOrder.YZX:
                    sz = inRotation[0, 1];
                    unlocked = Math.Abs(sz) < n;
                    return new Vector3(
                            unlocked ? Math.Atan2(-inRotation[2, 1], inRotation[1, 1]) : 0.0,
                            unlocked ? Math.Atan2(-inRotation[0, 2], inRotation[0, 0]) : Math.Atan2(inRotation[2, 0], inRotation[2, 2]),
                            NumericExtensions.Asin(sz)
                        );

                case EulerRotationOrder.ZXY:
                    sx = inRotation[1, 2];
                    unlocked = Math.Abs(sx) < n;
                    return new Vector3(
                            NumericExtensions.Asin(sx),
                            unlocked ? Math.Atan2(-inRotation[0, 2], inRotation[2, 2]) : 0.0,
                            unlocked ? Math.Atan2(-inRotation[1, 0], inRotation[1, 1]) : Math.Atan2(inRotation[0, 1], inRotation[0, 0])
                        );

                case EulerRotationOrder.ZYX:
                    sy = -inRotation[0, 2];
                    unlocked = Math.Abs(sy) < n;
                    return new Vector3(
                            unlocked ? Math.Atan2(inRotation[1, 2], inRotation[2, 2]) : 0.0,
                            NumericExtensions.Asin(sy),
                            unlocked ? Math.Atan2(inRotation[0, 1], inRotation[0, 0]) : Math.Atan2(-inRotation[1, 0], inRotation[1, 1])
                        );
            }
            throw new InvalidEnumValueException<EulerRotationOrder>(inOrder);
        }
    }
}
