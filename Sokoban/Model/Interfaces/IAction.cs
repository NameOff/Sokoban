using Sokoban.Infrastructure;

namespace Sokoban.Model.Interfaces
{
    public interface IAction
    {
        Direction Direction { get; }
        MovableObjects Move(MovableObjects movableObjects);
    }
}
