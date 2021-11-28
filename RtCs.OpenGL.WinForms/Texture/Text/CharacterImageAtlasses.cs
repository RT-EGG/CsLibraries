using RtCs.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace RtCs.OpenGL.WinForms.Texture.Text
{
    public interface ICharacterImageAtlas
    {
        IGLTexture2D Texture { get; }
        IReadOnlyDictionary<char, RectangleF> AssignedRectangles { get; }
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

            using (Graphics atlasCanvas = Graphics.FromImage(m_AtlasImage))
            using (StringFormat format = new StringFormat(StringFormat.GenericTypographic))
            using (TextMeasure textMeasure = new TextMeasure(atlasCanvas, Font, format)) {
                Size proposedSize = new Size(10000, 10000);

                for (int index = 0; index < inCharacters.Length; ++index) {
                    if (m_AssignedRectangle.ContainsKey(inCharacters[index])) {
                        continue;
                    }

                    string character = new string(new char[] { inCharacters[index] });

                    Rectangle roughRegion = default;
                    using (Graphics tmpCanvas = Graphics.FromImage(m_TemporaryCanvas)) {
                        tmpCanvas.Clear(background);

                        // get rough size
                        SizeF roughSize = TextRenderer.MeasureText(tmpCanvas, character, Font, m_TemporaryCanvas.Size, TextFormatFlags.NoPadding);
                        if (Font.Italic) {
                            // MeasureText often fails to measure width of italic font.
                            roughSize.Width *= 2.0f;
                        }
                        roughRegion = new Rectangle(new Point(), roughSize.ToSize());

                        // render to measure strict size
                        TextRenderer.DrawText(tmpCanvas, character, Font, new Point(), Color.White, TextFormatFlags.NoPadding);
                    }

                    Rectangle region;
                    try {
                        (int offset, int width) = TextMeasure.MeasureWidth(m_TemporaryCanvas, roughRegion, background);
                        region = new Rectangle(offset, 0, width, roughRegion.Height);

                    } catch (ArgumentException) {
                        // the case of no pixels rendered, space and so on.
                        region = roughRegion;
                    }

                    if ((m_NextDrawPoint.X + (Margin * 2) + region.Width) > m_AtlasImage.Width) {
                        m_NextDrawPoint.X = 0;
                        m_NextDrawPoint.Y += m_CurrentLineMaxHeight + (Margin * 2);

                        if ((m_NextDrawPoint.X + (Margin * 2) + region.Width) > m_AtlasImage.Width) {
                            return index;
                        }
                    }
                    if ((m_NextDrawPoint.Y + (Margin * 2) + region.Height) > m_AtlasImage.Height) {
                        return index;
                    }

                    m_CurrentLineMaxHeight = Math.Max(m_CurrentLineMaxHeight, region.Height);

                    var point = new Point(
                        (int)Math.Round(m_NextDrawPoint.X),
                        (int)Math.Round(m_NextDrawPoint.Y)
                    );
                    point.X += Margin;
                    point.Y += Margin;

                    //TextRenderer.DrawText(atlasCanvas, character, Font, point, Color.White, TextFormatFlags.NoPadding);
                    atlasCanvas.DrawImage(m_TemporaryCanvas, point.X, point.Y, region, GraphicsUnit.Pixel);
                    m_AssignedRectangle.Add(inCharacters[index], AssignRectangle(point, region.Size));

                    m_NextDrawPoint.X += region.Width + Margin;
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
    }

    public class CharacterImageAtlasses : IDisposable
    {
        public CharacterImageAtlasses(Font inFont, int inMargin, int inMaxResolution)
        {
            Font = inFont;
            MaxResolution = inMaxResolution;
            Margin = inMargin;
            m_LatestAtlas = new CharacterImageAtlas(Font, inMargin, MaxResolution, MaxResolution);
            m_Items.Add(m_LatestAtlas);
            return;
        }

        public void AddCharacters(string inCharacters)
        {
            string characters = new string(inCharacters.Distinct().Where(c => !m_AssignedAtlas.ContainsKey(c)).ToArray());
            while (characters.Length > 0) {
                if (!AddCharacters(m_LatestAtlas, ref characters)) {
                    m_LatestAtlas = new CharacterImageAtlas(Font, Margin, MaxResolution, MaxResolution);
                    m_Items.Add(m_LatestAtlas);

                    if (!AddCharacters(m_LatestAtlas, ref characters)) {
                        // TODO make exception best for here
                        throw new Exception();
                    }
                }
            }
        }

        private bool AddCharacters(CharacterImageAtlas inAtlas, ref string inCharacters)
        {
            int added = inAtlas.AddCharacter(inCharacters);
            if (added == 0) {
                return false;
            }
            for (int i = 0; i < added; ++i) {
                m_AssignedAtlas.Add(inCharacters[i], inAtlas);
            }
            inCharacters = inCharacters.Remove(0, added);
            return true;
        }

        public bool TryGetAtlasFor(char inCharacter, out ICharacterImageAtlas outValue)
        {
            outValue = null;
            if (m_AssignedAtlas.TryGetValue(inCharacter, out var value)) {
                outValue = value;
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            Dispose(inDisposing: true);
            GC.SuppressFinalize(this);
            return;
        }

        protected virtual void Dispose(bool inDisposing)
        {
            if (inDisposing) {
                foreach (var item in m_Items) {
                    item.Dispose();
                }
            }
            return;
        }

        public IReadOnlyList<CharacterImageAtlas> Items => m_Items;

        public readonly Font Font;
        public readonly int Margin;
        public readonly int MaxResolution;
        private CharacterImageAtlas m_LatestAtlas = null;
        private List<CharacterImageAtlas> m_Items = new List<CharacterImageAtlas>();
        private Dictionary<char, CharacterImageAtlas> m_AssignedAtlas = new Dictionary<char, CharacterImageAtlas>();
    }
}
