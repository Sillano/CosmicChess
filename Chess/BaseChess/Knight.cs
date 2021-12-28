namespace Chess.BaseChess
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

                if (this.Chessboard.IsValidMove(this.Position, nextMove))
                    movesList.Add(nextMove);
            }

            return movesList;
        }

        private IEnumerable<AlgebraicNotation> Capture()
        {
            var captureList = new List<AlgebraicNotation>();

            (int x, int y) basePosition = this.Position;

            foreach (var move in movesMatrix)
            {
                var capture = basePosition.Apply(move);

                if (this.Chessboard.IsValidCapture(this.Position, capture, this.IsWhite))
                    captureList.Add(capture);
            }

            return captureList;
        }
    }
}
