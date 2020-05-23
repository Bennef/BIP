using UnityEngine;

public class ZorbPuzzle : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public ZorbButton[] zorbButtons;
    public LockableDoors doorToUnlock;
    public int correctCount = 0;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update ()
    {
        if (correctCount >= 3 && doorToUnlock.locked)
        {
            doorToUnlock.UnlockDoor();
        }

        correctCount = 0;

        foreach (ZorbButton button in zorbButtons)
        {
            if (button.complete == true)
            {
                correctCount++;
            }
        }
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
