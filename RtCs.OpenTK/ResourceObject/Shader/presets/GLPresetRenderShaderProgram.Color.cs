using RtCs.MathUtils;
using System.Collections.Generic;
using System.Diagnostics;

namespace RtCs.OpenGL
{
    public partial class GLRenderShaderProgram
    {
        /// <summary>
        /// Color shader object as one of preset shader program.
        /// </summary>
        /// <remarks>
        /// This shader renders plane color.
        /// </remarks>
        public class Color : GLRenderShaderProgram.PresetType
        {
            public Color()
            {
                VertexAttribPointers.Add(new GLVertexAttributePointer.Position(0));

                AfterCreateResource += (sender, args) => {
                    m_VertexShader = new GLShader.GLVertexShader();
                    m_FragmentShader = new GLShader.GLFragmentShader();

                    m_VertexShader.Compile(GLShaderTextSource.CreateAssemblyTextResourceSource($"RtCs.OpenGL.Resources.Color.vertex.glsl.txt"));
                    m_FragmentShader.Compile(GLShaderTextSource.CreateAssemblyTextResourceSource($"RtCs.OpenGL.Resources.Color.fragment.glsl.txt"));

                    AttachShader(m_VertexShader);
                    AttachShader(m_FragmentShader);
                    Debug.Assert(Link());
                    return;
                };
                return;
            }

            public override IEnumerable<GLShaderUniformProperty> CreateDefaultProperties()
            {
                foreach (var @base in base.CreateDefaultProperties()) {
                    yield return @base;
                }
                yield return new GLShaderUniformProperty.Vec4(GetPropertySocket("inColor")) { Value = new Vector4(1.0f, 1.0f, 1.0f, 1.0f) };
                yield return new GLShaderUniformProperty.Mat4(GetPropertySocket("inProjectionMatrix")) { Value = Matrix4x4.Identity };
                yield return new GLShaderUniformProperty.Mat4(GetPropertySocket("inModelviewMatrix")) { Value = Matrix4x4.Identity };
                yield break;
            }

            private GLShader.GLVertexShader m_VertexShader = null;
            private GLShader.GLFragmentShader m_FragmentShader = null;
        }
    }
}
