using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Sokoban.Infrastructure;
using Sokoban.Model;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;
using Sokoban.WarehouseCreator;

namespace Sokoban.Tests
{
    class WarehouseCreator_Should
    {
        private WarehouseCreator.WarehouseCreator wc;

        [SetUp]
        public void SetUp()
        {
            wc = new WarehouseCreator.WarehouseCreator(5, 5);
        }

        [Test]
        public void ConsistsOfFloors_WhenCreated()
        {
            wc.StaticObjects.Should().OnlyContain(z => z.GetType() == typeof(Floor));
        }

        [Test]
        public void HaveWarehouseWhichConsistsOfFloors_WhenCreated()
        {
            wc.CurrentWarehouse.StaticObjects.Should().OnlyContain(z => z.GetType() == typeof(Floor));
        }

        [Test]
        public void NotHaveBoxes_WhenCreated()
        {
            wc.Boxes.Should().BeEmpty();
        }

        [Test]
        public void HaveWarehouseWhichDontHaveBoxes_WhenCreated()
        {
            wc.CurrentWarehouse.Boxes.Should().BeEmpty();
        }

        [Test]
        public void HaveCorrectWarehouse_WhenCreated()
        {
            wc.CurrentWarehouse.IsCorrectWarehouse();
        }

        [Test]
        public void ChangeToWall_OnFloorLocationNextStaticObjectCall()
        {
            var vector = new Vector(1, 0);
            wc.NextStaticObject(vector.X, vector.Y);
            wc.StaticObjects.FirstOrDefault(z => z.Location == vector).Should().BeOfType<Wall>();
        }

        [Test]
        public void ChangeToStorage_OnWallLocationNextStaticObjectCall()
        {
            var vector = new Vector(1, 0);
            wc.NextStaticObject(vector.X, vector.Y);
            wc.NextStaticObject(vector.X, vector.Y);
            wc.StaticObjects.FirstOrDefault(z => z.Location == vector).Should().BeOfType<Storage>();
        }

        [Test]
        public void NotChangeToWall_OnKeeperLocation()
        {
            var vector = new Vector(0, 0);
            wc.NextStaticObject(vector.X, vector.Y);
            wc.StaticObjects.FirstOrDefault(z => z.Location == vector).Should().BeOfType<Floor>();
        }

        [Test]
        public void NotChangeToWall_OnBoxLocation()
        {
            var vector = new Vector(1, 0);
            wc.SetBox(vector.X,vector.Y);
            wc.NextStaticObject(vector.X, vector.Y);
            wc.StaticObjects.FirstOrDefault(z => z.Location == vector).Should().BeOfType<Floor>();
        }

        [Test]
        public void SetBox_OnPassableStaticObjectLocation()
        {
            var vector = new Vector(1, 0);
            wc.SetBox(vector.X, vector.Y);
            wc.Boxes.FirstOrDefault().Location.ShouldBeEquivalentTo(vector);
        }

        [Test]
        public void NotSetBox_OnNotPassableStaticObjectLocation()
        {
            var vector = new Vector(1, 0);
            wc.NextStaticObject(vector.X, vector.Y);
            wc.SetBox(vector.X, vector.Y);
            wc.Boxes.Should().BeEmpty();
        }

        [Test]
        public void RemoveBox_OnBoxLocation()
        {
            var vector = new Vector(1, 0);
            wc.SetBox(vector.X, vector.Y);
            wc.SetBox(vector.X, vector.Y);
            wc.Boxes.Should().BeEmpty();
        }

        [Test]
        public void NotSetBox_OnKeeperLocation()
        {
            var vector = new Vector(0, 0);
            wc.SetBox(vector.X, vector.Y);
            wc.Boxes.Should().BeEmpty();
        }
    }
}
