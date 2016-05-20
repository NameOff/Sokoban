using System.Linq;
using Sokoban.Infrastructure;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model
{
    public class MoveOneBox : IAction
    {
        public Direction Direction { get; }

        private MoveOneBox(Direction direction)
        {
            Direction = direction;
        }

        public static MoveOneBox To(Direction direction)
        {
            return new MoveOneBox(direction);
        }

        public MovableObjects Move(MovableObjects movableObjects)
        {
            //TODO Downcast!!!
            var newKeeper = (WarehouseKeeper)movableObjects.WarehouseKeeper.MoveTo(Direction);
            var boxes = movableObjects.Boxes
                .Select(box =>
                    box.Location == newKeeper.Location ? box.MoveTo(Direction) : box)
                .Select(box => (Box) box);
            return new MovableObjects(boxes, newKeeper);
        }
    }
}
