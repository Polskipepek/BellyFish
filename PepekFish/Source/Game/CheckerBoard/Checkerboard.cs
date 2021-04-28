using BellyFish.Source.Game.Gameplay;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using System.Collections.Generic;
using System.Linq;

namespace BellyFish.Source.Game.CheckerBoard {
    class Checkerboard {
        private readonly Pawn[,] pawns;

        public Checkerboard(IEnumerable<Pawn> pawns) {
            this.pawns = new Pawn[8, 8];

            foreach (var pawn in pawns) {
                SetPawn(pawn.Position, pawn);
            }
        }

        public Pawn GetPawn(Position pos) => pawns[pos.Letters - 1, pos.Digits - 1];
        public Pawn GetPawn(int x, int y) => Exists(x, y) ? pawns[x - 1, y - 1] : null;
        void SetPawn(Position pos, Pawn pawn) => SetPawn(pos.Letters, pos.Digits, pawn);
        void SetPawn(int x, int y, Pawn pawn) => pawns[x - 1, y - 1] = pawn;

        public ICollection<Move> AllMoves { get; set; } = new List<Move>();

        public bool Exists(int x, int y) => x < 9 && y < 9 && x >= 1 && y >= 1;
        public bool Exists(Position pos) => Exists(pos.Letters, pos.Digits);

        public bool IsEmptyButExists(int x, int y) => Exists(x, y) && GetPawn(x, y) == null;
        public bool IsEmptyButExists(Position pos) => IsEmptyButExists(pos.Letters, pos.Digits);

        public bool IsOccupied(int x, int y, out Pawn occupiedPosPawn) {
            if (Exists(x, y) && !IsEmptyButExists(x, y)) {
                occupiedPosPawn = GetPawn(x, y);
                return true;
            }
            occupiedPosPawn = null;
            return false;
        }

        public bool IsOccupied(Position pos, out Pawn occupiedPosPawn) => IsOccupied(pos.Letters, pos.Digits, out occupiedPosPawn);

        public IEnumerable<Pawn> GetPawns(PawnColor color) {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    if (!IsEmptyButExists(x, y) && pawns[x, y].PawnColor == color)
                        yield return pawns[x, y];
        }

        public IEnumerable<Pawn> GetAllPawns() {
            for (int x = 1; x < 9; x++)
                for (int y = 1; y < 9; y++)
                    if (!IsEmptyButExists(x, y))
                        yield return pawns[x - 1, y - 1];
        }

        public static Checkerboard DeepCopy(Checkerboard original) {
            var pawns = original.GetAllPawns().Select(p => Pawn.DeepCopy(p)).ToList();
            return new Checkerboard(pawns);
        }

        public void MakeMove(Move move) {
            var pawn = pawns[move.PawnOriginPos.Letters, move.PawnOriginPos.Digits];
            pawn.SetNewPosition(move.NewPawnPos);
            SetPawn(move.NewPawnPos, pawn);
            SetPawn(move.PawnOriginPos, null);

            AllMoves.Add(move);
        }

        public bool isTerminal(out PawnColor winColor) {
            winColor = default;

            return false;
        }
    }
}