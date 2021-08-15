using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public partial class GLRenderShaderProgram : GLShaderProgram
    {
        public IList<GLVertexAttributePointer> VertexAttribPointers
        { get; } = new List<GLVertexAttributePointer>();
    }
}
