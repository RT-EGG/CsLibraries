using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace RtCs.WinForms
{
    public class TextMeasure : IDisposable
    {
        public TextMeasure(Graphics inGraphics, Font inFont, Size inProposedSize, StringFormat inFormat)
        {
            OriginalGraphics = inGraphics;
            Font = inFont;
            Format = inFormat;

            Canvas = new Bitmap(inProposedSize.Width, inProposedSize.Height, inGraphics);
            CanvasRect = new Rectangle(0, 0, Canvas.Width, Canvas.Height);
            return;
        }

        public TextMeasure(Graphics inGraphics, Font inFont, StringFormat inFormat)
            : this (inGraphics, inFont, new Size(inFont.Height * 2, inFont.Height), inFormat)
        { }

        protected virtual void Dispose(bool inDisposing)
        {
            if (!m_Disposed) {
                if (inDisposing) {
                    Canvas.Dispose();
                }
                m_Disposed = true;
            }
            return;
        }

        public void Dispose()
        {
            Dispose(inDisposing: true);
            GC.SuppressFinalize(this);
            return;
        }

        public (int Offset, int Width) MeasureWidth(string inText)
        {
            using (Graphics graphics = Graphics.FromImage(Canvas)) {
                graphics.TextRenderingHint = OriginalGraphics.TextRenderingHint;
                graphics.TextContrast = OriginalGraphics.TextContrast;
                graphics.PixelOffsetMode = OriginalGraphics.PixelOffsetMode;

                graphics.Clear(BackgroundColor);
                graphics.DrawString(inText, Font, Brushes.White, CanvasRect, Format);
            }

            return MeasureWidth(Canvas, CanvasRect, BackgroundColor);
        }

        public (int Offset, int Width) MeasureWidth(char inCharacter)
            => MeasureWidth(new string(inCharacter, 1));

        public static (int Offset, int inWidth) MeasureWidth(Bitmap inCanvas, Rectangle inDataRange, Color inBackgroundColor)
        {
            inDataRange.Height = inCanvas.Height;

            BitmapData data = inCanvas.LockBits(inCanvas.GetFullRect(), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try {
                int offsetLeft = MeasureLeftOffsets(data.Scan0, inDataRange, inCanvas.Width, inBackgroundColor);
                int offsetRight = MeasureRightOffsets(data.Scan0, inDataRange, inCanvas.Width, inBackgroundColor);
                return (inDataRange.Left + offsetLeft, inDataRange.Right - offsetRight - (inDataRange.Left + offsetLeft));

            } finally {
                inCanvas.UnlockBits(data);
            }
        }

        private static int MeasureLeftOffsets(IntPtr inData, Rectangle inDataRange, int inCanvasWidth, Color inBackgroundColor)
        {            
            for (int x = inDataRange.Left; x < inDataRange.Right; ++x) {
                for (int y = inDataRange.Top; y < inDataRange.Bottom; ++y) {
                    if (GetColorAt(inData, inCanvasWidth, x, y) != inBackgroundColor) {
                        return x;
                    }
                }
            }
            throw CreateNotFoundForegroundException();
        }

        private static int MeasureRightOffsets(IntPtr inData, Rectangle inDataRange, int inCanvasWidth, Color inBackgroundColor)
        {
            for (int x = inDataRange.Right; x >= inDataRange.Left; --x) {
                for (int y = inDataRange.Top; y < inDataRange.Bottom; ++y) {
                    if (GetColorAt(inData, inCanvasWidth, x, y) != inBackgroundColor) {
                        return inDataRange.Right - x;
                    }
                }
            }
            throw CreateNotFoundForegroundException();
        }

        private static Exception CreateNotFoundForegroundException()
            => new ArgumentException("Not found foreground color indata.");

        private static Color GetColorAt(IntPtr inData, int inDataWidth, int inX, int inY)
        {
            const int ColorSize = 4;
            int offset = (inY * inDataWidth * ColorSize) + (inX * ColorSize);
            return Color.FromArgb(
                    Marshal.ReadByte(inData, offset + 3),
                    Marshal.ReadByte(inData, offset + 0),
                    Marshal.ReadByte(inData, offset + 1),
                    Marshal.ReadByte(inData, offset + 2)
                );
        }

        private int CanvasWidth => CanvasRect.Width;
        private int CanvasHeight => CanvasRect.Height;

        private readonly Graphics OriginalGraphics;
        private readonly Font Font;
        private readonly StringFormat Format;
        private readonly Bitmap Canvas;
        private readonly Rectangle CanvasRect;
        private readonly Color BackgroundColor = Color.Transparent;
        private bool m_Disposed;
    }
}
