using UnityEngine;
using System.Collections;

public abstract class CharacterAnimationController : MonoBehaviour 
{
	// ----------------------------------------------- Data members ----------------------------------------------
	// This class is an abstract class that stores everything that any Character Animation script should need.
	// The idea is that all Character Animation scripts inherit from this class, and only need to overwrite the implementation of Update.
	// The actual state setting methods are likely to be universal.
	protected Rigidbody rigidbody;
	protected Animator anim;
	protected PlayerStates state;
	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	// Use this for initialization
	void Awake() 
	{
		rigidbody = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator>();
		state = GetComponent<PlayerStates>();
	}
	// --------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
	{
		if(GameManager.Instance.isPaused)
        {
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
        }
	}
	// --------------------------------------------------------------------
	public void StartRunState()
	{
		anim.SetBool(state.isRunningBool, true);
	}
	// --------------------------------------------------------------------
	public void StopRunState()
	{
		anim.SetBool(state.isRunningBool, false);
	}
	// --------------------------------------------------------------------
	public void StartPushState()
	{
		anim.SetBool(state.isPushingBool, true);
	}
	// --------------------------------------------------------------------
	public void StopPushState()
	{
		anim.SetBool(state.isPushingBool, false);
	}
	// --------------------------------------------------------------------
	public void StartRiseState()
	{
		anim.SetBool(state.isRisingBool, true);
	}
	// --------------------------------------------------------------------
	public void StopRiseState()
	{
		anim.SetBool(state.isRisingBool, false);
	}
	// --------------------------------------------------------------------
	public void StartClimbState()
	{
		anim.SetBool(state.isClimbingBool, true);
	}
	// --------------------------------------------------------------------
	public void StopClimbState()
	{
		anim.SetBool(state.isClimbingBool, false);
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}
