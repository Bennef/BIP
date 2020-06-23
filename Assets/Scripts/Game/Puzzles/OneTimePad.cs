using UnityEngine;

public class OneTimePad : MonoBehaviour
{    
    public PressableSwitch pressurePad;     // The switch that corresponds with this trigger.
    public LockableDoors door;              // The door we want to open.
    public bool puzzleComplete;             // True once the puzzle is complete.

    private void Start() => puzzleComplete = false;

    void Update()
    {
        if (pressurePad.hasBeenPressed && !puzzleComplete)
        {
            door.UnlockDoor();  // Unlock the door.
            puzzleComplete = true; // So we don't keep unlocking the door.
        }
    }   
}