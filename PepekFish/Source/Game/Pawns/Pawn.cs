using BellyFish.Source.Game.Gameplay;
using PepekFish.Source.Game.CheckerBoard;
using PepekFish.Source.Game.Misc;
using PepekFish.Source.Misc;
using System.Collections.Generic;

namespace PepekFish.Source.Game.Pawns {
    class Pawn {
        public Pawn(Int2D position, PawnColor pawnColor, PawnType pawnType) {
            Position = position;
            PawnColor = pawnColor;
            PawnType = pawnType;
        }
        private Pawn() { }

        public static Pawn DeepCopy(Pawn original) {
            return new Pawn {
                PawnColor = original.PawnColor,
                PawnType = original.PawnType,
                Position = original.Position,
            };
        }

        public Int2D Position { get; set; }
        public PawnColor PawnColor { get; set; }
        public PawnType PawnType { get; set; }

        public IEnumerable<Move> GetAvailableMoves(Checkerboard checkerboard) {
            yield return null;
        }
    }
}
