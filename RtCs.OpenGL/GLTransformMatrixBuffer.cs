using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RtCs.OpenGL
{
    public class GLTransformMatrixBuffer : GLObject
    {
        internal void SetBuffers(GLCamera inCamera, IReadOnlyList<GLRenderObject> inObjects)
        {
            byte[] viewportBuffer = new byte [sizeof(int) * 2];
            MemoryMarshal.Cast<int, byte>((new int[] { inCamera.RenderTarget.Width, inCamera.RenderTarget.Height }).AsSpan())
                .TryCopyTo(new Span<byte>(viewportBuffer));
            ViewportBuffer.AllocateBuffer(viewportBuffer.Length, viewportBuffer, BufferUsageHint.DynamicDraw);

            Matrix4x4 viewMatrix = inCamera.ViewMatrix;
            Matrix4x4 projectionMatrix = (inCamera.Projection == null) ? Matrix4x4.Identity : inCamera.Projection.ProjectionMatrix;
            Matrix4x4 viewProjectionMatrix = projectionMatrix * viewMatrix;
            Vector4 viewDirection = viewMatrix.Inversed * (new Vector4(0.0f, 0.0f, -1.0f, 0.0f));

            float[] viewProjectionMatrixBuffer = new float[(4 * 1) + (16 * 3)];
            int index = 0;
            viewDirection.CopyToArray(viewProjectionMatrixBuffer, ref index);
            viewMatrix.CopyToGLArray(viewProjectionMatrixBuffer, ref index);
            projectionMatrix.CopyToGLArray(viewProjectionMatrixBuffer, ref index);
            viewProjectionMatrix.CopyToGLArray(viewProjectionMatrixBuffer, ref index);

            ViewProjectionMatrixBuffer.AllocateBuffer(sizeof(float) * viewProjectionMatrixBuffer.Length, viewProjectionMatrixBuffer, BufferUsageHint.DynamicDraw);

            int count44 = BufferSize44 * inObjects.Count;
            int count33 = BufferSize33 * inObjects.Count;
            float[] modelMatrices = new float[count44];
            float[] modelViewMatrices = new float[count44];
            float[] modelViewProjectionMatrices = new float[count44];
            float[] normalMatrices = new float[count33];
            int index44 = 0;
            int index33 = 0;

            for (int i = 0; i < inObjects.Count; ++i) {
                var renderObject = inObjects[i];
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

                renderObject.RenderInstanceID = i;

                index44 += 16;
                index33 += 9;
            }

            ModelMatrixBuffer.AllocateBuffer(sizeof(float) * count44, modelMatrices, UsageHint);
            ModelViewMatrixBuffer.AllocateBuffer(sizeof(float) * count44, modelViewMatrices, UsageHint);
            ModelViewProjectionMatrixBuffer.AllocateBuffer(sizeof(float) * count44, modelViewProjectionMatrices, UsageHint);
            NormalMatrixBuffer.AllocateBuffer(sizeof(float) * count33, normalMatrices, UsageHint);
            return;
        }

        internal GLShaderStorageBufferObject ViewportBuffer
        { get; } = new GLShaderStorageBufferObject();
        internal GLShaderStorageBufferObject ViewProjectionMatrixBuffer
        { get; } = new GLShaderStorageBufferObject();
        internal GLShaderStorageBufferObject ModelMatrixBuffer
        { get; } = new GLShaderStorageBufferObject();
        internal GLShaderStorageBufferObject ModelViewMatrixBuffer
        { get; } = new GLShaderStorageBufferObject();
        internal GLShaderStorageBufferObject ModelViewProjectionMatrixBuffer
        { get; } = new GLShaderStorageBufferObject();
        internal GLShaderStorageBufferObject NormalMatrixBuffer
        { get; } = new GLShaderStorageBufferObject();

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);

            if (inDisposing) {
                ViewportBuffer.Dispose();
                ViewProjectionMatrixBuffer.Dispose();
                ModelMatrixBuffer.Dispose();
                ModelViewMatrixBuffer.Dispose();
                ModelViewProjectionMatrixBuffer.Dispose();
                NormalMatrixBuffer.Dispose();
            }
            return;
        }

        private const BufferUsageHint UsageHint = BufferUsageHint.DynamicDraw;
        private const int BufferSize44 = 16 * sizeof(float);
        private const int BufferSize33 = 9 * sizeof(float);
    }
}
