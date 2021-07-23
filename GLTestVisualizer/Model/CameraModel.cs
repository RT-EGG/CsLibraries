using RtCs.MathUtils;

namespace GLTestVisualizer
{
    interface ICameraModel
    {
        Matrix4x4 ViewMatrix { get; }
    }

    abstract class CameraModel : ICameraModel
    {
        public Transform Transform
        { get; } = new Transform();

        public virtual Matrix4x4 ViewMatrix => Transform.WorldMatrix.Inversed;
    }
}
