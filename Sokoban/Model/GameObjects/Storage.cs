using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban
{
    public class Storage : IImmovable
    {
        public Vector Location { get; }
        public bool IsPassable { get; } = true;
    }
}
