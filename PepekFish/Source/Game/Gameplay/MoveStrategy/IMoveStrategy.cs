using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Pawns;
using System.Collections.Generic;

namespace BellyFish.Source.Game.Gameplay.MoveStrategy {
    interface IMoveStrategy {
        public IEnumerable<Move> GetMoves(Checkerboard checkerboard, Pawn pawn);
        public IEnumerable<Move> GetTakes(Checkerboard checkerboard, Pawn pawn);
    }
}
