using BellyFish.Source.Game.Gameplay;
using BellyFish.Source.Game.Pawns;
using System.Collections.Generic;
using System.Linq;

namespace BellyFish.Source.Misc.Extensions {
    static class PawnExtensions {
        public static bool AreOppositeColor(this Pawn pawn, Pawn pawn1) => pawn.PawnColor != pawn1.PawnColor;
        public static IEnumerable<Move> GetAvailableTakes(this Pawn pawn, IEnumerable<Move> allMoves) => allMoves.Where(move => move.IsTake);
    }
}
