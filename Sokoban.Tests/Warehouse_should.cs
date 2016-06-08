using System.Collections.Immutable;
using System.Linq.Expressions;
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
     *    ######
     *    #    #
     *    # B ##
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
        private ImmutableArray<IGameObject> staticObjects;
        private ImmutableArray<Box> boxes;
        private WarehouseKeeper keeper;
        private Warehouse warehouse;

        public ImmutableArray<IGameObject> SetItemToStaticObjects(IGameObject staticObject)
        {
            var arrayIndex = staticObject.Location.Y*5 + staticObject.Location.X;
            return staticObjects.SetItem(arrayIndex, staticObject);
        }

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
                new Floor(2, 3),
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

        [Test]
        public void NotChanged_WhenKeeperRegularMoveOnWall()
        {
            var newWarehouse = warehouse.MoveKeeper<RegularMove>(Direction.Down);

            warehouse.Should().Be(newWarehouse);
        }

        [Test]
        public void NotChanged_WhenKeeperRegularMoveOnBlockedBox()
        {
            var newWarehouse = warehouse.MoveKeeper<RegularMove>(Direction.Right);

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
