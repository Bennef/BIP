using UnityEngine;

public class LookAtMe : MonoBehaviour {
    // ----------------------------------------------- Data members ----------------------------------------------
    public Transform head;
    public Transform labHead;
    public bool shouldLook;
    public float speed;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
	// LateUpdate is called once per frame after Update.
	void LateUpdate()
    {
        if (shouldLook)
        {
           // Vector3 direction = transform.position - head.position;
           // Quaternion toRotation = Quaternion.FromToRotation(direction, head.up);
           //head.localRotation = Quaternion.Lerp(head.localRotation, toRotation, speed * Time.time);
           // Vector3 direction2 = head.up;
           // Debug.DrawRay(head.position, direction2, Color.green);
           // //head.LookAt(transform.position);
            //head.rotation = Quaternion.LookRotation(transform.position - head.position);
        }
    }
    // --------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Vision Cone")
        {
            shouldLook = true;
        }
    }
    // --------------------------------------------------------------------
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Vision Cone")
        {
            shouldLook = false;
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
