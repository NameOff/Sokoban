using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sokoban.Model
{
    public class Game
    {
        public readonly ImmutableArray<Level> Levels;
        public readonly int LevelNumber;

        public Level CurrentLevel => Levels[LevelNumber];

        public Game(IEnumerable<Level> levels)
        {
            Levels = levels.ToImmutableArray();
            LevelNumber = 0;
        }

        private Game(ImmutableArray<Level> levels, int oldLevelNumber)
        {
            Levels = levels;
            LevelNumber = oldLevelNumber + 1;
        }

        public bool HasNextLevel()
        {
            return LevelNumber < Levels.Length;
        }

        public Game NextLevel()
        {
            if (HasNextLevel())
                return new Game(Levels, LevelNumber);
            throw new InvalidOperationException();
        }
    }
}
