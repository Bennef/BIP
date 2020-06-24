using Scripts.Game.Level_Dynamics;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class ZorbPuzzle : MonoBehaviour
    {
        public ZorbButton[] zorbButtons;
        public LockableDoors doorToUnlock;
        public int correctCount = 0;

        void Update()
        {
            if (correctCount >= 3 && doorToUnlock.locked)
                doorToUnlock.UnlockDoor();

            correctCount = 0;

            foreach (ZorbButton button in zorbButtons)
            {
                if (button.complete == true)
                    correctCount++;
            }
        }
    }
}