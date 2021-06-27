using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtCs.OpenGL
{
    public partial class GLRenderShaderProgram
    {
        public class Color : GLRenderShaderProgram.PresetType
        {
            public Color()
            {
                OnAfterCreateResource += _ => {
                    m_VertexShader = new GLShader.GLVertexShader();
                    m_FragmentShader = new GLShader.GLFragmentShader();

                    m_VertexShader.Compile(GLShaderTextSource.CreateTextSource(LoadAssemblyText("Color.vertex.glsl.txt")));
                    m_FragmentShader.Compile(GLShaderTextSource.CreateTextSource(LoadAssemblyText("Color.fragment.glsl.txt")));

                    AttachShader(m_VertexShader);
                    AttachShader(m_FragmentShader);
                    Debug.Assert(Link());
                    return;
                };
                return;
            }

            private GLShader.GLVertexShader m_VertexShader = null;
            private GLShader.GLFragmentShader m_FragmentShader = null;
        }
    }
}
