namespace BellyFish.Source.Misc {
    struct Position {
        public Position(int letters, int digits) {
            Letters = letters;
            Digits = digits;
        }

        public int Letters { get; }
        public int Digits { get; }

        public static Position operator +(Position a, Position b) => new Position(a.Digits + b.Digits, a.Letters + b.Letters);
        public static Position operator -(Position a, Position b) => new Position(a.Digits - b.Digits, a.Letters - b.Letters);
        public static bool operator ==(Position a, Position b) => a.Digits == b.Digits && a.Letters == b.Letters;
        public static bool operator !=(Position a, Position b) => a.Digits == b.Digits && a.Letters == b.Letters;

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
