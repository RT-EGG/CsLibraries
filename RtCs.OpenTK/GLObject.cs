using System;

namespace RtCs.OpenGL
{
    public class GLObject : IDisposable
    {
        ~GLObject() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            return;
        }

        protected virtual void Dispose(bool inDisposing)
        {
            if (!m_Disposed) {
                m_Disposed = true;
            }
            return;
        }

        protected bool Disposed => m_Disposed;
        private bool m_Disposed = false;
    }
}
