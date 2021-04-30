using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using BellyFish.Source.Misc.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BellyFish.Source.Game.Gameplay.Moves.MoveStrategy {
    class PawnMoveStrategy : IMoveStrategy {
        public IEnumerable<Move> GetMoves(Checkerboard checkerboard, Pawn pawn) {
            Position posPlus1;
            Position posPlus2;

            if (pawn.PawnColor == PawnColor.White) {
                posPlus1 = pawn.Position + new Position((char)0, 1);
                posPlus2 = posPlus1 + new Position((char)0, 1);

            } else {
                posPlus1 = pawn.Position - new Position((char)0, 1);
                posPlus2 = posPlus1 - new Position((char)0, 1);
            }

            bool canMove1SquareForward = checkerboard.IsEmptyButExists(posPlus1);
            bool canMove2SquareForward = pawn.Position.Digit == (pawn.PawnColor == PawnColor.White ? 2 : 7) && checkerboard.IsEmptyButExists(posPlus2);

            if (canMove1SquareForward) {
                yield return checkerboard.GetMove(pawn, posPlus1);

                if (canMove2SquareForward) {
                    yield return checkerboard.GetMove(pawn, posPlus2);
                }
            }

            foreach (var take in GetTakes(checkerboard, pawn)) {
                yield return checkerboard.GetMove(pawn, take.NewPawnPos, take.TakenPawn);
            }
        }

        public IEnumerable<Move> GetTakes(Checkerboard checkerboard, Pawn pawn) {
            Position leftPos;
            Position rightPos;
            Position frontLeftPos;
            Position frontRightPos;

            int curPawnLetter = pawn.Position.Letter;
            int curPawnDigit = pawn.Position.Digit;

            if (pawn.PawnColor == PawnColor.White) {
                frontLeftPos = new Position((char)(curPawnLetter - 1), curPawnDigit + 1);
                frontRightPos = new Position((char)(curPawnLetter + 1), curPawnDigit + 1);
                leftPos = new Position((char)(curPawnLetter - 1), curPawnDigit);
                rightPos = new Position((char)(curPawnLetter + 1), curPawnDigit);
            } else {
                frontLeftPos = new Position((char)(curPawnLetter + 1), curPawnDigit - 1);
                frontRightPos = new Position((char)(curPawnLetter - 1), curPawnDigit - 1);
                leftPos = new Position((char)(curPawnLetter - 1), curPawnDigit);
                rightPos = new Position((char)(curPawnLetter + 1), curPawnDigit);
            }

            if (checkerboard.IsOccupied(leftPos, out Pawn oppositeLeftPawn) && CanTakeEnPassant(checkerboard, pawn, oppositeLeftPawn, frontLeftPos, out Pawn frontLeftPawn)) {
                yield return checkerboard.GetMove(pawn, frontLeftPos, frontLeftPawn);
            }
            if (checkerboard.IsOccupied(rightPos, out Pawn oppositeRightPawn) && CanTakeEnPassant(checkerboard, pawn, oppositeRightPawn, frontRightPos, out Pawn frontRightPawn)) {
                yield return checkerboard.GetMove(pawn, frontRightPos, frontRightPawn);
            }

            if (checkerboard.IsOccupied(frontLeftPos, out Pawn frontLeftPawn1) && pawn.AreOppositeColor(frontLeftPawn1)) {
                yield return checkerboard.GetMove(pawn, frontLeftPos, frontLeftPawn1);
            }

            if (checkerboard.IsOccupied(frontRightPos, out Pawn frontRightPawn1) && pawn.AreOppositeColor(frontRightPawn1)) {
                yield return checkerboard.GetMove(pawn, frontRightPos, frontRightPawn1);
            }
        }

        bool CanTakeEnPassant(Checkerboard checkerboard, Pawn pawn, Pawn sidePawn, Position finalPawnPos, out Pawn sidePosPawn) {
            sidePosPawn = null;
            return pawn.Position.Digit == (pawn.PawnColor == PawnColor.White ? 5 : 4)
                && pawn.AreOppositeColor(sidePawn)
                && checkerboard.AllMoves.Last().Pawn.PawnType == PawnType.Pawn
                && checkerboard.AllMoves.Last().NewPawnPos == sidePawn.Position
                && checkerboard.IsOccupied(sidePawn.Position, out sidePosPawn)
                && !checkerboard.IsOccupied(finalPawnPos, out _);
        }
    }
}