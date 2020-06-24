using Assets.Scripts.Game.Puzzles;
using Scripts.Game.Level_Dynamics;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
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
            else if (!pad.switchDown && !doorToOpen.locked)
                doorToOpen.LockDoor();
        }
    }
}