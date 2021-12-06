using RtCs.MathUtils;

namespace GLTestVisualizer
{
    class FreeFlyCameraModel : CameraModel
    {
        public void LocalMove(Vector3 inTransfer)
            => Transform.LocalPosition += Transform.LocalRotation * inTransfer;

        public void TurnLeft(double inDegree)
            => SetLocalRotation(YawRad - inDegree.DegToRad(), PitchRad);
        public void TurnRight(double inDegree)
            => TurnLeft(-inDegree);
        public void TurnUp(double inDegree)
            => SetLocalRotation(YawRad, PitchRad - inDegree.DegToRad());
        public void TurnDown(double inDegree)
            => TurnUp(-inDegree);
        public Quaternion LocalRotation
        {
            get => Transform.LocalRotation;
            set => Transform.LocalRotation = value;
        }

        private void SetLocalRotation(double inYawRad, double inPitchRad)
        {
            YawRad = inYawRad;
            PitchRad = inPitchRad;

            LocalRotation = Quaternion.FromEuler(PitchRad, YawRad, 0.0, EEulerRotationOrder.YXZ);
            return;
        }

        private double YawRad
        { get; set; } = 0.0;
        private double PitchRad
        { get; set; } = 0.0;
    }
}
