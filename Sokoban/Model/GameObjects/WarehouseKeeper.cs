using Newtonsoft.Json;
using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class WarehouseKeeper : IGameObject
    {
        public WarehouseKeeper(int x, int y)
        {
            Location = new Vector(x, y);
        }

        [JsonConstructor]
        public WarehouseKeeper(Vector location)
        {
            Location = location;
        }

        public Vector Location { get; }

        public WarehouseKeeper Move(Direction direction)
        {
            return new WarehouseKeeper(Location + direction.Vector);
        }
    }
}