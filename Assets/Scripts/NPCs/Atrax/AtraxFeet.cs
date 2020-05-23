using UnityEngine;

public class AtraxFeet : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    AudioSource aSrc;
    public Transform sparkPrefab;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        aSrc = GetComponent<AudioSource>();	
	}
    // --------------------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        // This is for making sparks appear when Atrax hits the floor.
        //if (collision.gameObject.name == "Hex Floor")
        //{
            //aSrc.Play();
        //}

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(sparkPrefab, pos, rot);
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
