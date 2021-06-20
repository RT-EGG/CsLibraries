using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace RtCs.OpenGL
{
    public class GLViewport
    {
        public GLViewport()
            : this(0, 0, 1, 1)
        { }

        public GLViewport(int inX, int inY, int inWidth, int inHeight)
        {
            X = inX;
            Y = inY;
            Width = inWidth;
            Height = inHeight;
            return;
        }

        public GLViewport(Rectangle inRect)
        {
            Rect = inRect;
            return;
        }

        public void Adapt()
        {
            OpenTK.Graphics.OpenGL.GL.Viewport(X, Y, Width, Height);
            return;
        }

        public int X
        { get; set; } = 0;
        public int Y
        { get; set; } = 0;
        public int Width
        { get; set; } = 1;
        public int Height
        { get; set; } = 1;
        public double AspectRatio
            => Width / (double)Height;
        public Rectangle Rect
        {
            get => new Rectangle(X, Y, Width, Height);
            set {
                X = value.X;
                Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }

        public int[] ToArray() => new int[4] {
            X, Y, Width, Height
        };
    }
}
