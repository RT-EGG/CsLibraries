using System.Diagnostics;

namespace RtCs.OpenGL
{
    public partial class GLRenderShaderProgram
    {
        /// <summary>
        /// VertexColor shader object as one of preset shader program.
        /// </summary>
        /// <remarks>
        /// This shader renders using mesh's vertex color. The fragment color will be deciced from interpolated between vertices.
        /// </remarks>
        public class VertexColor : GLRenderShaderProgram.PresetType
        {
            public VertexColor()
            {
                AddAttributePointer(new GLVertexAttributePointer.Float3(0, GLVertexAttribute.AttributeName_Vertex));
                AddAttributePointer(new GLVertexAttributePointer.Float4(1, GLVertexAttribute.AttributeName_Color));

                AfterCreateResource += (sender, args) => {
                    m_VertexShader = new GLShader.GLVertexShader();
                    m_FragmentShader = new GLShader.GLFragmentShader();

                    GLShaderTextCompiler compiler = new GLShaderTextCompiler();
                    compiler.CompileFromTextResourceFile(m_VertexShader, "RtCs.OpenGL.Resources.VertexColor.vertex.glsl.txt");
                    compiler.CompileFromTextResourceFile(m_FragmentShader, "RtCs.OpenGL.Resources.VertexColor.fragment.glsl.txt");

                    AttachShader(m_VertexShader);
                    AttachShader(m_FragmentShader);
                    Link();
                    Debug.Assert(Linked);
                    return;
                };

                //AddAttributePointer(new GLVertexAttributePointer(GLVertexAttribute.AttributeName_Vertex, 0));
                //AddAttributePointer(new GLVertexAttributePointer(GLVertexAttribute.AttributeName_Color, 1));
                return;
            }

            private GLShader.GLVertexShader m_VertexShader = null;
            private GLShader.GLFragmentShader m_FragmentShader = null;
        }
    }
}
