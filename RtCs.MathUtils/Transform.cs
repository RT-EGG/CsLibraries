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

        public Matrix4x4 LocalMatrix
            => m_PosRotMatrix * m_ScaleMatrix;
        public Matrix4x4 WorldMatrix
            => (Parent == null) ? LocalMatrix : Parent.WorldMatrix * LocalMatrix;

        public Matrix4x4 LocalToWorldMatrix => WorldMatrix;
        public Matrix4x4 WorldToLocalMatrix => LocalToWorldMatrix.Inversed;

        private Matrix4x4 WorldRotateMatrix
            => ((Parent == null) ? m_PosRotMatrix : Parent.WorldRotateMatrix * m_PosRotMatrix)
                .SetElements((0, 3, 0.0f), (1, 3, 0.0f), (2, 3, 0.0f));

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

        /// <summary>
        /// Set rotation to look at the position.
        /// </summary>
        /// <param name="inLookAtPosition">The position of look at in world coordinate.</param>
        /// <remarks>
        /// This method is same that LookAt(inLookAtPosition, new Vector3(0.0f, 1.0f, 0.0f)).
        /// </remarks>
        public void LookAt(Vector3 inLookAtPosition)
            => LookAt(inLookAtPosition, new Vector3(0.0f, 1.0f, 0.0f));

        /// <summary>
        /// Set rotation to look at the position, and rotate to point its up diretion in the direction hinted by the inHeadDirection.
        /// </summary>
        /// <param name="inLookAtPosition">The position of look at in world coordinate.</param>
        /// <param name="inHeadDirection">The direction of head in world coordinate.</param>
        /// <remarks>
        /// The up vector of the rotation will only match the inHeadDirection vector if the forward direction is perpendicular to inHeadDirection.
        /// </remarks>
        public void LookAt(Vector3 inLookAtPosition, Vector3 inHeadDirection)
        {
            if (Parent != null) {
                var w2l = WorldToLocalMatrix;
                inLookAtPosition = Matrix4x4.Multiply(w2l, inLookAtPosition, 1.0f).XYZ;
                inHeadDirection = Matrix4x4.Multiply(w2l, inHeadDirection, 0.0f).XYZ;
            }
            m_PosRotMatrix = Matrix4x4.MakeLookAtMatrix(LocalPosition, inLookAtPosition, inHeadDirection);
            return;
        }

        private Transform m_Parent = null;
        private List<Transform> m_Children = new List<Transform>();
        private Matrix4x4 m_PosRotMatrix = Matrix4x4.Identity;
        private Matrix4x4 m_ScaleMatrix = Matrix4x4.Identity;
    }
}
