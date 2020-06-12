using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour 
{
    // ----------------------------------------------- Data members ----------------------------------------------
    // Manages the pause menu itself.
    public bool paused, skipIntro;	

	private AudioSource aSrc;
    public Scene scene;
    public AudioClip pause, resume, select, press;
    public Animator anim;
    public CharacterController charController;
    private CanvasGroup canvasGroup;
    public ScreenFader screenFader;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Start()
	{
        aSrc = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        if (GameObject.Find("Bip") != null)
        {
            charController = GameObject.Find("Bip").GetComponent<CharacterController>();
        }

        Cursor.visible = false;
        screenFader = GameObject.Find("Fader").GetComponent<ScreenFader>();
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(SetAlpha());
    }
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update() 
	{
		CheckInput();

        if (skipIntro)///////////////////////////////////////////////////////
        {
            scene = SceneManager.GetActiveScene(); 
            if (scene.name == "0 Main Menu" || scene.name == "Main Menu Portfolio")
            {
                GameObject.Find("Splash Screen").GetComponent<IntroToMainMenu>().straightToMenu = true;
                GameObject.Find("Black Image 2").GetComponent<IntroToMainMenu>().straightToMenu = true;
            }
        }
    }
	// --------------------------------------------------------------------
    // Check for and manage input.
    public void CheckInput()
    {
        if (Input.GetButtonDown("Pause") && SceneManager.GetActiveScene().name != "0 Main Menu" 
            && SceneManager.GetActiveScene().name != "Main Menu Portfolio"
            && SceneManager.GetActiveScene().name != "Main Menu Web")
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
	// --------------------------------------------------------------------
	public void Pause()
	{
        paused = true;
        aSrc.clip = pause;
        aSrc.Play();
        GameManager.Instance.Pause();
        anim.Play("SlidePausePanelIn");
        Cursor.visible = true;
        canvasGroup.interactable = true;
    }
    // --------------------------------------------------------------------
    public void Resume()
    {
        paused = false;
        GameManager.Instance.Resume();
        anim.Play("SlidePausePanelOut");
        Cursor.visible = false;
        canvasGroup.interactable = false;
        aSrc.clip = resume;
        aSrc.Play();
    }
    // --------------------------------------------------------------------
    public void Checkpoint()
    {
        StartCoroutine(CheckpointCo());
    }
    // --------------------------------------------------------------------
    public IEnumerator CheckpointCo()
    {
        screenFader.StartCoroutine(screenFader.FadeToBlack());
        yield return new WaitForSeconds(1f);
        charController.LoadCheckpoint();
    }
    // --------------------------------------------------------------------
    public void BackToMainMenu()
    {
        skipIntro = true;
        paused = false;
        anim.Play("SlidePausePanelOut");
        Cursor.visible = false;
        canvasGroup.interactable = false;
        //LocalSceneManager.Instance.LoadScene("0 Main Menu");  // GAME
        LocalSceneManager.Instance.LoadScene("Main Menu Portfolio"); // DEMO
    }
    // --------------------------------------------------------------------
    public void PlayClip(AudioClip clip)
    {
        float lastTimeScale = Time.timeScale;
        Time.timeScale = 1;
        AudioSource.PlayClipAtPoint(clip, Vector3.zero, 1f);
        Time.timeScale = lastTimeScale;
    }
    // --------------------------------------------------------------------
    public IEnumerator SetAlpha()
    {
        yield return new WaitForSeconds(0.01f);
        canvasGroup.alpha = 1f;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}