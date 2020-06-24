using UnityEngine;

namespace Scripts.Game.Game_Logic
{
    public class FadeMusicOnPad : MonoBehaviour
    {
        public AudioSource audioToFadeOut;

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "Bip")
                StartCoroutine(AudioFadeOut.FadeOut(audioToFadeOut, 2.0f));
        }
    }
}