namespace Chess.Base.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(AlgebraicNotation position, Board chessboard, bool isWhite) : base(position, chessboard, isWhite)
        {
            this.MovesFunctions += base.ExplorePositiveXPositiveYAxisForMoves;
            this.MovesFunctions += base.ExplorePositiveXNegativeYAxisForMoves;
            this.MovesFunctions += base.ExploreNegativeXPositiveYAxisForMoves;
            this.MovesFunctions += base.ExploreNegativeXNegativeYAxisForMoves;

            this.CaptureFunctions += base.ExplorePositiveXPositiveYAxisForCapture;
            this.CaptureFunctions += base.ExplorePositiveXNegativeYAxisForCapture;
            this.CaptureFunctions += base.ExploreNegativeXPositiveYAxisForCapture;
            this.CaptureFunctions += base.ExploreNegativeXNegativeYAxisForCapture;
        }
    }
}
