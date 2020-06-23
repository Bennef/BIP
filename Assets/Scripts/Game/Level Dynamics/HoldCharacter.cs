using UnityEngine;

public class HoldCharacter : MonoBehaviour 
{	
	// Holds stuff on platforms.
	

	
	
	// Make the thing on the platform the child of the platform.
	void OnTriggerEnter(Collider c)
	{
		c.transform.parent = gameObject.transform;
	}
	
	// Release the parantage.
	void OnTriggerExit(Collider c)
	{
		c.transform.parent = null;
	}
	
	
}
