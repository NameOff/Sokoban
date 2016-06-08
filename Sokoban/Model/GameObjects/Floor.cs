using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class Floor : IPassableObject
    {
        public Vector Location { get; }

        public Floor(int x, int y)
        {
            Location = new Vector(x, y);
        }
    }
}
