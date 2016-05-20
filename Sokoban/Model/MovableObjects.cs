   using System.Collections.Generic;
using System.Collections.Immutable;
using Sokoban.Infrastructure;
using Sokoban.Model.GameObjects;

namespace Sokoban.Model
{
    public class MovableObjects
    {
        public readonly ImmutableArray<Box> Boxes;
        public readonly WarehouseKeeper WarehouseKeeper;

        public IEnumerable<Vector> AllLocations
        {
            get
            {
                foreach (var box in Boxes)
                    yield return box.Location;
                yield return WarehouseKeeper.Location;
            }
        }
        public MovableObjects(IEnumerable<Box> boxes, WarehouseKeeper warehouseKeeper)
        {
            Boxes = boxes.ToImmutableArray();
            WarehouseKeeper = warehouseKeeper;
        }


    }
}
