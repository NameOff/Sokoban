using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class Storage : IStaticObject
    {
        public Vector Location { get; }
        public bool IsPassable { get; } = true;
    }
}
