using UnityEngine;

public class OverheadUITrigger : MonoBehaviour 
{
    // ----------------------------------------------- Data members ----------------------------------------------
    private OverheadUI prompt;      // The panel above Bip's head.
    public float displayTime;       // The time that temporary overhead UI is displayed for in seconds.
    public Sprite sprite;           // The actual sprite we want to display.
    public bool hasHappened;        // True once the UI has appeared once.
    public AudioSource aSrc;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    private void Awake()
    {
        prompt = GameObject.Find("Panel").GetComponent<OverheadUI>();
        aSrc = GameObject.Find("CoBot").GetComponent<AudioSource>();
    }
    // --------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
	{
        if (hasHappened == false)
        {
            if (other.tag == "Player")
            {
                if (prompt != null)
                {
                    aSrc.Play();
                    // Make the prompt appear for x seconds.
                    prompt.ShowUI(sprite, displayTime);
                    hasHappened = true;
                }
            }
        }
	}
    // --------------------------------------------------------------------
    public void OnCollisionEnter(Collision collision)
    {
        if (hasHappened == false)
        {
            if (collision.collider.gameObject.tag == "Player")
            {
                if (prompt != null)
                {
                    // Make the prompt appear for x seconds.
                    prompt.ShowUI(sprite, displayTime);
                    hasHappened = true;
                }
            }
        }
    }
    // --------------------------------------------------- End Methods --------------------------------------------
}
