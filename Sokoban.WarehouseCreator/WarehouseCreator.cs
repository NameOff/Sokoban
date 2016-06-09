using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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
        private Warehouse currentWarehouse;
        public readonly int Height;
        public readonly int Width;
        public IEnumerable<IGameObject> StaticsObjects => staticObjects
         .Cast<IEnumerator<IGameObject>>()
         .Select(obj => obj.Current);

        public IEnumerable<Box> Boxes => boxes.Cast<Box>();

        public IGameObject GetObject(int x, int y) => staticObjects[y, x].Current;
        public Box GetBox(int x, int y) => boxes[y, x];

        public WarehouseKeeper Keeper => keeper;

        public WarehouseCreator(int height, int width)
        {
            Height = height;
            Width = width;
            SetKeeper(0, 0);
            staticObjects = new IEnumerator<IGameObject>[height, width];

            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    staticObjects[y, x] = StaticObjectsGenerator(x, y).GetEnumerator();
                    staticObjects[y, x].MoveNext();
                }


            boxes = new Box[height, width];

            currentWarehouse = new Warehouse(StaticsObjects, Boxes, Keeper);
        }

        public void SetKeeper(int x, int y)
        {
            keeper = new WarehouseKeeper(x, y);
        }

        public void SetBox(int x, int y)
        {
            if (boxes[y, x] != null)
                boxes[y, x] = null;
            else
                boxes[y, x] = new Box(x, y);
        }

        public void NextStaticObject(int x, int y)
        {
            staticObjects[y, x].MoveNext();
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

            warehouse = new Warehouse(StaticsObjects, Boxes, keeper);

            if (keeper != null && warehouse.IsCorrectWarehouse())
                return true;

            warehouse = null;
            return false;
        }
    }
}