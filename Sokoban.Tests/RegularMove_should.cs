using System.Collections.Immutable;
using FluentAssertions;
using NUnit.Framework;
using Sokoban.Infrastructure;
using Sokoban.Model;
using Sokoban.Model.GameObjects;

namespace Sokoban.Tests
{
    public class RegularMove_should
    {
        private ImmutableArray<Box> boxes;
        private WarehouseKeeper keeper;
        private RegularMove regularMove;

        [SetUp]
        public void SetUp()
        {
            boxes = new[] {new Box(new Vector(3, 2)), new Box(new Vector(1, 1))}.ToImmutableArray();
            keeper = new WarehouseKeeper(new Vector(3, 3));
            regularMove = new RegularMove();
        }

        [Test]
        public void RelocateBoxes_AfterMoveToBox()
        {
            var newBoxes = regularMove.MoveBoxes(boxes, Direction.Up, keeper);
            newBoxes.Should().Equal(new Box(new Vector(3, 1)), new Box(new Vector(1, 1)));
        }

        [Test]
        public void DontRelocateBoxes_AfterMoveFromBox()
        {
            var newBoxes = regularMove.MoveBoxes(boxes, Direction.Right, keeper);
            newBoxes.Should().Equal(boxes);
        }
    }
}