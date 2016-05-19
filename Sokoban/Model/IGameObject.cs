using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    interface IGameObject
    {
        Location Location { get; }
        //int DrawingPriority { get; }
    }
}
