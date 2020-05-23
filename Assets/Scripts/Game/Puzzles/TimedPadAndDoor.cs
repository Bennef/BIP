using System.Collections;
using UnityEngine;

public class TimedPadAndDoor : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public PressableSwitch pad;
    public LockableDoors doorToUnlock;
    public AudioSource aSrc, mainMusic;
    public CharacterController bip;
    public IEnumerator routine;
    public bool deathBool = false;
    public bool hasBeenCompleted = false;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    private void Start()
    {
        bip = GameObject.Find("Bip").GetComponent<CharacterController>();
    }
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // Start the countdown.
        if (pad.hasBeenPressed && doorToUnlock.locked && !hasBeenCompleted)
        {
            routine = CountDown();
            StartCoroutine(routine);
        }
        
        // We have died, stop everything.
        if (pad.hasBeenPressed && bip.isDead && deathBool == false && !hasBeenCompleted)
        {
            StopCoroutine(routine);
            StartCoroutine(Wait());
            doorToUnlock.LockDoor();
            ResetCountDown();
        }

        // If we have made it to the door, set the bool so we can leave the door open.
        if (doorToUnlock.opening)
        {
            hasBeenCompleted = true;
            StopCoroutine(routine);
            ResetCountDown();
            pad.isActive = false;
        }
    }
    // --------------------------------------------------------------------
    IEnumerator Wait()
    {
        deathBool = true;
        yield return new WaitForSeconds(3);
        deathBool = false;
    }
    // --------------------------------------------------------------------
    IEnumerator CountDown()
    {
        if (doorToUnlock.locked)
        {
            doorToUnlock.UnlockDoor();
            aSrc.Play();
            mainMusic.volume = 0.4f;
            yield return new WaitForSeconds(10);
            doorToUnlock.LockDoor();
            ResetCountDown();
        }
    }
    // --------------------------------------------------------------------
    void ResetCountDown()
    {
        aSrc.Stop();
        mainMusic.volume = 1f;
        pad.hasBeenPressed = false;
        MeshRenderer[] ary = { pad.greenPattern, pad.greyPattern };
        pad.SetPadColour(pad.whitePattern, ary);
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
