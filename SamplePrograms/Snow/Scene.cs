using RtCs.MathUtils;
using RtCs.OpenGL;

namespace Snow
{
    class Scene : GLScene
    {
        public Scene()
        {
            m_SnowCover.Transform.LocalScale = new Vector3(5.0f, 5.0f, 1.0f);
            m_SnowCover.Transform.LocalRotation = Quaternion.FromEuler((-90.0f).DegToRad(), 0.0f, 0.0f, EEulerRotationOrder.YXZ);
            m_SnowCover.RegisterToDisplayList(DisplayList);

            m_SnowFall.RegisterToDisplayList(DisplayList);

            Lights.AmbientLight.Color = new ColorRGB(255, 255, 255);
            Lights.AmbientLight.Intensity = 0.75f;

            m_Light.Direction = new Vector3(-1.0f).Normalized;
            m_Light.Color = new ColorRGB(255, 255, 192);
            m_Light.Intensity = 0.5f;
            Lights.DirectionalLights.Add(m_Light);
            return;
        }

        public void TimeStep(float inTimeStepInSec)
        {
            m_SnowFall.TimeStep(inTimeStepInSec, m_SnowCover);
        }

        public SceneObject.SnowCover.Model SnowCover => m_SnowCover;

        private SceneObject.SnowCover.Model m_SnowCover = new SceneObject.SnowCover.Model();
        private SceneObject.SnowFall.Model m_SnowFall = new SceneObject.SnowFall.Model();

        private GLDirectionalLight m_Light = new GLDirectionalLight();
    }
}
