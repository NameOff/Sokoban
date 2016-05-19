using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban.Model;
using Sokoban.Model.Interfaces;

namespace Sokoban
{
    public class Box : IMovable
    {
        public Vector Location { get; }
        public Box(Vector location)
        {
            Location = location;
        }
        public IMovable MoveTo(Direction direction)
        {
            return new Box(Location + direction.Vector);
        }
    }
}
