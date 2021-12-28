using System.Text.RegularExpressions;

namespace Chess.Base
{
    public struct AlgebraicNotation
    {
        private int y = 1;

        private int x = 1;

        private int Y { get => this.y; set => this.y = Board.CheckInsideChessboard(value) ? value : throw new ArgumentOutOfRangeException(nameof(this.Y)); }

        private int X { get => this.x; set => this.x = Board.CheckInsideChessboard(value) ? value : throw new ArgumentOutOfRangeException(nameof(this.X)); }

        public AlgebraicNotation(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public AlgebraicNotation((int x, int y) pair) : this(pair.x, pair.y)
        { }

        public AlgebraicNotation((char x, int y) pair) : this(pair.x, pair.y)
        { }

        public AlgebraicNotation(char x, int y) : this(GetNumberFromLetter(x), y)
        { }

        public AlgebraicNotation(string notationValue) : this(GetPairFromString(notationValue))
        { }

        public static implicit operator AlgebraicNotation((char, int) pair) => new(pair);

        public static implicit operator (char, int)(AlgebraicNotation notation) => (GetCharFromNumber(notation.X), notation.Y);

        public static implicit operator AlgebraicNotation((int, int) pair) => new(pair);

        public static implicit operator (int, int)(AlgebraicNotation notation) => (notation.X, notation.Y);

        public static implicit operator AlgebraicNotation(string notation) => new(notation);

        public static implicit operator string(AlgebraicNotation notation) => notation.ToString();

        public static bool operator ==(AlgebraicNotation a, AlgebraicNotation b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(AlgebraicNotation a, AlgebraicNotation b) => a.X != b.X || a.Y != b.Y;

        public override string ToString() => $"{GetCharFromNumber(this.X)}{this.Y}";

        public static int GetNuberFromChar(string letter) => letter.Length == 1 ? GetNumberFromLetter(letter[0]) : throw new ArgumentOutOfRangeException(nameof(letter));

        public static int GetNumberFromLetter(char letter) => letter switch
        {
            'a' or 'A' or '1' => 1,
            'b' or 'B' or '2' => 2,
            'c' or 'C' or '3' => 3,
            'd' or 'D' or '4' => 4,
            'e' or 'E' or '5' => 5,
            'f' or 'F' or '6' => 6,
            'g' or 'G' or '7' => 7,
            'h' or 'H' or '8' => 8,
            _ => throw new ArgumentOutOfRangeException(nameof(letter))
        };

        public static char GetCharFromNumber(int number) => number switch
        {
            1 => 'a',
            2 => 'b',
            3 => 'c',
            4 => 'd',
            5 => 'e',
            6 => 'f',
            7 => 'g',
            8 => 'h',
            _ => throw new ArgumentOutOfRangeException(nameof(number))
        };

        public static (int, int) GetPairFromString(string stringNotation)
        {
            if (stringNotation.Length != 2)
                throw new InvalidOperationException("Notation does not met the requirements");

            if (string.IsNullOrEmpty(stringNotation))
                throw new ArgumentException("Notation is null or empty", nameof(stringNotation));

            var match = Regex.Match(stringNotation, "([a-h1-8])([1-8])");

            if (!match.Success)
                throw new ArgumentException("Notation is not valid", nameof(stringNotation));

            return (GetNuberFromChar(match.Groups[1].Value), GetNuberFromChar(match.Groups[2].Value));
        }
    }
}
