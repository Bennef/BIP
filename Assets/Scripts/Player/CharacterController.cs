using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour 
{
	// This class handles the interaction between the various Player scripts.
	// Following MVC, this is a Controller class, though not the only controller in this system
	private PlayerMovement playerMovement;							// Handles player movement. Controller
	private PlayerStates state;										// Stores the state of the player. Model.
	private Animator anim;
	private PowerUpManager powerUpManager;							// A reference to the PowerUpManager class.
	private CharacterSoundManager sound;                            // A Reference to Bip's sound manager.
    private PauseMenu pauseMenu;
    protected new Rigidbody rigidbody;
    public Transform Head;                                          // A Reference to Bip's head.
    public AudioSource mainMusic;
    public bool isDead = false;
    public Transform nextPlayer;                                    // For when we switch character.
    public CameraMovement cameraMovement;
    private OverheadUI overheadUI;

	// The View is the Unity GameObject and Animator. They don't need seperate classes here.
	
	void Start()
	{
        overheadUI = GetComponentInChildren<OverheadUI>();
        LoadCheckpoint();
    }
    
    void OnEnable()
    {
        // Setting up references to other objects.
        playerMovement = GetComponent<PlayerMovement>();
        state = GetComponent<PlayerStates>();
        anim = GetComponent<Animator>();
        powerUpManager = GetComponent<PowerUpManager>();
        sound = GetComponent<CharacterSoundManager>();
        pauseMenu = GameObject.Find("Pause Menu Canvas").GetComponent<PauseMenu>();

        rigidbody = GetComponent<Rigidbody>();

        //GetComponent<BuddyMind>().enabled = false;
        GameManager.Instance.Player = transform;
    }
    
    // Update is called once per frame
    protected virtual void Update()
	{
        if (!pauseMenu.paused)
        {
            CheckForDeath();
            HandleInput();
        }
	}
	
	// Moves Bip and the camera to last checkpoint encountered.
	public void LoadCheckpoint()
	{
        mainMusic.volume = 1f;
        GameManager.Instance.ResetPositions();                          // Make sure everything is reset to it's original position.
		GetComponentInChildren<OverheadUI>().ShrinkImmediately();       // Shrink the Overhead UI.
		rigidbody.isKinematic = false;
		rigidbody.velocity = Vector3.zero;
        anim.SetBool(state.isClimbingBool, false);
        transform.position = GameManager.Instance.currentCheckpointPos;// Move Bip to last checkpoint.
        //transform.forward = GameManager.Instance.currentCheckpoint.transform.forward; // Orient Bip how the checkpoint wants him.     // Put this somewhere that happens later, after Bip hits CP
        cameraMovement.shouldReset = true;
        
        if (this.gameObject.GetComponent<PlayerHealth>())
            this.gameObject.GetComponent<PlayerHealth>().Reset();

        if (this.gameObject.GetComponent<PlayerPower>())
            this.gameObject.GetComponent<PlayerPower>().Reset();
        GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<ScreenFader>().StartCoroutine("FadeToClear");
        if (this.gameObject.GetComponent<PlayerMovement>() && SceneManager.GetActiveScene().name != "Main Menu Portfolio")
            playerMovement.isHandlingInput = true;
		isDead = false;
	}
	
	void CheckForDeath()
	{
        // We are already dead. Return.
		if (isDead)
			return;
        // If health is 0 or less we are dead.
		if (this.gameObject.GetComponent<PlayerHealth>() != null && this.gameObject.GetComponent<PlayerHealth>().value <= 0)
		{
			isDead = true;
			StartCoroutine(Death());
		}
	}
    
    public void StartFallDeath() => StartCoroutine(FallDeath());

    public IEnumerator Death()
	{
        //GetComponent<Rigidbody>().isKinematic = true;
        overheadUI.ShrinkImmediately();
        playerMovement.isHandlingInput = false;
        mainMusic.volume = 0.4f;
        anim.SetTrigger("Death");
		GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<ScreenFader>().StartCoroutine("FadeToBlack");
		sound.PlayClip(sound.death);
		yield return new WaitForSeconds(2f);
		LoadCheckpoint();
	}
    
    public IEnumerator FallDeath()
    {
        isDead = true;
        playerMovement.isHandlingInput = false;
        mainMusic.volume = 0.4f;
        GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<ScreenFader>().StartCoroutine("FadeToBlack");
        sound.PlayClip(sound.deathFromFall);
        yield return new WaitForSeconds(4f);
        LoadCheckpoint();
    }
    
    protected void SwitchCharacter()
    {
        Debug.Log("Switching Character");
        SwitchingManager.SwitchCharacter(nextPlayer);
    }
    
    public virtual void HandleInput()
    {
        // Input handling.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // Take input and pass it to the PlayerMovement script.
        // If we're pulling, use a different function for movement handling
        playerMovement.ManageMovement(horizontalInput, verticalInput);

        // Jump.
        if (Input.GetButtonDown("Jump") && anim.GetBool(state.isGroundedBool) && playerMovement.isHandlingInput)
            playerMovement.Jump();

        // Double jump.
        if (Input.GetButtonDown("Jump") && !anim.GetBool(state.isGroundedBool) && !anim.GetBool(state.isClimbingBool) && !playerMovement.hasDoubleJumped && playerMovement.isHandlingInput)
            powerUpManager.DoubleJump();

        // Ledge grab jump.
        if (Input.GetButtonDown("Jump") && anim.GetBool(state.isClimbingBool))
            playerMovement.Jump();

        // Cut jump short for variable height.
        if (Input.GetButtonUp("Jump") && !anim.GetBool(state.isGroundedBool))
            playerMovement.CutJumpShort();

        // EMP attack. Bip must have the powerup for this to work.
        /*if (Input.GetButtonDown("EMP") && powerBar.value == 255) // And Bip has the EMP upgrade////////////////////////
        {
            powerUpManager.EMPBlast();  // Pass in blast duration and charge time.
        }
        
        // Speed boost. Bip must have the powerup for this to work.
        if (Input.GetButtonDown("Sprint") && powerBar.value >= 128) // And Bip has the Speed Boost upgrade////////////////////////
        {
            powerUpManager.SpeedBoost();
        }

        // Check Input for switching character
        if (Input.GetButtonUp("Switch"))
        {
            SwitchCharacter();
        }*/
    }
    
    public virtual void OnCharacterSwitch()
    {
        this.tag = Tags.Buddy; // Switch tag to "Buddy".
        GetComponent<BuddyMind>().enabled = true;
        GetComponent<BuddyMind>().Wait();
        this.enabled = false;
    }
}