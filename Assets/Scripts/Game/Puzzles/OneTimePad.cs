using Assets.Scripts.Game.Puzzles;
using Scripts.Game.Level_Dynamics;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class OneTimePad : MonoBehaviour
    {
        public PressableSwitch pressurePad;     // The switch that corresponds with this trigger.
        public LockableDoors door;              // The door we want to open.
        public bool puzzleComplete;             // True once the puzzle is complete.

        void Start() => puzzleComplete = false;

        void Update()
        {
            if (pressurePad.hasBeenPressed && !puzzleComplete)
            {
                door.UnlockDoor();  // Unlock the door.
                puzzleComplete = true; // So we don't keep unlocking the door.
            }
        }
    }
}