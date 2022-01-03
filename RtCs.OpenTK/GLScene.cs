using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public interface IGLScene
    {
        GLLightEnvironment Lights { get; }
    }

    /// <summary>
    /// Represent the world to render.
    /// </summary>
    public class GLScene : GLObject, IGLScene
    {
        /// <summary>
        /// Render scene from camera.
        /// </summary>
        /// <param name="inCamera">The camera displaying scene.</param>
        public void Render(GLCamera inCamera)
            => m_SceneRenderer.Render(this, inCamera);

        /// <summary>
        /// The render objects which render in scene.
        /// </summary>
        public GLDisplayList DisplayList
        { get; } = new GLDisplayList();

        /// <summary>
        /// The light objects in scene.
        /// </summary>
        public GLLightEnvironment Lights
        { get; } = new GLLightEnvironment();

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);
            if (inDisposing) {
                m_SceneRenderer.Dispose();
            }
            return;
        }

        private GLSceneRenderer m_SceneRenderer = new GLSceneRenderer();
    }
}
