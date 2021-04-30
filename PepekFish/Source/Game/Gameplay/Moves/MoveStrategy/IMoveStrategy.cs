using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Pawns;
using System.Collections.Generic;

namespace BellyFish.Source.Game.Gameplay.Moves.MoveStrategy {
    interface IMoveStrategy {
        public IEnumerable<Move> GetMoves(Checkerboard checkerboard, Pawn pawn);
    }
}
