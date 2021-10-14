using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.MathUtils.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    [Flags]
    public enum EGLRenderPolygonMode
    {
        Face = PolygonMode.Fill,
        Line = PolygonMode.Line,
        Point = PolygonMode.Point
    }

    public enum EGLFrustumCullingMode
    {
        AlwaysRender,
        BoundingBox,
        //BoundingSphere
    }

    [Flags]
    public enum EGLRenderFaceMode
    {
        Front = 0x01,
        Back = 0x02,
        FrontAndBack = Front | Back
    }

    public class GLRenderObject : GLObject, ILineIntersectable3D
    {
        public void Render(GLRenderingStatus inRenderingStatus)
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, (PolygonMode)PolygonMode);
            CullFace(RenderFaceMode);

            inRenderingStatus.ModelViewMatrix.Model.PushMatrix();
            try {
                inRenderingStatus.ModelViewMatrix.Model.MultiMatrix(Transform.WorldMatrix);

                PreRender(inRenderingStatus);
                Renderer.Render(inRenderingStatus);
                PostRender(inRenderingStatus);

            } finally {
                inRenderingStatus.ModelViewMatrix.Model.PopMatrix();
            }
            return;
        }

        public void CalculateBoundingBox()
        {
            var vertices = Renderer?.Mesh?.Positions;
            if (vertices.IsNullOrEmpty()) {
                BoundingBox = new AABB3D();
                return;
            }

            var matrix = Transform.WorldMatrix;
            BoundingBox = AABB3D.InclusionBoundary(vertices.Select(v => new Vector3(matrix * new Vector4(v, 1.0))));
            return;
        }

        public bool Visible
        { get; set; } = true;

        // TODO
        // safe access to GLRenderObject.RenderLevel
        public virtual EGLRenderLevel RenderLevel
            => Renderer.Material.RenderLevel;
        // TODO
        // safe access to GLRenderObject.BlendParameters
        public virtual IGLBlendParameters BlendParameters
            => Renderer.Material.BlendParameters;
        public EGLFrustumCullingMode FrustumCullingMode
        { get; set; } = EGLFrustumCullingMode.BoundingBox;

        protected virtual void PreRender(GLRenderingStatus inStatus)
        { }
        protected virtual void PostRender(GLRenderingStatus inStatus)
        { }

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
            inLine.Point0 = new Vector3(world2local.Multiply(inLine.Point0, 1.0));
            inLine.Point1 = new Vector3(world2local.Multiply(inLine.Point1, 1.0));
            foreach (var info in Renderer.Mesh.IsIntersectWith(inLine)) {
                info.HitObject = this;
                info.Position = local2world.Multiply(info.Position, 1.0);
                if (info.Normal.HasValue) {
                    info.Normal = local2world.Multiply(info.Normal.Value, 0.0);
                }
                yield return info;
            }
        }

        public AABB3D BoundingBox
        { get; set; } = new AABB3D();

        public EGLRenderPolygonMode PolygonMode
        { get; set; } = EGLRenderPolygonMode.Face;

        public EGLRenderFaceMode RenderFaceMode
        { get; set; } = EGLRenderFaceMode.Front;

        public Transform Transform
        { get; } = new Transform();
        public GLRenderer Renderer
        { get; } = new GLRenderer();
    }
}
