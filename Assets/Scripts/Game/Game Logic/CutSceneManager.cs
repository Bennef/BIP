using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour 
{	
	// ----------------------------------------------- Data members ----------------------------------------------
	// Manages cutscenes - playing them and stuff.

    private GameObject bip;                          // The Bip gameObject.   
    private Camera mainCamera;                       // The main camera.
    private UIManager UIManager;                     // Reference the UI Manager.
    private GameObject fader;                        // Screen fader.

    private PlayerMovement playerMovement;           // Handles player movement. Controller.
	private CameraMovement cameraMovement;           // The main camera script.
    private ScreenFader faderScript;                 // Fader script.
    
    private AudioSource bgMusic;                     
    // ----------------------------------------------- End Data members ------------------------------------------
    // --------------------------------------------------- Methods -----------------------------------------------
    public void Awake()
    {
        // Find all the GameObjects we need.
        bip = GameObject.Find("Bip");
        fader = GameObject.Find("Fader");
        UIManager = GameObject.Find("UI").GetComponent<UIManager>();

        playerMovement = bip.GetComponent<PlayerMovement>();    // Get the PlayerMovement script on Bip.
        faderScript = fader.GetComponent<ScreenFader>();        // Get the screen fader script.
        mainCamera = Camera.main;
        cameraMovement = mainCamera.GetComponent<CameraMovement>();      // Get the camera movenent script on main camera.
        //StartCoroutine(CutScene1());
    }
    // --------------------------------------------------------------------
    // Calls the CoRoutine. Pass in a unique CutScene ID.
    public void PlayCutScene(string cutSceneID)
    {
        // Play whichever cutscene we passed the ID of.
        StartCoroutine("CutScene" + cutSceneID);
    }
    // --------------------------------------------------------------------
    // The first cutscene. Bip wakes up at the bottom of a dumpster.
    public IEnumerator CutScene1()
    {
        DisableInput();

        yield return new WaitForSeconds(4.0f);
        
        faderScript.StartCoroutine("FadeToClear");  // Fade to clear.
       
        yield return new WaitForSeconds(5.0f);

        cameraMovement.enabled = true;          // Enable camera movement.
        playerMovement.isHandlingInput = true;  // Enable player movement.
    }
    // --------------------------------------------------------------------

    // The sencond cutscene. Bip clims out of the dumpster, looks around and runs into the vent in the wall.
    public IEnumerator CutScene2()////////////////////////////////////////////////////////////
    {
        faderScript.StartCoroutine("FadeToBlack");  // Fade to black.
        DisableInput();
        mainCamera.transform.position = new Vector3(15.75f, 29.84f, 19.26f);    // Set camera position.
        mainCamera.transform.rotation = Quaternion.Euler(32.0f, -164.64f, -0.0126f);    // Set camera rotation.

        yield return new WaitForSeconds(2.0f);
 
        faderScript.StartCoroutine("FadeToClear");  // Fade to clear.

        //// Play the Bip animations of Bip climbing out of the dumpster, looking around and running into the vent.

        faderScript.StartCoroutine("FadeToBlack");  // Fade to black.

        yield return new WaitForSeconds(5.0f);

        //// Load the next scene.  ///// is this right??? BF
        LocalSceneManager.Instance.LoadScene("Level 2");
    }
    // --------------------------------------------------------------------
    // Disable camera and player input for the cutscenes.
    public void DisableInput()
    {
        cameraMovement.enabled = false;          
        playerMovement.isHandlingInput = false;         
    }
    // --------------------------------------------------------------------
    public void EnableInput()
    {
        cameraMovement.enabled = true;          
        playerMovement.isHandlingInput = true;  
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
