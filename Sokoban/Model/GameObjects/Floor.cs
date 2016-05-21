using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class Floor : IStaticObject
    {
        public Vector Location { get; }
        public bool IsPassable { get; } = true;

        public Floor(Vector location)
        {
            Location = location;
        }
    }
}
