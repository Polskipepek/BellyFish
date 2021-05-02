using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Misc;

namespace BellyFish.Source.Game.Gameplay {
    class GameManager : Singleton<GameManager> {

        public void StartGame () {
            var board = CheckerboardGenerator.InitializeCheckerboard ();

        }

        void HandleMoves (Checkerboard checkerboard) {
            while (!checkerboard.IsTerminal (out PawnColor winColor)) {
                var currPlayer = checkerboard.CurrentColorToMove;
            }
        }
    }
}
