using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Gameplay.Moves.MoveStrategy;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using BellyFish.Source.Misc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BellyFish {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
       //     InitializeCheckerboard();
        }
        Checkerboard MainCheckerboard { get; set; }

        void InitializeCheckerboard() {
            IMoveStrategy pawnMoveStrategy = new PawnMoveStrategy();
            IMoveStrategy bishopMoveStrategy = new BishopMoveStrategy();
            IMoveStrategy knightMoveStrategy = new KnightMoveStrategy();
            IMoveStrategy rookMoveStrategy = new RookMoveStrategy();
            IMoveStrategy kingMoveStrategy = new KingMoveStrategy();
            IMoveStrategy queenMoveStrategy = new QueenMoveStrategy();

            MainCheckerboard = new Checkerboard(new List<Pawn>() {
                new Pawn(new Position('h',8), PawnColor.White, PawnType.King, kingMoveStrategy),
                new Pawn(new Position('h',1), PawnColor.Black, PawnType.King, kingMoveStrategy),

                new Pawn(new Position('f',6), PawnColor.White, PawnType.Pawn, pawnMoveStrategy),
                new Pawn(new Position('c',3), PawnColor.White, PawnType.Pawn, pawnMoveStrategy),

                new Pawn(new Position('d',4), PawnColor.Black, PawnType.Bishop, bishopMoveStrategy),
                new Pawn(new Position('e',4), PawnColor.White, PawnType.Bishop, bishopMoveStrategy),

                new Pawn(new Position('e',2), PawnColor.Black, PawnType.Knight, knightMoveStrategy),

                new Pawn(new Position('c',6), PawnColor.Black, PawnType.Rook, rookMoveStrategy),

                new Pawn(new Position('a',7), PawnColor.White, PawnType.Queen,queenMoveStrategy),
                new Pawn(new Position('e',5), PawnColor.Black, PawnType.Queen, queenMoveStrategy),

            });

            var pieces = MainCheckerboard.GetAllPawns();
            PiecesOnBoard.Text = $"Pawns: {pieces.Count()}{Environment.NewLine}{Environment.NewLine}";

            foreach (var pawn in pieces) {
                PiecesOnBoard.Text += $"{pawn.PawnColor} {pawn.PawnType}, {pawn.Position.Letter}{pawn.Position.Digit},{Environment.NewLine}";
                var moves = pawn.GetAvailableMoves(MainCheckerboard);
                if (moves?.Any() == true) {
                    PiecesOnBoard.Text += $"Moves: ";
                    foreach (var move in moves) {
                        PiecesOnBoard.Text += $"{move.NewPawnPos.Letter}{move.NewPawnPos.Digit}, ";
                    }
                    var takes = pawn.GetAvailableTakes(moves);
                    if (takes?.Any() == true) {
                        PiecesOnBoard.Text += $"{Environment.NewLine}Takes: ";
                        foreach (var move in takes) {
                            PiecesOnBoard.Text += $"{move.NewPawnPos.Letter}{move.NewPawnPos.Digit}, ";

                        }
                    }
                }
                PiecesOnBoard.Text += Environment.NewLine;
                PiecesOnBoard.Text += Environment.NewLine;
            }
        }
    }
}
