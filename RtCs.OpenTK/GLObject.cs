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

        protected virtual void DisposeObject(bool inDisposing)
        { }

        private void Dispose(bool inDisposing)
        {
            if (!m_Disposed) {
                DisposeObject(inDisposing);
                m_Disposed = true;
            }
            return;
        }

        
        private bool m_Disposed = false;
    }
}
