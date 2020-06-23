using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool showHUD;
    public static UIManager Instance;
    public Slider HealthSlider, PowerSlider;
    public ScreenFader ScreenFader;
    public GameObject HUD, pauseMenu;
    
    public void Awake()
    {
        HidePauseMenu();
        ScreenFader.GetComponent<CanvasGroup>().alpha = 1f;
        if (showHUD)
            ShowHUD();
        else
            HideHUD();
    }
    
    public void ShowHUD() => HUD.GetComponent<CanvasGroup>().alpha = 1f;

    public void HideHUD() => HUD.GetComponent<CanvasGroup>().alpha = 0f;

    public void HidePauseMenu() => pauseMenu.GetComponent<CanvasGroup>().alpha = 0f;
}
