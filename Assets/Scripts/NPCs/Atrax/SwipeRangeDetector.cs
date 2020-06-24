using UnityEngine;

namespace Scripts.NPCs.Atrax
{
    public class SwipeRangeDetector : MonoBehaviour
    {
        private AtraxAnimationController _anim;

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
                _anim.StartSwipeState();
        }

        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))
                _anim.StopSwipeState();
        }
    }
}