using RtCs.MathUtils;
using System;

namespace RtCs.OpenGL
{
    public interface IGLCamera
    {
        Matrix4x4 ProjectionMatrix { get; }
        Matrix4x4 ViewMatrix { get; }
    }

    public static class GLCameraExtensions
    {
        public static void Render(this IGLCamera inCamera, GLRenderParameter inParameter, GLScene inScene)
            => inCamera.Render(inParameter, s => inScene.Render(s));

        public static void Render(this IGLCamera inCamera, GLRenderParameter inStatus, Action<GLRenderParameter> inRender)
        {
            inStatus.ProjectionMatrix.PushMatrix();
            try {
                inStatus.ProjectionMatrix.LoadMatrix(inCamera.ProjectionMatrix);

                inStatus.ModelViewMatrix.View.PushMatrix();
                try {
                    inStatus.ModelViewMatrix.View.LoadMatrix(inCamera.ViewMatrix);

                    inRender(inStatus);

                } finally {
                    inStatus.ModelViewMatrix.View.PopMatrix();
                }
            } finally {
                inStatus.ProjectionMatrix.PopMatrix();
            }
        }
    }
}
