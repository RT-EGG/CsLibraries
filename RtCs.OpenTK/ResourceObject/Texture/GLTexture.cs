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

    public abstract class GLTexture : GLResourceIdObject, IGLTexture
    {
        public GLTexture(PixelInternalFormat inPixelInternalFormat)
        {
            PixelInternalFormat = inPixelInternalFormat;
            return;
        }

        public virtual void Apply()
        { }

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
            new GLMainThreadTask(args => LoadPixels(Pixels)).Enqueue();
            return;
        }

        public ColorRGBA[] Pixels
        { get; set; } = new ColorRGBA[0];

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

        public void ResizeBuffer(int inWidth, int inHeight)
        {
            Width = inWidth;
            Height = inHeight;
            Pixels = null;
            return;
        }

        public int Width
        { get; private set; } = 0;
        public int Height
        { get; private set; } = 0;
        public Vector2 Size
            => new Vector2(Width, Height);

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
