using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban.Model.Interfaces;

namespace Sokoban
{
    public class Storage : IImmovable
    {
        public Vector Location { get; }
        public bool IsPassable { get; }
    }
}
