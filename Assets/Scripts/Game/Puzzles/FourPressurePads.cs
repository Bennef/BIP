using UnityEngine;
using UnityEngine.UI;

public class FourPressurePads : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public bool hasBeenCompleted;
    public int counter, startNum;
    public Text text;
    public PressableSwitch[] pads;
    public CharacterController bip;
    public TeleportSend teleporter;
    public Light teleLight;
    public GameObject particleWaves;
    public LockableDoors doorToUnlock;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    private void Start()
    {
        if (particleWaves != null)
        {
            particleWaves.SetActive(false);
            teleLight.enabled = false;
        }
    }
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update ()
    {
        if (bip.isDead)
        {
            ResetPuzzle();
        }

        if (!hasBeenCompleted)
        {
            counter = startNum;

            foreach (PressableSwitch pad in pads)
            {
                if (pad.hasBeenPressed)
                {
                    counter--;
                }
            }

            switch (counter)
            {
                case 10:
                    text.text = "10";
                    break;
                case 9:
                    text.text = "9";
                    break;
                case 8:
                    text.text = "8";
                    break;
                case 7:
                    text.text = "7";
                    break;
                case 6:
                    text.text = "6";
                    break;
                case 5:
                    text.text = "5";
                    break;
                case 4:
                    text.text = "4";
                    break;
                case 3:
                    text.text = "3";
                    break;
                case 2:
                    text.text = "2";
                    break;
                case 1:
                    text.text = "1";
                    break;
                case 0:
                    text.text = "0";
                    FinishPuzzle();
                    break;
                default:
                    break;
            }
        }
	}
    // --------------------------------------------------------------------
    public void ResetPuzzle()
    {
        counter = 6;
        foreach (PressableSwitch pad in pads)
        {
            pad.hasBeenPressed = false;
            MeshRenderer[] ary = { pad.greenPattern, pad.greyPattern };
            pad.SetPadColour(pad.whitePattern, ary);
        }
    }
    // --------------------------------------------------------------------
    public void FinishPuzzle()
    {
        if (teleporter != null)
        {
            teleporter.isOn = true;
            teleLight.enabled = true;
            particleWaves.SetActive(true);
        }
        else
        {
            doorToUnlock.UnlockDoor();
        }
        hasBeenCompleted = true;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
