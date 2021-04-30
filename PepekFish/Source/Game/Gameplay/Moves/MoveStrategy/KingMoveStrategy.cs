using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using BellyFish.Source.Misc.Extensions;
using System.Collections.Generic;

namespace BellyFish.Source.Game.Gameplay.Moves.MoveStrategy {
    class KingMoveStrategy : IMoveStrategy {
        public IEnumerable<Move> GetMoves(Checkerboard checkerboard, Pawn pawn) {
            var pawnPos = pawn.Position;

            var moveUpperRight = FindMove(checkerboard, pawn, pawnPos + new Position((char)1, 1));
            var moveUpperLeft = FindMove(checkerboard, pawn, pawnPos - new Position((char)1, -1));
            var moveLowerRight = FindMove(checkerboard, pawn, pawnPos + new Position((char)1, -1));
            var moveLowerLeft = FindMove(checkerboard, pawn, pawnPos - new Position((char)1, 1));

            var moveRight = FindMove(checkerboard, pawn, pawnPos + new Position((char)1, 0));
            var moveLower = FindMove(checkerboard, pawn, pawnPos + new Position((char)0, -1));
            var moveLeft = FindMove(checkerboard, pawn, pawnPos - new Position((char)1, 0));
            var moveUpper = FindMove(checkerboard, pawn, pawnPos + new Position((char)0, 1));

            if (moveUpperRight.HasValue) yield return moveUpperRight.Value;
            if (moveUpperLeft.HasValue) yield return moveUpperLeft.Value;
            if (moveLowerLeft.HasValue) yield return moveLowerLeft.Value;
            if (moveLowerRight.HasValue) yield return moveLowerRight.Value;

            if (moveRight.HasValue) yield return moveRight.Value;
            if (moveLower.HasValue) yield return moveLower.Value;
            if (moveLeft.HasValue) yield return moveLeft.Value;
            if (moveUpper.HasValue) yield return moveUpper.Value;
        }

        Move? FindMove(Checkerboard checkerboard, Pawn pawn, Position position) {
            if (checkerboard.IsEmptyButExists(position)) {
                return checkerboard.GetMove(pawn, position);
            }
            return null;
        }
    }
}