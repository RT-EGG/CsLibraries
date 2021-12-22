using RtCs;
using RtCs.MathUtils;
using RtCs.OpenGL;
using RtCs.OpenGL.WinForms.Texture.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GLTestVisualizer.TestView.Text
{
    public partial class Ctrl_TextTestView : GLTestVisualizer.TestView.Ctrl_TestView
    {
        public Ctrl_TextTestView()
        {
            InitializeComponent();
        }

        public override void Start()
        {
            base.Start();

            using (Graphics g = GLView.CreateGraphics()) {
                FontInitializer.PointToPixels = 256.0f / g.DpiX;
            }

            ChangeFont(new Font(SystemFonts.DefaultFont.Name, 72.0f, FontStyle.Regular, GraphicsUnit.Pixel));
            TextBoxInput.Text = "Input text...";

            m_AtlasMesh.Vertices = new Vector3[] {
                new Vector3(-0.5f, 0.5f, 0.0f),
                new Vector3(-0.5f, -0.5f, 0.0f),
                new Vector3(0.5f, -0.5f, 0.0f),
                new Vector3(0.5f, 0.5f, 0.0f)
            };
            m_AtlasMesh.TexCoords = new Vector2[] {
                new Vector2(0.0f, 0.0f),
                new Vector2(0.0f, 1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(1.0f, 0.0f)
            };
            m_AtlasMesh.Topology = EGLMeshTopology.Triangles;
            m_AtlasMesh.Indices = new int[] {
                0, 1, 3,
                1, 2, 3
            };
            m_AtlasMesh.Apply();
            m_TextureSampler.MinFilter = EGLTextureMinFilter.Linear;
            m_TextureSampler.MagFilter = EGLTextureMagFilter.Linear;
            m_TextureSampler.WrapS = EGLTextureWrapMode.ClampToEdge;
            m_TextureSampler.WrapT = EGLTextureWrapMode.ClampToEdge;
            m_TextureSampler.Apply();
            m_AtlasTextureMaterial.TextureReference.Sampler = m_TextureSampler;
            m_AtlasTextureObject.Renderer.Material = m_AtlasTextureMaterial;
            m_AtlasTextureObject.Renderer.Mesh = m_AtlasMesh;
        }

        public override void Exit()
        {
            base.Exit();

            foreach (var obj in m_Characters) {
                obj.Dispose();
            }
            m_Characters.Clear();

            m_AtlasMesh.Dispose();
            foreach (var atlas in m_Atlasses.Values) {
                atlas.Dispose();
            }
            m_Atlasses.Clear();

            m_AtlasTextureMaterial.Dispose();
            m_TextureSampler.Dispose();
        }

        private void ChangeFont(Font inFont)
        {
            FontInitializer fontInitializer = new FontInitializer(inFont);
            if (!m_Atlasses.TryGetValue(fontInitializer, out m_CurrentAtlases)) {
                m_CurrentAtlases = new CharacterImageAtlasses(inFont, 4, AtlasSize);
                m_CurrentAtlases.AddCharacters(InitialCharacterSet);
                m_Atlasses.Add(fontInitializer, m_CurrentAtlases);
            }
            m_AtlasTexture = m_CurrentAtlases.Items[0].Texture;
            m_AtlasTextureMaterial.TextureReference.Texture = m_AtlasTexture;

            ChangeText(TextBoxInput.Text);
            return;
        }

        private void ChangeText(string inText)
        {
            m_CurrentAtlases.AddCharacters(inText);

            Queue<CharacterRenderObject> savedRenderObjects = new Queue<CharacterRenderObject>(m_Characters);
            Vector2 position = new Vector2();

            m_Scene.DisplayList.Clear();
            m_Characters.Clear();
            int index = 0;
            while (index < inText.Length) {
                if ((index + Environment.NewLine.Length - 1) < inText.Length) {
                    if (inText.Substring(index, Environment.NewLine.Length) == Environment.NewLine)
                    {
                        position.x = 0.0f;
                        position.y -= LineHeight;
                        inText.Remove(index, Environment.NewLine.Length);

                        index += Environment.NewLine.Length;
                        continue;
                    }
                }

                CharacterRenderObject newObject;
                if (savedRenderObjects.IsEmpty()) {
                    newObject = new CharacterRenderObject(m_TextureSampler);
                } else {
                    newObject = savedRenderObjects.Dequeue();
                }
                m_Characters.Add(newObject);
                m_Scene.DisplayList.Register(newObject);

                char character = inText[index++];
                if (!m_CurrentAtlases.TryGetAtlasFor(character, out var atlas)) {
                    throw new InvalidProgramException($"Atlas for character must be assigned in {typeof(CharacterImageAtlasses).Name}.AddCharacters().");
                }
                if (!atlas.AssignedRectangles.TryGetValue(character, out var texcoord)) {
                    throw new InvalidProgramException($"Coordinate for character must be assigned in {typeof(CharacterImageAtlasses).Name}.AddCharacters().");
                }
                if (!atlas.CharacterMetrics.TryGetValue(character, out var metrics)) {
                    throw new InvalidProgramException($"Metrics for character must be assigned in {typeof(CharacterImageAtlasses).Name}.AddCharacters().");
                }

                Vector2 size = new Vector2(
                        AtlasSize * texcoord.Width,
                        AtlasSize * texcoord.Height
                    );
                newObject.Setup(atlas.Texture, texcoord, position + new Vector2(metrics.PixelOffsetX, 0.0f), size);
                newObject.Transform.Parent = m_TextOrigin;

                position.x += metrics.FeedWidth;
            }

            GLView.Invalidate();
            return;
        }

        private void ButtonFont_Click(object sender, EventArgs e)
        {
            if (FontDialog.ShowDialog() == DialogResult.OK) {
                ChangeFont(FontDialog.Font);
            }
        }

        private void TextBoxInput_TextChanged(object sender, EventArgs e)
            => ChangeText((sender as TextBox).Text);

        private void GLView_OnRenderScene(RtCs.OpenGL.WinForms.GLControl inControl, GLRenderParameter inParameter)
        {
            GLRenderParameter status = new GLRenderParameter();
            status.Viewport.SetRect(inControl.ClientRectangle);
            status.ProjectionMatrix.LoadMatrix(Matrix4x4.MakeOrtho(0.0f, inControl.Width, -inControl.Height, 0.0f, -10.0f, 10.0f));
            status.ModelViewMatrix.View.LoadIdentity();

            m_Scene.Render(status);
            return;
        }

        private Font CurrentFont
        {
            get {
                if (m_CurrentAtlases == null) {
                    return SystemFonts.DefaultFont;
                }

                return m_CurrentAtlases.Font;
            }
        }

        private int LineHeight
            => CurrentFont.Height;

        private const int AtlasSize = 2048;
        private Dictionary<FontInitializer, CharacterImageAtlasses> m_Atlasses = new Dictionary<FontInitializer, CharacterImageAtlasses>();
        private CharacterImageAtlasses m_CurrentAtlases = null;

        private GLScene m_Scene = new GLScene();
        private Transform m_TextOrigin = new Transform();
        private List<CharacterRenderObject> m_Characters = new List<CharacterRenderObject>();

        private GLRenderObject m_AtlasTextureObject = new GLRenderObject();
        private GLMesh m_AtlasMesh = new GLMesh();
        private IGLColorTexture2D m_AtlasTexture = null;
        private GLTextMaterial m_AtlasTextureMaterial = new GLTextMaterial();
        private GLTextureSampler m_TextureSampler = new GLTextureSampler();

        private readonly string InitialCharacterSet
            = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"+
              "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん" +
              "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン";

        private class CharacterRenderObject : GLRenderObject
        {
            public CharacterRenderObject(GLTextureSampler inSamlper)
            {
                m_Material.TextureReference.Sampler = inSamlper;
                return;
            }

            public void Setup(IGLTexture2D inReferenceTexture, RectangleF inTexCoord, Vector2 inPosition, Vector2 inQuadSize)
            {
                Transform.LocalPosition = new Vector3(inPosition.Concat(0.0f));

                Renderer.Mesh = m_Mesh;
                Renderer.Material = m_Material;

                m_Mesh.Vertices = new Vector3[] {
                    new Vector3(0.0f, 0.0f, 0.0f),
                    new Vector3(0.0f, -inQuadSize.y, 0.0f),
                    new Vector3(inQuadSize.x, -inQuadSize.y, 0.0f),
                    new Vector3(inQuadSize.x, 0.0f, 0.0f)
                };
                m_Mesh.TexCoords = new Vector2[] {
                    new Vector2(inTexCoord.Left, inTexCoord.Top),
                    new Vector2(inTexCoord.Left, inTexCoord.Bottom),
                    new Vector2(inTexCoord.Right, inTexCoord.Bottom),
                    new Vector2(inTexCoord.Right, inTexCoord.Top)
                };
                m_Mesh.Topology = EGLMeshTopology.Quads;
                m_Mesh.Indices = new int[] {
                    0, 1, 2, 3
                };
                m_Mesh.Apply();

                m_Material.TextureReference.Texture = inReferenceTexture;
                return;
            }

            protected override void DisposeObject(bool inDisposing)
            {
                base.DisposeObject(inDisposing);

                m_Mesh?.Dispose();
                m_Mesh = null;
                return;
            }

            private GLMesh m_Mesh = new GLMesh();
            private GLTextMaterial m_Material = new GLTextMaterial();
        }
    }
}
