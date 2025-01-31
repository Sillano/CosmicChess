﻿namespace Chess.Base.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(AlgebraicNotation position, Board chessboard, bool isWhite) : base(position, chessboard, isWhite)
        {
            this.MovesFunctions += NormalMove;

            this.CaptureFunctions += NormalCapture;
        }

        public bool MovedDobule { get; set; }

        private IEnumerable<AlgebraicNotation> NormalMove()
        {
            (int x, int y) basePosition = this.Position;

            var nextMove = basePosition.Apply(0, this.IsWhite ? 1 : -1);

            if (!this.Chessboard.IsValidMove(nextMove))
                return Array.Empty<AlgebraicNotation>();

            return new [] { (AlgebraicNotation)nextMove };
        }

        private IEnumerable<AlgebraicNotation> DoubleMove()
        {
            if(this.MovedSinceStart)
                return Array.Empty<AlgebraicNotation>();

            (int x, int y) basePosition = this.Position;

            var nextMove = basePosition.Apply(0, this.IsWhite ? 2 : -2);

            if (!this.Chessboard.IsValidMove(nextMove))
                return Array.Empty<AlgebraicNotation>();

            return new[] { (AlgebraicNotation)nextMove };
        }

        private IEnumerable<AlgebraicNotation> NormalCapture()
        {
            var captureList = new List<AlgebraicNotation>();

            (int x, int y) basePosition = this.Position;

            var capture = basePosition.Apply(1, this.IsWhite ? 1 : -1);

            if (this.Chessboard.IsValidCapture(capture, this.IsWhite))
                captureList.Add(capture);

            capture = basePosition.Apply(-1, this.IsWhite ? 1 : -1);

            if (this.Chessboard.IsValidCapture(capture, this.IsWhite))
                captureList.Add(capture);

            return captureList;
        }
    }
}
