using PepekFish.Source.Misc;

namespace BellyFish.Source.Misc {
    static class Int2DExtensitons {

        public static Int2D moveLetterUp(this Int2D pos) => pos + new Int2D(1, 0);
        public static Int2D moveLetterDown(this Int2D pos) => pos + new Int2D(-1, 0);
        public static Int2D moveDigitUp(this Int2D pos) => pos + new Int2D(0, 1);
        public static Int2D moveDigitDown(this Int2D pos) => pos + new Int2D(0, -1);

        public static Int2D moveLetterUpDigitUp(this Int2D pos) => pos + new Int2D(1, 1);
        public static Int2D moveLetterUpDigitDown(this Int2D pos) => pos + new Int2D(1, -1);
        public static Int2D moveLetterDownDigitDown(this Int2D pos) => pos + new Int2D(-1, -1);
        public static Int2D moveLetterDownDigitUp(this Int2D pos) => pos + new Int2D(-1, -1);
    }
}
