namespace BellyFish.Source.Misc {
    struct Position {
        public Position(int x, int y) {
            Letter = x;
            Digit = y;
        }

        public int Letter { get; }
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
            return $"{(char)(Letter+97)}{Digit+1}";
        }
    }
}
