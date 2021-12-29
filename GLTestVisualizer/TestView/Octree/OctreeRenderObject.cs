using RtCs;
using RtCs.MathUtils;
using RtCs.MathUtils.Geometry;
using RtCs.OpenGL;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GLTestVisualizer.TestView.Octree
{
    class OctreeRenderObject : Transform, IEnumerable<GLRenderObject>, IDisposable
    {
        public void SetupForOctree(RtCs.MathUtils.Geometry.Octree inOctree)
        {
            int div = 2.Pow(inOctree.MaxLevel);

            m_Materials = new GLTransColorMaterial[inOctree.MaxLevel + 1];
            for (int i = 0; i <= inOctree.MaxLevel; ++i) {
                m_Materials[i] = new GLTransColorMaterial() {
                    Color = CellColors[i]
                };

                float size = inOctree.CellDimensions[i];
                int i2 = 2.Pow(i);
                int step = div / i2;
                for (int x = 0; x < i2; ++x) {
                    for (int y = 0; y < i2; ++y) {
                        for (int z = 0; z < i2; ++z) {
                            Vector3 position = new Vector3 (
                                (x * size) + (size * 0.5f),
                                (y * size) + (size * 0.5f),
                                (z * size) + (size * 0.5f)
                            );

                            GLRenderObject cellRenderer = new GLRenderObject();
                            cellRenderer.Renderer.Mesh = m_CellMesh;
                            cellRenderer.Renderer.Material = m_Materials[i];
                            cellRenderer.Transform.Parent = this;
                            cellRenderer.Transform.LocalPosition = position;
                            cellRenderer.Transform.LocalScale = new Vector3(size);

                            m_CellRenderers.Add(inOctree.GetCellAt(i, new Container3<int>(x * step, y * step, z * step)), cellRenderer);
                        }
                    }
                }
            }

            div = 2.Pow(2);
            m_GridMesh = GLPrimitiveMesh.CreateGrid(new Vector3(inOctree.Dimensions), div, div, div);
            m_GridRenderer.Renderer.Mesh = m_GridMesh;
            m_GridRenderer.Renderer.Material = m_GridMaterial;
            m_GridRenderer.Transform.Parent = this;
            return;
        }

        public void SetCellVisibility(IOctreeCell inCell, bool inVisible)
            => m_CellRenderers[inCell].Visible = inVisible;

        IEnumerator<GLRenderObject> IEnumerable<GLRenderObject>.GetEnumerator()
            => m_CellRenderers.Values.Concat(m_GridRenderer).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => m_CellRenderers.Values.Concat(m_GridRenderer).GetEnumerator();

        void IDisposable.Dispose()
        {
            m_CellRenderers.Clear();
            m_Materials.DisposeItems();
            m_CellMesh.Dispose();

            m_Materials = null;
            m_CellMesh = null;
            return;
        }

        private GLMesh m_CellMesh = GLPrimitiveMesh.CreateBox(1.0f, 1.0f, 1.0f);
        private GLMesh m_GridMesh = new GLMesh();
        private GLColorMaterial m_GridMaterial = new GLColorMaterial() { Color = new Vector4(1.0f) };
        private Dictionary<IOctreeCell, GLRenderObject> m_CellRenderers = new Dictionary<IOctreeCell, GLRenderObject>();
        private GLRenderObject m_GridRenderer = new GLRenderObject();
        private readonly Vector4[] CellColors = new Vector4[] {
            new Vector4(0.8f, 0.8f, 0.8f, 0.5f),
            new Vector4(0.8f, 0.0f, 0.0f, 0.5f),
            new Vector4(0.0f, 0.8f, 0.0f, 0.5f),
            new Vector4(0.0f, 0.0f, 0.8f, 0.5f),
            new Vector4(0.8f, 0.8f, 0.0f, 0.5f),
            new Vector4(0.0f, 0.8f, 0.8f, 0.5f),
            new Vector4(0.8f, 0.0f, 0.8f, 0.5f),
            new Vector4(1.0f, 1.0f, 1.0f, 0.5f),
        };
        private GLTransColorMaterial[] m_Materials = null;
    }
}
