using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using BellyFish.Source.Misc.Extensions;
using System.Collections.Generic;

namespace BellyFish.Source.Game.Gameplay.Moves.MoveStrategy {
    class KnightMoveStrategy : IMoveStrategy {
        public IEnumerable<Move> GetMoves(Checkerboard checkerboard, Pawn pawn) {
            var pawnPos = pawn.Position;

            var moveUpperRight = FindMove(checkerboard, pawn, pawnPos + new Position((char)1, 2));
            var moveUpperLeft = FindMove(checkerboard, pawn, pawnPos - new Position((char)1, -2));
            var moveLowerRight = FindMove(checkerboard, pawn, pawnPos + new Position((char)1, -2));
            var moveLowerLeft = FindMove(checkerboard, pawn, pawnPos - new Position((char)1, 2));

            var moveRightUpper = FindMove(checkerboard, pawn, pawnPos + new Position((char)2, 1));
            var moveRightLower = FindMove(checkerboard, pawn, pawnPos + new Position((char)2, -1));
            var moveLeftUpper = FindMove(checkerboard, pawn, pawnPos - new Position((char)2, -1));
            var moveLeftLower = FindMove(checkerboard, pawn, pawnPos - new Position((char)2, 1));

            if (moveUpperRight.HasValue) yield return moveUpperRight.Value;
            if (moveUpperLeft.HasValue) yield return moveUpperLeft.Value;
            if (moveLowerLeft.HasValue) yield return moveLowerLeft.Value;
            if (moveLowerRight.HasValue) yield return moveLowerRight.Value;

            if (moveRightUpper.HasValue) yield return moveRightUpper.Value;
            if (moveRightLower.HasValue) yield return moveRightLower.Value;
            if (moveLeftUpper.HasValue) yield return moveLeftUpper.Value;
            if (moveLeftLower.HasValue) yield return moveLeftLower.Value;
        }

        Move? FindMove(Checkerboard checkerboard, Pawn pawn, Position position) {
            if (checkerboard.IsEmptyButExists(position)) {
                return checkerboard.GetMove(pawn, position);
            }
            return null;
        }
    }
}
