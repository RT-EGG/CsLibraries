using System.Drawing;
using System.Drawing.Imaging;

namespace RtCs.WinForms
{
    public static class BitmapExtensions
    {
        public static Rectangle GetFullRect(this Image inImage)
            => new Rectangle(new Point(0, 0), inImage.Size);

        public static Bitmap ExportAs32bppArgb(this Bitmap inBmp)
        {
            if (inBmp.PixelFormat == PixelFormat.Format32bppArgb) {
                return inBmp.Clone(inBmp.GetFullRect(), PixelFormat.Format32bppArgb);
            }

            var result = new Bitmap(inBmp.Width, inBmp.Height, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(result)) {
                graphics.PageUnit = GraphicsUnit.Pixel;
                graphics.DrawImageUnscaled(inBmp, 0, 0);
            }
            return result;
        }
    }
}
