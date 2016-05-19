using Sokoban.Infrastructure;

namespace Sokoban.Model.Interfaces
{
    public interface IMovable : IGameObject
    {
        IMovable MoveTo(Direction direction);
    }
}
