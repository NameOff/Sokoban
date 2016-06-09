using System.Collections.Generic;
using System.Linq;
using Sokoban.Infrastructure;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model
{
    public class Level
    {
        public readonly int MovesCount;
        private readonly Level previousStep;
        public Warehouse Warehouse;

        public Level(Warehouse warehouse)
        {
            Warehouse = warehouse;
        }

        private Level(Level previousStep, Warehouse warehouse)
        {
            this.previousStep = previousStep;
            MovesCount = previousStep.MovesCount + 1;
            Warehouse = warehouse;
        }

        public IEnumerable<IGameObject> AllObjects
        {
            get
            {
                foreach (var staticObj in Warehouse.StaticObjects)
                    yield return staticObj;
                foreach (var box in Warehouse.Boxes)
                    yield return box;
                yield return Warehouse.Keeper;
            }
        }

        public Level NextStep<TAction>(Direction direction) where TAction : class, IAction, new()
        {
            var newWarehouse = Warehouse.MoveKeeper<TAction>(direction);
            return Warehouse == newWarehouse ? this : new Level(this, newWarehouse);
        }

        public Level Undo()
        {
            return previousStep ?? this;
        }

        public IEnumerable<T> GetAll<T>() where T : class, IGameObject
        {
            return AllObjects.GetAll<T, IGameObject>();
        }

        public bool IsOver()
        {
            return GetAll<Box>()
                .All(box => Warehouse.GetStaticObject(box.Location) is Storage);
        }
    }
}