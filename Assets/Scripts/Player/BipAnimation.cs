using UnityEngine;

public class BipAnimation : CharacterAnimationController 
{
	// ----------------------------------------------- Data members ----------------------------------------------

	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	// Update is called once per frame
	void Update() 
	{
		// If Bip is moving, and is currently grounded, he's running
		if (state.velocity.magnitude > 0 && anim.GetBool(state.isGroundedBool)) 
		{
			StartRunState();
		} 
		else 	// Otherwise he's not running
		{
			StopRunState();
		}
		// If Bip is moving up, and is not grounded, he's rising!
		if (rigidbody.velocity.y > 0.01f && !anim.GetBool(state.isGroundedBool))
		{
			StartRiseState();
		}
		else if (rigidbody.velocity.y < 0.01f || anim.GetBool(state.isGroundedBool))
		{
			// If we're moving down and we aren't grounded, he's falling (not rising.)
			StopRiseState();
		}
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}
