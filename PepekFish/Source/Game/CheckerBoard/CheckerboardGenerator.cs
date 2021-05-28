using BellyFish.Source.Game.Gameplay.Moves.MoveStrategy;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using System.Collections.Generic;

namespace BellyFish.Source.Game.CheckerBoard {
    class CheckerboardGenerator : Singleton<CheckerboardGenerator> {
        public static Checkerboard InitializeCheckerboard() {
            return new Checkerboard(GetInitialPawns());
        }

        static IEnumerable<Pawn> GetInitialPawns() {
            IMoveStrategy pawnMoveStrategy = new PawnMoveStrategy();
            IMoveStrategy bishopMoveStrategy = new BishopMoveStrategy();
            IMoveStrategy knightMoveStrategy = new KnightMoveStrategy();
            IMoveStrategy rookMoveStrategy = new RookMoveStrategy();
            IMoveStrategy kingMoveStrategy = new KingMoveStrategy();
            IMoveStrategy queenMoveStrategy = new QueenMoveStrategy();

            for (int x = 0; x < 8; x++) {
                yield return new Pawn(new Position((char)x, 1), PawnColor.White, PawnType.Pawn, pawnMoveStrategy);
                yield return new Pawn(new Position((char)x, 6), PawnColor.Black, PawnType.Pawn, pawnMoveStrategy);
            }

            yield return new Pawn(new Position(0, 0), PawnColor.White, PawnType.Rook, rookMoveStrategy);
            yield return new Pawn(new Position(7, 0), PawnColor.White, PawnType.Rook, rookMoveStrategy);

            yield return new Pawn(new Position(0, 7), PawnColor.Black, PawnType.Rook, rookMoveStrategy);
            yield return new Pawn(new Position(7, 7), PawnColor.Black, PawnType.Rook, rookMoveStrategy);

            yield return new Pawn(new Position(1, 0), PawnColor.White, PawnType.Knight, knightMoveStrategy);
            yield return new Pawn(new Position(6, 0), PawnColor.White, PawnType.Knight, knightMoveStrategy);

            yield return new Pawn(new Position(1, 7), PawnColor.Black, PawnType.Knight, knightMoveStrategy);
            yield return new Pawn(new Position(6, 7), PawnColor.Black, PawnType.Knight, knightMoveStrategy);

            yield return new Pawn(new Position(2, 0), PawnColor.White, PawnType.Bishop, bishopMoveStrategy);
            yield return new Pawn(new Position(5, 0), PawnColor.White, PawnType.Bishop, bishopMoveStrategy);

            yield return new Pawn(new Position(2, 7), PawnColor.Black, PawnType.Bishop, bishopMoveStrategy);
            yield return new Pawn(new Position(5, 7), PawnColor.Black, PawnType.Bishop, bishopMoveStrategy);

            yield return new Pawn(new Position(4, 0), PawnColor.White, PawnType.King, kingMoveStrategy);
            yield return new Pawn(new Position(4, 7), PawnColor.Black, PawnType.King, kingMoveStrategy);

            yield return new Pawn(new Position(3, 0), PawnColor.White, PawnType.Queen, queenMoveStrategy);
            yield return new Pawn(new Position(3, 7), PawnColor.Black, PawnType.Queen, queenMoveStrategy);
        }
    }
}
