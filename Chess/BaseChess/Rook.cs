namespace Chess.BaseChess
{
    public class Rook : Piece
    {
        public Rook(AlgebraicNotation position, Board chessboard, bool isWhite) : base(position, chessboard, isWhite)
        {
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
