using UnityEngine;

public class PadActivatedFan : MonoBehaviour
{
    public FanForce fan;
    public PressableSwitch pad;
    
    void Start()
    {
        fan = GetComponentInChildren<FanForce>();
        pad = GetComponentInChildren<PressableSwitch>();
	}
 
    void Update()
    {
	    if (pad.switchDown)
            fan.isOn = true;
        else
            fan.isOn = false;
	}
}