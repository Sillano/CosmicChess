using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.BaseChess
{
    public class Board
    {
        private readonly Piece[] pieces = new Piece[32];

        public Board()
        {
            //White
            this.pieces[0] = new Rook("a1", this, true);
            this.pieces[1] = new Knight("b1", this, true);
            this.pieces[2] = new Bishop("c1", this, true);
            this.pieces[3] = new Queen("d1", this, true);
            this.pieces[4] = new King("e1", this, true);
            this.pieces[5] = new Bishop("f1", this, true);
            this.pieces[6] = new Knight("g1", this, true);
            this.pieces[7] = new Rook("h1", this, true);

            this.pieces[8] = new WhitePawn("a2", this);
            this.pieces[9] = new WhitePawn("b2", this);
            this.pieces[10] = new WhitePawn("c2", this);
            this.pieces[11] = new WhitePawn("d2", this);
            this.pieces[12] = new WhitePawn("e2", this);
            this.pieces[13] = new WhitePawn("f2", this);
            this.pieces[14] = new WhitePawn("g2", this);
            this.pieces[15] = new WhitePawn("h2", this);

            //Black
            this.pieces[16] = new Rook("a8", this, false);
            this.pieces[17] = new Knight("b8", this, false);
            this.pieces[18] = new Bishop("c8", this, false);
            this.pieces[19] = new Queen("d8", this, false);
            this.pieces[20] = new King("e8", this, false);
            this.pieces[21] = new Bishop("f8", this, false);
            this.pieces[22] = new Knight("g8", this, false);
            this.pieces[23] = new Rook("h8", this, false);

            this.pieces[24] = new BlackPawn("a7", this);
            this.pieces[25] = new BlackPawn("b7", this);
            this.pieces[26] = new BlackPawn("c7", this);
            this.pieces[27] = new BlackPawn("d7", this);
            this.pieces[28] = new BlackPawn("e7", this);
            this.pieces[29] = new BlackPawn("f7", this);
            this.pieces[30] = new BlackPawn("g7", this);
            this.pieces[31] = new BlackPawn("h7", this);
        }

        public bool TryGetPiece(AlgebraicNotation position, out Piece piece)
        {
            if (this.pieces.Any(x => x.Position == position))
            {
                piece = this.pieces.First(x => x.Position == position);
                return true;
            }

            piece = null;
            return false;
        }

        public bool IsValidMove(AlgebraicNotation fromPosition, (int, int) toPosition)
        {
            if (!CheckInsideChessboard(toPosition))
                return false;

            return !pieces.Any(x => x.IsAlive && x.Position == toPosition);
        }

        public bool IsValidCapture(AlgebraicNotation fromPosition, (int, int) toPosition, bool isWhite)
        {
            if (!CheckInsideChessboard(toPosition))
                return false;

            return pieces.Any(x => x.Position == toPosition && x.IsWhite != isWhite);
        }

        public static bool CheckInsideChessboard((int x, int y) pair) => pair.x >= 1 && pair.x <= 8 && pair.y >= 1 && pair.y <= 8;

        public static bool CheckInsideChessboard((char x, int y) pair) => pair.x >= 1 && pair.x <= 8 && pair.y >= 1 && pair.y <= 8;

        public static bool CheckInsideChessboard(int value) => value >= 1 && value <= 8;

        public static bool CheckInsideChessboard(char value) => value >= 1 && value <= 8;
    }
}
