using UnityEngine;

public class PadOpensDoorWhenHeld : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public PressableSwitch pad;
    public LockableDoors doorToOpen;
    public bool hasBeenCompleted;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update ()
    {
        if (pad.switchDown && doorToOpen.locked)
        {
            doorToOpen.UnlockDoor();
        }
        else if (!pad.switchDown &&!doorToOpen.locked)
        {
            doorToOpen.LockDoor();
        }
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
