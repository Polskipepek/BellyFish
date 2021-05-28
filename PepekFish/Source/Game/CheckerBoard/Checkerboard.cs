﻿using BellyFish.Source.Game.Gameplay.Moves;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using BellyFish.Source.Misc.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BellyFish.Source.Game.CheckerBoard {
    class Checkerboard {
        private readonly Pawn[,] pawns;

        public Checkerboard(IEnumerable<Pawn> pawns) {
            this.pawns = new Pawn[8, 8];

            foreach (var pawn in pawns) {
                SetPawn(pawn.Position, pawn);
            }
        }
        public ICollection<Move> AllMoves { get; set; } = new List<Move>();

        public PawnColor CurrentColorToMove => AllMoves.Count % 2 == 0 ? PawnColor.White : PawnColor.Black;
        public Pawn GetPawn(Position pos) => GetPawn(pos.Letter, pos.Digit);
        public Pawn GetPawn(int letter, int digit) => Exists(letter, digit) ? pawns[letter, digit] : null;

        public Pawn GetKing(PawnColor pawnColor) => GetPawns(pawnColor).FirstOrDefault(pawn => pawn.PawnType == PawnType.King);

        public void SetPawn(Position pos, Pawn pawn) => SetPawn(pos.Letter, pos.Digit, pawn);
        public void SetPawn(int letter, int digit, Pawn pawn) => pawns[letter, digit] = pawn;

        public static bool Exists(int letter, int digit) => letter <= 7 && digit <= 7 && letter >= 0 && digit >= 0;
        public static bool Exists(Position pos) => Exists(pos.Letter, pos.Digit);

        public bool IsEmptyButExists(int letter, int digit) => Exists(letter, digit) && GetPawn(letter, digit) == null;
        public bool IsEmptyButExists(Position pos) => IsEmptyButExists(pos.Letter, pos.Digit);

        public bool IsOccupied(int letter, int digit, out Pawn occupiedPosPawn) {
            if (Exists(letter, digit) && !IsEmptyButExists(letter, digit)) {
                occupiedPosPawn = GetPawn(letter, digit);
                return true;
            }
            occupiedPosPawn = null;
            return false;
        }

        public bool CheckIfCheck(PawnColor checkedColor) => GetPawns(checkedColor.Opposite())
            .SelectMany(p => p.MoveStrategy.GetMoves(this, p))
            .Where(m => m.IsTake)
            .Any(m => m.NewPawnPos == GetKing(checkedColor).Position);

        public bool IsOccupied(Position pos, out Pawn occupiedPosPawn) => IsOccupied(pos.Letter, pos.Digit, out occupiedPosPawn);

        public IEnumerable<Pawn> GetPawns(PawnColor color) {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++) {
                    if (Exists(x, y) && !IsEmptyButExists(x, y) && pawns[x, y].PawnColor == color)
                        yield return pawns[x, y];
                }
        }

        public IEnumerable<Pawn> GetAllPawns() {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    if (Exists(x , y ) && !IsEmptyButExists(x , y )) {
                        var pawn = pawns[x, y];
                        yield return pawn;
                    }
        }

        public static Checkerboard DeepCopy(Checkerboard original) {
            var pawns = original.GetAllPawns().Select(p => Pawn.DeepCopy(p)).ToList();
            return new Checkerboard(pawns);
        }

        public void MakeMove(Move move) {
            var pawn = GetPawn(move.PawnOriginPos.Letter, move.PawnOriginPos.Digit);
            pawn.SetNewPosition(move.NewPawnPos);

            if (move.IsTake) {
                SetPawn(move.TakenPawn.Position, null);
            }

            if (move.IsCastling) {
                SetPawn(move.CastlingRookNewPos, move.CastlingRook);
            }

            SetPawn(move.PawnOriginPos, null);
            SetPawn(move.NewPawnPos, pawn);

            AllMoves.Add(move);

        }

        public bool IsTerminal(out PawnColor winColor) {
            winColor = default;
            foreach (var pawn in GetPawns(CurrentColorToMove)) {
                if (pawn.GetAvailableMoves(this).Any() == true)
                    return false;
            }
            MessageBox.Show("GAME OVER!!!");
            MessageBox.Show($"{winColor} wins.");
            return true;
        }
    }
}