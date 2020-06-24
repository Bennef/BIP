using UnityEngine;

namespace Scripts.NPCs.Atrax
{
    public class JumpRangeDetector : MonoBehaviour
    {
        [SerializeField] private AtraxAnimationController anim;

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
                anim.StartJumpState();
        }

        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
                anim.StopJumpState();
        }
    }
}