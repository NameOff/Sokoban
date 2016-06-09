using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Sokoban.Infrastructure;
using Sokoban.Model;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban.WarehouseCreator
{
    public class WarehouseCreator
    {
        public readonly int Height;
        public readonly int Width;
        private ImmutableDictionary<Vector, Box> boxes;
        private WarehouseKeeper keeper = new WarehouseKeeper(0, 0);
        private ImmutableDictionary<Vector, IEnumerator<IGameObject>> staticObjects;

        public WarehouseCreator(int height, int width)
        {
            Height = height;
            Width = width;

            var staticObjectsDict = new Dictionary<Vector, IEnumerator<IGameObject>>();

            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    staticObjectsDict[new Vector(x, y)] = StaticObjectsGenerator(x, y).GetEnumerator();
                    staticObjectsDict[new Vector(x, y)].MoveNext();
                }
            staticObjects = staticObjectsDict.ToImmutableDictionary();
            boxes = Enumerable.Empty<Box>().ToImmutableDictionary(b => b.Location);
            SetKeeper(0, 0);
            CurrentWarehouse = new Warehouse(StaticObjects, Boxes, keeper);
        }

        public Warehouse CurrentWarehouse { get; private set; }

        public IEnumerable<IGameObject> StaticObjects => staticObjects.Values.Select(g => g.Current);
        public IEnumerable<Box> Boxes => boxes.Values.Where(box => box != null);

        private bool IsOutOfRange(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }


        private void UpdateWarehouseIfCorrect()
        {
            if (keeper == null)
                return;

            var newWarehouse = new Warehouse(StaticObjects, Boxes, keeper);

            if (newWarehouse.IsCorrectWarehouse())
                CurrentWarehouse = newWarehouse;
        }

        public void SetKeeper(int x, int y)
        {
            var newKeeper = new WarehouseKeeper(x, y);
            var newWarehous = new Warehouse(StaticObjects, Boxes, newKeeper);
            if (!newWarehous.IsCorrectWarehouse()) return;
            keeper = newKeeper;
            CurrentWarehouse = newWarehous;
        }

        public void SetBox(int x, int y)
        {
            var newBoxes = boxes
                .SetItem(new Vector(x, y),
                    boxes.ContainsKey(new Vector(x, y)) && boxes[new Vector(x, y)] != null ? null : new Box(x, y));

            var newWarehouse = new Warehouse(StaticObjects, newBoxes.Values.Where(b => b != null), keeper);
            if (!newWarehouse.IsCorrectWarehouse()) return;
            boxes = newBoxes;
            CurrentWarehouse = newWarehouse;
        }


        public void NextStaticObject(int x, int y)
        {
            var vector = new Vector(x, y);
            staticObjects[vector].MoveNext();
            var newWarehouse = new Warehouse(StaticObjects, Boxes, keeper);
            if (!newWarehouse.IsCorrectWarehouse())
            {
                staticObjects = staticObjects.SetItem(vector, StaticObjectsGenerator(x, y).GetEnumerator());
                staticObjects[vector].MoveNext();
            }
            else
            {
                CurrentWarehouse = newWarehouse;
            }
        }

        private static IEnumerable<IGameObject> StaticObjectsGenerator(int x, int y)
        {
            while (true)
            {
                yield return new Floor(x, y);
                yield return new Wall(x, y);
                yield return new Storage(x, y);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}