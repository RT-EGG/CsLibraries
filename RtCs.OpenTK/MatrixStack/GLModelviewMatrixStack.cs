using OpenTK.Graphics.OpenGL;
using RtCs.MathUtils;
using System;
using System.Linq;

namespace RtCs.OpenGL
{
    public class GLModelviewMatrixStack
    {
        public GLModelviewMatrixStack()
        {
            Model.OnMatrixChanged += MatrixChanged;
            View.OnMatrixChanged += MatrixChanged;
            return;
        }

        public Matrix4x4 CurrentMatrix
        {
            get {
                if (m_MvMatrixChanged) {
                    m_ModelViewMatrix = View.CurrentMatrix * Model.CurrentMatrix;
                    m_MvMatrixChanged = false;
                }
                return m_ModelViewMatrix;
            }
        }

        public Matrix3x3 NormalMatrix
        {
            get {
                if (m_NormMatrixChanged) {
                    for (int r = 0; r < 3; ++r) {
                        for (int c = 0; c < 3; ++c) {
                            m_NormalMatrix[r, c] = CurrentMatrix[r, c];
                        }
                    }

                    if (m_NormalMatrix.Inverse()) {
                        m_NormalMatrix = m_NormalMatrix.Transposed;
                    } else {
                        m_NormalMatrix = Matrix3x3.Identity; ;
                    }

                    m_NormMatrixChanged = false;
                }
                return m_NormalMatrix;
            }
        }

        private void MatrixChanged(object sender, EventArgs e)
        {
            m_MvMatrixChanged = true;
            m_NormMatrixChanged = true;

            OpenTK.Graphics.OpenGL.GL.MatrixMode(MatrixMode.Modelview);
            OpenTK.Graphics.OpenGL.GL.LoadMatrix(CurrentMatrix.ToArray());

            return;
        }

        public GLModelMatrixStack Model { get; } = new GLModelMatrixStack();
        public GLViewMatrixStack View { get; } = new GLViewMatrixStack();
        private Matrix3x3 m_NormalMatrix = Matrix3x3.Identity;
        private Matrix4x4 m_ModelViewMatrix = Matrix4x4.Identity;
        private bool m_MvMatrixChanged = true;
        private bool m_NormMatrixChanged = true;
    }
}
