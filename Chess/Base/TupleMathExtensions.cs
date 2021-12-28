namespace Chess.Base
{
    internal static class TupleMathExtensions
    {
        public static (int, int) Apply(this (int x, int y) a, (int x, int y) b) => (a.x + b.x, a.y + b.y);

        public static (int, int) Apply(this (int x, int y) a, int x, int y) => (a.x + x, a.y + y);
    }
}
