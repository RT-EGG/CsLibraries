using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.OpenGL;
using System;

namespace GLTestVisualizer.TestView.TransformMatrixDexomposition
{
    public partial class Ctrl_TransformMatrixDecompositionTestView : GLTestVisualizer.TestView.Ctrl_TestView
    {
        public Ctrl_TransformMatrixDecompositionTestView()
        {
            InitializeComponent();

            Vector4[] vertColors = new Vector4[m_Cube.Positions.Length];
            for (int i = 0; i < m_Cube.Positions.Length; ++i) {
                if ((0 <= i) && (i <= 3)) {
                    vertColors[i] = new Vector4(1.0, 0.0, 0.0, 1.0); // -x
                } else if ((4 <= i) && (i <= 7)) {
                    vertColors[i] = new Vector4(0.0, 1.0, 1.0, 1.0); // +x
                } else if ((8 <= i) && (i <= 11)) {
                    vertColors[i] = new Vector4(0.0, 1.0, 0.0, 1.0); // -y
                } else if ((12 <= i) && (i <= 15)) {
                    vertColors[i] = new Vector4(1.0, 0.0, 1.0, 1.0); // +y
                } else if ((16 <= i) && (i <= 19)) {
                    vertColors[i] = new Vector4(0.0, 0.0, 1.0, 1.0); // -z
                } else if ((20 <= i) && (i <= 23)) {
                    vertColors[i] = new Vector4(1.0, 1.0, 0.0, 1.0); // +z
                }
            }
            m_Cube.Colors = vertColors;

            m_MatrixInputView.Renderer.Mesh = m_Cube;
            m_MatrixInputView.Renderer.Material = m_Material;
            m_MatrixInputView.Transform.LocalPosition = new Vector3(-1.0, 0.0, 0.0);
            m_MatrixOutputView.Renderer.Mesh = m_Cube;
            m_MatrixOutputView.Renderer.Material = m_Material;
            m_MatrixOutputView.Transform.LocalPosition = new Vector3(1.0, 0.0, 0.0);

            m_MatrixInputAxisView.Transform.Parent = m_MatrixInputView.Transform;
            m_MatrixOutputAxisView.Transform.Parent = m_MatrixOutputView.Transform;
            return;
        }

        public override string SceneName => "Transform Matrix Decomposition";

        public override void Start()
        {
            base.Start();

            ComboRotationOrder.Items.Clear();
            foreach (EEulerRotationOrder order in Enum.GetValues(typeof(EEulerRotationOrder))) {
                ComboRotationOrder.Items.Add(order);
            }
            ComboRotationOrder.SelectedIndex = 0;
            return;
        }

        public override void Exit()
        {
            base.Exit();

            timer1.Enabled = false;

            m_MatrixInputAxisView.Dispose();
            m_MatrixOutputAxisView.Dispose();
            m_MatrixInputView.Dispose();
            m_MatrixOutputView.Dispose();
            m_Material.Dispose();
            m_Cube.Dispose();
            return;
        }

        private void UpdateDecomposited()
        {
            Ctrl_MatrixOutput.Translation = m_DummyTransform.LocalPosition;
            Ctrl_MatrixOutput.Rotation = m_DummyTransform.LocalRotation.ToEuler(RotationOrder).RadToDeg();
            Ctrl_MatrixOutput.Scale = m_DummyTransform.LocalScale;

            m_MatrixInputView.Transform.LocalRotation = m_DummyTransform.LocalRotation;
            m_MatrixOutputView.Transform.LocalRotation = Quaternion.FromEuler(Ctrl_MatrixOutput.Rotation.DegToRad(), RotationOrder);
            return;
        }

        private EEulerRotationOrder RotationOrder
        {
            get => (EEulerRotationOrder)ComboRotationOrder.SelectedItem;
            set => ComboRotationOrder.SelectedItem = value;
        }

        private void Ctrl_MatrixInput_TranslationChanged(object inSender, Vector3 inValue)
        {
            m_DummyTransform.LocalPosition = inValue;
            UpdateDecomposited();
            return;
        }

        private void Ctrl_MatrixInput_RotationChanged(object inSender, Vector3 inValue)
        {
            m_DummyTransform.LocalRotation = Quaternion.FromEuler(inValue.DegToRad(), RotationOrder);
            UpdateDecomposited();
            return;
        }

        private void Ctrl_MatrixInput_ScaleChanged(object inSender, Vector3 inValue)
        {
            m_DummyTransform.LocalScale = inValue;
            UpdateDecomposited();
            return;
        }

        private void GLViewr_OnRenderScene(RtCs.OpenGL.WinForms.GLControl inControl, RtCs.OpenGL.GLRenderingStatus inStatus)
        {
            inStatus.ProjectionMatrix.PushMatrix();
            try {
                inStatus.ProjectionMatrix.LoadMatrix(Matrix4x4.MakePerspective(45.0, (double)GLViewer.Width / (double)GLViewer.Height, 0.01, 100.0));

                inStatus.ModelViewMatrix.View.PushMatrix();
                inStatus.ModelViewMatrix.Model.PushMatrix();
                try {
                    inStatus.ModelViewMatrix.View.LookAt(new Vector3(0.0, 2.0, 2.0), new Vector3(0.0), new Vector3(0.0, 1.0, 0.0));
                    inStatus.ModelViewMatrix.Model.LoadIdentity();

                    GL.Enable(EnableCap.DepthTest);
                    GL.Enable(EnableCap.CullFace);
                    GL.LineWidth(1.0f);

                    m_MatrixInputAxisView.Render(inStatus);
                    m_MatrixInputView.Render(inStatus);
                    m_MatrixOutputAxisView.Render(inStatus);
                    m_MatrixOutputView.Render(inStatus);

                } finally {
                    inStatus.ModelViewMatrix.Model.PopMatrix();
                    inStatus.ModelViewMatrix.View.PopMatrix();
                }
            } finally {
                inStatus.ProjectionMatrix.PopMatrix();
            }
            return;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GLViewer.Invalidate();
            return;
        }

        private void ComboRotationOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_DummyTransform.LocalRotation = Quaternion.FromEuler(Ctrl_MatrixInput.Rotation.DegToRad(), RotationOrder);
            UpdateDecomposited();
            return;
        }

        private GLMaterial m_Material = new GLBoxMaterial();
        private GLMesh m_Cube = GLPrimitiveMesh.CreateBox(1.0, 1.0, 1.0);
        private GLRenderObject m_MatrixInputView = new GLRenderObject();
        private GLAxisRenderObject m_MatrixInputAxisView = new GLAxisRenderObject();
        private GLRenderObject m_MatrixOutputView = new GLRenderObject();
        private GLAxisRenderObject m_MatrixOutputAxisView = new GLAxisRenderObject();
        private Transform m_DummyTransform = new Transform();
    }
}
