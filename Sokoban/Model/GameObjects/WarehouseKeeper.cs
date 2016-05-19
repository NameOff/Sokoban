using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban.Infrastructure;
using Sokoban.Model;
using Sokoban.Model.Interfaces;

namespace Sokoban
{
    public class WarehouseKeeper : IMovable
    {
        public Vector Location { get; }

        public WarehouseKeeper(Vector location)
        {
            Location = location;
        }

        public IMovable MoveTo(Direction direction)
        {
            return new WarehouseKeeper(Location + direction.Vector);
        }
    }
}
