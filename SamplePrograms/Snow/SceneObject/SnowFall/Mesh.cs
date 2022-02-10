using RtCs.OpenGL;
using System;
using System.Linq;

namespace Snow.SceneObject.SnowFall
{
    class Mesh : GLMesh
    {
        public Mesh()
        {
            Vertices = null;

            Attribute = AddAttribute<VertexParticleAttribute>(
                VertexParticleAttribute.AttributeName,
                new GLVertexAttributeDescriptor(AttributeName_Status, 1, sizeof(int)),
                new GLVertexAttributeDescriptor(AttributeName_Radius, 1, sizeof(float)),
                new GLVertexAttributeDescriptor(AttributeName_Density, 1, sizeof(float)),
                new GLVertexAttributeDescriptor(AttributeName_Position, 3, sizeof(float)),
                new GLVertexAttributeDescriptor(AttributeName_Velocity, 3, sizeof(float))
            );
            Attribute.Buffer = new VertexParticleAttribute[2048];

            for (int i = 0; i < Attribute.Buffer.Length; ++i) {
                VertexParticleAttribute particle = default;
                particle.status = 0;

                Attribute.Buffer[i] = particle;
            }

            Topology = EGLMeshTopology.Points;
            Indices = Enumerable.Range(0, Attribute.Buffer.Length).ToArray();

            Apply();
        }

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);
            if (inDisposing) {
                m_ParticleUpdater.Dispose();
            }
        }

        public void TimeStep(float inTimeSpan, SnowCover.Model inSnowCover)
        {
            Random randomizer = new Random();
            m_ParticleUpdater.TimeStep(inTimeSpan, this, inSnowCover, (float)randomizer.NextDouble());
        }

        public IGLVertexAttribute<VertexParticleAttribute> Attribute
        { get; } = null;

        private ParticleUpdater m_ParticleUpdater = new ParticleUpdater();

        public const string AttributeName_Status   = "ParticleStatus";
        public const string AttributeName_Radius   = "ParticleRadius";
        public const string AttributeName_Density  = "ParticleDensity";
        public const string AttributeName_Position = "ParticlePosition";
        public const string AttributeName_Velocity = "ParticleVelocity";
    }
}
