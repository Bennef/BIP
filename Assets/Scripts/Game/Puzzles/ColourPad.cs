using UnityEngine;
using System.Collections;
using Scripts.Player;
using Scripts.Game.Game_Logic;
using Assets.Scripts.Game.Puzzles;

namespace Scripts.Game.Puzzles
{
    public class ColourPad : MonoBehaviour
    {
        public bool isActive, isCorrect, switchDown;
        private Transform switchPad;           // The switch that corresponds with this trigger.
        public MeshRenderer whitePattern, greenPattern, redPattern;
        public Vector3 raisedPosition, pressedPosition, patternRaisedPosition, patternPressedPosition;
        private AudioSource aSource;           // The source that will play the sound when player steps on the switch.
        public AudioClip clickDown, clickUp, buzzer;   // The sound that plays on press and release.
        public GameObject pattern, partnerCube, otherCube, dummy;
        public PlayerHealth bipHealth;
        public ColourPadPuzzle colourPadPuzzle;
        public AlarmLight alarmLight;

        void Awake() => aSource = GetComponent<AudioSource>();

        // Use this for initialisation.
        void Start()
        {
            switchPad = gameObject.transform.GetChild(0);       // Get the first child of this gameObject - the switch itself.
            switchPad.transform.localPosition = raisedPosition;   // Set switch to raised position.
            pattern.transform.localPosition = patternRaisedPosition;
            whitePattern.enabled = true;
        }

        void OnTriggerEnter(Collider col)
        {
            // If the player is stepping on this switch or a moveable object is put on it.
            if (col.transform.CompareTag("Player") || col.transform.CompareTag("Moveable"))
            {
                if (!switchDown)
                {
                    aSource.clip = clickDown;
                    aSource.Play();
                }
                // Only perform the task if this pad is active.
                if (isActive && col.transform.name == partnerCube.name)
                {
                    SetPatternColour(whitePattern, greenPattern);
                    isCorrect = true;
                    colourPadPuzzle.correctCount++;
                }
                // Incorrect cube placed.
                if (col.transform.CompareTag("Moveable") && col.transform.name != partnerCube.name)
                {
                    SetPatternColour(whitePattern, redPattern);
                    otherCube = col.gameObject;
                    StartCoroutine(Explode());
                }
                LowerButtonAndPattern();
            }
        }

        void OnTriggerExit(Collider col)
        {
            // If the player or object leaves this switch.
            if ((col.transform.CompareTag("Player") || col.transform.CompareTag("Moveable")) && switchDown)
            {
                switchDown = false;
                switchPad.transform.localPosition = raisedPosition;   // Set switch to raised position.
                pattern.transform.localPosition = patternRaisedPosition;
                aSource.clip = clickUp;
                aSource.Play();

                // Correct cube removed.
                if (col.transform.name == partnerCube.name)
                {
                    SetPatternColour(greenPattern, whitePattern);
                    isCorrect = false;
                    colourPadPuzzle.correctCount--;
                }
            }
        }

        void OnTriggerStay(Collider col)
        {
            // If the player is stepping on this switch or a moveable object is put on it.
            if (col.transform.CompareTag("Player") || col.transform.CompareTag("Moveable"))
            {
                switchDown = true;
                LowerButtonAndPattern();
            }
        }

        public IEnumerator Explode()
        {
            aSource.clip = buzzer;
            aSource.volume = 0.2f;
            aSource.Play();
            alarmLight.AlarmOn = true;
            yield return new WaitForSeconds(0.7f);
            // Replace cube with explody cube.
            GameObject splody = (GameObject)Instantiate(Resources.Load("Cube Clear Fragmented"));
            aSource.Stop();
            splody.transform.position = otherCube.transform.position;
            otherCube.transform.position = dummy.transform.position;
            yield return new WaitForSeconds(0.3f);
            bipHealth.value = 0;
            aSource.volume = 1f;
            yield return new WaitForSeconds(1f);
            SetPatternColour(redPattern, whitePattern);
            RaiseButtonAndPattern();
            alarmLight.AlarmOn = false;
            GameManager.Instance.ResetPositions();
            Destroy(splody);
            colourPadPuzzle.correctCount = 0;
        }

        void SetPatternColour(MeshRenderer oldColour, MeshRenderer newColour)
        {
            newColour.enabled = true;
            oldColour.enabled = false;
            pattern = newColour.gameObject;
        }

        void RaiseButtonAndPattern()
        {
            switchPad.transform.localPosition = raisedPosition;   // Set switch to raised position.
            pattern.transform.localPosition = patternRaisedPosition;
        }

        void LowerButtonAndPattern()
        {
            switchPad.transform.localPosition = pressedPosition;   // Set switch to lower position.
            pattern.transform.localPosition = patternPressedPosition;
        }
    }
}