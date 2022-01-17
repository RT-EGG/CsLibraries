using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    class GLSceneRenderer : GLObject
    {
        public void Render(GLScene inScene, GLCamera inCamera)
        {
            m_ViewFrustum = inCamera.ViewFrustum;

            List<GLRenderObject> displayObjects = new List<GLRenderObject>(inScene.DisplayList.Items.Count);
            displayObjects.AddRange(inScene.DisplayList.Items.Where(o => CanRender(o)));

            m_MatrixBuffer.SetBuffers(inCamera, displayObjects);

            GLShaderStorageBufferObject.ClearBindingBufferBase();

            int viewProjectionMatrixBufferBinding = m_MatrixBuffer.ViewProjectionMatrixBuffer.BindBufferBase();
            int modelMatrixBufferBinding = m_MatrixBuffer.ModelMatrixBuffer.BindBufferBase();
            int modelViewMatrixBufferBinding = m_MatrixBuffer.ModelViewMatrixBuffer.BindBufferBase();
            int modelViewProjectionMatrixBufferBinding = m_MatrixBuffer.ModelViewProjectionMatrixBuffer.BindBufferBase();
            int normalMatrixBufferBinding = m_MatrixBuffer.NormalMatrixBuffer.BindBufferBase();

            inScene.Lights.UpdateBuffers();
            int ambientLightBufferBinding = inScene.Lights.AmbientLightBuffer.BindBufferBase();
            int directionalLightBufferBinding = inScene.Lights.DirectionalLightBuffer.BindBufferBase();
            int pointLightBufferBinding = inScene.Lights.PointLightBuffer.BindBufferBase();
            int spotLightBufferBinding = inScene.Lights.SpotLightBuffer.BindBufferBase();

            foreach (var shader in displayObjects.Select(o => o.Renderer.Material.Shader).Distinct()) {
                TryCommitShaderStorageBuffer(shader, "BuiltIn_ViewProjectionMatrix", viewProjectionMatrixBufferBinding);
                TryCommitShaderStorageBuffer(shader, "BuiltIn_ModelMatrix", modelMatrixBufferBinding);
                TryCommitShaderStorageBuffer(shader, "BuiltIn_ModelViewMatrix", modelViewMatrixBufferBinding);
                TryCommitShaderStorageBuffer(shader, "BuiltIn_ModelViewProjectionMatrix", modelViewProjectionMatrixBufferBinding);
                TryCommitShaderStorageBuffer(shader, "BuiltIn_NormalMatrix", normalMatrixBufferBinding);

                TryCommitShaderStorageBuffer(shader, "BuiltIn_AmbientLight", ambientLightBufferBinding);
                TryCommitShaderStorageBuffer(shader, "BuiltIn_DirectionalLight", directionalLightBufferBinding);
                TryCommitShaderStorageBuffer(shader, "BuiltIn_PointLight", pointLightBufferBinding);
                TryCommitShaderStorageBuffer(shader, "BuiltIn_SpotLight", spotLightBufferBinding);
            }

            var grouped = GroupByRenderLevel(displayObjects);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GroupOptimizedRender(grouped[EGLRenderLevel.Opaque]);
            GL.Enable(EnableCap.Blend);
            GL.DepthMask(false);
            DistanceSortedRender(grouped[EGLRenderLevel.Transparent], inCamera.Transform.WorldPosition);
            GroupOptimizedRender(grouped[EGLRenderLevel.Overlay]);

            GL.DepthMask(true);
            return;
        }

        private void GroupOptimizedRender(IEnumerable<GLRenderObject> inRenderObjects)
        {
            foreach (var shaderGroup in inRenderObjects.GroupBy(o => o.Renderer.Material.Shader)) {
                GLRenderShaderProgram shader = shaderGroup.Key;
                GL.UseProgram(shader.ID);

                foreach (var materialGroup in shaderGroup.GroupBy(o => o.Renderer.Material)) {
                    GLMaterial material = materialGroup.Key;
                    material.CommitProperties();
                    material.BlendParameters.Apply();

                    foreach (var meshGroup in materialGroup.GroupBy(o => o.Renderer.Mesh)) {
                        GLMesh mesh = meshGroup.Key;
                        mesh.BindAttributes(shader.VertexAttribPointers);

                        foreach (var renderObject in meshGroup) {
                            var location = GL.GetUniformLocation(shader.ID, "RenderInstanceID");
                            GL.ProgramUniform1(shader.ID, location, renderObject.RenderInstanceID);

                            GL.PolygonMode(MaterialFace.FrontAndBack, (PolygonMode)renderObject.PolygonMode);
                            renderObject.RenderFaceMode.CullFace();

                            renderObject.FireBeforeRender();
                            switch (mesh.Topology) {
                                case EGLMeshTopology.PatchedTriangles:
                                    GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
                                    break;
                                case EGLMeshTopology.PatchedQuads:
                                    GL.PatchParameter(PatchParameterInt.PatchVertices, 4);
                                    break;
                            }
                            GL.DrawElements(mesh.Topology.ToPrimitiveType(), mesh.Indices.Length, DrawElementsType.UnsignedInt, 0);
                            renderObject.FireAfterRender();
                        }                                                                       
                    }
                }
            }
            return;
        }

        private void DistanceSortedRender(IEnumerable<GLRenderObject> inRenderObjects, Vector3 inDistanceOrigin)
        {
            List<(float distance, GLRenderObject obj)> sorted = new List<(float distance, GLRenderObject obj)>(inRenderObjects.Select(o =>
                    ((inDistanceOrigin - o.Transform.WorldPosition).Length2, o)
                ));


            sorted.Sort((l, r) => Math.Sign(l.distance - r.distance));
            sorted.Select(item => item.obj).ForEach(obj => {
                var material = obj.Renderer.Material;
                var shader = material.Shader;
                var mesh = obj.Renderer.Mesh;
                GL.UseProgram(shader.ID);
                material.CommitProperties();
                material.BlendParameters.Apply();
                mesh.BindAttributes(shader.VertexAttribPointers);

                var location = GL.GetUniformLocation(shader.ID, "RenderInstanceID");
                GL.ProgramUniform1(shader.ID, location, obj.RenderInstanceID);

                GL.PolygonMode(MaterialFace.FrontAndBack, (PolygonMode)obj.PolygonMode);
                obj.RenderFaceMode.CullFace();

                obj.FireBeforeRender();
                GL.DrawElements(mesh.Topology.ToPrimitiveType(), mesh.Indices.Length, DrawElementsType.UnsignedInt, 0);
                obj.FireAfterRender();
            });
            return;
        }

        private Dictionary<EGLRenderLevel, IEnumerable<GLRenderObject>> GroupByRenderLevel(IEnumerable<GLRenderObject> inObjects)
        {
            var result = new Dictionary<EGLRenderLevel, IEnumerable<GLRenderObject>>();
            var grouped = inObjects.GroupBy(o => o.Renderer.Material.RenderLevel);

            foreach (EGLRenderLevel level in Enum.GetValues(typeof(EGLRenderLevel))) {
                var group = grouped.FirstOrDefault(g => g.Key == level);
                if (group == null) {
                    result.Add(level, new GLRenderObject[0]);
                } else {
                    result.Add(level, group);
                }
            }

            return result;
        }

        private void TryCommitUniformBuffer(GLShaderProgram inProgram, string inUniformName, int inBindingPoint)
        {
            if (inProgram.UniformBlockSockets.TryGetFirst(out var socket, s => s.Name == inUniformName)) {
                GL.UniformBlockBinding(inProgram.ID, socket.Binding, inBindingPoint);
            }
            return;
        }

        private void TryCommitShaderStorageBuffer(GLShaderProgram inProgram, string inBufferName, int inBindingPoiont)
        {
            if (inProgram.TryGetShaderStorageBufferBindPoint(inBufferName, out int binding)) {
                GL.ShaderStorageBlockBinding(inProgram.ID, binding, inBindingPoiont);
            }
            return;
        }

        private bool CanRender(GLRenderObject inObject)
        {
            if (!inObject.Visible) {
                return false;
            }
            if ((inObject.Renderer.Material == null) || (inObject.Renderer.Mesh == null)) {
                return false;
            }
            if (inObject.Renderer.Material.Shader == null) {
                return false;
            }
            switch (inObject.FrustumCullingMode) {
                case EGLFrustumCullingMode.BoundingBox:
                    if (!m_ViewFrustum.IsIntersectAABB(inObject.BoundingBox)) {
                        return false;
                    }
                    break;
            }
            return true;
        }

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);
            if (inDisposing) {
                m_MatrixBuffer.Dispose();
            }
            return;
        }

        private GLTransformMatrixBuffer m_MatrixBuffer = new GLTransformMatrixBuffer();
        private GLViewFrustum m_ViewFrustum = default;
    }
}
