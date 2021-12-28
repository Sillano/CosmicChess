// See https://aka.ms/new-console-template for more information

using Chess.BaseChess;
using Chess.ConsoleChess;
using System.Text.RegularExpressions;

namespace Chess
{
    public class Program
    {
        public static void Main(params string[] args)
        {
            var chessboard = new ConsoleBoard();

            var isOK = chessboard.TryGetPiece(new("c3"), out Piece piece);

            if (isOK)
            {
                var xd = piece.GetMoves();

                foreach (var x in xd)
                {
                    Console.WriteLine(x);
                }
            }
        }
    }
}


