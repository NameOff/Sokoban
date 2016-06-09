using Newtonsoft.Json;
using Sokoban.Infrastructure;
using Sokoban.Model.Interfaces;

namespace Sokoban.Model.GameObjects
{
    public class Box : IGameObject
    {
        public Box(int x, int y)
        {
            Location = new Vector(x, y);
        }


        [JsonConstructor]
        public Box(Vector location)
        {
            Location = location;
        }

        public Vector Location { get; }

        public Box Move(Direction direction)
        {
            return new Box(Location + direction.Vector);
        }

        #region Value semantics

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != GetType()) return false;
            var other = (Box) obj;
            return other.Location == Location;
        }

        public override int GetHashCode()
        {
            return Location.GetHashCode();
        }

        #endregion
    }
}