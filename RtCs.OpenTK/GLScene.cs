using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public interface IGLScene
    {
        IEnumerable<GLLight> Lights { get; }
    }

    /// <summary>
    /// Represent the world to render.
    /// </summary>
    public class GLScene : IGLScene
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
        public IEnumerable<GLLight> Lights
        { get; set; } = new GLLight[0];

        private GLSceneRenderer m_SceneRenderer = new GLSceneRenderer();
    }
}
