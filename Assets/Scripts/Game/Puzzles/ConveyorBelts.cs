using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class ConveyorBelts : MonoBehaviour
    {
        public Conveyor[] platforms;

        public void SwitchOn()
        {
            foreach (Conveyor platform in platforms)
                platform.isOn = true;
        }

        public void SwitchOff()
        {
            foreach (Conveyor platform in platforms)
                platform.isOn = false;
        }
    }
}