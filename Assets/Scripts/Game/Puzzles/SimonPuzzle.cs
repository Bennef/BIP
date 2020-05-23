using UnityEngine;
using System.Collections;

public class SimonPuzzle : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public GameObject[] simonArray;                  // Puzzle array that player will have to memorise - Fill array with the games light gameobjects in editor
    public GameObject[] playerArray;                 // When player steps on a colour it will be added to this array - leave blank
    public MeshRenderer checkOneRed, checkOneGreen, checkTwoRed, checkTwoGreen, checkThreeRed, checkThreeGreen;
    public Light redLight, greenLight, blueLight;    // Attach Lights in editor.

    public int roundLength;                          // How many lights the puzzle plays in a round.
    public int numberOfRounds;                       // How many rounds the puzzle will go through.

    public LockableDoors doorToUnlock;

    public AudioSource simonASrc, mainMusic;
    public AudioClip audioRed, audioGreen, audioBlue, audioWrong;

    public bool gameCompleted = false;    // This only becomes true when the game has been successfluly completed.

    [HideInInspector]
    public bool gameRunning;              // True if puzzle is currently playing.
    [HideInInspector]
    public bool playMode;                 // True if it is player's turn to guess.
    [HideInInspector]
    public int currentPositionInArray;    // Current location that the player is in the choosing sequence.
    [HideInInspector]
    public int roundsCompleted;           // How many rounds the player has already completed.
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        simonASrc = GetComponent<AudioSource>();
        RandomisePuzzle();    // Initiate the randomisation of the puzzle sequence.
    }
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // If player has completed the required number of rounds...
        if (roundsCompleted == numberOfRounds)
        {
            // Initiate reward for completing puzzle.
            PuzzleCompleted();    
        }

        // Resets all vars to start the new rounds with an additional light.
        if (currentPositionInArray == roundLength)
        {
            roundLength++;                      // Increase the round length by 1.
            roundsCompleted++;                  // Increase rounds completed by 1.
            StartCoroutine(Wait());
        }

        if (roundsCompleted == 1)
        {
            checkOneRed.enabled = false;
            checkOneGreen.enabled = true;
        }
        if (roundsCompleted == 2)
        {
            checkTwoRed.enabled = false;
            checkTwoGreen.enabled = true;
        }
        if (roundsCompleted == 3)
        {
            checkThreeRed.enabled = false;
            checkThreeGreen.enabled = true;
        }
    }
    // --------------------------------------------------------------------
    // Puzzle Sequence.
    IEnumerator SimonGame() 
    {
        for (var i = 0; i < roundLength; i++)
        {
            yield return new WaitForSeconds(0.25f);
            simonArray[i].gameObject.GetComponent<Light>().enabled = true;     // Turn light on. 

            if (simonArray[i].gameObject.GetComponent<Light>().name == "Red Light")
            {
                PlayAudioClip(audioRed);
            }
            else if (simonArray[i].gameObject.GetComponent<Light>().name == "Green Light")
            {
                PlayAudioClip(audioGreen);
            }
            else if (simonArray[i].gameObject.GetComponent<Light>().name == "Blue Light")
            {
                PlayAudioClip(audioBlue);
            }

            yield return new WaitForSeconds(1f);

            simonArray[i].gameObject.GetComponent<Light>().enabled = false;    // Turn light off.
        }
        playMode = true;    // Activate players turn.
    }
    // --------------------------------------------------------------------
    // Starts the game.
    public void ActivateGame() 
    {
        if (!gameRunning && roundsCompleted != numberOfRounds)
        {
            StartCoroutine(SimonGame()); // Activate puzzle sequence.
            gameRunning = true;
            mainMusic.volume = 0.4f; // Need to remove this after testing.
        }
    }
    // --------------------------------------------------------------------
    // Randomises the pattern.
    public void RandomisePuzzle() 
    {
        for (int i = simonArray.Length - 1; i > 0; i--)
        {
            var random = Random.Range(0, i);
            var newSimonOrder = simonArray[i];

            simonArray[i] = simonArray[random];
            simonArray[random] = newSimonOrder;
        }
    }
    // --------------------------------------------------------------------
    // If player gets sequence wrong then the game will be randomised again and will start over from the current round. 
    public void Reset()
    {
        playMode = false;                   // We are no longer in play mode.
        gameRunning = false;                // Game is no longer running.
        RandomisePuzzle();                  // Re-randomise the puzzle.
        currentPositionInArray = 0;         // Reset the position in the array so we can start checking from the beginning.
    }
    // --------------------------------------------------------------------
    // Checks to see if player got the current guess right otherwise it resets the round over. 
    public void CheckAnswer()
    {
        // If the player pressed the right button...
        if (playerArray[currentPositionInArray] == simonArray[currentPositionInArray])
        {
            currentPositionInArray++;
        }
        else
        {
            PlayAudioClip(audioWrong);
            Reset();
        }
    }
    // --------------------------------------------------------------------
    //  When the puzzle is completed, unlock the door.
    void PuzzleCompleted()
    {
        gameCompleted = true;
        mainMusic.volume = 1f;
        if (doorToUnlock.locked)
        {
            doorToUnlock.UnlockDoor();
        }
    }
    // --------------------------------------------------------------------
    // Pass in the Light of the button that has been pressed.
    public void ButtonPressed(GameObject button)
    {
        // Start button pressed.
        if (button.name == "Start Button" && gameRunning == false)
        {
            ActivateGame();
        }

        // Coloured button pressed.
        if (button.name == "Red Button" && playMode == true)
        {
            playerArray[currentPositionInArray] = redLight.gameObject;
            PlayAudioClip(audioRed);
            CheckAnswer();
        }

        if (button.name == "Green Button" && playMode == true)
        {
            playerArray[currentPositionInArray] = greenLight.gameObject;
            PlayAudioClip(audioGreen);
            CheckAnswer();
        }

        if (button.name == "Blue Button" && playMode == true)
        {
            playerArray[currentPositionInArray] = blueLight.gameObject;
            PlayAudioClip(audioBlue);
            CheckAnswer();
        }
    }
    // --------------------------------------------------------------------
    // Pass in an AudioClip and play it through the AudioSource.
    public void PlayAudioClip(AudioClip clip)
    {
        simonASrc.clip = clip;
        simonASrc.Play();
    }
    // --------------------------------------------------------------------
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);

        Reset();
        ActivateGame();    // Start the round atuomatically.
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}

