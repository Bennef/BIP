using UnityEngine;

public class LightPulse : MonoBehaviour 
{
	// ----------------------------------------------- Data members ----------------------------------------------
	public float PULSE_RANGE = 6.0f;
    public float PULSE_SPEED = 2.0f;
    public float PULSE_MINIMUM = 2.0f;
	private Light light;
	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		light = gameObject.GetComponent<Light> ();
	}
	// --------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
	{
		Pulse();
	}
	// --------------------------------------------------------------------
	// Give the blue light a pulse.
	void Pulse()
	{
		light.range = PULSE_MINIMUM + Mathf.PingPong(Time.time * PULSE_SPEED, PULSE_RANGE - PULSE_MINIMUM);
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}