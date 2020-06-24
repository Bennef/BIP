using Assets.Scripts.Game.Puzzles;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class ResetAlarm : MonoBehaviour
    {
        public TripLaserPuzzle tripLaserPuzzle;

        public void OnTriggerEnter(Collider other)
        {
            if (other.name == "Collider front")
                tripLaserPuzzle.ResetCo();
        }
    }
}