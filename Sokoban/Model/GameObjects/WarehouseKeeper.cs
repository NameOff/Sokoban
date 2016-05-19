using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class WarehouseKeeper : IGameObject
    {
        public Location Location { get; }

        public Game Move(Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
