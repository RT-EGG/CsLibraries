using OpenTK.Graphics.OpenGL;

namespace RtCs.OpenGL
{
    /// <summary>
    /// The object represents region of render target.
    /// </summary>
    public class GLViewport : GLObject
    {
        public GLViewport()
            : this(0, 0, 1, 1)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inX">Initial value of X.</param>
        /// <param name="inY">Initial value of Y.</param>
        /// <param name="inWidth">Initial value of Width.</param>
        /// <param name="inHeight">Initial value of Height.</param>
        public GLViewport(int inX, int inY, int inWidth, int inHeight)
        {
            X = inX;
            Y = inY;
            Width = inWidth;
            Height = inHeight;
            return;
        }

        /// <summary>
        /// Call glViewport with properties.
        /// </summary>
        /// <remarks>
        /// Read more, see official [reference](https://www.khronos.org/registry/OpenGL-Refpages/gl4/html/glViewport.xhtml).
        /// </remarks>
        public void Adapt()
        {
            GL.Viewport(X, Y, Width, Height);
            return;
        }

        /// <summary>
        /// Left corner of the viewport.
        /// </summary>
        public int X
        { get; set; } = 0;
        /// <summary>
        /// Bottom corner of the viewport.
        /// </summary>
        public int Y
        { get; set; } = 0;
        /// <summary>
        /// Width of the viewport.
        /// </summary>
        public int Width
        { get; set; } = 1;
        /// <summary>
        /// Height of the viewport.
        /// </summary>
        public int Height
        { get; set; } = 1;
        /// <summary>
        /// Value of Width / Height.
        /// </summary>
        public float AspectRatio
            => Width / (float)Height;

        public int[] ToArray() => new int[4] {
            X, Y, Width, Height
        };
    }
}
