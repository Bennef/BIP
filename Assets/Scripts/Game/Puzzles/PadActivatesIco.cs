using Scripts.NPCs.AI;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class PadActivatesIco : MonoBehaviour
    {
        [SerializeField] private HomingIco[] homingIcos;
        [SerializeField] private PressableSwitch pad;
        [SerializeField] private Player.CharacterController bip;

        void Start()
        {
            bip = GameObject.Find("Bip").GetComponent<Player.CharacterController>();
            pad = GameObject.Find("Pressure Pad Ico").GetComponentInChildren<PressableSwitch>();
        }

        void Update()
        {
            if (bip.IsDead)
            {
                pad.hasBeenPressed = false;
                foreach (HomingIco ico in homingIcos)
                    ico.GetComponent<SphereCollider>().enabled = false;
            }

            foreach (HomingIco ico in homingIcos)
            {
                if (!ico.isDead)
                {
                    if (pad.firstPattern.enabled)
                    {
                        ico.GetComponent<SphereCollider>().enabled = false;
                        ico.isInRange = false;
                    }
                    else if (pad.secondPattern.enabled)
                        ico.GetComponent<SphereCollider>().enabled = true;
                }
            }
        }
    }
}