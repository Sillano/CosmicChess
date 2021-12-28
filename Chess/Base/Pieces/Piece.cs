namespace Chess.Base.Pieces
{
    public abstract class Piece
    {
        public Piece(AlgebraicNotation position, Board chessboard, bool isWhite)
        {
            this.Position = position;
            this.Chessboard = chessboard;
            this.IsWhite = isWhite;
        }

        protected Func<IEnumerable<AlgebraicNotation>> MovesFunctions { get; set; } = () => Array.Empty<AlgebraicNotation>();

        protected Func<IEnumerable<AlgebraicNotation>> CaptureFunctions { get; set; } = () => Array.Empty<AlgebraicNotation>();

        protected Board Chessboard { get; set; }

        public AlgebraicNotation Position { get; set; }

        public bool IsWhite { get; set; }

        public bool MovedSinceStart { get; set; } = false;

        public bool IsAlive { get; set; } = true;

        public AlgebraicNotation[] GetMoves() => this.MovesFunctions
            .GetInvocationList()
            .Cast<Func<IEnumerable<AlgebraicNotation>>>()
            .SelectMany(x => x())
            .ToArray();

        public AlgebraicNotation[] GetCaptures() => this.CaptureFunctions
            .GetInvocationList()
            .Cast<Func<IEnumerable<AlgebraicNotation>>>()
            .SelectMany(x => x())
            .ToArray();

        protected IEnumerable<AlgebraicNotation> ExplorePositiveXAxisForMoves() => ExploreAxisForMoves(i => (i, 0));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeXAxisForMoves() => ExploreAxisForMoves(i => (-i, 0));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveYAxisForMoves() => ExploreAxisForMoves(i => (0, i));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeYAxisForMoves() => ExploreAxisForMoves(i => (0, -i));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveXPositiveYAxisForMoves() => ExploreAxisForMoves(i => (i, i));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveXNegativeYAxisForMoves() => ExploreAxisForMoves(i => (i, -i));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeXPositiveYAxisForMoves() => ExploreAxisForMoves(i => (-i, i));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeXNegativeYAxisForMoves() => ExploreAxisForMoves(i => (-i, -i));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveXAxisForCapture() => ExploreAxisForCapture(i => (i, 0));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeXAxisForCapture() => ExploreAxisForCapture(i => (-i, 0));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveYAxisForCapture() => ExploreAxisForCapture(i => (0, i));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeYAxisForCapture() => ExploreAxisForCapture(i => (0, -i));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveXPositiveYAxisForCapture() => ExploreAxisForCapture(i => (i, i));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveXNegativeYAxisForCapture() => ExploreAxisForCapture(i => (i, -i));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeXPositiveYAxisForCapture() => ExploreAxisForCapture(i => (-i, i));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeXNegativeYAxisForCapture() => ExploreAxisForCapture(i => (-i, -i));

        private IEnumerable<AlgebraicNotation> ExploreAxisForMoves(Func<int, (int, int)> func)
        {
            var movesList = new List<AlgebraicNotation>();

            (int x, int y) basePosition = this.Position;

            for (int i = 1; i < 8; i++)
            {
                var nextMove = basePosition.Apply(func(i));

                if (!this.Chessboard.IsValidMove(this.Position, nextMove))
                    break;

                movesList.Add(nextMove);
            }

            return movesList;
        }

        private IEnumerable<AlgebraicNotation> ExploreAxisForCapture(Func<int, (int, int)> func)
        {
            var captureList = new List<AlgebraicNotation>();

            (int x, int y) basePosition = this.Position;

            for (int i = 1; i < 8; i++)
            {
                var capture = basePosition.Apply(func(i));

                if (this.Chessboard.IsValidMove(this.Position, capture))
                    continue;

                if (this.Chessboard.IsValidCapture(this.Position, capture, this.IsWhite))
                    captureList.Add(capture);

                break;
            }

            return captureList;
        }
    }
}
