using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban.Infrastructure;

namespace Sokoban.Application
{
    public interface IGameController
    {
        Direction GetDirection();
        bool IsUndo();
    }
}
