using OpenTK.Graphics.OpenGL4;
using RtCs.OpenGL;
using Snow.OpenGL;
using System;

namespace Snow.SceneObject.SnowCover
{
    class HeightMapRandomizer : GLComputeShaderProgram
    {
        public HeightMapRandomizer()
        {
            AfterLinked += _ => {
                m_LocationDstTexture = GetUniformVariableSocket("inDstTexture").Location;
                m_LocationRandomSeed = GetUniformVariableSocket("inRandomSeed").Location;
            };

            AfterCreateResource += (sender, args) => {
                m_Shader = new GLShader.GLComputeShader();

                new ShaderCompiler().CompileFromTextResourceFile(m_Shader, "Snow.Resources.SnowCover.compute.randomize.glsl.txt");

                AttachShader(m_Shader);
                Link();
            };
        }

        public void Execute(HeightMap inMap)
            => Execute(inMap, (float)(new Random().NextDouble()));

        public void Execute(HeightMap inMap, float inSeed)
        {
            GLMainThreadTask.CreateNew(_ => {
                GL.UseProgram(ID);

                GL.BindImageTexture(0, inMap.ID, 0, false, 0, TextureAccess.WriteOnly, SizedInternalFormat.Rgba32f);
                GL.ProgramUniform1(ID, m_LocationDstTexture, 0);
                GL.ProgramUniform1(ID, m_LocationRandomSeed, inSeed);

                GL.DispatchCompute(inMap.Width / WorkGroupSize[0], inMap.Height / WorkGroupSize[1], 1);
            });
        }

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);

            if (inDisposing) {
                m_Shader.Dispose();
                m_Shader = null;
            }
        }

        private GLShader.GLComputeShader m_Shader = null;
        private int m_LocationDstTexture = 0;
        private int m_LocationRandomSeed = 0;
    }
}
