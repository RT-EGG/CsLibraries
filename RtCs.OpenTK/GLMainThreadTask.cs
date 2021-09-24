using System;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public class GLMainThreadTaskEventArgs : EventArgs
    {
        public static readonly new GLMainThreadTaskEventArgs Empty = new GLMainThreadTaskEventArgs();

        // TODO
        // remove needless part.
        public string StackTrace
        { get; } = Environment.StackTrace;
    }
    public delegate void GLMainThreadTaskEventHandler(GLMainThreadTaskEventArgs inArgs);

    public class GLMainThreadTask
    {
        public GLMainThreadTask(GLMainThreadTaskEventHandler inAction)
        {
            Action = inAction;
            return;
        }

        public void Enqueue()
            => GLMainThreadTaskQueue.Enqueue(this);

        public GLMainThreadTaskEventHandler Action
        { get; set; } = null;

        public GLMainThreadTaskEventArgs ActionArgument
        { get; set; } = GLMainThreadTaskEventArgs.Empty;

        public bool DoSoonIfCan
        { get; set; } = true;
    }

    public class GLMainThreadTaskQueue
    {
        internal static void Enqueue(GLMainThreadTask inTask)
        {
            if (CanProcessSoon && inTask.DoSoonIfCan) {
                ProcessTask(inTask);
            } else {
                m_TaskQueue.Enqueue(inTask);
            }
            return;
        }

        public static void Process()
        {
            while (m_TaskQueue.Count > 0) {
                ProcessTask(m_TaskQueue.Dequeue());
            }
            return;
        }

        private static void ProcessTask(GLMainThreadTask inTask)
            => inTask.Action.Invoke(inTask.ActionArgument);

        private static Queue<GLMainThreadTask> m_TaskQueue = new Queue<GLMainThreadTask>();
        private static bool CanProcessSoon => OpenTK.Graphics.GraphicsContext.CurrentContext != null;
    }
}
