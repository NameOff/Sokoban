namespace Sokoban.Infrastructure
{
    public class Direction
    {
        public static readonly Direction Up = new Direction(0, -1);
        public static readonly Direction Down = new Direction(0, 1);
        public static readonly Direction Left = new Direction(-1, 0);
        public static readonly Direction Right = new Direction(1, 0);
        public static readonly Direction None = new Direction(0, 0);


        public readonly Vector Vector;

        private Direction(int x, int y)
        {
            Vector = new Vector(x, y);
        }
    }
}