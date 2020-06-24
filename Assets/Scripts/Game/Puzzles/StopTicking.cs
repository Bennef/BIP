using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class StopTicking : MonoBehaviour
    {
        public LaserRunner laserRunner;

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "Bip")
                laserRunner.EndSequence();
        }
    }
}