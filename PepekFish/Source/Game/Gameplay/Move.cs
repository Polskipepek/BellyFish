using PepekFish.Source.Misc;

namespace BellyFish.Source.Game.Gameplay {
    class Move {
        public bool IsTake => TakePawnPos.HasValue;
        public Int2D PawnOriginPos { get; set; }
        public Int2D NewPawnPos { get; set; }
        public Int2D? TakePawnPos { get; set; }
    }
}
