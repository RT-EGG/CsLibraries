using RtCs.MathUtils;
using System.Runtime.InteropServices;

namespace Snow.SceneObject.SnowFall
{
    [StructLayout(LayoutKind.Sequential, Pack =1)]
    struct VertexParticleAttribute
    {
        static VertexParticleAttribute()
        {
            Size = Marshal.SizeOf(typeof(VertexParticleAttribute));
        }

#pragma warning disable CS0649
        public int status;
        public float radius;
        public float density;
        public Vector3 position;
        public Vector3 velocity;
#pragma warning restore CS0649

        public const string AttributeName = "ParticleAttribute";
        public static readonly int Size = 0;
    }
}
