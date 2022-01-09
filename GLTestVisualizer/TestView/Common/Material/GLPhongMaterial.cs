using RtCs.MathUtils;
using RtCs.OpenGL;

namespace GLTestVisualizer.TestView
{
    class GLPhongMaterial : GLMaterial
    {
        public GLPhongMaterial()
        {
            base.Shader = GLRenderShaderProgram.Preset.Phong;
            return;
        }

        protected override void CommitPropertiesCore()
        {
            SetVariableValue("inAmbient", Ambient);
            SetVariableValue("inDiffuse", Diffuse);
            SetVariableValue("inSpecular", Specular);
            SetVariableValue("inEmission", Emission);
            SetVariableValue("inShininess", Shininess);

            base.CommitPropertiesCore();
            return;
        }

        
        public Vector3 Ambient
        { get; set; } = new Vector3(0.25f);
        public Vector3 Diffuse
        { get; set; } = new Vector3(0.25f);
        public Vector3 Specular
        { get; set; } = new Vector3(0.75f);
        public Vector3 Emission
        { get; set; } = new Vector3(0.0f);
        public float Shininess
        { get; set; } = 10.0f;

        private new GLRenderShaderProgram Shader => base.Shader;
    }
}
