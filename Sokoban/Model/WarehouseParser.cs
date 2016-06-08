using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Sokoban.Infrastructure;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model
{
    public static class WarehouseParser
    {
        private static Dictionary<char,  List<Func<int, int, IGameObject>>> CharToFabric = new Dictionary<char, List<Func<int, int, IGameObject>>>();

        public static void Set(char symbol, Func<int, int, IGameObject> fabric)
        {
            if (!CharToFabric.ContainsKey(symbol))
                CharToFabric[symbol] = new List<Func<int, int, IGameObject>>();
            CharToFabric[symbol].Add(fabric);
        }


        static WarehouseParser()
        {
            Set('B', (x, y) => new Box(x, y));
            Set('B', (x, y) => new Storage(x, y));
            Set('b', (x, y) => new Box(x, y));
            //Set('b', (x, y) => new Floor(x, y));
            Set(' ', (x, y) => new Floor(x, y));
            Set('K', (x, y) => new WarehouseKeeper(x, y));
            Set('K', (x, y) => new Storage(x, y));
            Set('k', (x, y) => new WarehouseKeeper(x, y));
            //Set('k', (x, y) => new Floor(x, y));
            Set('W', (x, y) => new Wall(x, y));
        }

        public static Warehouse ParseFromString(IEnumerable<string> lines)
        {
            var objects = new List<IGameObject>();

            var y = 0;
            foreach (var line in lines)
            {
                for (var x = 0; x < line.Length; x++)
                {
                    objects.AddRange(CharToFabric[line[x]].Select(f => f(x, y)));
                }
                y++;
            }
            var keeper = objects.GetAll<WarehouseKeeper, IGameObject>().First();
            var boxes = objects.GetAll<Box, IGameObject>().ToList();

            objects.Remove(keeper);
            var staticObjects = objects.Except(boxes);

            return new Warehouse(staticObjects, boxes, keeper);
        }
    }
}
