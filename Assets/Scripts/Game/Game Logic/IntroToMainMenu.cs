using System.Collections;
using UnityEngine;

public class IntroToMainMenu : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public bool straightToMenu;
    public CanvasGroup canvasGroup;
    public float delay;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        
        if (straightToMenu)
        {
            canvasGroup.alpha = 0f;
        }
        else
        {
            StartCoroutine(FadeToClear(delay));
        }
    }

    // --------------------------------------------------------------------
    public IEnumerator FadeToClear(float delay)
    {
        yield return new WaitForSeconds(delay);
        // if not fading already
        while (canvasGroup.alpha >= 0.05f)
        {
            canvasGroup.alpha -= Time.deltaTime * 0.4f;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        yield return null;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
