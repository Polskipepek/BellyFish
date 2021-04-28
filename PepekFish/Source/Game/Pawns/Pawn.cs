using BellyFish.Source.Game.CheckerBoard;
using BellyFish.Source.Game.Gameplay;
using BellyFish.Source.Game.Gameplay.MoveStrategy;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Misc;
using BellyFish.Source.Misc.Attributes;
using System;
using System.Collections.Generic;

namespace BellyFish.Source.Game.Pawns {
    class Pawn {
        public Pawn(Position position, PawnColor pawnColor, PawnType pawnType, IMoveStrategy moveStrategy) {
            Position = position;
            PawnColor = pawnColor;
            PawnType = pawnType;
            MoveStrategy = moveStrategy;
        }

        private Pawn() { }

        public PawnColor PawnColor { get; init; }
        public Position Position { get; private set; }
        public PawnType PawnType { get; private set; }
        public IMoveStrategy MoveStrategy { get; private set; }

        public static Pawn DeepCopy(Pawn original) {
            return new Pawn {
                PawnColor = original.PawnColor,
                PawnType = original.PawnType,
                Position = original.Position,
            };
        }

        public void SetNewPosition(Position newPos) => Position = newPos;

        public IEnumerable<Move> GetAvailableMoves(Checkerboard checkerboard) {
            return MoveStrategy.GetMoves(checkerboard, this);
        }

        public PawnTypeValueAttribute GetPawnTypeValue(Type classname) {
            PawnTypeValueAttribute value = (PawnTypeValueAttribute)Attribute.GetCustomAttribute(classname, typeof(PawnTypeValueAttribute));

            if (value != null) return value;
            return null;
        }
    }
}
