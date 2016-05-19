using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Warehouse
    {
        public readonly ImmutableArray<ImmutableArray<Location>> Map;
    }
}
