using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    public class GLShaderStorageBufferObject : GLBufferObject
    {
        public static void ClearBindingBufferBase()
            => m_NextBufferBaseBinding = 0;

        public int BindBufferBase()
        {
            GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, m_NextBufferBaseBinding, ID);
            return m_NextBufferBaseBinding++;        
        }

        private static int m_NextBufferBaseBinding = 0;
    }
}
