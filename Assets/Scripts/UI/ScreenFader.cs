using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Will fade the screen to black. Or to clear.
public class ScreenFader : MonoBehaviour
{	
	// This class handles the screen fading in or out.
	public CanvasGroup canvasGroup;
    public CanvasRenderer blackPanel, whitePanel;
	
	// Use this for initialization
	void Awake() 
	{	
		canvasGroup = gameObject.GetComponent<CanvasGroup>();
        whitePanel.gameObject.SetActive(false);
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "0 Main Menu" || scene.name == "Main Menu Portfolio" || scene.name == "Main Menu Web")
            StartCoroutine(FadeToClear());
    }
    
    public IEnumerator FadeToClear()
	{
		// if not fading already
		while (canvasGroup.alpha >= 0.05f)
		{
			canvasGroup.alpha -= Time.deltaTime;
			yield return null;
		}
		canvasGroup.alpha = 0f;
		yield return null;
	}
	
	public IEnumerator FadeToBlack()
	{
        blackPanel.gameObject.SetActive(true);
        whitePanel.gameObject.SetActive(false);
        while (canvasGroup.alpha <= 0.95f)
		{
			canvasGroup.alpha += Time.deltaTime * 1.2f;
			yield return null;
		}
		canvasGroup.alpha = 1f;
		yield return null;
	}
    
    public void ToClear() => canvasGroup.alpha = 0f;

    public IEnumerator FadeToWhite()
    {
        blackPanel.gameObject.SetActive(false);
        whitePanel.gameObject.SetActive(true);
        while (canvasGroup.alpha <= 0.95f)
        {
            canvasGroup.alpha += Time.deltaTime * 0.1f;
            yield return null;
        }
        canvasGroup.alpha = 1f;
        yield return null;
    }   
}