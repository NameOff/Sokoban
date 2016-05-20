using Sokoban.Infrastructure;

namespace Sokoban.Model.Interfaces
{
    public interface IDynamicObject : IGameObject
    {
        IDynamicObject MoveTo(Direction direction);
    }
}
