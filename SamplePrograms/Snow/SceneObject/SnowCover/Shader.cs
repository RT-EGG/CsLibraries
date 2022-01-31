using OpenTK.Graphics.OpenGL4;
using RtCs.OpenGL;
using Snow.OpenGL;

namespace Snow.SceneObject.SnowCover
{
    class Shader : GLRenderShaderProgram
    {
        public Shader()
        {
            AfterCreateResource += (sender, args) => {
                m_VertexShader = new GLShader.GLVertexShader();
                m_TessControlShader = new GLShader.GLTessControlShader();
                m_TessEvaluateShader = new GLShader.GLTessEvaluationShader();
                m_FragmentShader = new GLShader.GLFragmentShader();

                ShaderCompiler compiler = new ShaderCompiler();
                compiler.CompileFromTextResourceFile(m_VertexShader, "Snow.Resources.SnowCover.vertex.glsl.txt");
                compiler.CompileFromTextResourceFile(m_TessControlShader, "Snow.Resources.SnowCover.tess_control.glsl.txt");
                compiler.CompileFromTextResourceFile(m_TessEvaluateShader, "Snow.Resources.SnowCover.tess_evaluate.glsl.txt");
                compiler.CompileFromTextResourceFile(m_FragmentShader, "Snow.Resources.SnowCover.fragment.glsl.txt");

                AttachShader(m_VertexShader);
                AttachShader(m_TessControlShader);
                AttachShader(m_TessEvaluateShader);
                AttachShader(m_FragmentShader);

                Link();
            };
        }

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);
            if (inDisposing) {
                m_VertexShader.Dispose();
                m_TessControlShader.Dispose();
                m_TessEvaluateShader.Dispose();
                m_FragmentShader.Dispose();
            }
        }

        public override void BindVertexAttributes(IGLVertexAttributeList inAttributes)
        {
            base.BindVertexAttributes(inAttributes);

            if (inAttributes.TryGetBufferPointerOffset(GLVertexAttribute.AttributeName_Vertex, out int vertex)) {
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, vertex);
            }
            if (inAttributes.TryGetBufferPointerOffset(GLVertexAttribute.AttributeName_Normal, out int normal)) {
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, normal);
            }
            if (inAttributes.TryGetBufferPointerOffset(GLVertexAttribute.AttributeName_TexCoord, out int texcoord)) {
                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, sizeof(float) * 2, texcoord);
            }
        }

        private GLShader m_VertexShader = null;
        private GLShader m_TessControlShader = null;
        private GLShader m_TessEvaluateShader = null;
        private GLShader m_FragmentShader = null;
    }
}
