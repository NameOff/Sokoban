using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class Wall : IImmovable
    {
        public Vector Location { get; }
        public bool IsPassable { get; } = false;
    }
}
