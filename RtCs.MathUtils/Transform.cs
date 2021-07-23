using System.Collections.Generic;

namespace RtCs.MathUtils
{
    public interface ITransform
    {
        Matrix4x4 LocalMatrix { get; }
    }

    public class Transform : ITransform
    {
        public Transform Parent
        {
            set {
                if (m_Parent != null) {
                    m_Parent.m_Children.Remove(this);
                }
                m_Parent = value;
                if (m_Parent != null) {
                    m_Parent.m_Children.Add(this);
                }
                return;
            }
            get => m_Parent;
        }

        public IReadOnlyList<Transform> Children => m_Children;

        public void LookAt(Vector3 inCenter, Vector3 inTarget, Vector3 inUp)
            => m_PosRotMatrix = Matrix4x4.MakeLookAt(inCenter, inTarget, inUp).Inversed;

        public Matrix4x4 LocalMatrix
            => m_PosRotMatrix * m_ScaleMatrix;
        public Matrix4x4 WorldMatrix
            => (Parent == null) ? LocalMatrix : Parent.WorldMatrix * LocalMatrix;

        private Matrix4x4 WorldRotateMatrix
            => ((Parent == null) ? m_PosRotMatrix : Parent.WorldRotateMatrix * m_PosRotMatrix)
                .SetElements((0, 3, 0.0), (1, 3, 0.0), (2, 3, 0.0));

        public Vector3 WorldPosition => WorldMatrix.Translation;
        public Quaternion WorldRotation => WorldRotateMatrix.Rotation;

        public Vector3 LocalPosition
        {
            get => new Vector3(m_PosRotMatrix.m03, m_PosRotMatrix.m13, m_PosRotMatrix.m23);
            set => m_PosRotMatrix = Matrix4x4.MakeTranslate(value) * Matrix4x4.MakeRotate(LocalRotation);
        }
        public Quaternion LocalRotation
        {
            get => m_PosRotMatrix.Rotation;
            set => m_PosRotMatrix = Matrix4x4.MakeTranslate(LocalPosition) * Matrix4x4.MakeRotate(value);
        }
        public Vector3 LocalScale
        {
            get => new Vector3(m_ScaleMatrix.m00, m_ScaleMatrix.m11, m_ScaleMatrix.m22);
            set => m_ScaleMatrix = Matrix4x4.MakeScale(value);
        }

        private Transform m_Parent = null;
        private List<Transform> m_Children = new List<Transform>();
        private Matrix4x4 m_PosRotMatrix = Matrix4x4.Identity;
        private Matrix4x4 m_ScaleMatrix = Matrix4x4.Identity;
    }
}
