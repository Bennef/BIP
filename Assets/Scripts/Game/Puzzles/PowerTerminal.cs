using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class PowerTerminal : MonoBehaviour
    {
        public bool isActive = false;       // True if this terminal can be powered.
        public bool isPowered = false;      // True when this pipe is connected to a power source.
        public PowerTerminal terminalToActivate;  // The terminals we want to activate if this one has power.
        public AudioSource aSrc;
        public Transform sparkPos;

        void Update()
        {
            if (!isActive && terminalToActivate != null)
            {
                terminalToActivate.isActive = false;
                terminalToActivate.isPowered = false;
            }
            else if (isPowered && terminalToActivate != null)
                terminalToActivate.isActive = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "Power Collider" && isActive && !isPowered)
                Spark();
        }

        void OnTriggerStay(Collider other)
        {
            if (other.name == "Power Collider" && isActive && !isPowered)
                isPowered = true;
        }

        void OnTriggerExit(Collider other)
        {
            if (other.name == "Power Collider")
            {
                isPowered = false;
                if (terminalToActivate != null)
                {
                    terminalToActivate.isActive = false;
                    terminalToActivate.isPowered = false;
                }
            }
        }

        // Make sparks fly.
        void Spark()
        {
            GameObject spark = (GameObject)Instantiate(Resources.Load("vulcan_spark"));
            spark.transform.position = sparkPos.position;
            aSrc.Play();
        }
    }
}