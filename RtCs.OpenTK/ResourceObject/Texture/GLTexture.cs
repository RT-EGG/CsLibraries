using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;

namespace RtCs.OpenGL
{
    public interface IGLTexture
    {
        int ID { get; }
        PixelInternalFormat PixelInternalFormat { get; }
    }

    public interface IGLColorTexture : IGLTexture
    {
        ColorRGBA[] Pixels { get; }
    }

    public interface IGLTexture2D : IGLTexture
    {
        int Width { get; }
        int Height { get; }
        Vector2 Size { get; }
    }

    public interface IGLColorTexture2D : IGLColorTexture, IGLTexture2D
    { }

    /// <summary>
    /// OpenGL texture object.
    /// </summary>
    public abstract class GLTexture : GLResourceIdObject, IGLTexture
    {
        public GLTexture(PixelInternalFormat inPixelInternalFormat)
        {
            PixelInternalFormat = inPixelInternalFormat;
            return;
        }

        /// <summary>
        /// Apply all changes.
        /// </summary>
        /// <remarks>
        /// Even if make changes by any method, the changes will not be applied internally until call this method.
        /// </remarks>
        public virtual void Apply()
        { }

        /// <summary>
        /// The pixel store format in graphics memory.
        /// </summary>
        public PixelInternalFormat PixelInternalFormat
        { get; }

        protected override void CreateResourceCore()
        {
            base.CreateResourceCore();
            ID = GL.GenTexture();
            return;
        }

        protected override void DestroyResourceCore()
        {
            base.DestroyResourceCore();
            GL.DeleteTexture(ID);
            ID = 0;
            return;
        }
    }

    public abstract class GLColorTexture : GLTexture, IGLColorTexture
    {
        public GLColorTexture()
            : base(PixelInternalFormat.Rgba)
        { }

        public override void Apply()
        {
            base.Apply();
            GLMainThreadTask.CreateNew(_ => LoadPixels(Pixels));
            return;
        }

        /// <summary>
        /// Get or set each pixels for texture object.
        /// </summary>
        public ColorRGBA[] Pixels
        { get; set; } = new ColorRGBA[0];

        /// <summary>
        /// Get size of pixels that must be set.
        /// </summary>
        public abstract int PixelSize { get; }

        protected abstract void LoadPixels(ColorRGBA[] inPixels);
    }

    public class GLColorTexture2D : GLColorTexture, IGLColorTexture2D
    {
        public GLColorTexture2D(int inWidth, int inHeight)
            : base()
        {
            Width = inWidth;
            Height = inHeight;
            return;
        }

        /// <summary>
        /// Reset texture size.
        /// </summary>
        /// <param name="inWidth">New texture width.</param>
        /// <param name="inHeight">New texture height.</param>
        /// <remarks>
        /// This method reset Pixels to null so you must reasign value and call Apply().
        /// </remarks>
        public void ResizeBuffer(int inWidth, int inHeight)
        {
            Width = inWidth;
            Height = inHeight;
            Pixels = null;
            return;
        }

        /// <summary>
        /// Get width of texture.
        /// </summary>
        /// <remarks>
        /// If you want to change width, call ResizeBuffer().
        /// </remarks>
        public int Width
        { get; private set; } = 0;
        /// <summary>
        /// Get height of texture.
        /// </summary>
        /// <remarks>
        /// If you want to change height, call ResizeBuffer().
        /// </remarks>
        public int Height
        { get; private set; } = 0;

        /// <summary>
        /// Get size as Vector2(Width, Height).
        /// </summary>
        public Vector2 Size
            => new Vector2(Width, Height);

        public override int PixelSize => Width * Height;

        protected override void LoadPixels(ColorRGBA[] inPixels)
        {
            byte[] bytes = new byte[inPixels.Length * 4];
            for (int i = 0; i < inPixels.Length; ++i) {
                bytes[(i * 4) + 0] = inPixels[i].R;
                bytes[(i * 4) + 1] = inPixels[i].G;
                bytes[(i * 4) + 2] = inPixels[i].B;
                bytes[(i * 4) + 3] = inPixels[i].A;
            }

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, ID);
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, bytes);

            GL.GetTextureImage(ID, 0, PixelFormat.Rgba, PixelType.UnsignedByte, bytes.Length, bytes);
            return;
        }
    }
}
