using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer
{
    abstract class CameraModel : IGLCamera
    {
        public Transform Transform
        { get; } = new Transform();

        public virtual Matrix4x4 ProjectionMatrix
        { get; set; } = Matrix4x4.MakePerspective(45.0f, 1.0f, 0.01f, 100.0f);
        public virtual Matrix4x4 ViewMatrix => Transform.WorldMatrix.Inversed;
    }
}
