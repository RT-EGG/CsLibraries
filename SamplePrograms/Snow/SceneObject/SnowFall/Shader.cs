using RtCs.OpenGL;
using Snow.OpenGL;

namespace Snow.SceneObject.SnowFall
{
    class Shader : GLRenderShaderProgram
    {
        public Shader()
        {
            AddAttributePointer(new GLVertexAttributePointer.Int1(0, Mesh.AttributeName_Status));
            AddAttributePointer(new GLVertexAttributePointer.Float1(1, Mesh.AttributeName_Radius));
            AddAttributePointer(new GLVertexAttributePointer.Float3(2, Mesh.AttributeName_Position));

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

        private GLShader m_VertexShader = null;
        private GLShader m_FragmentShader = null;
    }
}
