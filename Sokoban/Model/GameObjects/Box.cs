using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban
{
    public class Box : IMovable
    {
        public Vector Location { get; }
        public Box(Vector location)
        {
            Location = location;
        }
        public IMovable MoveTo(Direction direction)
        {
            return new Box(Location + direction.Vector);
        }
    }
}
