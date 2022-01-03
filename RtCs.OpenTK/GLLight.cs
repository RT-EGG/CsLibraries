using RtCs.MathUtils;
using RtCs.OpenGL.Utils;
using System;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public class GLLight : GLObject
    {
        static GLLight()
        {
            ConstantBufferSize.Add(typeof(GLLight), (sizeof(float) * (3 + 1)));
            ConstantBufferSize.Add(typeof(GLDirectionalLight), ConstantBufferSize[typeof(GLLight)] + (sizeof(float) * 3));
            ConstantBufferSize.Add(typeof(GLPointLight), ConstantBufferSize[typeof(GLLight)] + (sizeof(float) * 1));
            ConstantBufferSize.Add(typeof(GLSpotLight), ConstantBufferSize[typeof(GLLight)] + (sizeof(float) * 1));
            return;
        }
            

        public ColorRGB Color
        { get; set; } = new ColorRGB(255, 255, 255);
        public float Intensity
        { get; set; } = 1.0f;

        internal void WriteToBuffer(byte[] inDst, ref int inIndex)
            => WriteToBufferCore(inDst, ref inIndex);

        protected virtual void WriteToBufferCore(byte[] inDst, ref int inIndex)
        {
            Color.CopyToArray(inDst, ref inIndex);
            ByteConverter.WriteTo(Intensity, inDst, ref inIndex);
            return;
        }

        internal static int GetConstantBufferSize(Type inType)
            => ConstantBufferSize[inType];
        private static Dictionary<Type, int> ConstantBufferSize = new Dictionary<Type, int>();
    }

    public class GLDirectionalLight : GLLight
    {
        public Vector3 Direction
        { get; set; } = new Vector3(0.0f, 0.0f, -1.0f);

        protected override void WriteToBufferCore(byte[] inDst, ref int inIndex)
        {
            base.WriteToBufferCore(inDst, ref inIndex);
            Direction.CopyToArray(inDst, ref inIndex);
            return;
        }
    }

    public abstract class GLLocationLight : GLLight
    {
        public Transform Transform
        { get; } = new Transform();
    }

    public class GLPointLight : GLLocationLight
    {
        public float Range
        { get; set; } = 1.0f;

        protected override void WriteToBufferCore(byte[] inDst, ref int inIndex)
        {
            base.WriteToBufferCore(inDst, ref inIndex);
            ByteConverter.WriteTo(Range, inDst, ref inIndex);
            return;
        }
    }

    public class GLSpotLight : GLLocationLight
    {
        public float Range
        { get; set; } = 1.0f;

        protected override void WriteToBufferCore(byte[] inDst, ref int inIndex)
        {
            base.WriteToBufferCore(inDst, ref inIndex);
            ByteConverter.WriteTo(Range, inDst, ref inIndex);
            return;
        }
    }

    public static class GLLightBufferGenerator
    {
        public static byte[] GenerateBuffer<T>(this T inLight) where T : GLLight
        {
            byte[] buffer = new byte[GLLight.GetConstantBufferSize(typeof(T))];
            int index = 0;
            inLight.WriteToBuffer(buffer, ref index);

            return buffer;
        }

        public static byte[] GenerateBuffer<T>(this ICollection<T> inLights) where T : GLLight
        {
            byte[] buffer = new byte[sizeof(int) + GLLight.GetConstantBufferSize(typeof(T)) * inLights.Count];
            int index = 0;
            ByteConverter.WriteTo(inLights.Count, buffer, ref index);
            foreach (var light in inLights) {
                light.WriteToBuffer(buffer, ref index);
            }

            return buffer;
        }
    }
}
