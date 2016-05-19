using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban.Infrastructure;

namespace Sokoban.Model.Interfaces
{
    public interface IMovable : IGameObject
    {
        IMovable MoveTo(Direction direction);
    }
}
