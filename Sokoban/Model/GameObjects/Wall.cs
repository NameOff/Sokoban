using System.Runtime.Serialization.Formatters;
using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class Wall : IGameObject
    {
        public Vector Location { get; }

        public Wall(Vector location)
        {
            Location = location;
        }

        public Wall(int x, int y)
        {
            Location = new Vector(x, y);
        }
    }
}
