using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using BellyFish.Source.Misc.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BellyFish.Source.Game.Gameplay.Moves.MoveStrategy {
    class KingMoveStrategy : IMoveStrategy {
        public IEnumerable<Move> GetMoves(Checkerboard checkerboard, Pawn pawn) {
            var pawnPos = pawn.Position;

            var moveUpperRight = FindMove(checkerboard, pawn, pawnPos + new Position(1, 1));
            var moveUpperLeft = FindMove(checkerboard, pawn, pawnPos - new Position(1, -1));
            var moveLowerRight = FindMove(checkerboard, pawn, pawnPos + new Position(1, -1));
            var moveLowerLeft = FindMove(checkerboard, pawn, pawnPos - new Position(1, 1));

            var moveRight = FindMove(checkerboard, pawn, pawnPos + new Position(1, 0));
            var moveLower = FindMove(checkerboard, pawn, pawnPos + new Position(0, -1));
            var moveLeft = FindMove(checkerboard, pawn, pawnPos - new Position(1, 0));
            var moveUpper = FindMove(checkerboard, pawn, pawnPos + new Position(0, 1));

            if (moveUpperRight.HasValue) yield return moveUpperRight.Value;
            if (moveUpperLeft.HasValue) yield return moveUpperLeft.Value;
            if (moveLowerLeft.HasValue) yield return moveLowerLeft.Value;
            if (moveLowerRight.HasValue) yield return moveLowerRight.Value;

            if (moveRight.HasValue) yield return moveRight.Value;
            if (moveLower.HasValue) yield return moveLower.Value;
            if (moveLeft.HasValue) yield return moveLeft.Value;
            if (moveUpper.HasValue) yield return moveUpper.Value;

            //var castlingMoves = GetCastlingMoves(checkerboard, pawn);
            //foreach (var move in castlingMoves) {
            //    yield return move;
            //}

        }

        Move? FindMove(Checkerboard checkerboard, Pawn pawn, Position position) {
            if (checkerboard.IsEmptyButExists(position)) {
                return checkerboard.GetMove(pawn, position);
            }
            return null;
        }

        static IEnumerable<Move> GetCastlingMoves(Checkerboard checkerboard, Pawn king) {
            if (checkerboard.AllMoves.Any(move => move.Pawn == king) || checkerboard.CheckIfCheck(checkerboard.CurrentColorToMove)) {
                yield break;
            }

            var rookH = checkerboard.GetPawns(king.PawnColor).FirstOrDefault(p => p.PawnType == PawnType.Rook && p.Position.Letter == 7);
            if (rookH != null
                && !checkerboard.AllMoves.Any(m => m.Pawn == rookH)
                && GetPositionsInBetween(king.PawnColor, false).Any(pos => checkerboard.GetPawn(pos.Letter, pos.Digit) == null)
                && !checkerboard.GetPawns(king.PawnColor.Opposite())
                .Any(p => p.GetAvailableMoves(checkerboard)
                .Any(m => (m.NewPawnPos.Digit == rookH.Position.Digit - 1 && m.NewPawnPos.Letter == rookH.Position.Letter)
                || (m.NewPawnPos.Digit == rookH.Position.Digit - 2 && m.NewPawnPos.Letter == rookH.Position.Letter)))) {
                yield return checkerboard.GetCastlingMove(king, new Position(6, king.PawnColor == PawnColor.White ? 1 : 8), rookH, new Position(5, king.PawnColor == PawnColor.White ? 1 : 8));
            }

            var rookA = checkerboard.GetPawns(king.PawnColor).FirstOrDefault(p => p.PawnType == PawnType.Rook && p.Position.Letter == 0);
            if (rookA != null
                && !checkerboard.AllMoves.Any(m => m.Pawn == rookA)
                && GetPositionsInBetween(king.PawnColor, true).Any(pos => checkerboard.GetPawn(pos.Letter, pos.Digit) == null)
                && !checkerboard.GetPawns(king.PawnColor.Opposite())
                .Any(p => p.GetAvailableMoves(checkerboard)
                .Any(m => m.NewPawnPos.Digit == rookH.Position.Digit + 1 && m.NewPawnPos.Letter == rookH.Position.Letter
                || (m.NewPawnPos.Digit == rookH.Position.Digit + 2 && m.NewPawnPos.Letter == rookH.Position.Letter)
                || (m.NewPawnPos.Digit == rookH.Position.Digit + 3 && m.NewPawnPos.Letter == rookH.Position.Letter)))) {
                yield return checkerboard.GetCastlingMove(king, new Position(2, king.PawnColor == PawnColor.White ? 1 : 8), rookH, new Position(4, king.PawnColor == PawnColor.White ? 1 : 8));
            }

            IEnumerable<Position> GetPositionsInBetween(PawnColor pawnColor, bool smallCastle) {
                if (pawnColor == PawnColor.White && smallCastle) {
                    yield return new Position(5, 1);
                    yield return new Position(6, 1);
                } else if (pawnColor == PawnColor.White) {
                    yield return new Position(1, 1);
                    yield return new Position(2, 1);
                    yield return new Position(3, 1);
                } else if (smallCastle) {
                    yield return new Position(5, 8);
                    yield return new Position(6, 8);
                } else {
                    yield return new Position(1, 8);
                    yield return new Position(2, 8);
                    yield return new Position(3, 8);
                }
            }
        }
    }
}