using BellyFish.Source.Game.Misc;
using BellyFish.Source.Misc;

namespace BellyFish.Source.Game.CheckerBoard.Fields {
    class FieldsGenerator : Singleton<FieldsGenerator> {

        Field[,] Fields = new Field[8, 8];
        public Field[,] GenerateFields() {
            for (int x = 0; x < 8; x++) {
                for (int y = 0; y < 8; y++) {
                    Fields[x, y] = new Field(new Position(x, y), (x + y) % 2 == 0 ? PawnColor.White : PawnColor.Black);
                }
            }
            return Fields;
        }
    }
}
