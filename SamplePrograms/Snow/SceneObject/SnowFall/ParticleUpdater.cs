using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.MathUtils.Geometry;
using RtCs.OpenGL;
using Snow.OpenGL;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Snow.SceneObject.SnowFall
{
    class ParticleUpdater : GLComputeShaderProgram
    {
        public ParticleUpdater()
        {
            AfterLinked += _ => {
                m_LocationNewGenerationEnabled = GetUniformVariableSocket("inNewGenerationEnabled").Location;
                m_LocationNewGenerationBoundsMin = GetUniformVariableSocket("inNewGenerationBoundsMin").Location;
                m_LocationNewGenerationBoundsMax = GetUniformVariableSocket("inNewGenerationBoundsMax").Location;
                m_LocationInitializeRadiusRange = GetUniformVariableSocket("inInitRadiusRange").Location;
                m_LocationInitializeDensityRange = GetUniformVariableSocket("inInitDensityRange").Location;
                m_LocationDeadAltitude = GetUniformVariableSocket("inDeadAltitude").Location;
                m_LocationPointCount = GetUniformVariableSocket("inPointCount").Location;
                m_LocationTimeSpan = GetUniformVariableSocket("inTimeSpan").Location;
                m_LocationRandomSeed = GetUniformVariableSocket("inRandomSeed").Location;

                m_LocationLandHeights = GetUniformVariableSocket("inLandHeights").Location;
                m_LocationLandBoundsMin = GetUniformVariableSocket("inLandBoundsMin").Location;
                m_LocationLandBoundsMax = GetUniformVariableSocket("inLandBoundsMax").Location;
                m_LocationLandHeightScale = GetUniformVariableSocket("inLandHeightScale").Location;
            };

            AfterCreateResource += (sender, args) => {
                m_Shader = new GLShader.GLComputeShader();

                new ShaderCompiler().CompileFromTextResourceFile(m_Shader, "Snow.Resources.SnowFall.compute.update.glsl.txt");

                AttachShader(m_Shader);
                Link();
            };
        }

        public void TimeStep(float inTimeSpan, Mesh inParticle, Snow.SceneObject.SnowCover.Model inSnowCover, float inRandomSeed)
        {
            var attribute = inParticle.GetAttribute<VertexParticleAttribute>(VertexParticleAttribute.AttributeName);

            GLMainThreadTask.CreateNew(_ => {
                GL.UseProgram(ID);

                if (TryGetShaderStorageBufferBindPoint("Particle_", out int bindPointParticle)) {
                    GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, inParticle.VertexBuffer.ID);
                    GL.ShaderStorageBlockBinding(ID, bindPointParticle, 0);
                }

                GL.ProgramUniform1(ID, m_LocationNewGenerationEnabled, Enabled ? 1 : 0);
                GL.ProgramUniform3(ID, m_LocationNewGenerationBoundsMin, 1, NewGenerationBounds.Min.ToArray());
                GL.ProgramUniform3(ID, m_LocationNewGenerationBoundsMax, 1, NewGenerationBounds.Max.ToArray());
                GL.ProgramUniform2(ID, m_LocationInitializeRadiusRange, InitializeRadiusRange.Min, InitializeRadiusRange.Max);
                GL.ProgramUniform2(ID, m_LocationInitializeDensityRange, InitializeDensityRange.Min, InitializeDensityRange.Max);
                GL.ProgramUniform1(ID, m_LocationDeadAltitude, DeadAltitude);
                GL.ProgramUniform1(ID, m_LocationPointCount, attribute.Buffer.Length);
                GL.ProgramUniform1(ID, m_LocationTimeSpan, inTimeSpan);
                GL.ProgramUniform1(ID, m_LocationRandomSeed, inRandomSeed);

                GL.BindImageTexture(0, inSnowCover.HeightMap.ID, 0, false, 0, TextureAccess.ReadWrite, SizedInternalFormat.Rgba32f);
                GL.ProgramUniform1(ID, m_LocationLandHeights, 0);
                AABB3D landBounds = inSnowCover.LandBounds;
                GL.ProgramUniform3(ID, m_LocationLandBoundsMin, 1, landBounds.Min.ToArray());
                GL.ProgramUniform3(ID, m_LocationLandBoundsMax, 1, landBounds.Max.ToArray());
                GL.ProgramUniform1(ID, m_LocationLandHeightScale, inSnowCover.HeightScale.Value);

                GL.DispatchCompute(256, 256, 1);

                byte[] buffer = new byte[inParticle.Attribute.Buffer.Length * VertexParticleAttribute.Size];
                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, inParticle.VertexBuffer.ID);
                GL.GetBufferSubData(BufferTarget.ShaderStorageBuffer, (IntPtr)0, buffer.Length, buffer);

                inParticle.Attribute.Buffer = MemoryMarshal.Cast<byte, VertexParticleAttribute>(new Span<byte>(buffer)).ToArray();
            });
        }

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);
            if (inDisposing) {
                m_Shader.Dispose();
            }
        }

        public bool Enabled
        { get; set; } = true;
        public AABB3D NewGenerationBounds
        { get; set; } = new AABB3D {
            Min = new Vector3(-2.5f, 2.0f, -2.5f),
            Max = new Vector3(2.5f, 3.0f, 2.5f)
        };
        public Range1D InitializeRadiusRange
        { get; set; } = new Range1D(0.005f, 0.01f);
        public Range1D InitializeDensityRange
        { get; set; } = new Range1D(0.01f, 0.05f);
        public float DeadAltitude
        { get; set; } = 0.0f;

        private GLShader.GLComputeShader m_Shader = null;
        private int m_LocationNewGenerationEnabled = 0;
        private int m_LocationNewGenerationBoundsMin = 0;
        private int m_LocationNewGenerationBoundsMax = 0;
        private int m_LocationInitializeRadiusRange = 0;
        private int m_LocationInitializeDensityRange = 0;
        private int m_LocationDeadAltitude = 0;
        private int m_LocationPointCount = 0;
        private int m_LocationTimeSpan = 0;
        private int m_LocationRandomSeed = 0;

        private int m_LocationLandHeights = 0;
        private int m_LocationLandBoundsMin = 0;
        private int m_LocationLandBoundsMax = 0;
        private int m_LocationLandHeightScale = 0;
    }
}
