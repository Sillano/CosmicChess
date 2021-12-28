using Chess.Base.Pieces;

namespace Chess.Base
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

            this.pieces[8] = new Pawn("a2", this, true);
            this.pieces[9] = new Pawn("b2", this, true);
            this.pieces[10] = new Pawn("c2", this, true);
            this.pieces[11] = new Pawn("d2", this, true);
            this.pieces[12] = new Pawn("e2", this, true);
            this.pieces[13] = new Pawn("f2", this, true);
            this.pieces[14] = new Pawn("g2", this, true);
            this.pieces[15] = new Pawn("h2", this, true);

            //Black
            this.pieces[16] = new Rook("a8", this, false);
            this.pieces[17] = new Knight("b8", this, false);
            this.pieces[18] = new Bishop("c8", this, false);
            this.pieces[19] = new Queen("d8", this, false);
            this.pieces[20] = new King("e8", this, false);
            this.pieces[21] = new Bishop("f8", this, false);
            this.pieces[22] = new Knight("g8", this, false);
            this.pieces[23] = new Rook("h8", this, false);

            this.pieces[24] = new Pawn("a7", this, false);
            this.pieces[25] = new Pawn("b7", this, false);
            this.pieces[26] = new Pawn("c7", this, false);
            this.pieces[27] = new Pawn("d7", this, false);
            this.pieces[28] = new Pawn("e7", this, false);
            this.pieces[29] = new Pawn("f7", this, false);
            this.pieces[30] = new Pawn("g7", this, false);
            this.pieces[31] = new Pawn("h7", this, false);
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
