using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public class GLDisplayList
    {
        public void Register(GLRenderObject inObject)
        {
            Unregister(inObject);
            m_RenderObjectList.Add(inObject);
            return;
        }

        public void Register(IEnumerable<GLRenderObject> inObjects)
        {
            foreach (var obj in inObjects) {
                Unregister(obj);
            }
            m_RenderObjectList.AddRange(inObjects);
            return;
        }

        public void Unregister(GLRenderObject inObject)
        {
            m_RenderObjectList.Remove(inObject);
            return;
        }

        public void Clear()
        {
            m_RenderObjectList.Clear();
            return;
        }

        public IReadOnlyList<GLRenderObject> Items => m_RenderObjectList;
        private List<GLRenderObject> m_RenderObjectList = new List<GLRenderObject>();
    }
}
