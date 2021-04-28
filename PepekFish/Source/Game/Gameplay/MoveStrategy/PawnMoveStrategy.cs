using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using System.Collections.Generic;
using System.Linq;

namespace BellyFish.Source.Game.Gameplay.MoveStrategy {
    class PawnMoveStrategy : IMoveStrategy {
        public IEnumerable<Move> GetMoves(Checkerboard checkerboard, Pawn pawn) {

            var previousMoves = checkerboard.AllMoves.FirstOrDefault(move => move.Pawn == pawn);
            if (previousMoves.Pawn == null) {
                var posPlus2 = pawn.Position + new Position(0, 2);
                if (checkerboard.IsEmptyButExists(posPlus2)) {
                    yield return new Move {
                        Pawn = pawn,
                        PawnOriginPos = pawn.Position,
                        NewPawnPos = posPlus2,
                    };
                }
            }

            var posPlus1 = pawn.Position + new Position(0, 1);
            if (checkerboard.IsEmptyButExists(posPlus1)) {
                yield return new Move {
                    Pawn = pawn,
                    PawnOriginPos = pawn.Position,
                    NewPawnPos = posPlus1,
                };
            }
        }

        public IEnumerable<Move> GetTakes(Checkerboard checkerboard, Pawn pawn) {

            var upperLeftPos = pawn.Position + new Position(-1, 0) + new Position(0, 1);
            if (checkerboard.IsOccupied(upperLeftPos, out Pawn UpperLeftPosPawn) && AreOppositeColor(UpperLeftPosPawn, pawn)) {
                yield return new Move {
                    Pawn = pawn,
                    PawnOriginPos = pawn.Position,
                    NewPawnPos = upperLeftPos,
                };
            }

            var upperRightPos = pawn.Position + new Position(1, 0) + new Position(0, 1);
            if (checkerboard.IsOccupied(upperRightPos, out Pawn UpperRightPawn) && AreOppositeColor(UpperRightPawn, pawn)) {
                yield return new Move {
                    Pawn = pawn,
                    PawnOriginPos = pawn.Position,
                    NewPawnPos = upperRightPos,
                };
            }

            if (pawn.PawnColor == PawnColor.White) {
                GetWhiteEnPassantTakes(checkerboard, pawn);
            } else {
                GetBlackEnPassantTakes(checkerboard, pawn);
            }
        }

        IEnumerable<Move> GetWhiteEnPassantTakes(Checkerboard checkerboard, Pawn pawn) {
            var leftPos = pawn.Position + new Position(-1, 0);
            var rightPos = pawn.Position + new Position(1, 0);

            if (checkerboard.IsOccupied(leftPos, out Pawn leftPosPawn))
                if (CanWhiteEnPassant(checkerboard, pawn, leftPosPawn))
                    yield return new Move {
                        Pawn = pawn,
                        MoveNumber = checkerboard.AllMoves.Count() + 1,
                        PawnOriginPos = pawn.Position,
                        TakenPawn = leftPosPawn,
                        NewPawnPos = leftPos + new Position(0, 1),
                    };

            if (checkerboard.IsOccupied(rightPos, out Pawn rightPosPawn))
                if (CanWhiteEnPassant(checkerboard, pawn, rightPosPawn))
                    yield return new Move {
                        Pawn = pawn,
                        MoveNumber = checkerboard.AllMoves.Count() + 1,
                        PawnOriginPos = pawn.Position,
                        TakenPawn = rightPosPawn,
                        NewPawnPos = rightPos + new Position(0, -1),

                    };
        }

        IEnumerable<Move> GetBlackEnPassantTakes(Checkerboard checkerboard, Pawn pawn) {
            var leftPos = pawn.Position + new Position(1, 0);
            var rightPos = pawn.Position + new Position(-1, 0);

            if (checkerboard.IsOccupied(leftPos, out Pawn leftPosPawn) && CanBlackEnPassant(checkerboard, pawn, leftPosPawn))
                yield return new Move {
                    Pawn = pawn,
                    MoveNumber = checkerboard.AllMoves.Count() + 1,
                    PawnOriginPos = pawn.Position,
                    NewPawnPos = leftPos,
                };

            if (checkerboard.IsOccupied(rightPos, out Pawn rightPosPawn) && CanBlackEnPassant(checkerboard, pawn, rightPosPawn))
                yield return new Move {
                    Pawn = pawn,
                    MoveNumber = checkerboard.AllMoves.Count() + 1,
                    PawnOriginPos = pawn.Position,
                    NewPawnPos = rightPos,
                };
        }

        bool AreOppositeColor(Pawn pawn1, Pawn pawn2) => pawn1.PawnColor != pawn2.PawnColor;
        bool CanWhiteEnPassant(Checkerboard checkerboard, Pawn pawn, Pawn oppositePawn) => pawn.Position.Digits == 5 && pawn.PawnColor == PawnColor.White && checkerboard.AllMoves.Last().Pawn.PawnType == PawnType.Pawn && checkerboard.AllMoves.Last().NewPawnPos == oppositePawn.Position;
        bool CanBlackEnPassant(Checkerboard checkerboard, Pawn pawn, Pawn oppositePawn) => pawn.Position.Digits == 4 && pawn.PawnColor == PawnColor.Black && checkerboard.AllMoves.Last().Pawn.PawnType == PawnType.Pawn && checkerboard.AllMoves.Last().NewPawnPos == oppositePawn.Position;
    }
}