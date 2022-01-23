using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System;

namespace RtCs.OpenGL
{
    public interface IGLCamera
    {
        Matrix4x4 ProjectionMatrix { get; }
        Matrix4x4 ViewMatrix { get; }
    }

    public class GLCamera
    {
        public GLCamera()
        {
            Projection = new GLPerspectiveProjection();
            (Projection as GLPerspectiveProjection).SetAngleAndAspectRatio(45.0f, 1.0f);
            return;
        }

        public Transform Transform
        { get; } = new Transform();
        public GLProjection Projection
        { get; set; } = null;

        public Matrix4x4 ViewMatrix => Transform.WorldMatrix.Inversed;
        public Matrix4x4 ProjectionMatrix => (Projection == null) ? Matrix4x4.Identity : Projection.ProjectionMatrix;
        public GLViewFrustum ViewFrustum => new GLViewFrustum(ViewMatrix, ProjectionMatrix);
    }
}
