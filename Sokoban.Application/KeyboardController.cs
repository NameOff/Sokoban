using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Sokoban.Infrastructure;

namespace Sokoban.Application
{
    public class KeyboardController : IGameController
    {
        public Direction GetDirection()
        {
            if (Keyboard.IsKeyDown(Key.Left))
                return Direction.Left;
            if (Keyboard.IsKeyDown(Key.Right))
                return Direction.Right;
            if (Keyboard.IsKeyDown(Key.Up))
                return Direction.Up;
            if (Keyboard.IsKeyDown(Key.Down))
                return Direction.Down;
            return Direction.None;
        }

        public bool IsUndo()
        {
            return Keyboard.IsKeyDown(Key.Z);
        }
    }
}
