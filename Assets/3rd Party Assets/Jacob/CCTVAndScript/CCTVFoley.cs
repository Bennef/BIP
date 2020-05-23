using UnityEngine;

public class CCTVFoley : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public Ray sight;
	public GameObject Target;
	public bool RayBool;
	public bool ToggleBool;
	public bool AudioToggle;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Update()
	{
		sight.origin = new Vector3 (transform.position.x,transform.position.y,transform.position.z);
		RaycastHit rayHit;

		Vector3 playerheight = new Vector3 (Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);
		sight.direction = transform.forward;

		RaycastHit rayhit;

		Vector3 rayDirection = playerheight - sight.origin;

		Quaternion rot = Quaternion.LookRotation (rayDirection);

		float angle = Vector3.Angle (rayDirection, transform.forward);

		if (ToggleBool == true)
        {
			if (AudioToggle == true)
            {
				AudioToggle = false;
				GetComponent<AudioSource>().Stop();
			}
			//Debug.DrawLine (sight.origin, playerheight, Color.blue);  // Draw line for debugging.
		}
        
		if (ToggleBool == false)
        {
			if (AudioToggle == false)
            {
				AudioToggle = true;
				GetComponent<AudioSource>().Play();
			}

			if (angle < 30 * 0.5)
            {
				Debug.DrawLine (sight.origin, playerheight, Color.red);
                transform.rotation = Quaternion.Slerp (transform.rotation, rot, 2 * Time.deltaTime);
			}

			if (angle >= 30 * 0.5)
            {
				Debug.DrawLine (sight.origin, playerheight, Color.green);
				transform.rotation = Quaternion.Slerp (transform.rotation, rot, 2 * Time.deltaTime);
			}
		}

		if (angle < 10 * 0.5f)
        {
			ToggleBool = true;
		}
        
		if (angle > 30 * 0.5f)
        {
			ToggleBool = false;
		}
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
