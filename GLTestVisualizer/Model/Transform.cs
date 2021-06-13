using RtCs.MathUtils;

namespace GLTestVisualizer.Model
{
    public class Transform
    {
        public Vector3 LocalTranslation
        {
            get => new Vector3(m_TransRotMatrix.m03, m_TransRotMatrix.m13, m_TransRotMatrix.m23);
            set => m_TransRotMatrix = Matrix4x4.MakeTranslate(value) * Matrix4x4.MakeRotate(LocalRotation);
        }
        public Quaternion LocalRotation
        {
            get => m_TransRotMatrix.Rotation;
            set => m_TransRotMatrix = Matrix4x4.MakeTranslate(LocalTranslation) * Matrix4x4.MakeRotate(value);
        }        
        public Vector3 LocalScale
        {
            get => new Vector3(m_ScaleMatrix.m00, m_ScaleMatrix.m11, m_ScaleMatrix.m22);
            set => m_ScaleMatrix = Matrix4x4.MakeScale(value);
        }

        private Matrix4x4 m_TransRotMatrix = Matrix4x4.Identity;
        private Matrix4x4 m_ScaleMatrix = Matrix4x4.Identity;
    }
}
