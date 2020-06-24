using UnityEngine;

namespace Scripts.UI
{
    public class UIAnimationTriggerScript : MonoBehaviour
    {
        public GameObject interactWithMeText, bip;
        Animator textAnimation;

        void Start()
        {
            textAnimation = interactWithMeText.GetComponent<Animator>();
            bip = GameObject.FindGameObjectWithTag("Player");
        }

        // Triggers Animation when Bip enters
        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject == bip)
            {
                textAnimation.enabled = true;
                textAnimation.Play("InteractWithMe");
            }
        }

        // Reverses annimation when Bip leaves Trigger
        void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject == bip)
                textAnimation.Play("ReverseInteracctWithMe");
        }
    }
}