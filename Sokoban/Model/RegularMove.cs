using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Sokoban.Infrastructure;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model
{
    public class RegularMove : IAction
    {
        public ImmutableArray<Box> MoveBoxes(ImmutableArray<Box> boxes, Direction direction, WarehouseKeeper keeper)
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i].Location == keeper.Location + direction.Vector)
                    boxes = boxes.SetItem(i, boxes[i].Move(direction));
            }
            return boxes;
        }
    }
}
