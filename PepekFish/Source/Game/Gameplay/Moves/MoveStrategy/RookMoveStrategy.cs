using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using BellyFish.Source.Misc.Extensions;
using System.Collections.Generic;

namespace BellyFish.Source.Game.Gameplay.Moves.MoveStrategy {
    class RookMoveStrategy : IMoveStrategy {
        public IEnumerable<Move> GetMoves(Checkerboard checkerboard, Pawn pawn) {
            var movesUpper = checkerboard.FindMovesAlongDirection(pawn, new Position((char)0, 1));
            var movesLeft = checkerboard.FindMovesAlongDirection(pawn, new Position((char)0, 0) - new Position((char)1, 0));
            var movesLower = checkerboard.FindMovesAlongDirection(pawn, new Position((char)0, -1));
            var movesRight = checkerboard.FindMovesAlongDirection(pawn, new Position((char)1, 0));

            foreach (var move in movesUpper) {
                yield return move;
            }

            foreach (var move in movesLeft) {
                yield return move;
            }

            foreach (var move in movesLower) {
                yield return move;
            }

            foreach (var move in movesRight) {
                yield return move;
            }
        }
    }
}