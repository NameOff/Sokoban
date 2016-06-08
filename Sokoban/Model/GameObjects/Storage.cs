using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class Storage : IPassableObject
    {
        public Vector Location { get; }

        public Storage(int x, int y)
        {
            Location = new Vector(x, y);
        }
    }
}
