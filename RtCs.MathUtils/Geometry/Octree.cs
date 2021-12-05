using RtCs.MathUtils.Algorithm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.MathUtils.Geometry
{
    // refered http://marupeke296.com/COL_3D_No15_Octree.html

    public interface IOctreeRegistable
    {
        Vector3 BoundsMin { get; }
        Vector3 BoundsMax { get; }
        IOctreeCell AffiliationCell { get; set; }
    }

    public partial class Octree : IEnumerable<IOctreeCell>
    {
        public Octree(int inMaxLevel, double inDimensions)
            : this(inMaxLevel, inDimensions, new Vector3())
        { }

        public Octree(int inMaxLevel, double inDimensions, Vector3 inOffset)
        {
            if ((inMaxLevel <= 0) || (8 < inMaxLevel)) {
                throw new ArgumentOutOfRangeException($"Octree.constructor: Argument \"{nameof(inMaxLevel)}\" must be in range of 1 to 8.");
            }

            MaxLevel = inMaxLevel;
            Dimensions = inDimensions;
            CellDimensions = new double[MaxLevel + 1];
            for (int i = 0; i <= MaxLevel; ++i) {
                var f = (double)Math.Pow(2.0f, i);
                CellDimensions[i] = Dimensions / f;
            }

            Root = new RootCell(MaxLevel);
            LinearCells = Root.Linearize();

            AxisCellsAtEndLayer = (int)(Math.Pow(2, MaxLevel) + 0.5);

            Offset = inOffset;
            return;
        }

        public IOctreeCell Register(IOctreeRegistable inValue)
        {
            Vector3 Clamp(Vector3 value)
                => new Vector3(
                        value.x.Clamp(0.0, Dimensions),
                        value.y.Clamp(0.0, Dimensions),
                        value.z.Clamp(0.0, Dimensions)
                    );

            var min = GetMortonOrder(Clamp(inValue.BoundsMin - Offset));
            var max = GetMortonOrder(Clamp(inValue.BoundsMax - Offset));
            var order = min ^ max;

            const uint MASK = 0xFFFFFFFF;
            int layer = MaxLevel;
            while ((order & MASK) != 0) {
                order = order >> 3;
                --layer;
            }
            order = min >> ((MaxLevel - layer) * 3);

            var index = GetLinearIndex(layer, order);
            var result = LinearCells[index];
            result.Objects.AddLast(inValue);
            return result;
        }

        public void Unregister(IOctreeRegistable inValue)
        {
            if (inValue.AffiliationCell != null) {
                if (!(inValue.AffiliationCell is Cell)) {
                    throw new InvalidCastException($"Object belongs difference trees.");
                }

                (inValue.AffiliationCell as Cell).Objects.Remove(inValue);
            }
            return;
        }

        public void Clear()
        {
            foreach (var cell in LinearCells) {
                foreach (var obj in cell.Objects) {
                    obj.AffiliationCell = null;
                }
                cell.Objects.Clear();
            }
            return;
        }

        public IOctreeCell GetCellAt(int inLevel, Vector3 inPosition)
        {
            uint morton = GetMortonOrder(inPosition);
            int index = GetLinearIndex(MaxLevel, morton);
            var cell = LinearCells[index];

            while ((cell != null) && (cell.Level != inLevel)) {
                cell = cell.Parent;
            }
            return cell;
        }

        public IOctreeCell GetCellAt(int inLevel, Container3<int> inAxisIndicesInEndLayer)
        {
            uint morton = GetMortonOrder(inAxisIndicesInEndLayer);
            int index = GetLinearIndex(MaxLevel, morton);
            var cell = LinearCells[index];

            while ((cell != null) && (cell.Level != inLevel)) {
                cell = cell.Parent;
            }
            return cell;
        }

        public IEnumerable<IOctreeCell> TraverseOnRay(Line3D inRay)
        {
            VoxelTraverse traverser = new VoxelTraverse() {
                BoundsMin = Offset,
                CellCount = AxisCellsAtEndLayer,
                CellDimension = CellDimensions.Last()
            };

            foreach (var index in traverser.Traverse(inRay)) {
                if (IndexInRangeOfEndLayer(index[0], index[1], index[2])) {
                    yield return GetCellAt(MaxLevel, index);
                }
            }
            yield break;
        }

        private Container3<byte> GetMortonOrders(Vector3 inPosition)
        {
            var cellDim = CellDimensions.Last();
            return GetMortonOrders(new Container3<int>(
                    Clamp((int)(inPosition.x / cellDim), 0, AxisCellsAtEndLayer - 1),
                    Clamp((int)(inPosition.y / cellDim), 0, AxisCellsAtEndLayer - 1),
                    Clamp((int)(inPosition.z / cellDim), 0, AxisCellsAtEndLayer - 1)
                ));
        }

        private Container3<byte> GetMortonOrders(Container3<int> inAxisIndices)
        {
            return new Container3<byte>(
                (byte)inAxisIndices.Item0,
                (byte)inAxisIndices.Item1,
                (byte)inAxisIndices.Item2
            );
        }

        private uint GetMortonOrder(Vector3 inPosition)
        {
            var (x, y, z) = GetMortonOrders(inPosition);
            return SeparateBit(x)
                 | SeparateBit(y) << 1
                 | SeparateBit(z) << 2;
        }

        private uint GetMortonOrder(Container3<int> inAxisIndices)
        {
            var (x, y, z) = GetMortonOrders(inAxisIndices);
            return SeparateBit(x)
                 | SeparateBit(y) << 1
                 | SeparateBit(z) << 2;
        }

        private uint SeparateBit(byte inValue)
        {
            const uint n8 = 0x0000f00f;
            const uint n4 = 0x000c30c3;
            const uint n2 = 0x00249249;
            uint v = inValue;
            v = (v | v << 8) & n8;
            v = (v | v << 4) & n4;
            v = (v | v << 2) & n2;
            return v;
        }

        private static int GetLinearIndex(int inLevel, uint inIndexInLayer)
        {
            if (Exp8[inLevel] <= inIndexInLayer) {
                throw new ArgumentOutOfRangeException($"{nameof(inIndexInLayer)}({inIndexInLayer}) is bigger than cell count in the layer {nameof(inLevel)}({inLevel}). ");
            }
            if (inLevel == 0) {
                return 0;
            }
            return (int)(((Exp8[inLevel] - 1) / 7) + inIndexInLayer); ;
        }

        private int Clamp(int inValue, int inMin, int inMax) => Math.Max(inMin, Math.Min(inMax, inValue));

        private int MinIndex((double x, double y, double z) inValue)
        {
            if (inValue.x < inValue.y) {
                if (inValue.x < inValue.z) {
                    return 0;
                } else {
                    return 2;
                }
            } else {
                if (inValue.y < inValue.z) {
                    return 1;
                } else {
                    return 2;
                }
            }
            throw new InvalidProgramException();
        }

        private bool IndexInRangeOfEndLayer(int inIndexX, int inIndexY, int inIndexZ)
            => (0 <= inIndexX) && (inIndexX < AxisCellsAtEndLayer)
            && (0 <= inIndexY) && (inIndexY < AxisCellsAtEndLayer)
            && (0 <= inIndexZ) && (inIndexZ < AxisCellsAtEndLayer);

        IEnumerator<IOctreeCell> IEnumerable<IOctreeCell>.GetEnumerator()
            => LinearCells.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => LinearCells.GetEnumerator();

        public int MaxLevel
        { get; }
        public double Dimensions
        { get; }
        private RootCell Root
        { get; }
        private IReadOnlyList<Cell> LinearCells
        { get; }
        private int AxisCellsAtEndLayer
        { get; }

        public readonly Vector3 Offset;

        public readonly double[] CellDimensions;
        private static readonly uint[] Exp8 = {
            1, 8, 64, 512, 4096, 32768, 262144, 2097152, 16777216
        };
        public static readonly uint[] AxisCellsInLayer = {
            1, 2, 4, 8, 16, 32, 64, 128, 256
        };
    }
}
