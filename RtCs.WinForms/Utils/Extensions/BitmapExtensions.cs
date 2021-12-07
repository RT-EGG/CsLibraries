using System.Drawing;
using System.Drawing.Imaging;

namespace RtCs.WinForms
{
    public static class BitmapExtensions
    {
        public static Rectangle GetFullRect(this Image inImage)
            => new Rectangle(new Point(0, 0), inImage.Size);

        public static Bitmap ExportAs32bppArgb(this Bitmap inBitmap)
        {
            if (inBitmap.PixelFormat == PixelFormat.Format32bppArgb) {
                return inBitmap.Clone(inBitmap.GetFullRect(), PixelFormat.Format32bppArgb);
            }

            var result = new Bitmap(inBitmap.Width, inBitmap.Height, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(result)) {
                graphics.PageUnit = GraphicsUnit.Pixel;
                graphics.DrawImageUnscaled(inBitmap, 0, 0);
            }
            return result;
        }

        
    }
}
