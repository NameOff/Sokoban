using System.Collections.Immutable;
using Sokoban.Infrastructure;
using Sokoban.Model.GameObjects;

namespace Sokoban.Model.Interfaces
{
    public interface IAction
    {
        ImmutableArray<Box> MoveBoxes(ImmutableArray<Box> boxes, Direction direction, WarehouseKeeper keeper);
    }
}