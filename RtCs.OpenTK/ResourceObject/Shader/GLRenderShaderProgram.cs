using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtCs.OpenGL
{
    public partial class GLRenderShaderProgram : GLShaderProgram
    {
        public IList<GLVertexAttributePointer> VertexAttribPointers
        { get; } = new List<GLVertexAttributePointer>();
    }
}
