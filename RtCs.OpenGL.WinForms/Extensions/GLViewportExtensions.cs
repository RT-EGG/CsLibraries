using System.Drawing;

namespace RtCs.OpenGL
{
    public static class GLViewportExtensions
    {
        public static Rectangle GetRect(this GLViewport inViewport)
            => new Rectangle(inViewport.X, inViewport.Y, inViewport.Width, inViewport.Height);
        public static void SetRect(this GLViewport inViewport, Rectangle inValue)
        {
            inViewport.X = inValue.X;
            inViewport.Y = inValue.Y;
            inViewport.Width = inValue.Width;
            inViewport.Height = inValue.Height;
            return;
        }
    }
}
