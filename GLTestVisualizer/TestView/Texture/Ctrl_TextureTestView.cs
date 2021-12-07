using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.OpenGL;
using RtCs.OpenGL.WinForms;
using RtCs.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GLTestVisualizer.TestView.Texture
{
    public partial class Ctrl_TextureTestView : GLTestVisualizer.TestView.Ctrl_TestView
    {
        public Ctrl_TextureTestView()
        {
            InitializeComponent();

            return;
        }

        public override void Start()
        {
            base.Start();

            m_TextureMesh.Vertices = new Vector3[] {
                new Vector3(-1.0f,  1.0f, 0.0f),
                new Vector3(-1.0f, -1.0f, 0.0f),
                new Vector3( 1.0f, -1.0f, 0.0f),
                new Vector3( 1.0f,  1.0f, 0.0f),
            };
            m_TextureMesh.TexCoords = new Vector2[] {
                new Vector2(0.0f, 0.0f),
                new Vector2(0.0f, 1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(1.0f, 0.0f),
            };
            m_TextureMesh.Topology = EGLMeshTopology.Triangles;
            m_TextureMesh.Indices = new int[] {
                0, 1, 3,
                1, 2, 3
            };
            m_TextureMesh.Apply();
            m_TextureObject.Renderer.Mesh = m_TextureMesh;
            m_TextureObject.Renderer.Material = m_TextureMaterial;
            m_TextureObject.FrustumCullingMode = EGLFrustumCullingMode.AlwaysRender;

            m_TextureMaterial.TextureReference.Texture = m_Texture;
            m_TextureMaterial.TextureReference.Sampler = m_Sampler;

            ComboFilter.Items.Add(All.Nearest);
            ComboFilter.Items.Add(All.Linear);
            ComboFilter.SelectedIndex = 0;
            ComboWrap.Items.Add(EGLTextureWrapMode.ClampToEdge);
            ComboWrap.Items.Add(EGLTextureWrapMode.MirrorClampToEdge);
            ComboWrap.Items.Add(EGLTextureWrapMode.Repeat);
            ComboWrap.Items.Add(EGLTextureWrapMode.MirroredRepeat);
            ComboWrap.Items.Add(EGLTextureWrapMode.ClampToBorder);
            ComboWrap.SelectedIndex = 0;

            var borderColor = m_Sampler.BorderColor;
            UpDownBorderR.Value = (decimal)(borderColor.R);
            UpDownBorderG.Value = (decimal)(borderColor.G);
            UpDownBorderB.Value = (decimal)(borderColor.B);
            UpDownBorderA.Value = (decimal)(borderColor.A);
            UpDownBorderR.ValueChanged += UpDownBorderColor_ValueChanged;
            UpDownBorderG.ValueChanged += UpDownBorderColor_ValueChanged;
            UpDownBorderB.ValueChanged += UpDownBorderColor_ValueChanged;
            UpDownBorderA.ValueChanged += UpDownBorderColor_ValueChanged;
            return;
        }

        private void GLView_OnRenderScene(RtCs.OpenGL.WinForms.GLControl inControl, RtCs.OpenGL.GLRenderingStatus inStatus)
        {
            GLRenderingStatus status = new GLRenderingStatus();
            status.Viewport.SetRect(inControl.ClientRectangle);
            status.ProjectionMatrix.LoadMatrix(Matrix4x4.MakeOrtho(inControl.Width, inControl.Height, -10.0f, 10.0f));
            status.ModelViewMatrix.View.LoadIdentity();

            m_Scene.DisplayList = DisplayList;
            m_Scene.Render(status);
            return;
        }

        private void GLView_SizeChanged(object sender, EventArgs e)
            => CalcTextureDimensions(GLView.Size);

        private void ButtonImportImageFile_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK) {
                LoadImageFile(OpenFileDialog.FileName);
            }
            return;
        }

        private void ComboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboFilter.SelectedItem == null) {
                return;
            }

            m_Sampler.MinFilter = (EGLTextureMinFilter)ComboFilter.SelectedItem;
            m_Sampler.MagFilter = (EGLTextureMagFilter)ComboFilter.SelectedItem;
            m_Sampler.Apply();
            GLView.Invalidate();
            return;
        }

        private void ComboWrap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboWrap.SelectedItem == null) {
                return;
            }

            m_Sampler.WrapS = (EGLTextureWrapMode)ComboWrap.SelectedItem;
            m_Sampler.WrapT = (EGLTextureWrapMode)ComboWrap.SelectedItem;
            m_Sampler.Apply();
            GLView.Invalidate();
            return;
        }

        private void UpDownBorderColor_ValueChanged(object sender, EventArgs e)
        {
            ColorRGBA borderColor = new ColorRGBA(
                    (byte)UpDownBorderR.Value,
                    (byte)UpDownBorderG.Value,
                    (byte)UpDownBorderB.Value,
                    (byte)UpDownBorderA.Value
                );
            m_Sampler.BorderColor = borderColor;
            m_Sampler.Apply();
            GLView.Invalidate();
            return;
        }

        private void LoadImageFile(string inFilepath)
        {
            (new GLTextureImageImporter()).ImportFromFile(inFilepath, m_Texture);
            CalcTextureDimensions(GLView.Size);
            return;
        }

        private void CalcTextureDimensions(Size inClientSize)
        {
            // fill vertex positions to client
            Vector2 fitSizeHalf = inClientSize.ToVector() * 0.5f;
            m_TextureMesh.Vertices = new Vector3[] {
                new Vector3(-fitSizeHalf.x,  fitSizeHalf.y, 0.0f),
                new Vector3(-fitSizeHalf.x, -fitSizeHalf.y, 0.0f),
                new Vector3( fitSizeHalf.x, -fitSizeHalf.y, 0.0f),
                new Vector3( fitSizeHalf.x,  fitSizeHalf.y, 0.0f),
            };

            // fit texture coordinates
            fitSizeHalf = (new AspectRatioFitter2D()).CalcFitRectSizeWrapParent(inClientSize.ToVector(), m_Texture.Size);
            fitSizeHalf.x /= m_Texture.Width;
            fitSizeHalf.y /= m_Texture.Height;
            fitSizeHalf *= 0.5f;
            m_TextureMesh.TexCoords = new Vector2[] {
                new Vector2(0.5f - fitSizeHalf.x, 0.5f - fitSizeHalf.y),
                new Vector2(0.5f - fitSizeHalf.x, 0.5f + fitSizeHalf.y),
                new Vector2(0.5f + fitSizeHalf.x, 0.5f + fitSizeHalf.y),
                new Vector2(0.5f + fitSizeHalf.x, 0.5f - fitSizeHalf.y),
            };
            m_TextureMesh.Apply();

            GLView.Invalidate();
            return;
        }

        private IEnumerable<GLRenderObject> DisplayList
        {
            get {
                yield return m_TextureObject;
            }
        }

        private GLScene m_Scene = new GLScene();
        private GLRenderObject m_TextureObject = new GLRenderObject();
        private GLMesh m_TextureMesh = new GLMesh();
        private GLTextureMaterial m_TextureMaterial = new GLTextureMaterial();
        private GLTextureSampler m_Sampler = new GLTextureSampler();
        private GLColorTexture2D m_Texture = new GLColorTexture2D(64, 64);
    }
}
