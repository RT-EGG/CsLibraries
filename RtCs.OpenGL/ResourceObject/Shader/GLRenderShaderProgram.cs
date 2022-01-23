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
        public virtual void BindVertexAttributes(IGLVertexAttributeList inAttributes)
        { }
    }
}
