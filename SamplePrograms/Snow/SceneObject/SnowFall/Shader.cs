using OpenTK.Graphics.OpenGL4;
using RtCs.OpenGL;
using Snow.OpenGL;
using System;

namespace Snow.SceneObject.SnowFall
{
    class Shader : GLRenderShaderProgram
    {
        public Shader()
        {
            AfterCreateResource += (sender, args) => {
                m_VertexShader = new GLShader.GLVertexShader();
                m_FragmentShader = new GLShader.GLFragmentShader();

                ShaderCompiler compiler = new ShaderCompiler();
                compiler.CompileFromTextResourceFile(m_VertexShader, "Snow.Resources.SnowFall.vertex.glsl.txt");
                compiler.CompileFromTextResourceFile(m_FragmentShader, "Snow.Resources.SnowFall.fragment.glsl.txt");

                AttachShader(m_VertexShader);
                AttachShader(m_FragmentShader);

                Link();
            };
        }

        public override void BindVertexAttributes(IGLVertexAttributeList inAttributes)
        {
            base.BindVertexAttributes(inAttributes);

            if (inAttributes.TryGetBufferPointerOffset(Mesh.AttributeName, out int attribute)) {
                GL.EnableVertexAttribArray(0);
                GL.EnableVertexAttribArray(1);
                GL.EnableVertexAttribArray(2);

                GL.VertexAttribIPointer(0, 1, VertexAttribIntegerType.Int, VertexParticleAttribute.Size, (IntPtr)(attribute + 0));
                GL.VertexAttribPointer(1, 1, VertexAttribPointerType.Float, false, VertexParticleAttribute.Size, (IntPtr)(attribute + sizeof(int)));
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, VertexParticleAttribute.Size, (IntPtr)(attribute + sizeof(int) + sizeof(float) + sizeof(float)));
            }
        }

        private GLShader m_VertexShader = null;
        private GLShader m_FragmentShader = null;
    }
}
