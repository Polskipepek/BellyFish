using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;

namespace BellyFish.Source.Game.CheckerBoard.Fields {
    class Field {
        public Field (Position fieldPosition, PawnColor fieldColor) {
            FieldPosition = fieldPosition;
            FieldColor = fieldColor;
        }

        public Position FieldPosition { get; init; }
        public PawnColor FieldColor { get; init; }
        public Pawn GetPawn (Checkerboard checkerboard) => checkerboard.GetPawn (FieldPosition);
    }
}
