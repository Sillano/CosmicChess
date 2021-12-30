namespace Chess.Base.Pieces
{
    public class Knight : Piece
    {
        private static readonly (int x, int y)[] movesMatrix = { (2, 1), (-2, 1), (2, -1), (-2, -1), (1, 2), (-1, 2), (1, -2), (-1, -2) };

        public Knight(AlgebraicNotation position, Board chessboard, bool isWhite) : base(position, chessboard, isWhite)
        {
            this.MovesFunctions += Move;

            this.CaptureFunctions += Capture;
        }

        private IEnumerable<AlgebraicNotation> Move()
        {
            var movesList = new List<AlgebraicNotation>();

            (int x, int y) basePosition = this.Position;

            foreach (var move in movesMatrix)
            {
                var nextMove = basePosition.Apply(move);

                if (this.Chessboard.IsValidMove(nextMove) && !this.Chessboard.EvaluateForCheckAfterMove(new CheckValidationConditions(basePosition, nextMove, this.IsWhite)))
                    movesList.Add(nextMove);
            }

            return movesList;
        }

        private IEnumerable<AlgebraicNotation> Capture(CheckValidationConditions? conditions)
        {
            var captureList = new List<AlgebraicNotation>();

            (int x, int y) basePosition = this.Position;

            foreach (var move in movesMatrix)
            {
                var capture = basePosition.Apply(move);

                if (this.Chessboard.IsValidCapture(capture, this.IsWhite, conditions) && (conditions is not null || !this.Chessboard.EvaluateForCheckAfterMove(new CheckValidationConditions(basePosition, capture, capture, this.IsWhite))))
                    captureList.Add(capture);   
            }

            return captureList;
        }
    }
}
