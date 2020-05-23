using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Will fade text in if the player enters the collider, and out if they leave.
public class TutorialUIBehaviour : MonoBehaviour 
{
	// ----------------------------------------------- Data members ----------------------------------------------
	private SphereCollider sphereCollider;      // To trigger the fade-in.

	private float amplitude = 0.1f;             // For the bounce.
	private float period = 1f;                  // For the bounce.
    private Vector3 startPos;                   // For the bounce.

    public bool shouldBounce;                   // True if we want the text to move up and down.
    private bool activated;                     // true once the trigger has been activated. So we only activate it once.
    public bool onceOnly;                       // True if we only want to fade in and out once.
	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	// Use this for initialization
	void Start() 
	{
		sphereCollider = gameObject.GetComponent<SphereCollider>();
        startPos = transform.position;
       
        GetComponent<Text>().enabled = false;
        startPos = transform.position; 
    }
	// --------------------------------------------------------------------
	// Update is called once per frame
	void Update() 
	{
		if (shouldBounce)
		{
			Bounce();
		}
	}
	// --------------------------------------------------------------------
	// When Bip approaches the thing, make the thing fade in.
	public void OnTriggerEnter(Collider col)
	{
        if (!activated)
        {
            FadeIn();
            if (onceOnly)
            {
                activated = true;
            }
        }
    }
    // --------------------------------------------------------------------
    // When Bip walks away from the thing, make the thing fade out.
    public void OnTriggerExit(Collider col)
    {
        if (col.name == "Bip")
        {
            FadeOut();
        }
    }
    // --------------------------------------------------------------------
    public void FadeIn()
    {
        GetComponent<CanvasRenderer>().SetAlpha(0.01f);
        GetComponent<Text>().enabled = true;
        GetComponent<Text>().CrossFadeAlpha(1f, 0.9f, false);     
    }
    // --------------------------------------------------------------------
    public void FadeOut()
    {
        GetComponent<Text>().CrossFadeAlpha(0f, 0.9f, false);
    }
    // --------------------------------------------------------------------
    // Make the UI move up and down.
    public void Bounce()
    {
        float theta = Time.timeSinceLevelLoad / period;
        float distance = amplitude * Mathf.Sin(theta);
        transform.position = startPos + Vector3.up * distance;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
