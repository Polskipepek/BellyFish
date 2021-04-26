namespace PepekFish.Source.Misc {
    struct Int2D {
        public int Letters { get; private set; }
        public int Digits { get; private set; }
        public Int2D(int letters, int digits) {
            Letters = letters;
            Digits = digits;
        }

        public static Int2D operator +(Int2D a, Int2D b) => new Int2D(a.Digits + b.Digits, a.Letters + b.Letters);
        public static Int2D operator -(Int2D a, Int2D b) => new Int2D(a.Digits - b.Digits, a.Letters - b.Letters);
        public static bool operator ==(Int2D a, Int2D b) => a.Digits == b.Digits && a.Letters == b.Letters;
        public static bool operator !=(Int2D a, Int2D b) => a.Digits == b.Digits && a.Letters == b.Letters;

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
