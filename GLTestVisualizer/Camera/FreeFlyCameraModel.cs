using RtCs.MathUtils;

namespace GLTestVisualizer
{
    class FreeFlyCameraModel : CameraModel
    {
        public void LocalMove(Vector3 inTransfer)
            => Transform.LocalPosition += Transform.LocalRotation * inTransfer;

        public void TurnLeft(float inDegree)
            => SetLocalRotation(YawRad - inDegree.DegToRad(), PitchRad);
        public void TurnRight(float inDegree)
            => TurnLeft(-inDegree);
        public void TurnUp(float inDegree)
            => SetLocalRotation(YawRad, PitchRad - inDegree.DegToRad());
        public void TurnDown(float inDegree)
            => TurnUp(-inDegree);
        public Quaternion LocalRotation
        {
            get => Transform.LocalRotation;
            set => Transform.LocalRotation = value;
        }

        private void SetLocalRotation(float inYawRad, float inPitchRad)
        {
            YawRad = inYawRad;
            PitchRad = inPitchRad;

            LocalRotation = Quaternion.FromEuler(PitchRad, YawRad, 0.0f, EEulerRotationOrder.YXZ);
            return;
        }

        private float YawRad
        { get; set; } = 0.0f;
        private float PitchRad
        { get; set; } = 0.0f;
    }
}
