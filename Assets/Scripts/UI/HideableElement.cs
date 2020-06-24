using Scripts.UI;
using UnityEngine;

namespace Scripts.UI
{
    public class HideableElement : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Menu menu;

        void OnEnable()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            menu = GetComponent<Menu>();
        }

        public void Hide()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            menu.enabled = false;
        }

        public void Show()
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            menu.enabled = true;
        }
    }
}