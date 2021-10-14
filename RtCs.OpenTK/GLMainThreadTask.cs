using System;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public class GLMainThreadTaskEventArgs : EventArgs
    {
        public static readonly new GLMainThreadTaskEventArgs Empty = new GLMainThreadTaskEventArgs();
    }
    public delegate void GLMainThreadTaskEventHandler(GLMainThreadTaskEventArgs inArgs);

    public class GLMainThreadTask
    {
        public GLMainThreadTask(GLMainThreadTaskEventHandler inTask)
            : this (inTask, GLMainThreadTaskEventArgs.Empty)
        { }

        public GLMainThreadTask(GLMainThreadTaskEventHandler inTask, GLMainThreadTaskEventArgs inArgs)
            => m_TaskQueue.Enqueue(new Task(inTask, inArgs));

        public static void Process()
        {
            foreach (var task in m_TaskQueue) {
                task.EventHandler(task.Args);
            }
            m_TaskQueue.Clear();
            return;
        }

        private static Queue<Task> m_TaskQueue = new Queue<Task>();
        private class Task
        {
            public Task(GLMainThreadTaskEventHandler inHandler, GLMainThreadTaskEventArgs inArgs)
            {
                EventHandler = inHandler;
                Args = inArgs;
                return;
            }

            public GLMainThreadTaskEventHandler EventHandler
            { get; set; } = null;
            public GLMainThreadTaskEventArgs Args
            { get; set; } = GLMainThreadTaskEventArgs.Empty;
        }
    }
}
