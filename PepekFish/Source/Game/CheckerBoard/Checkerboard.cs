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

        public Pawn GetPawn(Position pos) => pawns[pos.Letter - 1, pos.Digit - 1];
        public Pawn GetPawn(char letter, int digit) => Exists(letter, digit) ? pawns[letter - 97, digit - 1] : null;
        void SetPawn(Position pos, Pawn pawn) => SetPawn(pos.Letter, pos.Digit, pawn);
        void SetPawn(char letter, int digit, Pawn pawn) => pawns[letter - 97, digit - 1] = pawn;

        public ICollection<Move> AllMoves { get; set; } = new List<Move>();

        public bool Exists(char letter, int digit) => letter < 'h' && digit < 9 && letter >= 'a' && digit >= 1;
        public bool Exists(Position pos) => Exists(pos.Letter, pos.Digit);

        public bool IsEmptyButExists(char letter, int digit) => GetPawn(letter, digit) == null;
        public bool IsEmptyButExists(Position pos) => IsEmptyButExists(pos.Letter, pos.Digit);

        public bool IsOccupied(char letter, int digit, out Pawn occupiedPosPawn) {
            if (Exists(letter, digit) && !IsEmptyButExists(letter, digit)) {
                occupiedPosPawn = GetPawn(letter, digit);
                return true;
            }
            occupiedPosPawn = null;
            return false;
        }

        public bool IsOccupied(Position pos, out Pawn occupiedPosPawn) => IsOccupied(pos.Letter, pos.Digit, out occupiedPosPawn);

        public IEnumerable<Pawn> GetPawns(PawnColor color) {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    if (!IsEmptyButExists(char.ToLower((char)(x + 96)), y) && pawns[x, y].PawnColor == color)
                        yield return pawns[x, y];
        }

        public IEnumerable<Pawn> GetAllPawns() {
            for (int x = 1; x < 9; x++)
                for (int y = 1; y < 9; y++)
                    if (!IsEmptyButExists(char.ToLower((char)(x + 96)), y)) {
                        var pawn = pawns[x - 1, y - 1];
                        yield return pawn;
                    }
        }

        public static Checkerboard DeepCopy(Checkerboard original) {
            var pawns = original.GetAllPawns().Select(p => Pawn.DeepCopy(p)).ToList();
            return new Checkerboard(pawns);
        }

        public void MakeMove(Move move) {
            var pawn = pawns[move.PawnOriginPos.Letter, move.PawnOriginPos.Digit];
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