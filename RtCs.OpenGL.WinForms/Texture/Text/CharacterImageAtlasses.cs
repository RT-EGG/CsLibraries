using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RtCs.OpenGL.WinForms.Texture.Text
{
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
