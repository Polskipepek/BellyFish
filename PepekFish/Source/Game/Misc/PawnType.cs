using BellyFish.Source.Misc.Attributes;

namespace PepekFish.Source.Game.Misc {
    enum PawnType {
        [PawnTypeValue(1)]
        Pawn,

        [PawnTypeValue(3)]
        Bishop,

        [PawnTypeValue(3)]
        Knight,

        [PawnTypeValue(5)]
        Rook,

        [PawnTypeValue(8)]
        Queen,

        [PawnTypeValue(10)]
        King,
    }
}
