using UnityEngine;

public class PadOpensDoorWhenHeld : MonoBehaviour
{
    public PressableSwitch pad;
    public LockableDoors doorToOpen;
    public bool hasBeenCompleted;
    
    // Update is called once per frame
    void Update()
    {
        if (pad.switchDown && doorToOpen.locked)
            doorToOpen.UnlockDoor();
        else if (!pad.switchDown &&!doorToOpen.locked)
            doorToOpen.LockDoor();
	}
}