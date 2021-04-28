using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using System.Collections.Generic;
using System.Linq;

namespace BellyFish.Source.Game.Gameplay.MoveStrategy {
    class PawnMoveStrategy : IMoveStrategy {
        public IEnumerable<Move> GetMoves(Checkerboard checkerboard, Pawn pawn) {
            var previousPawnMove = checkerboard.AllMoves.FirstOrDefault(move => move.Pawn == pawn);
            Position posPlus1;
            Position posPlus2;

            if (pawn.PawnColor == PawnColor.White) {
                posPlus1 = pawn.Position + new Position('\0', 1);
                posPlus2 = posPlus1 + new Position('\0', 1);

            } else {
                posPlus1 = pawn.Position - new Position('\0', 1);
                posPlus2 = posPlus1 - new Position('\0', 1);
            }

            bool canMove1SquareForward = previousPawnMove.Pawn == null && checkerboard.IsEmptyButExists(posPlus1);
            bool canMove2SquareForward = canMove1SquareForward && checkerboard.IsEmptyButExists(posPlus2);

            if (canMove1SquareForward) {
                yield return GetMove(checkerboard, pawn, posPlus1);
            }
            if (canMove2SquareForward) {
                yield return GetMove(checkerboard, pawn, posPlus2);
            }
        }

        Move GetMove(Checkerboard checkerboard, Pawn pawn, Position newPosition, Pawn takenPawn = null) {
            return new Move {
                Pawn = pawn,
                MoveNumber = checkerboard.AllMoves.Count + 1,
                PawnOriginPos = pawn.Position,
                NewPawnPos = newPosition,
                TakenPawn = takenPawn,
                IsPromoting = newPosition.Digit == (pawn.PawnColor == PawnColor.White ? 8 : 1),
            };
        }

        public IEnumerable<Move> GetTakes(Checkerboard checkerboard, Pawn pawn) {
            Position leftPos;
            Position rightPos;
            Position frontLeftPos;
            Position frontRightPos;
            int curPawnLetter = pawn.Position.Letter;

            if (pawn.PawnColor == PawnColor.White) {
                frontLeftPos = new Position((char)(curPawnLetter - 1), 1);
                frontRightPos = new Position((char)(curPawnLetter + 1), 1);
                leftPos = new Position((char)(curPawnLetter - 1), 0);
                rightPos = new Position((char)(curPawnLetter + 1), 0);

                if (checkerboard.IsOccupied(leftPos, out Pawn oppositeLeftPawn) && CanWhiteEnPassant(checkerboard, pawn, oppositeLeftPawn, leftPos, out Pawn frontLeftPawn)) {
                    yield return GetMove(checkerboard, pawn, frontLeftPos, frontLeftPawn);
                }
                if (checkerboard.IsOccupied(rightPos, out Pawn oppositeRightPawn) && CanWhiteEnPassant(checkerboard, pawn, oppositeRightPawn, leftPos, out Pawn frontRightPawn)) {
                    yield return GetMove(checkerboard, pawn, frontRightPos, frontRightPawn);
                }
            } else {
                frontLeftPos = new Position((char)(curPawnLetter + 1), -1);
                frontRightPos = new Position((char)(curPawnLetter - 1), -1);
                leftPos = new Position((char)(curPawnLetter - 1), 0);
                rightPos = new Position((char)(curPawnLetter + 1), 0);

                if (checkerboard.IsOccupied(leftPos, out Pawn oppositeLeftPawn) && CanBlackEnPassant(checkerboard, pawn, oppositeLeftPawn, leftPos, out Pawn frontLeftPawn)) {
                    yield return GetMove(checkerboard, pawn, frontLeftPos, frontLeftPawn);
                }
                if (checkerboard.IsOccupied(rightPos, out Pawn oppositeRightPawn) && CanBlackEnPassant(checkerboard, pawn, oppositeRightPawn, leftPos, out Pawn frontRightPawn)) {
                    yield return GetMove(checkerboard, pawn, frontRightPos, frontRightPawn);
                }
            }

            if (checkerboard.IsOccupied(frontLeftPos, out Pawn frontLeftPawn1) && AreOppositeColor(pawn, frontLeftPawn1)) {
                yield return GetMove(checkerboard, pawn, frontLeftPos, frontLeftPawn1);
            }

            if (checkerboard.IsOccupied(frontRightPos, out Pawn frontRightPawn1) && AreOppositeColor(pawn, frontRightPawn1)) {
                yield return GetMove(checkerboard, pawn, frontRightPos, frontRightPawn1);
            }
        }

        bool AreOppositeColor(Pawn pawn1, Pawn pawn2) => pawn1.PawnColor != pawn2.PawnColor;

        bool CanWhiteEnPassant(Checkerboard checkerboard, Pawn pawn, Pawn sidePawn, Position finalPawnPos, out Pawn leftPosPawn) {
            leftPosPawn = null;
            return pawn.Position.Digit == 5
            && pawn.PawnColor == PawnColor.White
            && checkerboard.AllMoves.Last().Pawn.PawnType == PawnType.Pawn
            && checkerboard.AllMoves.Last().NewPawnPos == sidePawn.Position
            && checkerboard.IsOccupied(sidePawn.Position, out leftPosPawn)
            && !checkerboard.IsOccupied(finalPawnPos, out Pawn finalPawnPosPawn);
        }

        bool CanBlackEnPassant(Checkerboard checkerboard, Pawn pawn, Pawn sidePawn, Position finalPawnPos, out Pawn leftPosPawn) {
            leftPosPawn = null;
            return pawn.Position.Digit == 4
                && pawn.PawnColor == PawnColor.Black
                && checkerboard.AllMoves.Last().Pawn.PawnType == PawnType.Pawn
                && checkerboard.AllMoves.Last().NewPawnPos == sidePawn.Position
                && checkerboard.IsOccupied(sidePawn.Position, out leftPosPawn)
                && !checkerboard.IsOccupied(finalPawnPos, out Pawn finalPawnPosPawn);
        }
    }
}