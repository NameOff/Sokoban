using Newtonsoft.Json;
using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class Floor : IPassableObject
    {
        public Floor(int x, int y)
        {
            Location = new Vector(x, y);
        }

        [JsonConstructor]
        public Floor(Vector location)
        {
            Location = location;
        }

        public Vector Location { get; }
    }
}