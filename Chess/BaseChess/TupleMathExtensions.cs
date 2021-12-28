using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.BaseChess
{
    internal static class TupleMathExtensions
    {
        public static (int, int) Apply(this (int x, int y) a, (int x, int y) b) => (a.x + b.x, a.y + b.y);

        public static (int, int) Apply(this (int x, int y) a, int x, int y) => (a.x + x, a.y + y);
    }
}
