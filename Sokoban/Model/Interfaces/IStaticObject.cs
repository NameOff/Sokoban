namespace Sokoban.Model.Interfaces
{
    public interface IStaticObject : IGameObject
    {
        bool IsPassable { get; }
    }
}