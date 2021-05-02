using BellyFish.Source.Game.Gameplay.Moves;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using BellyFish.Source.Misc.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BellyFish.Source.Game.CheckerBoard {
    class Checkerboard {
        private readonly Pawn[,] pawns;

        public Checkerboard (IEnumerable<Pawn> pawns) {
            this.pawns = new Pawn[8, 8];

            foreach (var pawn in pawns) {
                SetPawn (pawn.Position, pawn);
            }
        }
        public PawnColor CurrentColorToMove => AllMoves.Count % 2 == 0 ? PawnColor.White : PawnColor.Black;
        public Pawn GetPawn (Position pos) => pawns[pos.Letter - 1, pos.Digit - 1];
        public Pawn GetPawn (char letter, int digit) => Exists (letter, digit) ? pawns[letter - 97, digit - 1] : null;
        public Pawn GetKing (PawnColor pawnColor) => GetPawns (pawnColor).FirstOrDefault (pawn => pawn.PawnType == PawnType.King);
        void SetPawn (Position pos, Pawn pawn) => SetPawn (pos.Letter, pos.Digit, pawn);
        void SetPawn (char letter, int digit, Pawn pawn) => pawns[letter - 97, digit - 1] = pawn;

        public ICollection<Move> AllMoves { get; set; } = new List<Move> ();

        public static bool Exists (char letter, int digit) => letter <= 'h' && digit <= 8 && letter >= 'a' && digit >= 1;
        public static bool Exists (Position pos) => Exists (pos.Letter, pos.Digit);

        public bool IsEmptyButExists (char letter, int digit) => Exists (letter, digit) && GetPawn (letter, digit) == null;
        public bool IsEmptyButExists (Position pos) => IsEmptyButExists (pos.Letter, pos.Digit);

        public bool IsOccupied (char letter, int digit, out Pawn occupiedPosPawn) {
            if (Exists (letter, digit) && !IsEmptyButExists (letter, digit)) {
                occupiedPosPawn = GetPawn (letter, digit);
                return true;
            }
            occupiedPosPawn = null;
            return false;
        }

        public bool CheckIfCheck (PawnColor checkedColor) => GetPawns (checkedColor.Opposite ())
            .SelectMany (p => p.GetAvailableTakes (p.MoveStrategy.GetMoves (this, p)))
            .Where (move => move.IsTake)
            .Any (m => m.NewPawnPos == GetKing (checkedColor).Position);

        public bool IsOccupied (Position pos, out Pawn occupiedPosPawn) => IsOccupied (pos.Letter, pos.Digit, out occupiedPosPawn);

        public IEnumerable<Pawn> GetPawns (PawnColor color) {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++) {
                    if (Exists ((char) (x + 97), y + 1) && !IsEmptyButExists (char.ToLower ((char) (x + 97)), y + 1) && pawns[x, y].PawnColor == color)
                        yield return pawns[x, y];
                }
        }

        public IEnumerable<Pawn> GetAllPawns () {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    if (Exists ((char) (x + 97), y + 1) && !IsEmptyButExists (char.ToLower ((char) (x + 97)), y + 1)) {
                        var pawn = pawns[x, y];
                        yield return pawn;
                    }
        }

        public static Checkerboard DeepCopy (Checkerboard original) {
            var pawns = original.GetAllPawns ().Select (p => Pawn.DeepCopy (p)).ToList ();
            return new Checkerboard (pawns);
        }

        public void MakeMove (Move move) {
            var pawn = GetPawn (move.PawnOriginPos.Letter, move.PawnOriginPos.Digit);
            pawn.SetNewPosition (move.NewPawnPos);

            SetPawn (move.NewPawnPos, pawn);
            SetPawn (move.PawnOriginPos, null);

            AllMoves.Add (move);
        }

        public bool IsTerminal (out PawnColor winColor) {
            winColor = default;

            return false;
        }
    }
}