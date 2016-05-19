using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban
{
    public class Wall : IImmovable
    {
        public Vector Location { get; }
        public bool IsPassable { get; } = false;
    }
}
