using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Scripts.UI;
using Scripts.NPCs.AI;
using Scripts.Game;
using Scripts.Game.Cameras;
using Scripts.Game.Game_Logic;

namespace Scripts.Player
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private bool isDead = false;
        [SerializeField] private AudioSource mainMusic;
        [SerializeField] private Transform head;
        private PlayerMovement playerMovement;
        private PlayerStates state;
        private Animator anim;
        private PowerUpManager powerUpManager;
        private CharacterSoundManager soundManager;
        private PauseMenu pauseMenu;
        private Transform nextPlayer; // For when we switch character.
        private CameraMovement _cameraMovement;
        private OverheadUI _overheadUI;
        protected Rigidbody rb;

        public bool IsDead { get => isDead; set => isDead = value; }
        public Transform Head { get => head; set => head = value; }

        void Start()
        {
            _cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
            _overheadUI = GetComponentInChildren<OverheadUI>();
            LoadCheckpoint();
        }

        void OnEnable()
        {
            playerMovement = GetComponent<PlayerMovement>();
            state = GetComponent<PlayerStates>();
            anim = GetComponent<Animator>();
            powerUpManager = GetComponent<PowerUpManager>();
            soundManager = GetComponent<CharacterSoundManager>();
            pauseMenu = GameObject.Find("Pause Menu Canvas").GetComponent<PauseMenu>();
            rb = GetComponent<Rigidbody>();

            //GetComponent<BuddyMind>().enabled = false;
            GameManager.Instance.Player = transform;
        }

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
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            anim.SetBool(state.IsClimbingBool, false);
            transform.position = GameManager.Instance.currentCheckpointPos;// Move Bip to last checkpoint.
                                                                           //transform.forward = GameManager.Instance.currentCheckpoint.transform.forward; // Orient Bip how the checkpoint wants him.     // Put this somewhere that happens later, after Bip hits CP
            _cameraMovement.ShouldReset = true;

            if (gameObject.GetComponent<PlayerHealth>())
                gameObject.GetComponent<PlayerHealth>().Reset();

            if (gameObject.GetComponent<PlayerPower>())
                gameObject.GetComponent<PlayerPower>().Reset();
            GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<ScreenFader>().StartCoroutine("FadeToClear");
            if (gameObject.GetComponent<PlayerMovement>() && SceneManager.GetActiveScene().name != "Main Menu Portfolio")
                playerMovement.isHandlingInput = true;
            IsDead = false;
        }

        void CheckForDeath()
        {
            if (IsDead)
                return;
            // If health is 0 or less we are dead.
            if (gameObject.GetComponent<PlayerHealth>() != null && gameObject.GetComponent<PlayerHealth>().value <= 0)
            {
                IsDead = true;
                StartCoroutine(Death());
            }
        }

        public void StartFallDeath() => StartCoroutine(FallDeath());

        public IEnumerator Death()
        {
            _overheadUI.ShrinkImmediately();
            playerMovement.isHandlingInput = false;
            mainMusic.volume = 0.4f;
            anim.SetTrigger("Death");
            GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<ScreenFader>().StartCoroutine("FadeToBlack");
            soundManager.PlayClip(soundManager.Death);
            yield return new WaitForSeconds(2f);
            LoadCheckpoint();
        }

        public IEnumerator FallDeath()
        {
            IsDead = true;
            playerMovement.isHandlingInput = false;
            mainMusic.volume = 0.4f;
            GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<ScreenFader>().StartCoroutine("FadeToBlack");
            soundManager.PlayClip(soundManager.DeathFromFall);
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
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            // Take input and pass it to the PlayerMovement script.
            // If we're pulling, use a different function for movement handling
            playerMovement.ManageMovement(horizontalInput, verticalInput);

            // Jump.
            if (Input.GetButtonDown("Jump") && anim.GetBool(state.IsGroundedBool) && playerMovement.isHandlingInput)
                playerMovement.Jump();

            // Double jump.
            if (Input.GetButtonDown("Jump") && !anim.GetBool(state.IsGroundedBool) && !anim.GetBool(state.IsClimbingBool) && !playerMovement.hasDoubleJumped && playerMovement.isHandlingInput)
                powerUpManager.DoubleJump();

            // Ledge grab jump.
            if (Input.GetButtonDown("Jump") && anim.GetBool(state.IsClimbingBool))
                playerMovement.Jump();

            // Cut jump short for variable height.
            if (Input.GetButtonUp("Jump") && !anim.GetBool(state.IsGroundedBool))
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
            tag = Tags.Buddy; // Switch tag to "Buddy".
            GetComponent<BuddyMind>().enabled = true;
            GetComponent<BuddyMind>().Wait();
            enabled = false;
        }
    }
}