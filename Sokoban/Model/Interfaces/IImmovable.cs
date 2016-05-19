namespace Sokoban.Model.Interfaces
{
    public interface IImmovable : IGameObject
    {
        bool IsPassable { get; }
    }
}