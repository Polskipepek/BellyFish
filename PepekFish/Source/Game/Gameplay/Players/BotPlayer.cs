using BellyFish.Source.Game.Gameplay.Moves;
using BellyFish.Source.Game.Misc;
using System;

namespace BellyFish.Source.Game.Gameplay.Players {
    class BotPlayer : IPlayer {
        public event EventHandler<Move> SingleMoveMadeEvent;
        public event EventHandler TurnFinishedEvent;
        public BotPlayer (PawnColor pawnColor, int depth) {
            this.pawnColor = pawnColor;
            this.depth = depth;
        }

        private PawnColor pawnColor;
        private int depth;
        public void MoveInit () {
            throw new NotImplementedException ();
        }

        public void MoveUpdate () {
            throw new NotImplementedException ();
        }
    }
}
