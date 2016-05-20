using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class WarehouseKeeper : IDynamicObject
    {
        public Vector Location { get; }

        public WarehouseKeeper(Vector location)
        {
            Location = location;
        }

        public IDynamicObject MoveTo(Direction direction)
        {
            return new WarehouseKeeper(Location + direction.Vector);
        }
    }
}
