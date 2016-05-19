using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public interface IGameObject
    {
        Vector Location { get; }
    }
}
