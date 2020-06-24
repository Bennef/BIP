using UnityEngine;
using System.Collections;
using Scripts.Player;
using Scripts.UI;
using Scripts.Game.Game_Logic;
using Scripts.Game.Level_Dynamics;
using Assets.Scripts.Game.Puzzles;

namespace Scripts.Game.Puzzles
{
    public class PipeChallenge : MonoBehaviour
    {
        public Player.CharacterController bip;
        public GameObject glassDome, audioManager, thanksText, fader;
        public GameObject lightARed, lightBRed, lightCRed, lightAGreen, lightBGreen, lightCGreen;
        public PressableSwitch startPad;
        public LockableDoors doorToIUnlock;
        public ElectricalDischarge dischargeSphere;
        public PowerTerminal[] terminals;
        public int poweredTerminals = 0;
        public bool hasStarted, hasBeenCompleted, deathBool, challengeFailed, compCheck;

        public Transform lowerPos, upperPos;
        public float speed = 1.0f;

        public AudioSource challengeMusic, mainMusic, darkScary, credits;
        public IEnumerator routine;
        public PlayerHealth bipHealth;
        public GameObject elecPos;
        public AudioSource aSrc, aSrcSlide;
        public DamageByCollision deathFloor;
        public TeleportSend teleportToTurnOff;

        void Start()
        {
            aSrcSlide = glassDome.GetComponent<AudioSource>();
            bip = GameObject.Find("Bip").GetComponent<Player.CharacterController>();
            fader = GameObject.Find("Fader");
        }

        // Update is called once per frame
        void Update()
        {
            if (startPad.hasBeenPressed && !hasStarted)
            {
                teleportToTurnOff.isOn = false;
                hasStarted = true;
                routine = CountDown();
                StartCoroutine(routine);
            }

            if (!challengeFailed)
            {
                foreach (PowerTerminal terminal in terminals)
                {
                    if (terminal.isPowered)
                        poweredTerminals++;
                }
                // If all terminals are connected, unlock the door.
                if (poweredTerminals >= 3)
                {
                    lightARed.SetActive(false);
                    lightAGreen.SetActive(true);
                    teleportToTurnOff.isOn = true;
                }
                if (poweredTerminals >= 5)
                {
                    lightBRed.SetActive(false);
                    lightBGreen.SetActive(true);
                }
                if (poweredTerminals >= 7)
                {
                    lightCRed.SetActive(false);
                    lightCGreen.SetActive(true);
                }
                if (poweredTerminals >= 7 && !hasBeenCompleted)
                {
                    doorToIUnlock.UnlockDoor();
                    hasBeenCompleted = true;
                }
                poweredTerminals = 0;   // Reset counter.

                if (hasBeenCompleted && !compCheck)
                {
                    //StartCoroutine(AudioFadeOut.FadeOut(challengeMusic, 2.0f));
                    //StartCoroutine(AudioFadeOut.FadeIn(credits, 2.0f));
                    StopCoroutine(routine);
                    compCheck = true;
                    thanksText.SetActive(true);
                    StartCoroutine(LoadVentScene());
                }
            }

            // We have died, stop everything.
            if (startPad.hasBeenPressed && bip.IsDead && deathBool == false && !hasBeenCompleted)
            {
                challengeFailed = true;
                ResetPuzzle();
            }
        }

        public void ResetPuzzle()
        {
            teleportToTurnOff.isOn = true;
            StartCoroutine(ResetCo());
            StopCoroutine(routine);
        }

        IEnumerator CountDown()
        {
            dischargeSphere.HideElec(); // Stop the discharge.
            GameObject elec = (GameObject)Instantiate(Resources.Load("Sparks"));    // Power down sound?
            elec.transform.position = elecPos.transform.position;
            aSrc.Stop();
            deathFloor.isOn = false;
            challengeMusic.Play();  // Start the countdown music.
            StartCoroutine(MoveDomeToPosition(upperPos.position, 1f)); // Open dome;
            yield return new WaitForSeconds(65);
            StartCoroutine(MoveDomeToPosition(lowerPos.position, 1f)); // Close dome.
            yield return new WaitForSeconds(1.5f);
            aSrc.Play();
            dischargeSphere.SpawnElec(); // Start the discharge.
            deathFloor.isOn = true;
            ResetPuzzle();
        }

        IEnumerator MoveDomeToPosition(Vector3 newPosition, float time)
        {
            aSrcSlide.Play();
            float elapsedTime = 0;
            Vector3 startingPos = glassDome.transform.position;
            while (elapsedTime < time)
            {
                glassDome.transform.position = Vector3.Lerp(startingPos, newPosition, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, elapsedTime)));
                elapsedTime += Time.deltaTime / time;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        IEnumerator ResetCo()
        {
            deathBool = true;
            yield return new WaitForSeconds(2);
            glassDome.transform.position = lowerPos.position; // Reset position of dome.
            challengeMusic.Stop(); // Stop music.
            dischargeSphere.SpawnElec(); // Start the discharge.
            aSrc.Play();
            if (!doorToIUnlock.locked)
                doorToIUnlock.LockDoor();
            deathBool = false;
            challengeFailed = false;
            hasStarted = false;
            startPad.hasBeenPressed = false;
            MeshRenderer[] ary = { startPad.greenPattern, startPad.greyPattern };
            startPad.SetPadColour(startPad.whitePattern, ary);
            yield return new WaitForSeconds(2);
            lightARed.SetActive(true);
            lightBRed.SetActive(true);
            lightCRed.SetActive(true);
            lightAGreen.SetActive(false);
            lightBGreen.SetActive(false);
            lightCGreen.SetActive(false);
            hasBeenCompleted = false;
        }

        IEnumerator LoadVentScene()
        {
            yield return new WaitForSeconds(1);
            fader.GetComponent<ScreenFader>().StartCoroutine("FadeToBlack");
            yield return new WaitForSeconds(2);
            LocalSceneManager.Instance.LoadScene("2 Vent Lab");
        }
    }
}