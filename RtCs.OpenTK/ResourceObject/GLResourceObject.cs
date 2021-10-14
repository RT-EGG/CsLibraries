using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public delegate void GLResourceObjectNotifyCallback(GLResourceObject inObject);
    public delegate void GLResourceObjectNotifyEventHandler(GLResourceObject inObject);

    public abstract class GLResourceObject : GLObject
    {
        public GLResourceObject()
        {
            new GLMainThreadTask(_ => CreateResource()).Enqueue();
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

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);

            new GLMainThreadTask(_ => DestroyResource()).Enqueue();
            return;
        }

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
    }
}
