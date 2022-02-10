using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.MathUtils.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    /// <summary>
    /// Value to decide render mesh element.
    /// </summary>
    [Flags]
    public enum EGLRenderPolygonMode
    {
        Face = PolygonMode.Fill,
        Line = PolygonMode.Line,
        Point = PolygonMode.Point
    }

    /// <summary>
    /// How to do frustum culling test.
    /// </summary>
    public enum EGLFrustumCullingMode
    {
        /// <summary>
        /// No frustum test.
        /// </summary>
        AlwaysRender,
        /// <summary>
        /// Frustum test by RenderObject.BoundingBox.
        /// </summary>
        BoundingBox,
    }

    /// <summary>
    /// Value to decide face culling.
    /// </summary>
    [Flags]
    public enum EGLRenderFaceMode
    {
        Front = 0x01,
        Back = 0x02,
        FrontAndBack = Front | Back
    }


    public static class GLRenderFaceModeExtensions
    {
        public static void CullFace(this EGLRenderFaceMode inValue)
        {
            switch (inValue) {
                case EGLRenderFaceMode.Back:
                    GL.Enable(EnableCap.CullFace);
                    GL.CullFace(CullFaceMode.Front);
                    break;
                case EGLRenderFaceMode.Front:
                    GL.Enable(EnableCap.CullFace);
                    GL.CullFace(CullFaceMode.Back);
                    break;
                case EGLRenderFaceMode.FrontAndBack:
                    GL.Disable(EnableCap.CullFace);
                    break;
                default:
                    throw new InvalidEnumValueException<EGLRenderFaceMode>(inValue);
            }
        }
    }
    

/// <summary>
/// The object to render from transform and renderer.
/// </summary>
public class GLRenderObject : GLObject, ILineIntersectable3D
{
        /// <summary>
        /// Update BoundingBox by Renderer.Mesh.Vertices.
        /// </summary>
        public void CalculateBoundingBox()
        {
            var vertices = Renderer?.Mesh?.Vertices;
            if (vertices.IsNullOrEmpty()) {
                BoundingBox = new AABB3D();
                return;
            }

            var matrix = Transform.WorldMatrix;
            BoundingBox = AABB3D.InclusionBoundary(vertices.Select(v => new Vector3(matrix * new Vector4(v, 1.0f))));
            return;
        }

        /// <summary>
        /// Flag whether do render.
        /// </summary>
        public bool Visible
        { get; set; } = true;

        /// <summary>
        /// How to do frustum culling test.
        /// </summary>
        public EGLFrustumCullingMode FrustumCullingMode
        { get; set; } = EGLFrustumCullingMode.BoundingBox;

        public event EventHandler BeforeRender;
        public event EventHandler AfterRender;

        private void CullFace(EGLRenderFaceMode inMode)
        {
            switch (inMode) {
                case EGLRenderFaceMode.Back:
                    GL.Enable(EnableCap.CullFace);
                    GL.CullFace(CullFaceMode.Front);
                    break;
                case EGLRenderFaceMode.Front:
                    GL.Enable(EnableCap.CullFace);
                    GL.CullFace(CullFaceMode.Back);
                    break;
                case EGLRenderFaceMode.FrontAndBack:
                    GL.Disable(EnableCap.CullFace);
                    break;
            }
        }

        public IEnumerable<LineIntersectionInfo3D> IsIntersectWith(Line3D inLine)
        {
            if (Renderer.Mesh == null) {
                yield break;
            }

            Matrix4x4 local2world = Transform.WorldMatrix;
            Matrix4x4 world2local = local2world.Inversed;
            inLine.Point0 = new Vector3(world2local.Multiply(inLine.Point0, 1.0f));
            inLine.Point1 = new Vector3(world2local.Multiply(inLine.Point1, 1.0f));
            foreach (var info in Renderer.Mesh.IsIntersectWith(inLine)) {
                info.HitObject = this;
                info.Position = local2world.Multiply(info.Position, 1.0f).XYZ;
                if (info.Normal.HasValue) {
                    info.Normal = local2world.Multiply(info.Normal.Value, 0.0f).XYZ;
                }
                yield return info;
            }
        }

        /// <summary>
        /// Bounds used for various culling.
        /// </summary>
        public AABB3D BoundingBox
        { get; set; } = new AABB3D();

        /// <summary>
        /// Flag to decide render mesh element.
        /// </summary>
        public EGLRenderPolygonMode PolygonMode
        { get; set; } = EGLRenderPolygonMode.Face;

        /// <summary>
        /// Flag to decide face culling.
        /// </summary>
        public EGLRenderFaceMode RenderFaceMode
        { get; set; } = EGLRenderFaceMode.Front;

        /// <summary>
        /// Object's transform.
        /// </summary>
        public Transform Transform
        { get; } = new Transform();
        /// <summary>
        /// Renderer object following the object.
        /// </summary>
        public GLRenderer Renderer
        { get; set; } = new GLRenderer();

        internal void FireBeforeRender()
            => BeforeRender?.Invoke(this, EventArgs.Empty);
        internal void FireAfterRender()
            => AfterRender?.Invoke(this, EventArgs.Empty);
        internal int RenderInstanceID
        { get; set; } = -1;
    }
}
