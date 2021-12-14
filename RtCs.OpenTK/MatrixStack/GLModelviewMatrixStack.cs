using RtCs.MathUtils;
using System;

namespace RtCs.OpenGL
{
    /// <summary>
    /// The matrix stack of model matrix and view matrix.
    /// </summary>
    public class GLModelviewMatrixStack
    {
        public GLModelviewMatrixStack()
        {
            Model.OnMatrixChanged += MatrixChanged;
            View.OnMatrixChanged += MatrixChanged;
            return;
        }

        /// <summary>
        /// Get current matrix combined view matrix and model matrix.
        /// </summary>
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

        /// <summary>
        /// Get current normal matrix used for normal transform.
        /// </summary>
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
            return;
        }

        /// <summary>
        /// Get model matrix stack.
        /// </summary>
        public GLModelMatrixStack Model { get; } = new GLModelMatrixStack();
        /// <summary>
        /// Get view matrix stack.
        /// </summary>
        public GLViewMatrixStack View { get; } = new GLViewMatrixStack();
        private Matrix3x3 m_NormalMatrix = Matrix3x3.Identity;
        private Matrix4x4 m_ModelViewMatrix = Matrix4x4.Identity;
        private bool m_MvMatrixChanged = true;
        private bool m_NormMatrixChanged = true;
    }
}
