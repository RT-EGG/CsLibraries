using RtCs.MathUtils;
using System.Drawing;

namespace RtCs.WinForms
{
    public static class SizeExtensions
    {
        public static Vector2 ToVector(this Size inValue)
            => new Vector2(inValue.Width, inValue.Height);
    }
}
