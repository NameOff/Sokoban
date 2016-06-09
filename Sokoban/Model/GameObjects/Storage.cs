using Newtonsoft.Json;
using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class Storage : IPassableObject
    {
        public Storage(int x, int y)
        {
            Location = new Vector(x, y);
        }

        [JsonConstructor]
        public Storage(Vector location)
        {
            Location = location;
        }

        public Vector Location { get; }
    }
}