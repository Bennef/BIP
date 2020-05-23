using UnityEngine;

public class DemoEndCam : MonoBehaviour 
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public GameObject target;
    public float smoothing = 8.0f;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    public void Update()
    {
        transform.LookAt(target.transform.position, Vector3.up);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * smoothing);
    }
    // --------------------------------------------------------------------
    public void SetPosition()
	{
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        transform.position = new Vector3(-251, 24, -126);  // Position camera.
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}