using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using BellyFish.Source.Misc.Extensions;
using System.Collections.Generic;
namespace BellyFish.Source.Game.Gameplay.Moves.MoveStrategy {
    class BishopMoveStrategy : IMoveStrategy {
        public IEnumerable<Move> GetMoves(Checkerboard checkerboard, Pawn pawn) {

            var movesUpperRight = checkerboard.FindMovesAlongDirection(pawn, new Position(1, 1));
            var movesUpperLeft = checkerboard.FindMovesAlongDirection(pawn, new Position(1, -1));
            var movesLowerRight = checkerboard.FindMovesAlongDirection(pawn,  new Position(-1, 1));
            var movesLowerLeft = checkerboard.FindMovesAlongDirection(pawn, new Position(-1, -1));

            foreach (var move in movesUpperRight) 
                yield return move;
            
            foreach (var move in movesUpperLeft) 
                yield return move;
            
            foreach (var move in movesLowerRight) 
                yield return move;
            
            foreach (var move in movesLowerLeft) 
                yield return move;
        }
    }
}
