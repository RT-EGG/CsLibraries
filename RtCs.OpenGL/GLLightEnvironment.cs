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

        public List<GLPointLight> PointLights
        { get; } = new List<GLPointLight>();

        public List<GLSpotLight> SpotLights
        { get; } = new List<GLSpotLight>();

        internal void UpdateBuffers()
        {
            byte[] buffer = AmbientLight.GenerateBuffer();
            AmbientLightBuffer.AllocateBuffer(buffer.Length, buffer, BufferUsageHint.DynamicDraw);

            buffer = DirectionalLights.GenerateBuffer();
            DirectionalLightBuffer.AllocateBuffer(buffer.Length, buffer, BufferUsageHint.DynamicDraw);

            buffer = PointLights.GenerateBuffer();
            PointLightBuffer.AllocateBuffer(buffer.Length, buffer, BufferUsageHint.DynamicDraw);

            buffer = SpotLights.GenerateBuffer();
            SpotLightBuffer.AllocateBuffer(buffer.Length, buffer, BufferUsageHint.DynamicDraw);

            return;
        }

        internal GLShaderStorageBufferObject AmbientLightBuffer
        { get; } = new GLShaderStorageBufferObject();
        internal GLShaderStorageBufferObject DirectionalLightBuffer
        { get; } = new GLShaderStorageBufferObject();
        internal GLShaderStorageBufferObject PointLightBuffer
        { get; } = new GLShaderStorageBufferObject();
        internal GLShaderStorageBufferObject SpotLightBuffer
        { get; } = new GLShaderStorageBufferObject();

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);

            if (inDisposing) {
                AmbientLightBuffer.Dispose();
                DirectionalLightBuffer.Dispose();
                PointLightBuffer.Dispose();
            }
            return;
        }
    }
}
