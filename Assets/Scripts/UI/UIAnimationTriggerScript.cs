using UnityEngine;

public class UIAnimationTriggerScript : MonoBehaviour 
{
	public GameObject interactWithMeText;
	Animator textAnimation;

	public GameObject bip;

	// Use this for initialization
	void Start() 
	{
		textAnimation = interactWithMeText.GetComponent<Animator>();
		bip = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Triggers Animation when Bip enters
	void OnTriggerEnter (Collider collider) 
	{
		if (collider.gameObject == bip)
		{
			textAnimation.enabled = true;
			textAnimation.Play("InteractWithMe");
		}
	}
	
	// Reverses annimation when Bip leaves Trigger
	void OnTriggerExit (Collider collider) 
	{
		if (collider.gameObject == bip)
			textAnimation.Play("ReverseInteracctWithMe");
	}
}