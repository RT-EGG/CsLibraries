using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public class GLTransformMatrixBuffer
    {
        public void SetBuffers(GLCamera inCamera, ICollection<GLRenderObject> inObjects)
        {
            Matrix4x4 viewMatrix = inCamera.ViewMatrix;
            Matrix4x4 projectionMatrix = (inCamera.Projection == null) ? Matrix4x4.Identity : inCamera.Projection.ProjectionMatrix;
            Matrix4x4 viewProjectionMatrix = projectionMatrix * viewMatrix;

            ViewMatrixBuffer.AllocateBuffer(BufferSize44, viewMatrix.ToGLFloatArray(), UsageHint);
            ProjectionMatrixBuffer.AllocateBuffer(BufferSize44, projectionMatrix.ToGLFloatArray(), UsageHint);
            ViewProjectionMatrixBuffer.AllocateBuffer(BufferSize44, viewProjectionMatrix.ToGLFloatArray(), UsageHint);

            int count44 = BufferSize44 * inObjects.Count;
            int count33 = BufferSize33 * inObjects.Count;
            float[] modelMatrices = new float[count44];
            float[] modelViewMatrices = new float[count44];
            float[] modelViewProjectionMatrices = new float[count44];
            float[] normalMatrices = new float[count33];
            int index44 = 0;
            int index33 = 0;

            foreach (var renderObject in inObjects) {
                Matrix4x4 modelMatrix = renderObject.Transform.WorldMatrix;
                Matrix4x4 modelViewMatrix = viewMatrix * modelMatrix;
                Matrix4x4 modelViewProjectionMatrix = projectionMatrix * modelViewMatrix;
                Matrix3x3 normalMatrix = modelViewMatrix.Extract3x3(0, 0);
                if (normalMatrix.Inverse()) {
                    normalMatrix = normalMatrix.Transposed;
                } else {
                    normalMatrix = Matrix3x3.Identity;
                }

                modelMatrix.CopyToGLArray(modelMatrices, index44);
                modelViewMatrix.CopyToGLArray(modelViewMatrices, index44);
                modelViewProjectionMatrix.CopyToGLArray(modelViewProjectionMatrices, index44);
                normalMatrix.CopyToGLArray(normalMatrices, index33);

                index44 += 16;
                index33 += 9;
            }

            ModelMatrixBuffer.AllocateBuffer(sizeof(float) * count44, modelMatrices, UsageHint);
            ModelViewMatrixBuffer.AllocateBuffer(sizeof(float) * count44, modelViewMatrices, UsageHint);
            ModelViewProjectionMatrixBuffer.AllocateBuffer(sizeof(float) * count44, modelViewProjectionMatrices, UsageHint);
            NormalMatrixBuffer.AllocateBuffer(sizeof(float) * count33, normalMatrices, UsageHint);
            return;
        }

        public GLBufferObject ModelMatrixBuffer
        { get; private set; } = new GLBufferObject();
        public GLBufferObject ViewMatrixBuffer
        { get; private set; } = new GLBufferObject();
        public GLBufferObject ModelViewMatrixBuffer
        { get; private set; } = new GLBufferObject();
        public GLBufferObject ProjectionMatrixBuffer
        { get; private set; } = new GLBufferObject();
        public GLBufferObject ViewProjectionMatrixBuffer
        { get; private set; } = new GLBufferObject();
        public GLBufferObject ModelViewProjectionMatrixBuffer
        { get; private set; } = new GLBufferObject();
        public GLBufferObject NormalMatrixBuffer
        { get; private set; } = new GLBufferObject();

        private const BufferUsageHint UsageHint = BufferUsageHint.DynamicDraw;
        private const int BufferSize44 = 16 * sizeof(float);
        private const int BufferSize33 = 9 * sizeof(float);
    }
}
