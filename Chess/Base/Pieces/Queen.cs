namespace Chess.Base.Pieces
{
    public class Queen : Piece
    {
        public Queen(AlgebraicNotation position, Board chessboard, bool isWhite) : base(position, chessboard, isWhite)
        {
            this.MovesFunctions += base.ExplorePositiveXPositiveYAxisForMoves;
            this.MovesFunctions += base.ExplorePositiveXNegativeYAxisForMoves;
            this.MovesFunctions += base.ExploreNegativeXPositiveYAxisForMoves;
            this.MovesFunctions += base.ExploreNegativeXNegativeYAxisForMoves;

            this.CaptureFunctions += base.ExplorePositiveXPositiveYAxisForCapture;
            this.CaptureFunctions += base.ExplorePositiveXNegativeYAxisForCapture;
            this.CaptureFunctions += base.ExploreNegativeXPositiveYAxisForCapture;
            this.CaptureFunctions += base.ExploreNegativeXNegativeYAxisForCapture;

            this.MovesFunctions += base.ExplorePositiveYAxisForMoves;
            this.MovesFunctions += base.ExploreNegativeYAxisForMoves;
            this.MovesFunctions += base.ExplorePositiveXAxisForMoves;
            this.MovesFunctions += base.ExploreNegativeXAxisForMoves;

            this.CaptureFunctions += base.ExplorePositiveYAxisForCapture;
            this.CaptureFunctions += base.ExploreNegativeYAxisForCapture;
            this.CaptureFunctions += base.ExplorePositiveXAxisForCapture;
            this.CaptureFunctions += base.ExploreNegativeXAxisForCapture;
        }
    }
}
