using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    public interface IGLScene
    {
        IEnumerable<GLLight> Lights { get; }
    }

    /// <summary>
    /// Represent the world to render.
    /// </summary>
    public class GLScene : IGLScene
    {
        public void Render()
            => Render(new GLRenderParameter());

        /// <summary>
        /// Execute rendering command.
        /// </summary>
        /// <param name="inParameter">Rendering data store.</param>
        public void Render(GLRenderParameter inParameter)
        {
            if (DisplayList == null) {
                return;
            }

            inParameter.ModelViewMatrix.Model.PushMatrix();
            try {
                inParameter.ModelViewMatrix.Model.LoadIdentity();

                //
                var list = DisplayList.Items;
                List<(GLRenderObject @object, float distance)> transparents = new List<(GLRenderObject, float)>(list.Count);
                List<GLRenderObject> overlays = new List<GLRenderObject>(list.Count);

                Vector3 viewPosition = inParameter.ModelViewMatrix.View.CurrentMatrix.Inversed.Translation;
                var viewFrustum = new GLViewFrustum(inParameter.ModelViewMatrix.View.CurrentMatrix, inParameter.ProjectionMatrix.CurrentMatrix);

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

                    obj.Render(inParameter);
                });

                GL.Enable(EnableCap.Blend);
                GL.DepthMask(false);

                transparents.Sort((l, r) => Math.Sign(l.distance - r.distance));
                transparents.Select(item => item.@object).ForEach(obj => {
                    obj.BlendParameters.Apply();
                    obj.Render(inParameter);
                });

                overlays.ForEach(obj => {
                    obj.Render(inParameter);
                });

                GL.Disable(EnableCap.Blend);
                GL.DepthMask(true);

            } finally {
                inParameter.ModelViewMatrix.Model.PopMatrix();
            }
            return;
        }

        public virtual float EvaluateObjectDistance(Vector3 inViewPosition, GLRenderObject inObject)
            => (inObject.BoundingBox.Center - inViewPosition).Length2;

        private bool CanRender(GLRenderObject inObject)
            => inObject.Visible
            && (inObject.Renderer.Material != null)
            && (inObject.Renderer.Mesh != null);

        /// <summary>
        /// The render objects which render in scene.
        /// </summary>
        public GLDisplayList DisplayList
        { get; } = new GLDisplayList();

        /// <summary>
        /// The light objects in scene.
        /// </summary>
        public IEnumerable<GLLight> Lights
        { get; set; } = new GLLight[0];
    }
}
