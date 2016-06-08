using System.Collections.Generic;
using System.Linq;
using Sokoban.Infrastructure;
using Sokoban.Model;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban.WarehouseCreator
{
    public class WarehouseCreator
    {
        private readonly IEnumerator<IGameObject>[,] staticObjects;
        private readonly Box[,] boxes;
        private WarehouseKeeper keeper;

        public WarehouseCreator(int height, int width)
        {
            staticObjects = new IEnumerator<IGameObject>[height, width];

            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    staticObjects[y, x] = StaticObjectsGenerator(x, y).GetEnumerator();
                    staticObjects[y, x].MoveNext();
                }


            boxes = new Box[height, width];
        }

        public WarehouseKeeper SetKeeper(int x, int y)
        {
            keeper = new WarehouseKeeper(x, y);
            return keeper;
        }

        public Box SetBox(int x, int y)
        {
            if (boxes[y, x] != null)
                boxes[y, x] = null;
            else
                boxes[y, x] = new Box(x, y);
            return boxes[y, x];
        }

        public IGameObject NextStaticObject(int x, int y)
        {
            staticObjects[y, x].MoveNext();
            return staticObjects[y, x].Current;
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

        public bool TryGetWarehouse(out Warehouse warehouse)
        {
            var staticObjectsCurrents = staticObjects
                .Cast<IEnumerator<IGameObject>>()
                .Select(obj => obj.Current);

            warehouse = new Warehouse(staticObjectsCurrents, boxes.Cast<Box>(), keeper);

            if (keeper != null && warehouse.IsCorrectWarehouse())
                return true;

            warehouse = null;
            return false;
        }
    }
}
