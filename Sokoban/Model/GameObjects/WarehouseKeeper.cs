using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class WarehouseKeeper : IGameObject
    {
        public Vector Location { get; }

        public WarehouseKeeper(int x, int y)
        {
            Location = new Vector(x, y);
        }

        public WarehouseKeeper(Vector location)
        {
            Location = location;
        }

        public WarehouseKeeper Move(Direction direction)
        {
            return new WarehouseKeeper(Location + direction.Vector);
        }
    }
}
