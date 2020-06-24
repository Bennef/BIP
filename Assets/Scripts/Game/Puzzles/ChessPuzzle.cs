using Scripts.Game.Level_Dynamics;
using UnityEngine;

namespace Assets.Scripts.Game.Puzzles
{
    public class ChessPuzzle : MonoBehaviour
    {
        public bool hasBeenCompleted;
        public int correctPieceCount = 0;

        public LockableDoors doorToUnlock;

        void Update()
        {
            if (correctPieceCount > 2)
                correctPieceCount = 2;

            if (correctPieceCount < 0)
                correctPieceCount = 0;

            if (correctPieceCount >= 2 && !hasBeenCompleted)
            {
                doorToUnlock.UnlockDoor();
                hasBeenCompleted = true;
            }
        }
    }
}