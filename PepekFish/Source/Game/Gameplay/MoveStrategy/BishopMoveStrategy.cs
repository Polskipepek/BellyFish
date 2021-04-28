using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using System.Collections.Generic;

namespace BellyFish.Source.Game.Gameplay.MoveStrategy {
    class BishopMoveStrategy : IMoveStrategy {
        public IEnumerable<Move> GetMoves(Checkerboard checkerboard, Pawn pawn) {
            var tempPos = pawn.Position;
            while (checkerboard.IsEmptyButExists(tempPos + new Position(1, 1))) {
                tempPos += new Position(1, 1);
                yield return ReturnMove(checkerboard, pawn, tempPos);
            }

            tempPos = pawn.Position;
            while (checkerboard.IsEmptyButExists(tempPos + new Position(1, -1))) {
                tempPos += new Position(1, -1);
                yield return ReturnMove(checkerboard, pawn, tempPos);
            }

            tempPos = pawn.Position;
            while (checkerboard.IsEmptyButExists(tempPos + new Position(-1, 1))) {
                tempPos += new Position(-1, 1);
                yield return ReturnMove(checkerboard, pawn, tempPos);
            }

            tempPos = pawn.Position;
            while (checkerboard.IsEmptyButExists(tempPos + new Position(-1, -1))) {
                tempPos += new Position(-1, -1);
                yield return ReturnMove(checkerboard, pawn, tempPos);
            }
        }

        public IEnumerable<Move> GetTakes(Checkerboard checkerboard, Pawn pawn) {
            var tempPos = pawn.Position;
            while (!checkerboard.IsOccupied(tempPos + new Position(1, 1), out Pawn tempPosPawn) && AreOppositeColor(pawn, tempPosPawn)) {
                tempPos += new Position(1, 1);
                yield return ReturnMove(checkerboard, pawn, tempPos, tempPosPawn);
            }

            tempPos = pawn.Position;
            while (!checkerboard.IsOccupied(tempPos + new Position(1, -1), out Pawn tempPosPawn) && AreOppositeColor(pawn, tempPosPawn)) {
                tempPos += new Position(1, -1);
                yield return ReturnMove(checkerboard, pawn, tempPos, tempPosPawn);
            }

            tempPos = pawn.Position;
            while (!checkerboard.IsOccupied(tempPos + new Position(-1, 1), out Pawn tempPosPawn) && AreOppositeColor(pawn, tempPosPawn)) {
                tempPos += new Position(-1, 1);
                yield return ReturnMove(checkerboard, pawn, tempPos, tempPosPawn);
            }

            tempPos = pawn.Position;
            while (!checkerboard.IsOccupied(tempPos + new Position(-1, -1), out Pawn tempPosPawn) && AreOppositeColor(pawn, tempPosPawn)) {
                tempPos += new Position(-1, -1);
                yield return ReturnMove(checkerboard, pawn, tempPos, tempPosPawn);
            }
        }

        Move ReturnMove(Checkerboard checkerboard, Pawn pawn, Position newPos, Pawn takenPawn = null) {
            return new Move {
                MoveNumber = checkerboard.AllMoves.Count + 1,
                Pawn = pawn,
                PawnOriginPos = pawn.Position,
                NewPawnPos = newPos,
                TakenPawn = takenPawn,
            };
        }

        bool AreOppositeColor(Pawn pawn1, Pawn pawn2) => pawn1.PawnColor != pawn2.PawnColor;

    }
}
