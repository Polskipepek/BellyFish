using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;

namespace BellyFish.Source.Game.Gameplay {
    struct Move {
        public int MoveNumber { get; init; }
        public bool IsPromoting { get; init; }
        public bool IsTake => TakenPawn != null;
        public bool IsCastling => CastlingRook.HasValue;
        public Pawn Pawn { get; init; }
        public Pawn TakenPawn { get; init; }
        public Position PawnOriginPos { get; init; }
        public Position NewPawnPos { get; init; }
        public Position? CastlingRook { get; init; }
    }
}