using UnityEngine;

public class HideableElement : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    CanvasGroup canvasGroup;
    Menu menu;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        menu = GetComponent<Menu>();
    }
    // --------------------------------------------------------------------
    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        menu.enabled = false;
    }
    // --------------------------------------------------------------------
    public void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        menu.enabled = true;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
