using OpenTK.Graphics.OpenGL;
using RtCs.MathUtils;
using System;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    /// <summary>
    /// Base class of matrix stack.
    /// </summary>
    /// <remarks>
    /// Used as an alternative to OpenGL Matrix method group (glPushMatrix, glLoadMatrix, glMultMatrix and so on).
    /// </remarks>
    public abstract class GLMatrixStack : GLObject
    {
        /// <summary>
        /// Set current matrix to identity.
        /// </summary>
        /// <remarks>
        /// Same as LoadMatrix(Matrix4x4.Identity).
        /// </remarks>
        public void LoadIdentity()
            => LoadMatrix(Matrix4x4.Identity);

        /// <summary>
        /// Set current matrix to the specified.
        /// </summary>
        /// <param name="inValue">New matrix to set current.</param>
        public void LoadMatrix(Matrix4x4 inValue)
        {
            m_CurrentMatrix = inValue;
            OnMatrixChanged?.Invoke(this, EventArgs.Empty);
            return;
        }

        /// <summary>
        /// Multply specified matrix to current matrix.
        /// </summary>
        /// <param name="inValue">Matrix to multiply.</param>
        /// <remarks>
        /// Same as LoadMatrix(CurrentMatrix * aValue).
        /// </remarks>
        public void MultiMatrix(Matrix4x4 inValue)
            => LoadMatrix(CurrentMatrix * inValue);

        /// <summary>
        /// Push current matrix to stack.
        /// </summary>
        public void PushMatrix()
            => m_Stack.Push(CurrentMatrix);

        /// <summary>
        /// Pop matrix from peek of stack and set to current matrix.
        /// </summary>
        /// <exception cref="InvalidOperationException">The case that the stack is empty.</exception>
        public void PopMatrix()
        {
            GL.MatrixMode(TargetMatrixMode);
            LoadMatrix(m_Stack.Pop());
            return;
        }

        /// <summary>
        /// Get current matrix.
        /// </summary>
        public Matrix4x4 CurrentMatrix
        { get => m_CurrentMatrix; }

        /// <summary>
        /// The event called at last of LoadMatrix.
        /// </summary>
        /// <param name="sender">This instance.</param>
        /// <param name="e">Set EventArgs.Empty.</param>
        public event EventHandler OnMatrixChanged;

        protected abstract MatrixMode TargetMatrixMode
        { get; }

        private Matrix4x4 m_CurrentMatrix;
        private Stack<Matrix4x4> m_Stack = new Stack<Matrix4x4>();
    }
}
