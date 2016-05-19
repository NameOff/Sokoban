using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model
{
    public class MoveOneBox : IAction
    {
        public Direction Direction { get; }

        public MoveOneBox(Direction direction)
        {
            Direction = direction;
        }

        public MovableObjects Move(MovableObjects movableObjects)
        {
            var newKeeper = (WarehouseKeeper)movableObjects.WarehouseKeeper.MoveTo(Direction);
            var boxes = movableObjects.Boxes
                .Select(box =>
                    box.Location == newKeeper.Location ? box.MoveTo(Direction) : box)
                .Select(box => (Box) box);
            return new MovableObjects(boxes, newKeeper);
        }
    }
}
