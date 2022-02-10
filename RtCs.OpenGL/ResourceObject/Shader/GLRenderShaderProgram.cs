using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    /// <summary>
    /// The shader program object especial for rendering.
    /// </summary>
    /// <remarks>
    /// Render shader program should attach only shader unit for rendering, VertexShader, FragmentShader, TessControl(Evaluation)Shader.
    /// </remarks>
    public partial class GLRenderShaderProgram : GLShaderProgram
    {
        public IEnumerable<GLVertexAttributePointer> VertexAttributePointers => m_VertexAttributePointers;

        protected void AddAttributePointer(GLVertexAttributePointer inPointer)
            => m_VertexAttributePointers.Add(inPointer);

        private List<GLVertexAttributePointer> m_VertexAttributePointers = new List<GLVertexAttributePointer>();
    }
}
