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

            var castlingMoves = GetCastlingMoves(checkerboard, pawn);
            foreach (var move in castlingMoves) {
                yield return move;
            }

        }

        Move? FindMove(Checkerboard checkerboard, Pawn pawn, Position position) {
            if (checkerboard.IsEmptyButExists(position)) {
                return checkerboard.GetMove(pawn, position);
            }
            return null;
        }

        IEnumerable<Move> GetCastlingMoves(Checkerboard checkerboard, Pawn pawn) {
            if (checkerboard.AllMoves.Any(move => move.Pawn == pawn)) {
                yield break;
            }

            var rookH = checkerboard.GetPawns(pawn.PawnColor).FirstOrDefault(p => p.PawnType == Misc.PawnType.Rook && p.Position.Letter == 'h');
            if (rookH == null
                || checkerboard.AllMoves.Any(m => m.Pawn == rookH)
                || GetPositionsInBetween(pawn.PawnColor, false).Any(pos => checkerboard.GetPawn(pos.Letter,pos.Digit) != null)
                || checkerboard.GetPawns(pawn.PawnColor.Opposite()).Any(p => p.GetAvailableMoves(checkerboard).Any(m => m.TakenPawn == rookH))) {
                
            } else {
                yield return checkerboard.GetCastlingMove(pawn, new Position('g', pawn.PawnColor == PawnColor.White ? 1 : 8), rookH, new Position('f', pawn.PawnColor == PawnColor.White ? 1 : 8));
            }

            var rookA = checkerboard.GetPawns(pawn.PawnColor).FirstOrDefault(p => p.PawnType == Misc.PawnType.Rook && p.Position.Letter == 'a');
            if (rookA == null
                || checkerboard.AllMoves.Any(m => m.Pawn == rookA)
                || GetPositionsInBetween(pawn.PawnColor, true).Any(pos => checkerboard.GetPawn(pos.Letter,pos.Digit) != null)
                || checkerboard.GetPawns(pawn.PawnColor.Opposite()).Any(p => p.GetAvailableMoves(checkerboard).Any(m => m.TakenPawn == rookA))) {
            } else {
                yield return checkerboard.GetCastlingMove(pawn, new Position('c', pawn.PawnColor == PawnColor.White ? 1 : 8), rookH, new Position('d', pawn.PawnColor == PawnColor.White ? 1 : 8));
            }

                IEnumerable<Position> GetPositionsInBetween(PawnColor pawnColor, bool smallCastle) {
                if (pawnColor == PawnColor.White && smallCastle) {
                    yield return new Position('f', 1);
                    yield return new Position('g', 1);
                } else if (pawnColor == PawnColor.White) {
                    yield return new Position('b', 1);
                    yield return new Position('c', 1);
                    yield return new Position('d', 1);
                } else if (smallCastle) {
                    yield return new Position('f', 8);
                    yield return new Position('g', 8);
                } else {
                    yield return new Position('b', 8);
                    yield return new Position('c', 8);
                    yield return new Position('d', 8);
                }
            }
        }
    }
}