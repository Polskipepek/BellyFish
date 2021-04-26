using System;

namespace BellyFish.Source.Misc.Attributes {
    class PawnTypeValueAttribute : Attribute {
        private readonly int value;
        public PawnTypeValueAttribute(int value) {
            this.value = value;
        }
    }
}
