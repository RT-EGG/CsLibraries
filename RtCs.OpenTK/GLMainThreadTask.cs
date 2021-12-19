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

    public class GLMainThreadTaskOptions<T> where T : GLMainThreadTaskEventArgs
    {
        /// <summary>
        /// The argument used for GLMainThreadTask's TaskMethod.
        /// </summary>
        public T Argument
        { get; set; } = default;
    }

    /// <summary>
    /// The options for GLMainThreadTask execution.
    /// </summary>
    public class GLMainThreadTaskOptions : GLMainThreadTaskOptions<GLMainThreadTaskEventArgs>
    { 
        public GLMainThreadTaskOptions()
            => Argument = GLMainThreadTaskEventArgs.Empty;
    }

    /// <summary>
    /// The object to register process run once on thread which is OpenGL context valid.
    /// </summary>
    public abstract class GLMainThreadTask
    {
        /// <summary>
        /// Create new task.
        /// </summary>
        /// <typeparam name="T">Specially method argument type.</typeparam>
        /// <param name="inTaskMethod">The method called when next timing the OpenGL is valid.</param>
        /// <param name="inOptions">The option for task method execution.</param>
        /// <returns></returns>
        public static GLMainThreadTask CreateNew<T>(Action<T> inTaskMethod, GLMainThreadTaskOptions<T> inOptions = null) where T : GLMainThreadTaskEventArgs
        {
            var task = new TaskCore<T>(inTaskMethod, inOptions);
            GLMainThreadTaskQueue.Enqueue(task);
            return task;
        }

        /// <summary>
        /// Create new task.
        /// </summary>
        /// <param name="inTaskMethod">The method called when next timing the OpenGL is valid.</param>
        /// <param name="inOptions">The option for task method execution.</param>
        /// <returns></returns>
        public static GLMainThreadTask CreateNew(Action<GLMainThreadTaskEventArgs> inTaskMethod, GLMainThreadTaskOptions inOptions = null)
            => CreateNew<GLMainThreadTaskEventArgs>(inTaskMethod, inOptions);

        private GLMainThreadTask()
        { }

        internal void Execute()
            => ExecuteCore();

        protected abstract void ExecuteCore();

        private class TaskCore<T> : GLMainThreadTask where T : GLMainThreadTaskEventArgs
        {
            public TaskCore(Action<T> inTaskMethod, GLMainThreadTaskOptions<T> inOptions)
            {
                TaskMethod = inTaskMethod;
                Options = inOptions ?? new GLMainThreadTaskOptions<T>();
                return;
            }

            protected override void ExecuteCore()
                => TaskMethod(Options.Argument);

            private readonly Action<T> TaskMethod;
            private readonly GLMainThreadTaskOptions<T> Options;
        }
    }

    public class GLMainThreadTaskQueue
    {
        internal static void Enqueue(GLMainThreadTask inTask)
        {
            if (CanProcessSoon) {
                inTask.Execute();
            } else {
                m_TaskQueue.Enqueue(inTask);
            }
            return;
        }

        public static void Process()
        {
            while (m_TaskQueue.Count > 0) {
                m_TaskQueue.Dequeue().Execute();
            }
            return;
        }

        private static Queue<GLMainThreadTask> m_TaskQueue = new Queue<GLMainThreadTask>();
        private static bool CanProcessSoon => OpenTK.Graphics.GraphicsContext.CurrentContext != null;
    }
}
