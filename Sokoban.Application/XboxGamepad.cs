using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Sokoban.Infrastructure;
using Microsoft.Xna.Framework.Input;

namespace Sokoban.Application
{
    public class XboxGamepad : IGameController
    {
        public Direction GetDirection()
        {
            var state = GamePad.GetState(PlayerIndex.One);
            if (state.DPad.Down == ButtonState.Pressed)
                return Direction.Down;
            if (state.DPad.Up == ButtonState.Pressed)
                return Direction.Up;
            if (state.DPad.Left == ButtonState.Pressed)
                return Direction.Left;
            if (state.DPad.Right == ButtonState.Pressed)
                return Direction.Right;
            return Direction.None;
        }

        public bool IsUndo()
        {
            return GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed;
        }
    }
}
