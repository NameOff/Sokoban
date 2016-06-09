using System.Linq;

namespace Sokoban.Infrastructure
{
    public class Vector
    {
        public readonly int X;
        public readonly int Y;

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return !ReferenceEquals(v1, null) && v1.Equals(v2);
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }

        public static Vector Parse(string str)
        {
            var values = str.Split().Select(int.Parse).ToList();
            return new Vector(values[0], values[1]);
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != GetType()) return false;
            var other = (Vector) obj;
            return other.X == X && other.Y == Y;
        }

        public override int GetHashCode()
        {
            return X*397 + Y;
        }
    }
}