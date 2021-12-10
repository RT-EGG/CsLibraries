using System;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public abstract class GLResourceObject : GLObject
    {
        public GLResourceObject()
        {
            new GLMainThreadTask(_ => CreateResource()).Enqueue();
            return;
        }
        
        public void CreateResource()
        {
            CreateResourceCore();
            m_OnAfterCreateResource?.Invoke(this, EventArgs.Empty);
            return;
        }

        public void DestroyResource()
        {
            BeforeDestroyResource?.Invoke(this, EventArgs.Empty);
            DestroyResourceCore();
            return;
        }

        protected virtual void CreateResourceCore() { }
        protected virtual void DestroyResourceCore() { }
        protected abstract bool IsResourceCreated { get; }

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);

            new GLMainThreadTask(_ => DestroyResource()).Enqueue();
            return;
        }

        public event EventHandler AfterCreateResource
        {
            add {
                if (IsResourceCreated) {
                    value?.Invoke(this, EventArgs.Empty);
                } else {
                    m_OnAfterCreateResource += value;
                }
            }
            remove {
                m_OnAfterCreateResource -= value;
            }
        }
        private event EventHandler m_OnAfterCreateResource;

        public event EventHandler BeforeDestroyResource;
    }
}
