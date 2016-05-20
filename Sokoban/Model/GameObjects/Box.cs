using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class Box : IDynamicObject
    {
        public Vector Location { get; }
        public Box(Vector location)
        {
            Location = location;
        }
        public IDynamicObject MoveTo(Direction direction)
        {
            return new Box(Location + direction.Vector);
        }
    }
}
