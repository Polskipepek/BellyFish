using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Gameplay.Moves;
using BellyFish.Source.Game.Misc;
using System;

namespace BellyFish.Source.Game.Gameplay.Players {
    class HumanPlayer : IPlayer {

        public HumanPlayer (PawnColor pawnColor, Checkerboard checkerboard) {
            this.pawnColor = pawnColor;
            this.checkerboard = checkerboard;
        }

        private PawnColor pawnColor;
        private Checkerboard checkerboard;

        public event EventHandler<Move> SingleMoveMadeEvent;
        public event EventHandler TurnFinishedEvent;

        public void MoveInit () {
            throw new NotImplementedException ();
        }

        public void MoveUpdate () {
            throw new NotImplementedException ();
        }
    }
}
