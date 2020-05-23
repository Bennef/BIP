using UnityEngine;

public class PadActivatedFan : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public FanForce fan;
    public PressableSwitch pad;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Use this for initialization
    void Start ()
    {
        fan = GetComponentInChildren<FanForce>();
        pad = GetComponentInChildren<PressableSwitch>();
	}
    // --------------------------------------------------------------------

    // Update is called once per frame
    void Update ()
    {
	    if (pad.switchDown)
        {
            fan.isOn = true;
        }
        else
        {
            fan.isOn = false;
        }
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
