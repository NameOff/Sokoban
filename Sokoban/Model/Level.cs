using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban.Infrastructure;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model
{
    public class Level
    {
        public Warehouse Warehouse;
        public readonly int MovesCount;
        public readonly Level PreviousStep;

        public Level(Warehouse warehouse)
        {
            Warehouse = warehouse;
        }

        private Level(Level previousStep, Warehouse warehouse)
        {
            PreviousStep = previousStep;
            MovesCount = Warehouse == warehouse ? previousStep.MovesCount : previousStep.MovesCount + 1;
            Warehouse = warehouse;
        }

        public Level NextStep<TAction>(Direction direction) where TAction : class, IAction, new()
        {
            var newWarehouse = Warehouse.MoveKeeper<TAction>(direction);
            return new Level(this, newWarehouse);
        }

        public IEnumerable<IGameObject> AllObjects
        {
            get
            {
                foreach (var staticObj in Warehouse.StaticObjects.Values)
                    yield return staticObj;
                foreach (var box in Warehouse.Boxes)
                    yield return box;
                yield return Warehouse.Keeper;
            }
        }

        public IEnumerable<T> GetAll<T>() where T : class, IGameObject
        {
            return AllObjects.GetAll<T, IGameObject>();
        }

        public bool IsOver()
        {
            return GetAll<Box>()
                .All(box => Warehouse.StaticObjects[box.Location] is Storage);
        }
    }
}
