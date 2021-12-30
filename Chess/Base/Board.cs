using Chess.Base.Pieces;

namespace Chess.Base
{
    public class Board
    {
        private readonly Piece[] pieces = new Piece[32];

        public Board()
        {
            //White
            this.pieces[0] = new Rook("e5", this, true);
            this.pieces[1] = new Knight("b1", this, true);
            this.pieces[2] = new Bishop("c1", this, true);
            this.pieces[3] = new Queen("d1", this, true);
            this.pieces[4] = new King("f5", this, true);
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
            this.pieces[19] = new Queen("b5", this, false);
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

        internal bool IsValidMove((int, int) toPosition, CheckValidationConditions? conditions = null)
        {
            if (!CheckInsideChessboard(toPosition) || toPosition == conditions?.TreatPositionAsOccupied)
                return false;

            // Explanation:
            // if we force treat as dead or free there no need for "piece-check" because there is no possibility to be 2 pieces on one spot
            if (toPosition == conditions?.TreatPositionAsFree)
                return true;

            return !pieces.Any(x => x.IsAlive && x.Position == toPosition && (x.Position != conditions?.TreatPieceOnPositionAsDead));
        }

        internal bool IsValidCapture((int,int) capturingPosition, bool isWhite, CheckValidationConditions? conditions)
            => this.IsValidCapture(capturingPosition, capturingPosition, isWhite, conditions);   

        internal bool IsValidCapture((int, int) toPosition, (int, int) capturingPosition, bool isWhite, CheckValidationConditions? conditions)
        {
            // Explanation:
            // if we force treat as dead or free there no need for "piece-check" because there is no possibility to be 2 pieces on one spot
            if (!CheckInsideChessboard(toPosition))
                return false;

            return pieces.Any(x => x.IsAlive && x.Position == capturingPosition && (x.Position != conditions?.TreatPieceOnPositionAsDead) && x.IsWhite != isWhite);
        }

        internal bool EvaluateForCheckAfterMove(CheckValidationConditions conditions)
        {
            var kingPosition = conditions.IsKing ? conditions.TreatPositionAsOccupied : this.pieces.OfType<King>().First(x => x.IsWhite == conditions.IsWhite).Position;

            var possibleOpponentCaptures = this.pieces.Where(x => x.IsWhite != conditions.IsWhite && x.IsAlive && (x.Position != conditions?.TreatPieceOnPositionAsDead)).SelectMany(x => x.GetCaptures(conditions));

            return possibleOpponentCaptures.Any(x => x == kingPosition);
        }

        internal static bool CheckInsideChessboard((int x, int y) pair) => pair.x >= 1 && pair.x <= 8 && pair.y >= 1 && pair.y <= 8;

        internal static bool CheckInsideChessboard((char x, int y) pair) => pair.x >= 1 && pair.x <= 8 && pair.y >= 1 && pair.y <= 8;

        internal static bool CheckInsideChessboard(int value) => value >= 1 && value <= 8;

        internal static bool CheckInsideChessboard(char value) => value >= 1 && value <= 8;
    }

    public class CheckValidationConditions
    {
        public CheckValidationConditions(
            AlgebraicNotation moveFrom,
            AlgebraicNotation moveTo,
            bool isWhite,
            bool isKing = false)
        {
            this.TreatPositionAsFree = moveFrom;

            this.TreatPositionAsOccupied = moveTo;

            this.IsKing = isKing;

            this.IsWhite = isWhite;
        }

        public CheckValidationConditions(
            AlgebraicNotation moveFrom,
            AlgebraicNotation moveTo,
            AlgebraicNotation treatPieceOnPositionAsDead,
            bool isWhite,
            bool isKing = false) : this(moveFrom, moveTo, isWhite, isKing)
        {
            this.TreatPieceOnPositionAsDead = treatPieceOnPositionAsDead;
        }

        public bool IsWhite { get; set; }

        public bool IsKing { get; set; }

        public AlgebraicNotation TreatPositionAsOccupied { get; set; }

        public AlgebraicNotation TreatPositionAsFree { get; set; }

        public AlgebraicNotation? TreatPieceOnPositionAsDead { get; set; }
    }
}
