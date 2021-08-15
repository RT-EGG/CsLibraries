using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer
{
    abstract class CameraModel : IGLCamera
    {
        public Transform Transform
        { get; } = new Transform();

        public virtual Matrix4x4 ProjectionMatrix
        { get; set; } = Matrix4x4.MakePerspective(45.0, 1.0, 0.01, 100.0);
        public virtual Matrix4x4 ViewMatrix => Transform.WorldMatrix.Inversed;
    }
}
