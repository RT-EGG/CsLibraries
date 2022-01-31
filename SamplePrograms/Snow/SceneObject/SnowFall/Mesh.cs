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

            Attribute = AddAttribute(new GLVertexAttributeDescriptor<VertexParticleAttribute>(AttributeName));
            Attribute.Buffer = new VertexParticleAttribute[2048];

            Random randomizer = new Random();
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

        public const string AttributeName = "ParticleAttribute";
    }
}
