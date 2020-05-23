using UnityEngine;

public class PressableSwitch : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public bool isActive, switchDown, hasBeenPressed;               
    private Transform switchPad;           // The switch that corresponds with this trigger.
    public GameObject pattern;             // The coloured pattern on the switch - this will change colour when pressed.
    public MeshRenderer whitePattern, greenPattern, greyPattern, firstPattern, secondPattern;
    public Vector3 raisedPosition, pressedPosition;      
    private AudioSource aSource;           // The source that will play the sound when player steps on the switch.
    public AudioClip clickDown, clickUp;   // The sound that plays on press and release.

    public enum PadType
    {
        OnOff,
        MultiState,
        HoldToActivate
    }
    public PadType type = PadType.OnOff;    // Set as default.
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }
    // --------------------------------------------------------------------
    // Use this for initialisation.
    void Start()
    {
        // Set colour for pad when up.
        if (type == PadType.OnOff || type == PadType.HoldToActivate)
        {
            MeshRenderer[] ary = { greenPattern, greyPattern };
            SetPadColour(whitePattern, ary);
            pattern = whitePattern.gameObject;
        }
        else if (type == PadType.MultiState)
        {
            MeshRenderer[] ary = { greenPattern, greyPattern, whitePattern, secondPattern };
            SetPadColour(firstPattern, ary);
            pattern = firstPattern.gameObject;
        }

        switchPad = this.gameObject.transform.GetChild(0);       // Get the first child of this gameObject - the switch itself.
        hasBeenPressed = false;
        switchPad.transform.localPosition = raisedPosition;   // Set switch to raised position.
        pattern.transform.localPosition = raisedPosition;
    }
    // --------------------------------------------------------------------
    void OnTriggerEnter(Collider col)
    {
        // If the player is stepping on this switch or a moveable object is put on it.
        if ((col.transform.tag == "Player" || col.transform.tag == "Moveable" || col.transform.tag == "Gravity") && !switchDown)
        {
            switchDown = true;
            aSource.clip = clickDown;
            aSource.Play();
            
            // Only perform the task if this pad is active.
            if (isActive)
            {
                if (type == PadType.OnOff)
                {
                    MeshRenderer[] ary = { whitePattern, greyPattern };
                    SetPadColour(greenPattern, ary);
                }
                else if (type == PadType.MultiState)
                {
                    if (firstPattern.enabled == true)
                    {
                        firstPattern.enabled = false;
                        secondPattern.enabled = true;
                        pattern = secondPattern.gameObject; 
                    }
                    else
                    {
                        firstPattern.enabled = true;
                        secondPattern.enabled = false;
                        pattern = firstPattern.gameObject;
                    }
                }
                hasBeenPressed = true;
            }
            switchPad.transform.localPosition = pressedPosition;  // Depress switch.
            pattern.transform.localPosition = pressedPosition;
        }
    }
    // --------------------------------------------------------------------
    void OnTriggerExit(Collider col)
    {
        // If the player or object leaves this switch.
        if (col.transform.tag == "Player" || col.transform.tag == "Moveable" || col.transform.tag == "Gravity")
        {
            switchPad.transform.localPosition = raisedPosition;   // Set switch to raised position.
            pattern.transform.localPosition = raisedPosition;
            if (isActive)
            {
                aSource.clip = clickUp;
                aSource.Play();
                switchDown = false;

                if (type == PadType.HoldToActivate)
                {
                    MeshRenderer[] ary = { greenPattern, greyPattern };
                    SetPadColour(whitePattern, ary);
                }
            }
        }
    }
    // --------------------------------------------------------------------
    void OnTriggerStay(Collider col)
    {
        // If the player is stepping on this switch or a moveable object is put on it.
        if (col.transform.tag == "Player" || col.transform.tag == "Moveable" || col.transform.tag == "Gravity")
        {
            switchDown = true; 
            if (type == PadType.HoldToActivate)
            {
                MeshRenderer[] ary = { whitePattern, greyPattern };
                SetPadColour(greenPattern, ary);
            }
        }
    }
    // --------------------------------------------------------------------
    public void SetPadColour(MeshRenderer colourToSet, MeshRenderer[] coloursToUnset)
    {
        foreach (MeshRenderer colourToUnset in coloursToUnset)
        {
            colourToUnset.enabled = false;
        }
        colourToSet.enabled = true;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
