using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    public class GLScene
    {
        public void Render()
            => Render(new GLRenderingStatus());

        public void Render(GLRenderingStatus inStatus)
        {
            if (DisplayList == null) {
                return;
            }

            inStatus.ModelViewMatrix.Model.PushMatrix();
            try {
                inStatus.ModelViewMatrix.Model.LoadIdentity();

                //
                var list = DisplayList.ToList();
                List<(GLRenderObject @object, float distance)> transparents = new List<(GLRenderObject, float)>(list.Count);
                List<GLRenderObject> overlays = new List<GLRenderObject>(list.Count);

                Vector3 viewPosition = inStatus.ModelViewMatrix.View.CurrentMatrix.Inversed.Translation;
                var viewFrustum = new GLViewFrustum(inStatus.ModelViewMatrix.View.CurrentMatrix, inStatus.ProjectionMatrix.CurrentMatrix);

                list.Where(o => CanRender(o)).ForEach(obj => {
                    switch (obj.FrustumCullingMode) {
                        case EGLFrustumCullingMode.BoundingBox:
                            if (!viewFrustum.IsIntersectAABB(obj.BoundingBox)) {
                                return;
                            }
                            break;
                    }

                    switch (obj.RenderLevel) {
                        case EGLRenderLevel.Transparent:
                            transparents.Add((obj, EvaluateObjectDistance(viewPosition, obj)));
                            return;
                        case EGLRenderLevel.Overlay:
                            overlays.Add(obj);
                            return;
                    }

                    obj.Render(inStatus);
                });

                GL.Enable(EnableCap.Blend);
                GL.DepthMask(false);

                transparents.Sort((l, r) => Math.Sign(l.distance - r.distance));
                transparents.Select(item => item.@object).ForEach(obj => {
                    obj.BlendParameters.Apply();
                    obj.Render(inStatus);
                });

                overlays.ForEach(obj => {
                    obj.Render(inStatus);
                });

                GL.Disable(EnableCap.Blend);
                GL.DepthMask(true);

            } finally {
                inStatus.ModelViewMatrix.Model.PopMatrix();
            }
            return;
        }

        public virtual float EvaluateObjectDistance(Vector3 inViewPosition, GLRenderObject inObject)
            => (inObject.BoundingBox.Center - inViewPosition).Length2;

        private bool CanRender(GLRenderObject inObject)
            => inObject.Visible
            && (inObject.Renderer.Material != null)
            && (inObject.Renderer.Mesh != null);

        public IEnumerable<GLRenderObject> DisplayList
        { get; set; } = null;
    }
}
