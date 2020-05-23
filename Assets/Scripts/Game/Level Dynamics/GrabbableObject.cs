using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
	// ----------------------------------------------- Data members ----------------------------------------------
	public bool isGrabbable;			// True if Bip is able to grab this object.
	public bool isGrabbing = false;		// True if Bip is already grabbing the object.
    private Rigidbody grabbableRigidbody;
    private Rigidbody player;           // The rigibody of the player.
    public LayerMask layerMask;			// To allow only the player to grab the object.

	public Vector3 closestGrabPoint;	// To store the grab point closest to Bip.

    public float grabbingMass;          // To change when object is grabbed.

    public Vector3 startingPosition;    // So we can reset the cubes position on death.
    private CharacterController charController;

    public GrabbableObject[] otherCubes;
    public CapsuleCollider headCollider;  // Disable this when we grab.
    public Animator bipAnim;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Awake()
	{
		grabbableRigidbody = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        charController = GameObject.Find("Bip").GetComponent<CharacterController>();
        headCollider = GameObject.Find("CTRL_Head").GetComponent<CapsuleCollider>();
        bipAnim = GameObject.Find("Bip").GetComponent<Animator>();
        player = GameObject.Find("Bip").GetComponent<Rigidbody>();
    }
	// --------------------------------------------------------------------
	void Update()
    { 
		if (Input.GetButtonDown("Grab"))
		{
			if (isGrabbable && !isGrabbing)
			{
                headCollider.enabled = false;
                
                // For each other cube...
                foreach (GrabbableObject cube in otherCubes)
                {
                    cube.isGrabbable = false;  // ... make sure Bip cannot grab it.
                }
                Grab(player);
			}
		}

		if (Input.GetButtonUp("Grab"))
		{
			LetGo();
            headCollider.enabled = true;
        }

		if (player)
		{
			if (!player.GetComponent<Animator>().GetBool(player.GetComponent<PlayerStates>().isGroundedBool) && isGrabbing)
			{
				LetGo(); 
            }
		}
  	}
	// --------------------------------------------------------------------
	public void Grab(Rigidbody player)
	{
        if (!isGrabbing) 
		{
			isGrabbing = true;
            grabbableRigidbody.mass = grabbingMass;
            GetClosestGrabPoint();

			// Move Bip to the remaining stored point as it is the closest one to him.
			player.transform.position = closestGrabPoint;
            
            // Make Bip look at the centre of the object.
            Vector3 targetPosition = new Vector3(transform.position.x, player.position.y ,transform.position.z);
			player.transform.LookAt(targetPosition);

			// When grabbed, add a FixedJoint component to this object.
			this.gameObject.AddComponent<FixedJoint>();
			/* Then we connect this joint to the player's rigidbody.
			This connects this objects movement to that of the players, this goes both ways
			so this object can also pull the player if it moves due to something else.
			*/
			this.gameObject.GetComponent<FixedJoint>().connectedBody = player;
            player.velocity = Vector3.zero;
			player.gameObject.GetComponent<Animator>().SetBool(player.gameObject.GetComponent<PlayerStates>().isPushingBool, true);
		}
	}
    // --------------------------------------------------------------------
    public Vector3 GetClosestGrabPoint()
    {
        float smallestdistance = 1000;  // The smallest distance between Bip and Grabbable points around the object. Initialise to a high number.
        float currentDistance;          // The distance from Bip to the current point.

        // Loop through all the children in the object. They are grabbable points that Bip can hold on to.
        foreach (Transform point in transform)
        {
            // Measure the distance between Bip and the point.
            currentDistance = Vector3.Distance(player.transform.position, point.transform.position);

            // If the current point distance is smaller than the current one then overwrite the variable with it.
            if (currentDistance < smallestdistance)
            {
                smallestdistance = currentDistance;             // Store this current distance in the smallest distance variable.
                closestGrabPoint = point.transform.position;    // Store the Vector3 point location.
            }
        }
        return closestGrabPoint;
    }
    // --------------------------------------------------------------------
    public void LetGo()
	{
		isGrabbing = false;
        grabbableRigidbody.mass = 1000;

        // When this object isn't being grabbed, we destroy the joint.
        if (this.gameObject.GetComponent<FixedJoint>())
		{
			Destroy(this.gameObject.GetComponent<FixedJoint>());
		}

		// Player control shit.
		if (player)
		{
			player.gameObject.GetComponent<Animator>().SetBool(player.gameObject.GetComponent<PlayerStates>().isPushingBool, false);
		}
        //isGrabbable = true; // not sure if we need this
    }
	// --------------------------------------------------------------------
	void OnTriggerStay(Collider other)
	{
		// If we collide with the player.
		if (other.gameObject.tag == "Player" && otherCubes.Length > 0)
		{
            foreach (GrabbableObject cube in otherCubes)
            {
                if (cube.isGrabbable)
                {
                    cube.isGrabbable = false;
                }
            }
		}
	}
    // --------------------------------------------------------------------
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log(other.name);
            isGrabbable = true;
        }
    }
    // --------------------------------------------------------------------
    public void OnTriggerExit(Collider other)
	{
        // If we collide with the player.
        //if (other.gameObject.name == "CTRL_Head")
        //{
			isGrabbable = false;
		//}
	}
	// --------------------------------------------------------------------
    public void ResetPosition()
    {
        transform.position = startingPosition;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
