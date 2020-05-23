using UnityEngine;
using System.Collections;

public class AutoDestroyParticle : MonoBehaviour
 {
	// ----------------------------------------------- Data members ----------------------------------------------
	// Creates a spark when somethin hits a laser.
 	private ParticleSystem ps;
	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	// Use this for initialization
	void Awake() 
	{
		ps = gameObject.GetComponent<ParticleSystem>();
	}
	// --------------------------------------------------------------------
	// Update is called once per frame
	void Update() 
	{
	    // If is not null.
		if (ps)
		{
			// If particle system has finished.
			if (!ps.IsAlive())
			{
				Destroy(gameObject);
			}
		}
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}
