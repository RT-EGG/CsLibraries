using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtCs.OpenGL
{
    public abstract class GLShaderSource
    {
        internal abstract void LoadSource(int inShaderID);
    }
}
