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
                new Pawn(new Position(1,1), PawnColor.White, PawnType.Pawn, pawnMoveStrategy),
                new Pawn(new Position(2,1), PawnColor.White, PawnType.Bishop, bishopMoveStrategy),
                new Pawn(new Position(1,6), PawnColor.White, PawnType.Pawn, pawnMoveStrategy),
            });

            PiecesOnBoard.Text = $"Pionów: {MainCheckerboard.GetAllPawns().Count()}{Environment.NewLine}";

            foreach (var pawn in MainCheckerboard.GetAllPawns()) {
                PiecesOnBoard.Text += $"{pawn.PawnColor} {pawn.PawnType}, Position: {pawn.Position.Letters}{pawn.Position.Digits},{Environment.NewLine}";
                foreach (var move in pawn.GetAvailableMoves(MainCheckerboard)) {
                    PiecesOnBoard.Text += $"{move.PawnOriginPos.Letters}{move.PawnOriginPos.Digits}-{move.NewPawnPos.Letters}{move.NewPawnPos.Digits}, ";

                }
                PiecesOnBoard.Text += Environment.NewLine;
            }
        }
    }
}
