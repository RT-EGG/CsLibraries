using System.Collections;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public interface IGLDisplayList
    {
        void Render(GLRenderingStatus inStatus);
    }

    public class GLDisplayList : IGLDisplayList, IEnumerable<GLRenderObject>
    {
        public void Render(GLRenderingStatus inStatus)
            => this.ForEach(item => item.Render(inStatus));

        public void Register(GLRenderObject inObject)
        {
            Unregister(inObject);
            m_RenderObjects.Add(inObject);
            return;
        }

        public void Unregister(GLRenderObject inObject)
            => m_RenderObjects.Remove(inObject);

        private List<GLRenderObject> m_RenderObjects = new List<GLRenderObject>();

        IEnumerator<GLRenderObject> IEnumerable<GLRenderObject>.GetEnumerator()
            => m_RenderObjects.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => m_RenderObjects.GetEnumerator();
    }
}
