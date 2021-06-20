using System;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public delegate void GLResourceObjectNotifyCallback(GLResourceObject inObject);
    public delegate void GLResourceObjectNotifyEventHandler(GLResourceObject inObject);

    public abstract class GLResourceObject : IDisposable
    {
        public GLResourceObject()
        {
            if (CanResourceProcess) {
                CreateResource();
            } else {
                CreationQueue.Enqueue(this);
            }
            return;
        }
        
        public void CreateResource()
        {
            InternalCreateResource();
            m_OnAfterCreateResource?.Invoke(this);
            return;
        }

        public void DestroyResource()
        {
            OnBeforeDestroyResource?.Invoke(this);
            InternalDestroyResource();
            return;
        }

        protected virtual void InternalCreateResource() { }
        protected virtual void InternalDestroyResource() { }
        protected abstract bool IsResourceCreated { get; }

        public event GLResourceObjectNotifyEventHandler OnAfterCreateResource
        {
            add {
                if (IsResourceCreated) {
                    value?.Invoke(this);
                } else {
                    m_OnAfterCreateResource += value;
                }
            }
            remove {
                m_OnAfterCreateResource -= value;
            }
        }
        private event GLResourceObjectNotifyEventHandler m_OnAfterCreateResource;

        public event GLResourceObjectNotifyEventHandler OnBeforeDestroyResource;

        internal static void CreateResourcesInQueue()
        {
            while (!CreationQueue.IsEmpty()) {
                CreationQueue.Dequeue().CreateResource();
            }
            return;
        }
        internal static void DestroyResourcesInQueue()
        {
            while (!DestroyQueue.IsEmpty()) {
                DestroyQueue.Dequeue().DestroyResource();
            }
            return;
        }
        private static Queue<GLResourceObject> CreationQueue { get; } = new Queue<GLResourceObject>();
        private static Queue<GLResourceObject> DestroyQueue { get; } = new Queue<GLResourceObject>();
        private bool CanResourceProcess => Controls.GLControl.PaintingControl != null;

        protected virtual void Dispose(bool inDisposing)
        {
            if (!m_Disposed) {
                if (inDisposing) {
                    if (CanResourceProcess) {
                        DestroyResource();
                    } else {
                        DestroyQueue.Enqueue(this);
                    }
                }

                m_Disposed = true;
            }
            return;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            return;
        }

        private bool m_Disposed;
    }
}
