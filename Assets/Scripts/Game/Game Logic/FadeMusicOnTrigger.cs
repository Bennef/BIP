using UnityEngine;

public class FadeMusicOnTrigger : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public AudioSource fadeFrom, fadeTo;
    private bool hasBeenEntered;
    public float fadeTime;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bip" && !hasBeenEntered)
        {
            StartCoroutine(AudioFadeOut.FadeOut(fadeFrom, fadeTime));
            StartCoroutine(AudioFadeOut.FadeIn(fadeTo, fadeTime));
            hasBeenEntered = true;
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}

