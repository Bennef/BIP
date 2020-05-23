using UnityEngine;

public class LockableDoors : MonoBehaviour 
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public float doorSpeed;                 // How quickly the inner doors will track the outer doors.

	public Transform leftDoor;              // Reference to the transform of the left door.
	public Transform rightDoor;             // Reference to the transform of the right door.
    public Transform leftTarget;            // The open position of the left door.
    public Transform rightTarget;           // The open position of the right door.

    private Vector3 leftClosedPos;          // The initial x component of position of the left doors.
	private Vector3 rightClosedPos;         // The initial x component of position of the right doors.

    public bool opening;                    // True when opening.
	public bool closing;                    // True when closing.
    public bool locked;                     // True when locked. If door is locked, it cannot be opened.

    public BoxCollider doorCollider;        // The door collider, we want to disable this when open.

    private Light[] doorLights;             // The lights will be red when locked and green when unlocked.

    private AudioSource aSrc;
    public AudioClip openSound, closeSound, lockedSound, unlockedSound;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Awake()
	{
		// Setting the closed x position of the doors.
		leftClosedPos = leftDoor.position;
		rightClosedPos = rightDoor.position;
        doorLights = GetComponentsInChildren<Light>();
        aSrc = GetComponent<AudioSource>();

        foreach (Light doorLight in doorLights)
        {
            if (locked)
            {
                doorLight.color = Color.red;
            }
            else
            {
                doorLight.color = Color.green;
            }
        }
    }
    // --------------------------------------------------------------------
    void Update()
	{
        // If the door is unlocked...
        if (!locked)
        {
            if (opening)
            {
                OpenDoors();
            }
            else if (closing)
            {
                CloseDoors();
            }
        }
	}
    // --------------------------------------------------------------------
    void MoveDoors(Vector3 newLeftTarget, Vector3 newRightTarget)
	{
		// Create a float that is a proportion of the distance from the left inner door's x position to it's target x position.
		Vector3 newPos = Vector3.Lerp(leftDoor.position, newLeftTarget, doorSpeed * Time.deltaTime);

		// Move the left inner door to it's new position proportionally closer to it's target.
		leftDoor.position = newPos;

		// Reassign the float for the right door's x position.
		newPos = Vector3.Lerp(rightDoor.position, newRightTarget, doorSpeed * Time.deltaTime);

		// Move the right inner door similarly.
		rightDoor.position = newPos;
	}
    // --------------------------------------------------------------------
    public void OpenDoors()
	{
		// Move the inner doors towards the outer doors.
		MoveDoors(leftTarget.position, rightTarget.position);
        
        // Disable the box collider.
        doorCollider.enabled = false;
    }
    // --------------------------------------------------------------------
    public void CloseDoors()
	{
		// Move the inner doors towards their closed position.
		MoveDoors(leftClosedPos, rightClosedPos);
        
        // Enable the box collider.
        doorCollider.enabled = true;
    }
    // --------------------------------------------------------------------
    public void UnlockDoor()
    {
        locked = false;
        
        // Lights are green.
        foreach (Light doorLight in doorLights)
        {
            doorLight.color = Color.green;
        }

        // Play unlocked sound.    
        if (!aSrc.isPlaying)
        {
            aSrc.PlayOneShot(unlockedSound);
        }
    }
    // --------------------------------------------------------------------
    public void LockDoor()
    {
        locked = true;

        // Lights are red.
        foreach (Light doorLight in doorLights)
        {
            doorLight.color = Color.red;
        }

        // Play locked sound.
        aSrc.PlayOneShot(lockedSound);
    }
    // --------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
	{
        if (other.name == "Collider front" && !locked)
        {
            closing = false;
			opening = true;

            // Play opening sound.       
            if (!aSrc.isPlaying) 
            {
                aSrc.PlayOneShot(openSound);
            }
        }
	}
    // --------------------------------------------------------------------
    void OnTriggerExit(Collider other)
	{
		if (other.name == "Collider front" && !locked)
		{
			opening = false;
			closing = true;

            // Play closing sound.
            if (!aSrc.isPlaying)
            {
                aSrc.PlayOneShot(closeSound); 
            }
        }
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
