using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Gameplay.MoveStrategy;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BellyFish {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            InitializeCheckerboard();
        }
        Checkerboard MainCheckerboard { get; set; }

        void InitializeCheckerboard() {
            IMoveStrategy pawnMoveStrategy = new PawnMoveStrategy();
            IMoveStrategy bishopMoveStrategy = new BishopMoveStrategy();
            MainCheckerboard = new Checkerboard(new List<Pawn>() {
                new Pawn(new Position('a',1), PawnColor.White, PawnType.Pawn, pawnMoveStrategy),
                new Pawn(new Position('b',1), PawnColor.White, PawnType.Pawn, pawnMoveStrategy),
                /*new Pawn(new Position('c',6), PawnColor.White, PawnType.Pawn, pawnMoveStrategy),*/
            });
            var pieces = MainCheckerboard.GetAllPawns();
            PiecesOnBoard.Text = $"Pionów: {pieces.Count()}{Environment.NewLine}";

            foreach (var pawn in pieces) {
                PiecesOnBoard.Text += $"{pawn.PawnColor} {pawn.PawnType}, {pawn.Position.Letter}{pawn.Position.Digit},{Environment.NewLine}";
                var moves = pawn.GetAvailableMoves(MainCheckerboard);
                PiecesOnBoard.Text += "Moves: ";
                foreach (var move in moves) {
                    PiecesOnBoard.Text += $"{move.PawnOriginPos.Letter}{move.PawnOriginPos.Digit}-{move.NewPawnPos.Letter}{move.NewPawnPos.Digit}, ";

                }
                PiecesOnBoard.Text += Environment.NewLine;
            }
        }
    }
}
