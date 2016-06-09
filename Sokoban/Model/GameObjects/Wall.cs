using Newtonsoft.Json;
using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class Wall : IGameObject
    {
        [JsonConstructor]
        public Wall(Vector location)
        {
            Location = location;
        }

        public Wall(int x, int y)
        {
            Location = new Vector(x, y);
        }

        public Vector Location { get; }
    }
}