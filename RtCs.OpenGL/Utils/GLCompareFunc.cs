using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    public enum EGLCompareFunc
    {
        LEqual = All.Lequal,
        GEqual = All.Gequal,
        Less = All.Less,
        Greater = All.Greater,
        Euqal = All.Equal,
        NotEqual = All.Notequal,
        Always = All.Always,
        Never = All.Never
    }
}
