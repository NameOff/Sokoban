using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model
{
    public class Game
    {
        public IEnumerable<Level> Play(Level level, Func<Level, Direction> getNextMove)
        {
            var currentLevel = level;
            yield return currentLevel;
            while (!level.IsOver())
            {
                currentLevel = currentLevel.NextStep<RegularMove>(getNextMove(currentLevel));
                yield return currentLevel;
            }
        }

    }
}