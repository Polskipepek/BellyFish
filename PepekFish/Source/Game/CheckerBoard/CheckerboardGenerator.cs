using BellyFish.Source.Game.Gameplay.Moves.MoveStrategy;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using System.Collections.Generic;

namespace BellyFish.Source.Game.CheckerBoard {
    class CheckerboardGenerator : Singleton<CheckerboardGenerator> {
        public static Checkerboard InitializeCheckerboard () {
            return new Checkerboard (GetInitialPawns ());
        }

        static IEnumerable<Pawn> GetInitialPawns () {
            IMoveStrategy pawnMoveStrategy = new PawnMoveStrategy ();
            IMoveStrategy bishopMoveStrategy = new BishopMoveStrategy ();
            IMoveStrategy knightMoveStrategy = new KnightMoveStrategy ();
            IMoveStrategy rookMoveStrategy = new RookMoveStrategy ();
            IMoveStrategy kingMoveStrategy = new KingMoveStrategy ();
            IMoveStrategy queenMoveStrategy = new QueenMoveStrategy ();

            int a = 'a';
            for (int x = a; x < a + 8; x++) {
                yield return new Pawn (new Position ((char) x, 2), PawnColor.White, PawnType.Pawn, pawnMoveStrategy);
                yield return new Pawn (new Position ((char) x, 7), PawnColor.Black, PawnType.Pawn, pawnMoveStrategy);
            }

            yield return new Pawn (new Position ('a', 1), PawnColor.White, PawnType.Rook, rookMoveStrategy);
            yield return new Pawn (new Position ('h', 1), PawnColor.White, PawnType.Rook, rookMoveStrategy);

            yield return new Pawn (new Position ('a', 8), PawnColor.Black, PawnType.Rook, rookMoveStrategy);
            yield return new Pawn (new Position ('h', 8), PawnColor.Black, PawnType.Rook, rookMoveStrategy);

            yield return new Pawn (new Position ('b', 1), PawnColor.White, PawnType.Knight, knightMoveStrategy);
            yield return new Pawn (new Position ('g', 1), PawnColor.White, PawnType.Knight, knightMoveStrategy);

            yield return new Pawn (new Position ('b', 8), PawnColor.Black, PawnType.Knight, knightMoveStrategy);
            yield return new Pawn (new Position ('g', 8), PawnColor.Black, PawnType.Knight, knightMoveStrategy);

            yield return new Pawn (new Position ('c', 1), PawnColor.White, PawnType.Bishop, bishopMoveStrategy);
            yield return new Pawn (new Position ('f', 1), PawnColor.White, PawnType.Bishop, bishopMoveStrategy);

            yield return new Pawn (new Position ('c', 8), PawnColor.Black, PawnType.Bishop, bishopMoveStrategy);
            yield return new Pawn (new Position ('f', 8), PawnColor.Black, PawnType.Bishop, bishopMoveStrategy);

            yield return new Pawn (new Position ('e', 1), PawnColor.White, PawnType.King, kingMoveStrategy);
            yield return new Pawn (new Position ('e', 8), PawnColor.Black, PawnType.King, kingMoveStrategy);

            yield return new Pawn (new Position ('d', 1), PawnColor.White, PawnType.Queen, queenMoveStrategy);
            yield return new Pawn (new Position ('d', 8), PawnColor.Black, PawnType.Queen, queenMoveStrategy);
        }
    }
}
