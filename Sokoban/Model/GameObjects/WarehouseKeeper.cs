using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban
{
    public class WarehouseKeeper : IMovable
    {
        public Vector Location { get; }

        public WarehouseKeeper(Vector location)
        {
            Location = location;
        }

        public IMovable MoveTo(Direction direction)
        {
            return new WarehouseKeeper(Location + direction.Vector);
        }
    }
}
