using UnityEngine;

public class FadeMusicOnPad : MonoBehaviour
{
    public AudioSource darkScary;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bip")
            StartCoroutine(AudioFadeOut.FadeOut(darkScary, 2.0f));
    }
}