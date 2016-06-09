using System.Collections.Immutable;
using FluentAssertions;
using NUnit.Framework;
using Sokoban.Infrastructure;
using Sokoban.Model;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban.Tests
{
    /* Game objects view
     * 
     *    #####
     *    #   #
     *    # B #
     *    # KB#
     *    #####  Restart
     *  
     * '#' is Wall
     * 'B' is Box
     * 'S' is Storage
     * ' ' is Floor
     */

    [TestFixture]
    public class Warehouse_should
    {
        [SetUp]
        public void SetUp()
        {
            staticObjects = new IGameObject[]
            {
                new Wall(0, 0),
                new Wall(1, 0),
                new Wall(2, 0),
                new Wall(3, 0),
                new Wall(4, 0),
                new Wall(0, 1),
                new Floor(1, 1),
                new Floor(2, 1),
                new Floor(3, 1),
                new Wall(4, 1),
                new Wall(0, 2),
                new Floor(1, 2),
                new Floor(2, 2),
                new Floor(3, 2),
                new Wall(4, 2),
                new Wall(0, 3),
                new Floor(1, 3),
                new Storage(2, 3), 
                new Floor(3, 3), 
                new Wall(4, 3),
                new Wall(0, 4),
                new Wall(1, 4),
                new Wall(2, 4),
                new Wall(3, 4),
                new Wall(4, 4)
            }.ToImmutableArray();

            boxes = new[] {new Box(2, 2), new Box(3, 3)}.ToImmutableArray();
            keeper = new WarehouseKeeper(2, 3);

            warehouse = new Warehouse(staticObjects, boxes, keeper);
        }

        private ImmutableArray<IGameObject> staticObjects;
        private ImmutableArray<Box> boxes;
        private WarehouseKeeper keeper;
        private Warehouse warehouse;

        public ImmutableArray<IGameObject> SetItemToStaticObjects(IGameObject staticObject)
        {
            var arrayIndex = staticObject.Location.Y*5 + staticObject.Location.X;
            return staticObjects.SetItem(arrayIndex, staticObject);
        }

        [Test]
        public void BePassable_OnFloor()
        {
            warehouse.IsPassable(new Vector(1, 1)).Should().BeTrue();
        }

        [Test]
        public void BePassable_OnStorage()
        {
            warehouse.IsPassable(new Vector(2, 3)).Should().BeTrue();
        }

        [Test]
        public void NotBePassable_OnWall()
        {
            warehouse.IsPassable(new Vector(0, 0)).Should().BeFalse();
        }

        [Test]
        public void BeCorrect_WhenKeeperOnFloor()
        {
            warehouse.IsCorrectWarehouse().Should().BeTrue();
        }

        [Test]
        public void BeCorrect_WhenKeeperOnStorage()
        {
            var newWarehouse = new Warehouse(warehouse.StaticObjects, warehouse.Boxes, new WarehouseKeeper(new Vector(2, 3)));
            newWarehouse.IsCorrectWarehouse().Should().BeTrue();
        }

        [Test]
        public void NotBeCorrect_WhenKeeperOnWall()
        {
            var newWarehouse = new Warehouse(warehouse.StaticObjects,warehouse.Boxes, new WarehouseKeeper(new Vector(0, 0)));
            newWarehouse.IsCorrectWarehouse().Should().BeFalse();
        }

        [Test]
        public void ReturnFloor_OnFloorLocation()
        {
            warehouse.GetStaticObject(new Vector(1, 1)).Should().BeOfType<Floor>();
        }

        [Test]
        public void ReturnStorage_OnStorageLocation()
        {
            warehouse.GetStaticObject(new Vector(2, 3)).Should().BeOfType<Storage>();
        }

        [Test]
        public void ReturnWall_OnWallLocation()
        {
            warehouse.GetStaticObject(new Vector(0, 0)).Should().BeOfType<Wall>();
        }

        [Test]
        public void ReturnWall_OnOutrangedLocation()
        {
            warehouse.GetStaticObject(new Vector(1000, 100)).Should().BeOfType<Wall>();
        }

        [Test]
        public void NotChanged_WhenKeeperRegularMoveOnBlockedBox()
        {
            var newWarehouse = warehouse.MoveKeeper<RegularMove>(Direction.Right);

            warehouse.Should().Be(newWarehouse);
        }

        [Test]
        public void NotChanged_WhenKeeperRegularMoveOnWall()
        {
            var newWarehouse = warehouse.MoveKeeper<RegularMove>(Direction.Down);

            warehouse.Should().Be(newWarehouse);
        }

        [Test]
        public void NotChanged_WhenKeeperRegularMoveTwoBox()
        {
            staticObjects = SetItemToStaticObjects(new Floor(2, 0));
            boxes = new[] {new Box(2, 2), new Box(2, 1)}.ToImmutableArray();
            warehouse = new Warehouse(staticObjects, boxes, keeper);

            var newWarehouse = warehouse.MoveKeeper<RegularMove>(Direction.Up);

            newWarehouse.Should().Be(warehouse);
        }

        [Test]
        public void RelocateKeeper_WhenKeeperRegularMoveOnFloor()
        {
            var newWarehouse = warehouse.MoveKeeper<RegularMove>(Direction.Left);

            newWarehouse.Keeper.Location.Should().Be(new Vector(1, 3));
        }

        [Test]
        public void RelocateKeeperAndBox_WhenKeeperRegularMoveOnBoxBeforeFloor()
        {
            var newWarehouse = warehouse.MoveKeeper<RegularMove>(Direction.Up);

            newWarehouse.Keeper.Location.Should().Be(new Vector(2, 2));
            newWarehouse.Boxes.Should().Equal(new Box(2, 1), new Box(3, 3));
        }

        [Test]
        public void RelocateKeeperAndBox_WhenKeeperRegularMoveOnBoxBeforeStorage()
        {
            staticObjects = SetItemToStaticObjects(new Storage(2, 1));
            warehouse = new Warehouse(staticObjects, boxes, keeper);

            var newWarehouse = warehouse.MoveKeeper<RegularMove>(Direction.Up);

            newWarehouse.Keeper.Location.Should().Be(new Vector(2, 2));
            newWarehouse.Boxes.Should().Equal(new Box(2, 1), new Box(3, 3));
        }
    }
}