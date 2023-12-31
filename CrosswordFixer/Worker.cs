﻿namespace CrosswordFixer {
    public class Worker : Construction {
        static private List<Label> selectedTiles = new(); //List of all the tiles that is green. it is when there is a potentiel word/correct word.
        static private List<Label[]> markedWords = new();

        public static string CWord() { //all seleted tiles get set together to a word. Use it to compare words to your string
            string theWord = selectedTiles[0].Text;

            for (int i = 1; i < selectedTiles.Count(); i++) {
                theWord += selectedTiles[i].Text;
            }

            return theWord;
        }
        public static string PickRoot(int col, int row) {                           //pickroot gives an string(basicly a char) of the position,
            Label chosenTile = MainPage.Tiles[new Position(col, row).ToString()];   //and changes the postition to red
                                                                                    //Puts the string in a list. USED IN CWord
            chosenTile.BackgroundColor = Colors.IndianRed;

            selectedTiles.Add(chosenTile);
            return chosenTile.Text;
        }
        public static string AddBranch(int col, int row) {                          //Changes the position to yellow
            Label chosenTile = MainPage.Tiles[new Position(col, row).ToString()];   //Gives the word/char like Pickroot
                                                                                    //Puts the string in a list. USED IN CWord
            chosenTile.BackgroundColor = Colors.YellowGreen;

            selectedTiles.Add(chosenTile);

            return chosenTile.Text;
        }
        public static void UnSelectAll() {                                      //Unselects every tiles and string form the selected list
            while (selectedTiles.Count > 0) {                                   //also makes the tiles white
                selectedTiles.First().BackgroundColor = Color.FromArgb(selectedTiles.First().StyleId);
                selectedTiles.Remove(selectedTiles.First());
            }
        }
        public static void UnSelectBranch() {                               //Unselects the branch and goes back to the root
                                                                            //Just like UnselectAll but without the root
            while (selectedTiles.Count > 1) {
                selectedTiles.Last().BackgroundColor = Color.FromArgb(selectedTiles.Last().StyleId);
                selectedTiles.Remove(selectedTiles.Last());
            }
        }
        public static void MarkAsGreen() {                              //makes the tiles in the tiles list green permanently
            markedWords.Add(selectedTiles.ToArray());
            for (int i = 0; i < selectedTiles.Count; i++) {
                selectedTiles[i].StyleId = "37fd12";
                selectedTiles[i].BackgroundColor = Color.FromArgb(selectedTiles[i].StyleId);
            }
        }
        private static int highlightedWord = 0;
        private static int previousHighligtedWord = 0;
        public static void HighlightWord(string direction) {
            switch (direction) {
                default:
                    break;
                case "next":
                    highlightedWord++;
                    if (highlightedWord >= markedWords.Count)
                        highlightedWord = 0;
                    break;
                case "previous":
                    highlightedWord--;
                    if (highlightedWord < 0)
                        highlightedWord = markedWords.Count - 1;
                    break;
            }

            for (int i = 0; i < markedWords[previousHighligtedWord].Length; i++) {
                markedWords[previousHighligtedWord][i].BackgroundColor = Color.FromArgb(markedWords[previousHighligtedWord][i].StyleId);
            }
            previousHighligtedWord = highlightedWord;
            for (int i = 0; i < markedWords[highlightedWord].Length; i++) {
                markedWords[highlightedWord][i].BackgroundColor = Colors.LightSteelBlue;
            }
        }
    }
}
