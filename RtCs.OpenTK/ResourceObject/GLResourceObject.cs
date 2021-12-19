using System;

namespace RtCs.OpenGL
{
    /// <summary>
    /// Base class of OpenGL resourced object classes.
    /// </summary>
    /// <remarks>
    /// OpenGL resourced object in this library means that object is managed by ID created by glGenXXX function.
    /// For example, buffer, texture and so on.
    /// </remarks>
    public abstract class GLResourceObject : GLObject
    {
        public GLResourceObject()
            => GLMainThreadTask.CreateNew(_ => CreateResource());

        internal void CreateResource()
        {
            CreateResourceCore();
            m_AfterCreateResource?.Invoke(this, EventArgs.Empty);
            return;
        }

        internal void DestroyResource()
        {
            m_BeforeDestroyResource?.Invoke(this, EventArgs.Empty);
            DestroyResourceCore();
            return;
        }

        /// <summary>
        /// Call opengl-initialization method.
        /// </summary>
        /// <remarks>
        /// The class inherits GLResourceObject should be call initialize method in this method,
        /// for example, glGenTextures, glGenBuffers and so on.\n
        /// This method will be called when opengl context is valid.
        /// </remarks>
        protected virtual void CreateResourceCore() { }

        /// <summary>
        /// Call opengl-destroy method.
        /// </summary>
        /// <remarks>
        /// The class inherits GLResourceObject should be call destroy method in this method,
        /// for example, glDeleteTextures, glDeleteBuffers and so on.\n
        /// This method will be called when opengl context is valid.
        /// </remarks>
        protected virtual void DestroyResourceCore() { }

        /// <summary>
        /// The status that the object is already generated.
        /// </summary>
        /// <remarks>
        /// If the object is generated, returns true, Otherwise false.
        /// </remarks>
        protected abstract bool IsResourceCreated { get; }

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);

            GLMainThreadTask.CreateNew(_ => DestroyResource());
            return;
        }

        /// <summary>
        /// The event called after CreateResourceCore.
        /// </summary>
        /// <remarks>
        /// The event will be called after CreateResourceCore, this means that the event will be called after the resource created.\n
        /// </remarks>
        /// <param name="sender">This object.</param>
        /// <param name="e">EventArgs.Empty</param>
        public event EventHandler AfterCreateResource
        {
            add {
                if (IsResourceCreated) {
                    value?.Invoke(this, EventArgs.Empty);
                } else {
                    m_AfterCreateResource += value;
                }
            }
            remove {
                m_AfterCreateResource -= value;
            }
        }

        /// <summary>
        /// The event called before DestroyResourceCore.
        /// </summary>
        /// <remarks>
        /// The event will be called after DestroyResourceCore, this means that the event will be called befire the resource destroyed.\n
        /// </remarks>
        /// <param name="sender">This object.</param>
        /// <param name="e">EventArgs.Empty</param>
        public event EventHandler BeforeDestroyResource
        {
            add {
                m_BeforeDestroyResource += value;
            }
            remove {
                m_BeforeDestroyResource -= value;
            }
        }

        private event EventHandler m_AfterCreateResource;
        private event EventHandler m_BeforeDestroyResource;

    }
}
