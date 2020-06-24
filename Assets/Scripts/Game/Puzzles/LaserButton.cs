using UnityEngine;
using System.Collections;
using Assets.Scripts.Game.Puzzles;

namespace Scripts.Game.Puzzles
{
    public class LaserButton : MonoBehaviour
    {
        public Transform button;            // The button object that corresponds with this trigger.
        public Vector3 raisedPosition;      // The raised position of the button.
        public Vector3 pressedPosition;     // The pressed position for when player stands on the button.

        public bool playerOn;               // True when player is on the button.
        public bool lasersDisabled;         // True if the lasers are disabled.

        public float delayTime;             // The time to wait when pressed before turning laser back on.

        public SwitchedWallLasers laserScript;

        private AudioSource aSource;        // The source that will play the sound when player steps on the button.
        public AudioClip switchSound;       // The sound that plays on press.

        void Awake() => aSource = GetComponent<AudioSource>();

        // Use this for initialisation.
        void Start()
        {
            aSource.clip = switchSound;
            button = gameObject.transform.GetChild(0); // Get the first child of this gameObject - the button itself.
        }

        void Update()
        {
            if (playerOn == true)
                button.transform.localPosition = pressedPosition;  // Depress button.
            else
                button.transform.localPosition = raisedPosition;   // Return button to raised position.

            // If player is on button and lasers are still on...
            if (playerOn && lasersDisabled == false)
            {
                aSource.Play();
                StartCoroutine(StartCountdown(delayTime));
            }

            // If lasers are disabled...
            if (lasersDisabled == true)
                laserScript.TurnLasersOff();

            // If lasers are not disabled...
            if (lasersDisabled == false)
                laserScript.TurnLasersOn();
        }

        void OnTriggerEnter(Collider col)
        {
            // If the player steps on this button.
            if (col.transform.CompareTag("Player"))
            {
                playerOn = true;
                aSource.Play();
            }
        }

        void OnTriggerExit(Collider col)
        {
            // If the player leaves this button.
            if (col.transform.CompareTag("Player"))
                StartCoroutine(WaitForButton(0.1f));
        }

        void OnTriggerStay(Collider col)
        {
            // If the player is stepping on this button.
            if (col.transform.CompareTag("Player"))
                playerOn = true;
        }

        // Wait for the time specified before the button goes back up.
        IEnumerator WaitForButton(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            playerOn = false;
        }

        // Wait for the time specified before the lasers turn back on.
        IEnumerator StartCountdown(float seconds)
        {
            lasersDisabled = true;
            yield return new WaitForSeconds(seconds);
            lasersDisabled = false;
        }
    }
}