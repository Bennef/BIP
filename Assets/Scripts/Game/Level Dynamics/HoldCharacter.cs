using UnityEngine;

public class HoldCharacter : MonoBehaviour {
	// ----------------------------------------------- Data members ----------------------------------------------
	// Holds stuff on platforms.
	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	// Make the thing on the platform the child of the platform.
	void OnTriggerEnter(Collider c)
	{
		c.transform.parent = gameObject.transform;
	}
	// --------------------------------------------------------------------
	// Release the parantage.
	void OnTriggerExit(Collider c)
	{
		c.transform.parent = null;
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}
