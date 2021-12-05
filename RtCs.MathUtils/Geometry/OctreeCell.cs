using System.Collections.Generic;

namespace RtCs.MathUtils.Geometry
{
    public interface IOctreeCell
    {
        IOctreeCell Parent { get; }

        int Level { get; }
        int LocalIndex { get; }

        IEnumerable<IOctreeRegistable> Objects { get; }
    }

    public partial class Octree
    {
        private class Cell : IOctreeCell
        {
            protected Cell(Cell inParent, int inLocalIndex, int inMaxLevel)
            {
                Parent = inParent;
                LocalIndex = inLocalIndex;

                Level = (inParent == null) ? 0 : inParent.Level + 1;
                if (inMaxLevel > Level) {
                    var children = new List<Cell>(8);
                    for (int i = 0; i < 8; ++i) {
                        children.Add(new Cell(this, i, inMaxLevel));
                    }
                    Children = children;
                }
                return;
            }

            public Cell Parent { get; }
            public IReadOnlyList<Cell> Children
            { get; } = null;
            public int Level
            { get; }
            public int LocalIndex
            { get; }

            public LinkedList<IOctreeRegistable> Objects
            { get; } = new LinkedList<IOctreeRegistable>();
            IEnumerable<IOctreeRegistable> IOctreeCell.Objects => Objects;

            IOctreeCell IOctreeCell.Parent => Parent;

            protected IEnumerable<Cell> GetCellsInLevel(int inLevel)
            {
                if (inLevel == Level) {
                    yield return this;
                } else {
                    foreach (var child in Children) {
                        foreach (var cell in child.GetCellsInLevel(inLevel)) {
                            yield return cell;
                        }
                    }
                }
                yield break;
            }
        }

        private class RootCell : Cell
        {
            public RootCell(int inMaxLevel)
                : base(null, 0, inMaxLevel)
            {
                MaxLevel = inMaxLevel;
                return;
            }

            public IReadOnlyList<Cell> Linearize()
            {
                List<Cell> result = new List<Cell>(CalcNumberOfCell(MaxLevel));
                for (int level = 0; level <= MaxLevel; ++level) {
                    result.AddRange(GetCellsInLevel(level));
                }

                return result;
            }

            private int CalcNumberOfCell(int inMaxLevel)
                => (inMaxLevel == 0) ? 0 : (CalcNumberOfCell(inMaxLevel - 1) * 8) + 1;

            private int MaxLevel
            { get; }
        }
    }
}
