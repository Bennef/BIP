using UnityEngine;
using System.Collections;

public class PlayerStates : MonoBehaviour
{
	// ----------------------------------------------- Data members ----------------------------------------------
	public int idleState;
	public int runningState;
	public int riseState;
	public int fallState;
	public int pushState;
	public int surpriseState;
	public int isRunningBool;
	public int isRisingBool;
	public int isPushingBool;
	public int isGroundedBool;
	public int isClimbingBool;
	public int speedFloat;

	public Vector3 velocity;
	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	void Awake()
	{
		// Assigning HashIDs
		idleState = Animator.StringToHash("Base Layer.Idle");
		runningState = Animator.StringToHash("Base Layer.Run");
		riseState = Animator.StringToHash("Base Layer.Rise");
		fallState = Animator.StringToHash("Base Layer.Fall");
		pushState = Animator.StringToHash("Base Layer.Push");
		surpriseState = Animator.StringToHash("Base Layer.Surprise!");

		isRunningBool = Animator.StringToHash("isRunning");
		isRisingBool = Animator.StringToHash("isRising");
		isPushingBool = Animator.StringToHash("isPushing");
		isGroundedBool = Animator.StringToHash("isGrounded");
		isClimbingBool = Animator.StringToHash("isClimbing");

        speedFloat = Animator.StringToHash("Speed");
	}
	// --------------------------------------------------------------------
	public void SetVelocity(Vector3 value)
	{
		velocity = value.normalized;
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}
