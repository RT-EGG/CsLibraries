using RtCs.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace RtCs.OpenGL.WinForms.Texture.Text
{
    public interface ICharacterImageAtlas
    {
        IGLTexture2D Texture { get; }
        IReadOnlyDictionary<char, RectangleF> AssignedRectangles { get; }
        IReadOnlyDictionary<char, CharacterMetrics> CharacterMetrics { get; }
    }

    public class CharacterImageAtlas : ICharacterImageAtlas, IDisposable
    {
        public CharacterImageAtlas(Font inFont, int inMargin, int inTextureWidth, int inTextureHeight)
        {
            Font = inFont;
            Margin = inMargin;
            m_Texture = new GLColorTexture2D(inTextureWidth, inTextureHeight);
            m_AtlasImage = new Bitmap(inTextureWidth, inTextureHeight, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(m_AtlasImage)) {
                graphics.Clear(Color.Transparent);
            }
            return;
        }

        public int AddCharacter(string inCharacters)
        {
            Color background = Color.FromArgb(0, 0, 0, 0);

            using (Graphics atlasCanvas = Graphics.FromImage(m_AtlasImage)) {
                for (int index = 0; index < inCharacters.Length; ++index) {
                    if (m_AssignedRectangle.ContainsKey(inCharacters[index])) {
                        continue;
                    }

                    string character = new string(new char[] { inCharacters[index] });

                    // the region returns windows api.
                    // has character feed width.
                    Rectangle renderRegion = default;
                    // the region that TextMeasure should search bounds.
                    // should be bigger than renderRegion, because renderRegion may fail to contain all pixles completely.
                    Rectangle measureRegion = default;
                    // the region the minimum bounds of pixels of a character on bitmap.
                    Rectangle pixelRegion = default;

                    int feedWidth = 0;
                    using (Graphics tmpCanvas = Graphics.FromImage(m_TemporaryCanvas)) {
                        tmpCanvas.Clear(background);

                        TextFormatFlags textFormatFlags = TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix;
                        // get rough size
                        SizeF renderSize = TextRenderer.MeasureText(tmpCanvas, character, Font, m_TemporaryCanvas.Size, textFormatFlags);
                        Point renderPoint = new Point((int)renderSize.Width, 0);

                        // offset as left padding
                        renderRegion = new Rectangle((int)renderSize.Width, 0, (int)renderSize.Width, (int)renderSize.Height);
                        // add padding to width
                        measureRegion = new Rectangle(0, 0, (int)renderSize.Width * 3, (int)renderSize.Height);

                        feedWidth = (int)renderSize.Width;

                        // render to measure strict size
                        TextRenderer.DrawText(tmpCanvas, character, Font, renderRegion.Location, Color.White, textFormatFlags);
                    }

                    try {
                        (int offset, int width) = TextMeasure.MeasureWidth(m_TemporaryCanvas, measureRegion, background);
                        pixelRegion = new Rectangle(offset, 0, width, renderRegion.Height);

                    } catch (ArgumentException) {
                        // the case of no pixels rendered, space and so on.
                        pixelRegion = renderRegion;
                    }

                    if ((m_NextDrawPoint.X + (Margin * 2) + pixelRegion.Width) > m_AtlasImage.Width) {
                        m_NextDrawPoint.X = 0;
                        m_NextDrawPoint.Y += m_CurrentLineMaxHeight + (Margin * 2);

                        if ((m_NextDrawPoint.X + (Margin * 2) + pixelRegion.Width) > m_AtlasImage.Width) {
                            return index;
                        }
                    }
                    if ((m_NextDrawPoint.Y + (Margin * 2) + pixelRegion.Height) > m_AtlasImage.Height) {
                        return index;
                    }

                    m_CurrentLineMaxHeight = Math.Max(m_CurrentLineMaxHeight, pixelRegion.Height);

                    var point = new Point(
                        (int)Math.Round(m_NextDrawPoint.X),
                        (int)Math.Round(m_NextDrawPoint.Y)
                    );
                    point.X += Margin;
                    point.Y += Margin;

                    atlasCanvas.DrawImage(m_TemporaryCanvas, point.X, point.Y, pixelRegion, GraphicsUnit.Pixel);
                    m_AssignedRectangle.Add(inCharacters[index], AssignRectangle(point, pixelRegion.Size));
                    m_CharacterMetrics.Add(inCharacters[index], new CharacterMetrics(
                            pixelRegion.X - (renderRegion.Left - measureRegion.Left), pixelRegion.Width, feedWidth
                        ));

                    m_NextDrawPoint.X += pixelRegion.Width + Margin;
                }
            }

            (new GLTextureImageImporter()).ImportFromImage(m_AtlasImage, m_Texture);
            return inCharacters.Length;
        }

        private RectangleF AssignRectangle(PointF inPoint, SizeF inSize)
            => new RectangleF(
                    (float)inPoint.X / (float)m_AtlasImage.Width,
                    (float)inPoint.Y / (float)m_AtlasImage.Height,
                    (float)inSize.Width / (float)m_AtlasImage.Width,
                    (float)inSize.Height / (float)m_AtlasImage.Height
                );

        public IGLTexture2D Texture
            => m_Texture;
        public IReadOnlyDictionary<char, RectangleF> AssignedRectangles
            => m_AssignedRectangle;
        public IReadOnlyDictionary<char, CharacterMetrics> CharacterMetrics
            => m_CharacterMetrics;

        public void Dispose()
        {
            Dispose(inDisposing: true);
            GC.SuppressFinalize(this);
            return;
        }

        protected virtual void Dispose(bool inDisposing)
        {
            if (inDisposing) {
                m_Texture.Dispose();
                m_Texture = null;
            }
            return;
        }

        private readonly Font Font;
        private readonly int Margin;
        private PointF m_NextDrawPoint = new Point(0, 0);
        private float m_CurrentLineMaxHeight = 0;
        private Bitmap m_TemporaryCanvas = new Bitmap(1000, 1000);
        private Bitmap m_AtlasImage = null;
        private GLColorTexture2D m_Texture = null;
        private Dictionary<char, RectangleF> m_AssignedRectangle = new Dictionary<char, RectangleF>();
        private Dictionary<char, CharacterMetrics> m_CharacterMetrics = new Dictionary<char, CharacterMetrics>();
    }
}
