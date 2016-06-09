using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Sokoban.Infrastructure;
using Sokoban.Model;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban.Tests
{
    public class Level_Should
    {
        private Level level;

        [SetUp]
        public void SetUp()
        {
            var staticObjects = new IGameObject[]
            {
                new Wall(0, 0),
                new Wall(1, 0),
                new Wall(2, 0),
                new Wall(3, 0),
                new Wall(4, 0),
                new Wall(0, 1),
                new Floor(1, 1),
                new Storage(2, 1), 
                new Storage(3, 1), 
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

            var boxes = new[] { new Box(2, 2), new Box(3, 3) }.ToImmutableArray();
            var keeper = new WarehouseKeeper(2, 3);

            level = new Level(new Warehouse(staticObjects, boxes, keeper));
        }

        [Test]
        public void HaveZeroScore_WhenCreated()
        {
            level.MovesCount.Should().Be(0);
        }

        [Test]
        public void ReturnCurrentLevel_WhenUndoOnCreated()
        {
            level.Undo().Should().Be(level);
        }

        [Test]
        public void ReturnDifferentLevel_WhenMoveCorrect()
        {
            var newLevel = level.NextStep<RegularMove>(Direction.Up);
            newLevel.Should().NotBeSameAs(level);
        }

        [Test]
        public void ReturnCurrentLevel_WhenMoveIncorrect()
        {
            var newLevel = level.NextStep<RegularMove>(Direction.Down);
            newLevel.ShouldBeEquivalentTo(level);
        }

        [Test]
        public void ReturnSameLevel_AfterUndoOnCorrectMove()
        {
            var newLevel = level.NextStep<RegularMove>(Direction.Up);
            newLevel.Undo().Should().BeSameAs(level);
        }

        [Test]
        public void NotOver_WhenSomeBoxesNotOnStorages()
        {
            level.IsOver().Should().BeFalse();
        }

        private Level SimpleLevel()
        {
            var staticObjects = new IGameObject[]
            {
                new Storage(0, 0), new Floor(0, 1),new Floor(0, 2)
            };
            var keeper = new WarehouseKeeper(0, 1);
            var box = new Box(0, 0);
            var warehouse = new Warehouse(staticObjects, new List<Box> { box }, keeper);
            return new Level(warehouse);
        }

        [Test]
        public void Over_WhenAllBoxesOnStorages()
        {
            var newLevel = SimpleLevel();
            newLevel.IsOver().Should().BeTrue();
        }

        [Test]
        public void ReturnAllObjects_WhenAllObjectsCalled()
        {
            var newLevel = SimpleLevel();
            var objects = newLevel.AllObjects.ToList();
            var expectedItems = new List<IGameObject>
            {
                new Box(0, 0),
                new Storage(0, 0),
                new Floor(0, 1),
                new Floor(0, 2),
                new WarehouseKeeper(0, 1)
            };
            objects.ShouldBeEquivalentTo(expectedItems);
        }

        [Test]
        public void ReturnBox_WhenGetAllBoxesCalled()
        {
            var newLevel = SimpleLevel();
            newLevel.GetAll<Box>().FirstOrDefault().ShouldBeEquivalentTo(new Box(0,0));
        }

        [Test]
        public void ReturnFloors_WhenGetAllFloorsCalled()
        {
            var newLevel = SimpleLevel();
            newLevel.GetAll<Floor>().ToList().ShouldBeEquivalentTo(new List<Floor> { new Floor(0, 1), new Floor(0, 2)});
        }

        [Test]
        public void ReturnKeeper_WhenGetAllKeeperCalled()
        {
            var newLevel = SimpleLevel();
            newLevel.GetAll<WarehouseKeeper>().FirstOrDefault().ShouldBeEquivalentTo(new WarehouseKeeper(0, 1));
        }

        [Test]
        public void ReturnStorage_WhenGetAllStoragesCalled()
        {
            var newLevel = SimpleLevel();
            newLevel.GetAll<Storage>().FirstOrDefault().ShouldBeEquivalentTo(new Storage(0, 0));
        }
    }
}
