using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public class GLLightEnvironment : GLObject
    {
        public GLLight AmbientLight
        { get; } = new GLLight();

        public List<GLDirectionalLight> DirectionalLights
        { get; } = new List<GLDirectionalLight>();

        internal void UpdateBuffers()
        {
            byte[] buffer = AmbientLight.GenerateBuffer();
            AmbientLightBuffer.AllocateBuffer(buffer.Length, buffer, BufferUsageHint.DynamicDraw);

            buffer = DirectionalLights.GenerateBuffer();
            DirectionalLightBuffer.AllocateBuffer(buffer.Length, buffer, BufferUsageHint.DynamicDraw);

            return;
        }

        internal GLShaderStorageBufferObject AmbientLightBuffer
        { get; } = new GLShaderStorageBufferObject();
        internal GLShaderStorageBufferObject DirectionalLightBuffer
        { get; } = new GLShaderStorageBufferObject();

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);

            if (inDisposing) {
                AmbientLightBuffer.Dispose();
                DirectionalLightBuffer.Dispose();
            }
            return;
        }
    }
}
