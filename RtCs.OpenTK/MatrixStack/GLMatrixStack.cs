using OpenTK.Graphics.OpenGL;
using RtCs.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    public abstract class GLMatrixStack : GLObject
    {
        public GLMatrixStack()
        {
            return;
        }

        public void LoadIdentity()
            => LoadMatrix(Matrix4x4.Identity);

        public void LoadMatrix(Matrix4x4 inValue)
        {
            m_CurrentMatrix = inValue;
            GL.MatrixMode(TargetMatrixMode);
            GL.LoadMatrix(m_CurrentMatrix.ToArray());

            OnMatrixChanged?.Invoke(this, EventArgs.Empty);
            return;
        }

        public void MultiMatrix(Matrix4x4 aValue)
            => LoadMatrix(CurrentMatrix * aValue);

        public void PushMatrix()
        {
            GL.MatrixMode(TargetMatrixMode);
            m_Stack.Push(CurrentMatrix);
            return;
        }

        public void PopMatrix()
        {
            GL.MatrixMode(TargetMatrixMode);
            LoadMatrix(m_Stack.Pop());
            return;
        }

        public Matrix4x4 CurrentMatrix => m_CurrentMatrix;

        public event EventHandler OnMatrixChanged;

        protected abstract MatrixMode TargetMatrixMode
        { get; }

        private Matrix4x4 m_CurrentMatrix;
        private Stack<Matrix4x4> m_Stack = new Stack<Matrix4x4>();
    }
}
