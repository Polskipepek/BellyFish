using BellyFish.Source.Game.CheckerBoard.Fields;

namespace BellyFish.Source.Misc.Extensions {
    static class Array2DExtensions {
        public static Field[,] Reverse2DimField (this Field[,] theArray) {
            for (int rowIndex = 0; rowIndex <= (theArray.GetUpperBound (0)); rowIndex++) {
                for (int colIndex = 0; colIndex <= (theArray.GetUpperBound (1) / 2); colIndex++) {

                    Field tempHolder = theArray[rowIndex, colIndex];
                    theArray[rowIndex, colIndex] =
                      theArray[rowIndex, theArray.GetUpperBound (1) - colIndex];
                    theArray[rowIndex, theArray.GetUpperBound (1) - colIndex] =
                      tempHolder;
                }
            }
            return theArray;
        }
    }
}