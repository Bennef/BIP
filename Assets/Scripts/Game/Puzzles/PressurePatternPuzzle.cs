using UnityEngine;

// In order to unlock the door, every tile in the array must have their "switchDown" value as true.
public class PressurePatternPuzzle : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public PressableSwitch[] padsToPress;       // The pads we need to press.
    public LockableDoors door;                  // The door we want to unlock.
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
	    // For each pad in the array...
        for (int i = 0; i < padsToPress.Length; i++) 
        {
            // If any pads are still up, we keep the door locked.
            if (!padsToPress[i].switchDown)
            {
                door.locked = true;
            }
            else
            {
                door.locked = false;
            }
        }
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
