// See https://aka.ms/new-console-template for more information

using Chess.Base;
using Chess.Base.Pieces;
using Chess.ConsoleChess;
using System.Text.RegularExpressions;

namespace Chess
{
    public class Program
    {
        public static void Main(params string[] args)
        {
            var chessboard = new Board();

            Piece piece;

            var isOK = chessboard.TryGetPiece(new("e5"), out piece);

            if (isOK)
            {
                var xd = piece.GetMoves();

                Console.WriteLine("Moves:");

                foreach (var x in xd)
                {
                    Console.WriteLine(x);
                }

                xd = piece.GetCaptures();

                Console.WriteLine("Captures:");

                foreach (var x in xd)
                {
                    Console.WriteLine(x);
                }
            }
        }
    }
}


