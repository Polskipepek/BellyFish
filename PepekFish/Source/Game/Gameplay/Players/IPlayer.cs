using BellyFish.Source.Game.Gameplay.Moves;
using System;

namespace BellyFish.Source.Game.Gameplay.Players {
    interface IPlayer {
        public event EventHandler<Move> SingleMoveMadeEvent;
        public event EventHandler TurnFinishedEvent;
        public void MoveInit ();
        public void MoveUpdate ();
    }
}
