using GLTestVisualizer.Model;
using OpenTK.Graphics.OpenGL;
using RtCs.MathUtils;
using System;
using System.Windows.Forms;

namespace GLTestVisualizer.TestView.TransformMatrixDexomposition
{
    public partial class Ctrl_TransformMatrixDecompositionTestView : GLTestVisualizer.TestView.Ctrl_TestView
    {
        public Ctrl_TransformMatrixDecompositionTestView()
        {
            InitializeComponent();
        }

        public override string SceneName => "Transform Matrix Decomposition";

        public override void Start()
        {
            base.Start();

            ComboRotationOrder.Items.Clear();
            foreach (EulerRotationOrder order in Enum.GetValues(typeof(EulerRotationOrder))) {
                ComboRotationOrder.Items.Add(order);
            }
            ComboRotationOrder.SelectedIndex = 0;
            return;
        }

        private void UpdateDecomposited()
        {
            Ctrl_MatrixOutput.Translation = m_Transform.LocalTranslation;
            Ctrl_MatrixOutput.Rotation = m_Transform.LocalRotation.ToEuler(RotationOrder).RadToDeg();
            Ctrl_MatrixOutput.Scale = m_Transform.LocalScale;
            return;
        }

        private EulerRotationOrder RotationOrder
        {
            get => (EulerRotationOrder)ComboRotationOrder.SelectedItem;
            set => ComboRotationOrder.SelectedItem = value;
        }

        private void Ctrl_MatrixInput_TranslationChanged(object inSender, Vector3 inValue)
        {
            m_Transform.LocalTranslation = inValue;
            UpdateDecomposited();
            return;
        }

        private void Ctrl_MatrixInput_RotationChanged(object inSender, Vector3 inValue)
        {
            m_Transform.LocalRotation = Quaternion.FromEuler(inValue.DegToRad(), RotationOrder);
            UpdateDecomposited();
            return;
        }

        private void Ctrl_MatrixInput_ScaleChanged(object inSender, Vector3 inValue)
        {
            m_Transform.LocalScale = inValue;
            UpdateDecomposited();
            return;
        }

        private void GLViewer_OnPaintScene(OpenTK.GLControl inControl, PaintEventArgs inArgs)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            try {
                GL.LoadMatrix(Matrix4x4.MakePerspective(45.0, (double)GLViewer.Width / (double)GLViewer.Height, 0.01, 100.0).ToGLArray());

                GL.MatrixMode(MatrixMode.Modelview);
                GL.PushMatrix();
                try {
                    GL.LoadIdentity();
                    GL.MultMatrix(Matrix4x4.MakeLookAt(new Vector3(0.0, 2.0, 2.0), new Vector3(0.0), new Vector3(0.0, 1.0, 0.0)).ToGLArray());

                    GL.Disable(EnableCap.Lighting);
                    GL.Enable(EnableCap.DepthTest);
                    GL.Enable(EnableCap.CullFace);
                    GL.LineWidth(1.0f);

                    GL.PushMatrix();
                    try {
                        Matrix4x4 trans = Matrix4x4.MakeTranslate(-1.0, 0.0, 0.0) 
                                        * Matrix4x4.MakeRotate(Quaternion.FromEuler(Ctrl_MatrixInput.Rotation.DegToRad(), RotationOrder));

                        GL.MultMatrix(trans.ToGLArray());

                        DrawAxis();
                        GL.Color4(1.0, 1.0, 1.0, 1.0);
                        DrawBox();

                    } finally {
                        GL.PopMatrix();
                    }

                    GL.PushMatrix();
                    try {
                        Matrix4x4 trans = Matrix4x4.MakeTranslate(1.0, 0.0, 0.0)
                                        * Matrix4x4.MakeRotate(Quaternion.FromEuler(Ctrl_MatrixOutput.Rotation.DegToRad(), RotationOrder));

                        GL.MultMatrix(trans.ToGLArray());

                        DrawAxis();
                        GL.Color4(1.0, 1.0, 1.0, 1.0);
                        DrawBox();

                    } finally {
                        GL.PopMatrix();
                    }

                } finally {
                    GL.MatrixMode(MatrixMode.Modelview);
                    GL.PopMatrix();
                }
            } finally {
                GL.MatrixMode(MatrixMode.Projection);
                GL.PopMatrix();
            }
            return;
        }

        private void DrawAxis()
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color4(1.0, 0.0, 0.0, 1.0); GL.Vertex3(0.0, 0.0, 0.0); GL.Vertex3(1.0, 0.0, 0.0);
            GL.Color4(0.0, 1.0, 0.0, 1.0); GL.Vertex3(0.0, 0.0, 0.0); GL.Vertex3(0.0, 1.0, 0.0);
            GL.Color4(0.0, 0.0, 1.0, 1.0); GL.Vertex3(0.0, 0.0, 0.0); GL.Vertex3(0.0, 0.0, 1.0);
            GL.End();
        }

        private void DrawBox()
        {
            GL.Begin(PrimitiveType.Quads);
            // X
            GL.Vertex3(-0.5, 0.5, -0.5); GL.Vertex3(-0.5, -0.5, -0.5); GL.Vertex3(-0.5, -0.5, 0.5); GL.Vertex3(-0.5, 0.5, 0.5);
            GL.Vertex3(0.5, 0.5, 0.5); GL.Vertex3(0.5, -0.5, 0.5); GL.Vertex3(0.5, -0.5, -0.5); GL.Vertex3(0.5, 0.5, -0.5);
            // Y
            GL.Vertex3(-0.5, -0.5, -0.5); GL.Vertex3(0.5, -0.5, -0.5); GL.Vertex3(0.5, -0.5, 0.5); GL.Vertex3(-0.5, -0.5, 0.5);
            GL.Vertex3(-0.5, 0.5, -0.5); GL.Vertex3(-0.5, 0.5, 0.5); GL.Vertex3(0.5, 0.5, 0.5); GL.Vertex3(0.5, 0.5, -0.5);
            // Z
            GL.Vertex3(0.5, 0.5, -0.5); GL.Vertex3(0.5, -0.5, -0.5); GL.Vertex3(-0.5, -0.5, -0.5); GL.Vertex3(-0.5, 0.5, -0.5);
            GL.Vertex3(-0.5, 0.5, 0.5); GL.Vertex3(-0.5, -0.5, 0.5); GL.Vertex3(0.5, -0.5, 0.5); GL.Vertex3(0.5, 0.5, 0.5);
            //
            GL.End();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GLViewer.Invalidate();
            return;
        }

        private Transform m_Transform = new Transform();
    }
}
