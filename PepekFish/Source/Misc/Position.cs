namespace BellyFish.Source.Misc {
    struct Position {
        public Position(char letter, int digit) {
            Letter = letter;
            Digit = digit;
        }

        public char Letter { get; }
        public int Digit { get; }

        public static Position operator +(Position a, Position b) => new((char)(a.Letter + b.Letter), a.Digit + b.Digit);
        public static Position operator -(Position a, Position b) => new((char)(a.Letter - b.Letter), a.Digit - b.Digit);
        public static bool operator ==(Position a, Position b) => a.Letter == b.Letter && a.Digit == b.Digit;
        public static bool operator !=(Position a, Position b) => a.Letter != b.Letter && a.Digit != b.Digit;

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public override string ToString() {
            return $"{Letter}{Digit}";
        }
    }
}
