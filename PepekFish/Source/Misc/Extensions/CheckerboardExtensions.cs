using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Gameplay;
using BellyFish.Source.Game.Pawns;
using System.Collections.Generic;

namespace BellyFish.Source.Misc.Extensions {
    static class CheckerboardExtensions {
        public static Move GetMove(this Checkerboard checkerboard, Pawn pawn, Position newPos, Pawn takenPawn = null) {
            return new Move {
                MoveNumber = checkerboard.AllMoves.Count + 1,
                Pawn = pawn,
                PawnOriginPos = pawn.Position,
                NewPawnPos = newPos,
                TakenPawn = takenPawn,
            };
        }

        public static IEnumerable<Move> FindMovesAlongDirection(this Checkerboard checkerboard, Pawn pawn, Position delta) {
            var currPosition = pawn.Position + delta;
            while (checkerboard.Exists(currPosition)) {
                if (checkerboard.IsOccupied(currPosition, out Pawn currPosPawn)) {
                    yield return checkerboard.GetMove(pawn, currPosition, currPosPawn);
                    break;
                } else if (checkerboard.IsEmptyButExists(currPosition)) {
                    yield return checkerboard.GetMove(pawn, currPosition);
                }
                currPosition += delta;
            }
        }
    }
}
