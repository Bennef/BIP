using UnityEngine;

public class RotationPuzzle : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public int[] PuzzleSuquence;                  // Set the Puzzle sequence array to match the image you need to replicate - Set to the number of tiles in puzzle
    public int[] PlayerSequence;                  // Player input their sequence here - Keep Blank
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    public void TileRotated(GameObject tile) // Detects which tile is being intereacted with and progresses the number in the specific array associated with that tile, will loop back round to zero
    {
        if (tile.name == "Tile A")
        {
            if (PlayerSequence[0] < 3)
                PlayerSequence[0]++;
            else
                PlayerSequence[0] = 0;
        }
        if (tile.name == "Tile B")
        {
            if (PlayerSequence[1] < 3)
                PlayerSequence[1]++;
            else
                PlayerSequence[1] = 0;
        }
        if (tile.name == "Tile C")
        {
            if (PlayerSequence[2] < 3)
                PlayerSequence[2]++;
            else
                PlayerSequence[2] = 0;
        }
        if (tile.name == "Tile D")
        {
            if (PlayerSequence[3] < 3)
                PlayerSequence[3]++;
            else
                PlayerSequence[3] = 0;
        }
        CheckAnswer();
    }
    // --------------------------------------------------------------------
    public void CheckAnswer() // Circles through both arrays and to see if they match
    {
        bool isSequenceCorrect = true;
        for (int i = 0; i < PuzzleSuquence.Length; i++)
        {
            if (PuzzleSuquence[i] != PlayerSequence[i]) // If one array element doesn't match then it will see the SequenceCorrect bool to false so it doesn't trigger the GameCompleted function
            {
                isSequenceCorrect = false;
            }
        }
        if(isSequenceCorrect)
        {
            GameCompleted();
        }
    }
    // --------------------------------------------------------------------
    public void GameCompleted() // Triggers once puzzle sequence has been completed correctly
    {
        //Do Something
        Debug.Log("Correct");
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
