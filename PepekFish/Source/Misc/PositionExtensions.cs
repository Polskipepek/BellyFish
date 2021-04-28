namespace BellyFish.Source.Misc {
    static class PositionExtensions {

        public static Position MoveLetterUp(this Position pos) => pos + new Position(1, 0);
        public static Position MoveLetterDown(this Position pos) => pos + new Position(-1, 0);
        public static Position MoveDigitUp(this Position pos) => pos + new Position(0, 1);
        public static Position MoveDigitDown(this Position pos) => pos + new Position(0, -1);

        public static Position MoveLetterUpDigitUp(this Position pos) => pos + new Position(1, 1);
        public static Position MoveLetterUpDigitDown(this Position pos) => pos + new Position(1, -1);
        public static Position MoveLetterDownDigitDown(this Position pos) => pos + new Position(-1, -1);
        public static Position MoveLetterDownDigitUp(this Position pos) => pos + new Position(-1, -1);
    }
}
