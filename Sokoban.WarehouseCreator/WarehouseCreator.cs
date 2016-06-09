using System;
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
        private WarehouseKeeper keeper = new WarehouseKeeper(0, 0);
        public readonly int Height;
        public readonly int Width;
        public Warehouse CurrentWarehouse { get; private set; }

        public IEnumerable<IGameObject> StaticsObjects => staticObjects
            .Cast<IEnumerator<IGameObject>>()
            .Select(obj => obj.Current);

        public IEnumerable<Box> Boxes => boxes.Cast<Box>().Where(box => box != null);

        public WarehouseCreator(int height, int width)
        {
            Height = height;
            Width = width;
            staticObjects = new IEnumerator<IGameObject>[height, width];

            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    staticObjects[y, x] = StaticObjectsGenerator(x, y).GetEnumerator();
                    staticObjects[y, x].MoveNext();
                }

            boxes = new Box[height, width];
            SetKeeper(0, 0);

            CurrentWarehouse = new Warehouse(StaticsObjects, Boxes, keeper);
        }

        private bool IsOutOfRange(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        private void DoAct(int x, int y, Action act)
        {
            if (!IsOutOfRange(x, y))
                throw new ArgumentOutOfRangeException();
            act();
            UpdateWarehouseIfCorrect();
        }


        private void UpdateWarehouseIfCorrect()
        {
            if (keeper == null)
                return;

            var newWarehouse = new Warehouse(StaticsObjects, Boxes, keeper);

            if (newWarehouse.IsCorrectWarehouse())
                CurrentWarehouse = newWarehouse;
        }

        public void SetKeeper(int x, int y)
        {
            keeper = new WarehouseKeeper(x, y);
            UpdateWarehouseIfCorrect();
        }

        public void SetBox(int x, int y)
        {
            DoAct(x, y, () => boxes[y, x] = boxes[y, x] == null ? new Box(x, y) : null);
        }


        public void NextStaticObject(int x, int y) =>
            DoAct(x, y, () => staticObjects[y, x].MoveNext());

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