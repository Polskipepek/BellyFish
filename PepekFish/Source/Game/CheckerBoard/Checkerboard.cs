using BellyFish.Source.Game.Gameplay;
using PepekFish.Source.Game.Misc;
using PepekFish.Source.Game.Pawns;
using PepekFish.Source.Misc;
using System.Collections.Generic;
using System.Linq;

namespace PepekFish.Source.Game.CheckerBoard {
    class Checkerboard {
        private readonly Pawn[,] pawns;

        public Checkerboard(IEnumerable<Pawn> pawns) {
            this.pawns = new Pawn[8, 8];

            foreach (var pawn in pawns) {
                this.pawns[pawn.Position.Letters, pawn.Position.Digits] = pawn;
            }
        }

        public Pawn this[int x, int y] {
            get => Exists(x, y) ? pawns[x, y] : null;
            private set => pawns[x, y] = value;
        }

        public Pawn this[Int2D pos] {
            get => this.pawns[pos.Letters, pos.Digits];
            private set => this[pos.Letters, pos.Digits] = value;
        }

        public bool Exists(int x, int y) => x < 8 && y < 8 && x >= 0 && y >= 0;

        public bool Exists(Int2D pos) => Exists(pos.Letters, pos.Digits);

        public bool IsEmptyButExists(int x, int y) => Exists(x, y) && pawns[x, y] == null;
        public bool IsEmptyButExists(Int2D pos) => IsEmptyButExists(pos.Letters, pos.Digits);

        public IEnumerable<Pawn> GetPawns(PawnColor color) {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    if (!IsEmptyButExists(x, y) && this[x, y].PawnColor == color)
                        yield return this[x, y];
        }

        public IEnumerable<Pawn> GetAllPawns() {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    if (!IsEmptyButExists(x, y))
                        yield return this[x, y];
        }

        public static Checkerboard DeepCopy(Checkerboard original) {
            var pawns = original.GetAllPawns().Select(p => Pawn.DeepCopy(p)).ToList();
            return new Checkerboard(pawns);
        }

        public void MakeMove(Move move) {
            var pawn = this[move.PawnOriginPos];
            pawn.Position = move.NewPawnPos;

            this[move.NewPawnPos] = pawn;
            this[move.NewPawnPos] = null;
        }

        public bool isTerminal(out PawnColor winColor) {
            winColor = default;


            return false;
        }
    }
}