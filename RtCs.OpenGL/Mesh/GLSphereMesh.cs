using RtCs.MathUtils;
using System;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public partial class GLPrimitiveMesh
    {
        /// <summary>
        /// Create uv sphere mesh.
        /// </summary>
        /// <param name="inSlices">Number of vertices divide horizontal direction. It must be at least 3.</param>
        /// <param name="inStacks">Number of vertices divide vertical direction. It must be at least 3.</param>
        /// <returns>Created mesh object.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The case that inSlices is less than 3 or inStacks is less than 3.</exception>
        /// <remarks>
        /// Returned mesh has setup Vertices and Normals.
        /// </remarks>
        public static GLMesh CreateSphereUV(int inSlices, int inStacks)
        {
            if ((inSlices < 3) || (inStacks < 3)) {
                throw new ArgumentOutOfRangeException($"CreateSphereUV(inSlices: {inSlices}, inStacks: {inStacks});{Environment.NewLine}" +
                                                      $"{nameof(inSlices)} and {nameof(inStacks)} must be greater than or equals 3.");
            }

            List<Vector3> positions = new List<Vector3>((inSlices * (inStacks - 2)) + 2);
            List<int> indices = new List<int>();

            float uStep = (float)((Math.PI * 2.0) / inSlices);
            float vStep = (float)((Math.PI) / (inStacks - 1));

            List<float> latRadius = new List<float>();
            for (int i = 0; i <= inSlices; ++i) {
                latRadius.Add((float)Math.Sin(vStep * i));
            }

            positions.Add(new Vector3(0.0f, 1.0f, 0.0f)); // top
            for (int v = 1; v < (inStacks - 1); ++v) {
                float vThete = vStep * v;
                float y = (float)Math.Cos(vThete);
                for (int u = 0; u < inSlices; ++u) {
                    float uThete = uStep * u;

                    positions.Add(new Vector3(latRadius[v] * (float)Math.Sin(uThete), y, latRadius[v] * (float)Math.Cos(uThete)));
                }
            }
            positions.Add(new Vector3(0.0f, -1.0f, 0.0f)); // bottom

            int startIndex = 1;
            // top
            for (int i = 0; i < inSlices; ++i) {
                indices.Add(0, startIndex + i, startIndex + ((i + 1) % inSlices));
            }

            // side
            startIndex = 1;
            for (int v = 0; v < (inStacks - 3); ++v) {
                for (int u = 0; u < inSlices; ++u) {
                    int i00 = 1 + (v * inSlices) + u;
                    int i01 = 1 + (v * inSlices) + ((u + 1) % inSlices);
                    int i10 = i00 + inSlices;
                    int i11 = i01 + inSlices;

                    indices.Add(i00, i10, i11);
                    indices.Add(i00, i11, i01);
                }
            }

            // bottom
            startIndex = positions.Count - inSlices - 1;
            int endIndex = positions.Count - 1;
            for (int i = 0; i < inSlices; ++i) {
                indices.Add(endIndex, startIndex + ((i + 1) % inSlices), startIndex + i);
            }

            return CreateSphereMesh(positions, indices);
        }

        /// <summary>
        /// Create ico (icosahedron) sphere mesh.
        /// </summary>
        /// <param name="inSubdivision">Number of recursive division.</param>
        /// <returns>Created mesh object.</returns>
        /// <remarks>
        /// Returned mesh has setup Vertices and Normals.
        /// There is no limit to inSubdivision. However, since the memory used will increase enormously, it is realistic to set the upper limit at around 8.
        /// </remarks>
        public static GLMesh CreateSphereICO(int inSubdivision)
        {
            List<Vector3> positions = new List<Vector3>(12);
            List<int> indices = new List<int>();

            // create 12 vertices of a icosahedron
            float t = (1.0f + (float)Math.Sqrt(5.0f)) / 2.0f;
            positions.Add(new Vector3(-1.0f, t, 0.0f));
            positions.Add(new Vector3(1.0f, t, 0.0f));
            positions.Add(new Vector3(-1.0f, -t, 0.0f));
            positions.Add(new Vector3(1.0f, -t, 0.0f));

            positions.Add(new Vector3(0.0f, -1.0f, t));
            positions.Add(new Vector3(0.0f, 1.0f, t));
            positions.Add(new Vector3(0.0f, -1.0f, -t));
            positions.Add(new Vector3(0.0f, 1.0f, -t));

            positions.Add(new Vector3(t, 0.0f, -1.0f));
            positions.Add(new Vector3(t, 0.0f, 1.0f));
            positions.Add(new Vector3(-t, 0.0f, -1.0f));
            positions.Add(new Vector3(-t, 0.0f, 1.0f));

            Dictionary<(int, int), int> midPointIndexCache = new Dictionary<(int, int), int>();

            int GetMidPoint(int inIndex0, int inIndex1)
            {
                if (!midPointIndexCache.TryGetValue((inIndex0, inIndex1), out var result)) {
                    result = positions.Count;
                    var newPosition = (positions[inIndex0] + positions[inIndex1]) * 0.5f;
                    positions.Add(newPosition);

                    midPointIndexCache.Add((inIndex0, inIndex1), result);
                    midPointIndexCache.Add((inIndex1, inIndex0), result);
                }
                return result;
            }

            void AddSubdivided(int inSubdivStep, int inIndex0, int inIndex1, int inIndex2)
            {
                if (inSubdivStep >= inSubdivision) {
                    indices.Add(inIndex0, inIndex1, inIndex2);

                } else {
                    int index01 = GetMidPoint(inIndex0, inIndex1);
                    int index12 = GetMidPoint(inIndex1, inIndex2);
                    int index20 = GetMidPoint(inIndex2, inIndex0);
                    int nextStep = inSubdivStep + 1;

                    AddSubdivided(nextStep, inIndex0,  index01,  index20);
                    AddSubdivided(nextStep,  index01, inIndex1,  index12);
                    AddSubdivided(nextStep,  index20,  index12, inIndex2);
                    AddSubdivided(nextStep,  index01,  index12,  index20);
                }
                return;
            }

            AddSubdivided(0, 0, 11, 5);
            AddSubdivided(0, 0, 5, 1);
            AddSubdivided(0, 0, 1, 7);
            AddSubdivided(0, 0, 7, 10);
            AddSubdivided(0, 0, 10, 11);

            AddSubdivided(0, 1, 5, 9);
            AddSubdivided(0, 5, 11, 4);
            AddSubdivided(0, 11, 10, 2);
            AddSubdivided(0, 10, 7, 6);
            AddSubdivided(0, 7, 1, 8);

            AddSubdivided(0, 3, 9, 4);
            AddSubdivided(0, 3, 4, 2);
            AddSubdivided(0, 3, 2, 6);
            AddSubdivided(0, 3, 6, 8);
            AddSubdivided(0, 3, 8, 9);

            AddSubdivided(0, 4, 9, 5);
            AddSubdivided(0, 2, 4, 11);
            AddSubdivided(0, 6, 2, 10);
            AddSubdivided(0, 8, 6, 7);
            AddSubdivided(0, 9, 8, 1);

            for (int i = 0; i < positions.Count; ++i) {
                positions[i] = positions[i].Normalized;
            }

            return CreateSphereMesh(positions, indices);
        }

        /// <summary>
        /// Create rounded cube sphere mesh.
        /// </summary>
        /// <param name="inSubdivision">Amount of divissions per cube face.</param>
        /// <returns>Created mesh object.</returns>
        /// <remarks>
        /// Returned mesh has setup Vertices and Normals.
        /// </remarks>
        public static GLMesh CreateSphereRoundedCube(int inSubdivision)
        {
            inSubdivision += 2;

            List<Vector3> positions = new List<Vector3>();
            List<int> indices = new List<int>();

            positions.Add(new Vector3(-1.0f, -1.0f, -1.0f)); // -x, -y, -z
            positions.Add(new Vector3(-1.0f, -1.0f,  1.0f)); // -x, -y, +z
            positions.Add(new Vector3(-1.0f,  1.0f, -1.0f)); // -x, +y, -z
            positions.Add(new Vector3(-1.0f,  1.0f,  1.0f)); // -x, +y, +z
            positions.Add(new Vector3( 1.0f, -1.0f, -1.0f)); // +x, -y, -z
            positions.Add(new Vector3( 1.0f, -1.0f,  1.0f)); // +x, -y, +z
            positions.Add(new Vector3( 1.0f,  1.0f, -1.0f)); // +x, +y, -z
            positions.Add(new Vector3( 1.0f,  1.0f,  1.0f)); // +x, +y, +z

            Dictionary<(int, int, int), int> divPointIndexCache = new Dictionary<(int, int, int), int>();
            void RegisterInitPair(int inIndex0, int inIndex1)
            {
                divPointIndexCache.Add((inIndex0, inIndex1, 0), inIndex0); divPointIndexCache.Add((inIndex0, inIndex1, inSubdivision - 1), inIndex1);
                divPointIndexCache.Add((inIndex1, inIndex0, 0), inIndex1); divPointIndexCache.Add((inIndex1, inIndex0, inSubdivision - 1), inIndex0);
            }
            // link with -x, -y, -z
            RegisterInitPair(0, 1);
            RegisterInitPair(0, 2);
            RegisterInitPair(0, 4);
            // link with +x, -y, +z
            RegisterInitPair(5, 1);
            RegisterInitPair(5, 4);
            RegisterInitPair(5, 7);
            // link with -x, +y, +z
            RegisterInitPair(3, 1);
            RegisterInitPair(3, 2);
            RegisterInitPair(3, 7);
            // link with +x, +y, -z
            RegisterInitPair(6, 2);
            RegisterInitPair(6, 4);
            RegisterInitPair(6, 7);

            int GetIndexAt(int inIndex0, int inIndex1, int t)
            {
                if (!divPointIndexCache.TryGetValue((inIndex0, inIndex1, t), out int result)) {
                    result = positions.Count;
                    divPointIndexCache.Add((inIndex0, inIndex1, t), result);
                    divPointIndexCache.Add((inIndex1, inIndex0, inSubdivision - t - 1), result);

                    float delta = t / ((float)inSubdivision - 1.0f);
                    positions.Add((positions[inIndex0] * (1.0f - delta)) + (positions[inIndex1] * delta));
                }
                return result;
            }

            void SubdivideLoop(int inIndex0, int inIndex1, int inIndex2, int inIndex3)
            {
                for (int v = 0; v < inSubdivision - 1; ++v) {
                    int index01_0 = GetIndexAt(inIndex0, inIndex1, v + 0);
                    int index01_1 = GetIndexAt(inIndex0, inIndex1, v + 1);
                    int index32_0 = GetIndexAt(inIndex3, inIndex2, v + 0);
                    int index32_1 = GetIndexAt(inIndex3, inIndex2, v + 1);

                    for (int u = 0; u < inSubdivision - 1; ++u) {
                        int i00 = GetIndexAt(index01_0, index32_0, u + 0);
                        int i01 = GetIndexAt(index01_0, index32_0, u + 1);
                        int i10 = GetIndexAt(index01_1, index32_1, u + 0);
                        int i11 = GetIndexAt(index01_1, index32_1, u + 1);

                        indices.Add(i00, i10, i11);
                        indices.Add(i00, i11, i01);
                    }
                }
            }

            SubdivideLoop(0, 1, 3, 2); // -x
            SubdivideLoop(7, 5, 4, 6); // +x
            SubdivideLoop(0, 4, 5, 1); // -y
            SubdivideLoop(2, 3, 7, 6); // +y
            SubdivideLoop(0, 2, 6, 4); // -z
            SubdivideLoop(1, 5, 7, 3); // +z

            for (int i = 0; i < positions.Count; ++i) {
                positions[i] = positions[i].Normalized;
            }

            return CreateSphereMesh(positions, indices);
        }

        private static GLMesh CreateSphereMesh(List<Vector3> inPositions, List<int> inIndices)
        {
            GLMesh mesh = new GLMesh();
            mesh.Topology = EGLMeshTopology.Triangles;
            mesh.Vertices = inPositions.ToArray();
            IGLVertexAttribute<Vector3> normals = mesh.AddAttribute(new GLVertexAttributeDescriptor<Vector3>(GLVertexAttribute.AttributeName_Normal));
            normals.Buffer = SetupSphereNormals(mesh.Vertices);

            mesh.Indices = inIndices.ToArray();
            mesh.Apply();
            return mesh;
        }

        private static Vector3[] SetupSphereNormals(Vector3[] inPositions)
        {
            Vector3[] normals = new Vector3[inPositions.Length];

            for (int i = 0; i < inPositions.Length; ++i) {
                normals[i] = inPositions[i].Normalized;
            }
            return normals;
        }
    }
}
