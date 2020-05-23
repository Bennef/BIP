using UnityEngine;

public class FanForce : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public float force;                  // The force of the fan.
    public bool isOn;                    // True when the fan is on.
    public RotatingObject fanBlades;     // THe fan blades' rotation script so we can stop the rotation.
    public AudioSource aSrc;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Start()
    {
        aSrc = GetComponent<AudioSource>();
    }
    // --------------------------------------------------------------------
    void Update()
    {
        // If the fan is on...
        if (isOn)
        {
            // Rotate the blades.
            fanBlades.shouldRotate = true;

            // If the fan sound is not already playing...
            if (!aSrc.isPlaying)
            {
                // Play the fan sound.
                aSrc.Play();
            }
        }
        else
        {
            // Stop the blades.
            fanBlades.shouldRotate = false;

            // If the fan sound is playing...
            if (!aSrc.isPlaying)
            {
                // Stop the fan sound.
                aSrc.Stop();
            }
        }
    }
    // --------------------------------------------------------------------
    void OnTriggerStay(Collider col)
    {
        // If the player is stepping on this pad or a moveable object is put on it.
        if (isOn && col.gameObject.GetComponent<Rigidbody>() != null)
        {
            // Add the force.
            if (col.gameObject.tag == "Zorb")
            {
                col.transform.GetComponent<Rigidbody>().AddForce((transform.forward * force * Time.deltaTime / 40));
            }
            else
            {
                col.transform.GetComponent<Rigidbody>().AddForce((transform.forward * force * Time.deltaTime));
            }
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
