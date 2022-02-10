using System.Collections.Generic;

namespace RtCs.OpenGL
{
    class GLMeshRendererDictionary
    {
        public static GLMeshRendererDictionary Instance
            => m_Instance;

        private static GLMeshRendererDictionary m_Instance = new GLMeshRendererDictionary();

        public GLMeshRenderer Query(GLMesh inMesh, GLRenderShaderProgram inShader)
        {
            if (!m_Items.TryGetFirst(out var renderer, r => Equals(r, inMesh, inShader))) {
                renderer = new GLMeshRenderer(inMesh, inShader);
                m_Items.Add(renderer);
            }
            return renderer;
        }

        private bool Equals(GLMeshRenderer inRenderer, GLMesh inMesh, GLRenderShaderProgram inShader)
            => (inRenderer.Mesh == inMesh) && (inRenderer.Shader == inShader);

        private List<GLMeshRenderer> m_Items = new List<GLMeshRenderer>();
    }
}
