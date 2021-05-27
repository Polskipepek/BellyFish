using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;

namespace BellyFish.Source.Game.Gameplay.Moves {
    struct Move {
        public int MoveNumber { get; init; }
        public bool IsPromoting { get; init; }
        public bool IsTake => TakenPawn != null;
        public bool IsCastling => CastlingRookNewPos.Digit>0;
        public Pawn Pawn { get; init; }
        public Pawn TakenPawn { get; init; }
        public Position PawnOriginPos { get; init; }
        public Position NewPawnPos { get; init; }
        public Position CastlingRookNewPos { get; init; }
        public Pawn CastlingRook{ get; init; }

        public override string ToString() {
            return $"{Pawn.PawnType} {PawnOriginPos} - {NewPawnPos}{(IsTake ? $" Taken pawn: {TakenPawn}" : "")} {(IsCastling ? $" Castling rook: {CastlingRookNewPos} " : $"")}";
        }
    }
}