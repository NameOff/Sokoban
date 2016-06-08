using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Sokoban.Infrastructure;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model
{
    public class Warehouse
    {
        public readonly ImmutableDictionary<Vector, IGameObject> StaticObjects;
        public readonly ImmutableArray<Box> Boxes;
        public readonly WarehouseKeeper Keeper;


        public Warehouse(IEnumerable<IGameObject> staticObjects, IEnumerable<Box> boxes, WarehouseKeeper keeper)
        {
            StaticObjects = staticObjects.ToImmutableDictionary(obj => obj.Location);
            Boxes = boxes.ToImmutableArray();
            Keeper = keeper;
        }

        private Warehouse(ImmutableDictionary<Vector, IGameObject> staticObjects, ImmutableArray<Box> boxes, WarehouseKeeper keeper)
        {
            StaticObjects = staticObjects;
            Boxes = boxes;
            Keeper = keeper;
        }

        public Warehouse MoveKeeper<TAction>(Direction direction) where TAction : class, IAction, new()
        {
            var newBoxes = new TAction().MoveBoxes(Boxes, direction, Keeper);
            var newKeeper = Keeper.Move(direction);

            if (IsPassable(newKeeper.Location) &&
                newBoxes.All(box => box.Location != newKeeper.Location) &&
                newBoxes.All(box => IsPassable(box.Location)) &&
                newBoxes.AllDifferent())
                return new Warehouse(StaticObjects, newBoxes, newKeeper);

            return this;
        }


        private bool IsPassable(Vector location)
        {
            return StaticObjects.ContainsKey(location) && StaticObjects[location] is IPassableObject;
        }
    }
}
