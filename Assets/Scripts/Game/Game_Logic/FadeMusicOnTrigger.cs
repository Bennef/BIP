using UnityEngine;

namespace Scripts.Game.Game_Logic
{
    public class FadeMusicOnTrigger : MonoBehaviour
    {
        public AudioSource fadeFrom, fadeTo;
        private bool hasBeenEntered;
        public float fadeTime;

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "Bip" && !hasBeenEntered)
            {
                StartCoroutine(AudioFadeOut.FadeOut(fadeFrom, fadeTime));
                StartCoroutine(AudioFadeOut.FadeIn(fadeTo, fadeTime));
                hasBeenEntered = true;
            }
        }
    }
}