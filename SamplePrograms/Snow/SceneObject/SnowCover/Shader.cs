using RtCs.OpenGL;
using Snow.OpenGL;

namespace Snow.SceneObject.SnowCover
{
    class Shader : GLRenderShaderProgram
    {
        public Shader()
        {
            AddAttributePointer(new GLVertexAttributePointer.Float3(0, GLVertexAttribute.AttributeName_Vertex));
            AddAttributePointer(new GLVertexAttributePointer.Float3(1, GLVertexAttribute.AttributeName_Normal));
            AddAttributePointer(new GLVertexAttributePointer.Float2(2, GLVertexAttribute.AttributeName_TexCoord));

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

        private GLShader m_VertexShader = null;
        private GLShader m_TessControlShader = null;
        private GLShader m_TessEvaluateShader = null;
        private GLShader m_FragmentShader = null;
    }
}
