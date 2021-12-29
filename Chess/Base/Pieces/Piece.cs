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

        internal Func<IEnumerable<AlgebraicNotation>> MovesFunctions { get; set; } = () => Array.Empty<AlgebraicNotation>();

        internal Func<CheckValidationConditions, IEnumerable<AlgebraicNotation>> CaptureFunctions { get; set; } = _ => Array.Empty<AlgebraicNotation>();

        protected Board Chessboard { get; set; }

        public AlgebraicNotation Position { get; set; }

        public bool IsWhite { get; set; }

        public bool MovedSinceStart { get; set; } = false;

        public bool IsAlive { get; set; } = true;

        internal AlgebraicNotation[] GetMoves() => this.MovesFunctions
            .GetInvocationList()
            .Cast<Func<IEnumerable<AlgebraicNotation>>>()
            .SelectMany(x => x())
            .ToArray();

        internal AlgebraicNotation[] GetCaptures(CheckValidationConditions? conditions = null) => this.CaptureFunctions
            .GetInvocationList()
            .Cast<Func<CheckValidationConditions, IEnumerable<AlgebraicNotation>>>()
            .SelectMany(x => x(conditions))
            .ToArray();

        protected IEnumerable<AlgebraicNotation> ExplorePositiveXAxisForMoves() => ExploreAxisForMoves(i => (i, 0));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeXAxisForMoves() => ExploreAxisForMoves(i => (-i, 0));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveYAxisForMoves() => ExploreAxisForMoves(i => (0, i));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeYAxisForMoves() => ExploreAxisForMoves(i => (0, -i));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveXPositiveYAxisForMoves() => ExploreAxisForMoves(i => (i, i));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveXNegativeYAxisForMoves() => ExploreAxisForMoves(i => (i, -i));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeXPositiveYAxisForMoves() => ExploreAxisForMoves(i => (-i, i));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeXNegativeYAxisForMoves() => ExploreAxisForMoves(i => (-i, -i));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveXAxisForCapture(CheckValidationConditions conditions) => ExploreAxisForCapture(conditions, i => (i, 0));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeXAxisForCapture(CheckValidationConditions conditions) => ExploreAxisForCapture(conditions, i => (-i, 0));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveYAxisForCapture(CheckValidationConditions conditions) => ExploreAxisForCapture(conditions, i => (0, i));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeYAxisForCapture(CheckValidationConditions conditions) => ExploreAxisForCapture(conditions, i => (0, -i));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveXPositiveYAxisForCapture(CheckValidationConditions conditions) => ExploreAxisForCapture(conditions, i => (i, i));

        protected IEnumerable<AlgebraicNotation> ExplorePositiveXNegativeYAxisForCapture(CheckValidationConditions conditions) => ExploreAxisForCapture(conditions, i => (i, -i));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeXPositiveYAxisForCapture(CheckValidationConditions conditions) => ExploreAxisForCapture(conditions, i => (-i, i));

        protected IEnumerable<AlgebraicNotation> ExploreNegativeXNegativeYAxisForCapture(CheckValidationConditions conditions) => ExploreAxisForCapture(conditions, i => (-i, -i));

        private IEnumerable<AlgebraicNotation> ExploreAxisForMoves(Func<int, (int, int)> func)
        {
            var movesList = new List<AlgebraicNotation>();

            (int x, int y) basePosition = this.Position;

            for (int i = 1; i < 8; i++)
            {
                var nextMove = basePosition.Apply(func(i));

                if (!this.Chessboard.IsValidMove(nextMove))
                    break;

                movesList.Add(nextMove);
            }

            return movesList;
        }

        private IEnumerable<AlgebraicNotation> ExploreAxisForCapture(CheckValidationConditions conditions, Func<int, (int, int)> func)
        {
            var captureList = new List<AlgebraicNotation>();

            (int x, int y) basePosition = this.Position;

            for (int i = 1; i < 8; i++)
            {
                var capture = basePosition.Apply(func(i));

                if (this.Chessboard.IsValidMove(capture, conditions))
                    continue;

                if (this.Chessboard.IsValidCapture(capture, this.IsWhite, conditions))
                    captureList.Add(capture);

                break;
            }

            return captureList;
        }
    }
}
