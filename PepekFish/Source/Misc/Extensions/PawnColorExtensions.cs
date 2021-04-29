using BellyFish.Source.Game.Misc;

namespace BellyFish.Source.Misc.Extensions {
    static class PawnColorExtensions {
        public static PawnColor Opposite(this PawnColor pawnColor) => pawnColor == PawnColor.White ? PawnColor.Black : PawnColor.White;
    }
}
