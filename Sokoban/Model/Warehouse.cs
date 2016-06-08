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
        private readonly Warehouse previousWarehouse;

        public Warehouse(IEnumerable<IGameObject> staticObjects, IEnumerable<Box> boxes, WarehouseKeeper keeper)
        {
            StaticObjects = staticObjects.ToImmutableDictionary(obj => obj.Location);
            Boxes = boxes.ToImmutableArray();
            Keeper = keeper;
        }

        private Warehouse(Warehouse previous, ImmutableArray<Box> boxes, WarehouseKeeper keeper)
        {
            previousWarehouse = previous;
            StaticObjects = previous.StaticObjects;
            Boxes = boxes;
            Keeper = keeper;
        }

        public IEnumerable<T> GetAll<T>() where T : class, IGameObject
        {
            return AllObjects
                .Select(e => e as T)
                .Where(e => e != null);
        }

        public IEnumerable<IGameObject> AllObjects
        {
            get
            {
                foreach (var staticObj in StaticObjects.Values)
                    yield return staticObj;
                foreach (var box in Boxes)
                    yield return box;
            }
        }

        public Warehouse MoveKeeper<TAction>(Direction direction) where TAction : class, IAction, new()
        {
            var newBoxes = new TAction().MoveBoxes(Boxes, direction, Keeper);
            var newKeeper = Keeper.Move(direction);

            if (IsPassable(newKeeper.Location) &&
                newBoxes.All(box => box.Location != newKeeper.Location) &&
                newBoxes.All(box => IsPassable(box.Location)) &&
                newBoxes.AllDifferent())
                return new Warehouse(this, newBoxes, newKeeper);

            return this;
        }

        public Warehouse Undo()
        {
            return previousWarehouse ?? this;
        }

        private bool IsPassable(Vector location)
        {
            return StaticObjects.ContainsKey(location) && StaticObjects[location] is IPassableObject;
        }
    }
}
