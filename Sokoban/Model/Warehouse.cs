using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Sokoban.Infrastructure;
using Sokoban.Model;
using Sokoban.Model.Interfaces;

namespace Sokoban
{
    public class Warehouse
    {
        public readonly ImmutableDictionary<Vector, IImmovable> ImmovableObjects;
        public readonly MovableObjects MovableObjects;
        private readonly Warehouse previousWarehouse;

        public Warehouse(MovableObjects movableObjects, IEnumerable<IImmovable> immovableObjects)
        {
            MovableObjects = movableObjects;
            ImmovableObjects = immovableObjects.ToImmutableDictionary(obj => obj.Location);
        }

        private Warehouse(Warehouse previousWarehouse, MovableObjects movableObjects)
        {
            this.previousWarehouse = previousWarehouse;
            ImmovableObjects = previousWarehouse.ImmovableObjects;
            MovableObjects = movableObjects;
        }

        public Warehouse ChangeAfter(IAction action)
        {
            var newMovable = action.Move(MovableObjects);

            if (newMovable.AllLocations.AllDifferent() &&
                newMovable.AllLocations.Any(IsPassable))
                return new Warehouse(this, newMovable);

            return this;
        }

        public Warehouse Undo()
        {
            return previousWarehouse ?? this;
        }

        private bool IsPassable(Vector location)
        {
            return ImmovableObjects.ContainsKey(location) && ImmovableObjects[location].IsPassable;
        }
    }
}
