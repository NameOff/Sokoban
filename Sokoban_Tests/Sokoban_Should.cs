using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using Sokoban.Infrastructure;
using Sokoban.Model;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban_Tests
{
    [TestFixture]
    class Sokoban_Should
    {
        private static Warehouse warehouse;
        [SetUp]
        public void SetUp()
        {
            var stringWarehouse = new[] {
                "#####",
                "#   #",
                "#HBB#",
                "#B @#",
                "#####" };
            warehouse = ParseStringArrayWarehouse(stringWarehouse);
        }

        private Warehouse ParseStringArrayWarehouse(string[] stringWarehouse)
        {
            var boxes = new List<Box>();
            WarehouseKeeper warehouseKeeper = null;
            var staticObjects = new List<IStaticObject>();
            for (var y = 0; y < stringWarehouse.Length; y++)
            {
                for (var x = 0; x < stringWarehouse[y].Length; x++)
                {
                    var location = new Vector(x, y);
                    switch (stringWarehouse[y][x])
                    {
                        case '#':
                            staticObjects.Add(new Wall(location));
                            break;
                        case '@':
                            staticObjects.Add(new Storage(location));
                            break;
                        case 'H':
                            warehouseKeeper = new WarehouseKeeper(location);
                            break;
                        case 'B':
                            boxes.Add(new Box(location));
                            break;
                    }
                    if (stringWarehouse[y][x] != '#' && stringWarehouse[y][x] != '@')
                        staticObjects.Add(new Floor(location));
                }
            }
            var movableObjects = new MovableObjects(boxes, warehouseKeeper);
            return new Warehouse(movableObjects, staticObjects);
        }


        [Test]
        public void ChangeHeroCoordinates_AfterMoveHero()
        {
            var newWarehouse = warehouse.ChangeAfter(MoveOneBox.To(Direction.Up));
            var newKeeper = newWarehouse.MovableObjects.WarehouseKeeper;
            newKeeper.Location.Should().Be(new Vector(1, 1));
        }

        [Test]
        public void DoSomething_WhenSomething()
        {
            var newWarehouse = warehouse.ChangeAfter(MoveOneBox.To(Direction.Right));
            var newKeeper = newWarehouse.MovableObjects.WarehouseKeeper;
            newKeeper.Location.Should().Be(new Vector(1, 2));
        }
    }
}
