using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class SwitchedWallLasers : MonoBehaviour
    {
        public GameObject[] lasers;        // An array to store the lasers.
        public LaserButton buttonScript;   // Reference to the button script.

        // Turn all the lasers on.
        public void TurnLasersOn()
        {
            foreach (GameObject laser in lasers)
                laser.SetActive(true);
        }

        // Turn all the lasers off.
        public void TurnLasersOff()
        {
            foreach (GameObject laser in lasers)
                laser.SetActive(false);
        }
    }
}