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
        private readonly Level previousLevel;

        public Level(Warehouse warehouse)
        {
            Warehouse = warehouse;
        }

        private Level(Level previousLevel, Warehouse warehouse)
        {
            this.previousLevel = previousLevel;
            MovesCount = Warehouse == warehouse ? previousLevel.MovesCount : previousLevel.MovesCount + 1;
            Warehouse = warehouse;
        }

        public Level NextStep<TAction>(Direction dir) where TAction : class, IAction, new()
        {
            var newWarehouse = Warehouse.MoveKeeper<TAction>(dir);
            return new Level(this, newWarehouse);
        }

        public Level PreviousStep()
        {
            if (previousLevel == null)
                throw new InvalidOperationException();
            return previousLevel;
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
            return AllObjects
                .Select(e => e as T)
                .Where(e => e != null);
        }

        public bool IsOver()
        {
            return GetAll<Box>().All(box => Warehouse.StaticObjects[box.Location] is Storage);
        }
    }
}
