using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIAnimationTriggerScript : MonoBehaviour 
{
	// ----------------------------------------------- Data members ----------------------------------------------
	public GameObject interactWithMeText;
	Animator textAnimation;

	public GameObject bip;

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		textAnimation = interactWithMeText.GetComponent<Animator>();
		bip = GameObject.FindGameObjectWithTag("Player");
	}
	// --------------------------------------------------------------------
	// Triggers Animation when Bip enters
	void OnTriggerEnter (Collider collider) 
	{
		if(collider.gameObject == bip)
		{
			textAnimation.enabled = true;
			textAnimation.Play("InteractWithMe");
		}
	}
	// --------------------------------------------------------------------
	// Reverses annimation when Bip leaves Trigger
	void OnTriggerExit (Collider collider1) 
	{
		if(collider1.gameObject == bip)
		{
			textAnimation.Play("ReverseInteracctWithMe");
		}
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
