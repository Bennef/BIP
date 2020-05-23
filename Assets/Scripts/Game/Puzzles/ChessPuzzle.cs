using UnityEngine;

public class ChessPuzzle : MonoBehaviour
{// ----------------------------------------------- Data members ----------------------------------------------
    public bool hasBeenCompleted;
    public int correctPieceCount = 0;

    public LockableDoors doorToUnlock;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Update()
    {
        if (correctPieceCount > 2)
        {
            correctPieceCount = 2;
        }

        if (correctPieceCount < 0)
        {
            correctPieceCount = 0;
        }

        if (correctPieceCount >= 2 && !hasBeenCompleted)
        {
            doorToUnlock.UnlockDoor();
            hasBeenCompleted = true;
        }
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
