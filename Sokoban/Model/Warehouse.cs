using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Newtonsoft.Json;
using Sokoban.Infrastructure;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model
{
    public class Warehouse
    {
        [JsonProperty] public readonly ImmutableArray<Box> Boxes;

        [JsonProperty] public readonly WarehouseKeeper Keeper;

        public readonly ImmutableArray<IGameObject> StaticObjects;

        [JsonConstructor]
        public Warehouse(IEnumerable<IGameObject> staticObjects, IEnumerable<Box> boxes, WarehouseKeeper keeper)
        {
            StaticObjects = staticObjects.ToImmutableArray();
            Boxes = boxes.ToImmutableArray();
            Keeper = keeper;
        }


        private Warehouse(ImmutableArray<IGameObject> staticObjects, ImmutableArray<Box> boxes, WarehouseKeeper keeper)
        {
            StaticObjects = staticObjects;
            Boxes = boxes;
            Keeper = keeper;
        }

        public Warehouse MoveKeeper<TAction>(Direction direction) where TAction : class, IAction, new()
        {
            var newBoxes = new TAction().MoveBoxes(Boxes, direction, Keeper);
            var newKeeper = Keeper.Move(direction);

            var newWarehouse = new Warehouse(StaticObjects, newBoxes, newKeeper);

            return newWarehouse.IsCorrectWarehouse() ? newWarehouse : this;
        }

        public bool IsCorrectWarehouse()
        {
            return IsPassable(Keeper.Location) &&
                   Boxes.All(box => box.Location != Keeper.Location) &&
                   Boxes.All(box => IsPassable(box.Location)) &&
                   Boxes.AllDifferent();
        }

        public IGameObject GetStaticObject(Vector location)
        {
            if (!StaticObjects.Select(obj => obj.Location).Contains(location))
                return new Wall(location);
            return StaticObjects.First(obj => obj.Location == location);
        }


        public bool IsPassable(Vector location)
        {
            return GetStaticObject(location) is IPassableObject;
        }
    }
}