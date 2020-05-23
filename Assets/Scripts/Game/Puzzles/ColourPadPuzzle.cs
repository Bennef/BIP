using UnityEngine;

public class ColourPadPuzzle : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public LockableDoors doorToUnlock;
    public int correctCount;
    public bool hasUnlocked;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Use this for initialization
    void Start ()
    {
        correctCount = 0;
        hasUnlocked = false;
    }
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update ()
    {
		if (correctCount >= 4 && !hasUnlocked)
        {
            doorToUnlock.UnlockDoor();
            hasUnlocked = true;
        }
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
