using UnityEngine;

// Needs editing, only very basic starting moving values, clipping points and route behaviour - More to be added : Sam
public class PathingRoutes : MonoBehaviour 
{    
    public Transform[] wayPoints;					// Group of waypoints transforms the camera paths through.
	public bool toggleLerpSlerp = true;				// How the camera moves towards its next waypoint.
	public float waypointSpeed = 0.5f;				// How fast the camera moves towards waypoints.
	public float clipRange = 140.0f;				// The cutoff distance for pathing when aproaching waypoints.

	private PlayerMovement playerMovement;		    // Reference to PlayerMovementBip scripte so we have access to enable/disable it.
	private CameraMovement cameraMovement;			// Reference to the CameraMovement script so we have access to enable/disable it.
	private Transform player;						// Reference of player object.

	private float[] clippingSpeed = new float[2];
	private bool cameraPathing = false;				// If the camera is currently pathing and can't be controlled by the player.

	// Temp Variable	- Delete later...
	private int wayPointID = 0;				        // Holds the current path point in the array.  - Temp fixed at 0
	private bool tempHolder = true;                 //@EDIT: Change to wayPoints.Length == LastWayPointInPath
    
    // Setting Object References
    void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerMovement = player.GetComponent<PlayerMovement>();
		cameraMovement = GetComponentInParent<CameraMovement>();

		// Disable camera and player movement
		cameraMovement.enabled = false;
		playerMovement.enabled = false;
	}
    
    // Use this for initialization
    void Start() 
	{
		// Check population of waypoints.
		if (wayPoints.Length == 0) 
		{
			cameraPathing = false;
			cameraMovement.enabled = true;
			playerMovement.enabled = true;
			Debug.Log ("No Pathing Option...");
		} 
		else
		{
			// Last waypoint looks at Bip
			wayPoints [wayPoints.Length - 1].LookAt (player.position);
			transform.LookAt(wayPoints[wayPointID].position);
			cameraPathing = true;
			Debug.Log ("Started Pathing...");
		}
	}
    
    // Update is called once per frame
    void FixedUpdate() 
	{
		if (cameraPathing) 
		{
			// Update path position...
			cameraPathing = !StartPathing();

			// Check if finished...
			if (!cameraPathing)
			{
				Debug.Log ("Finished Pathing...");
				cameraMovement.enabled = true;
				playerMovement.enabled = true;
			}
		}
	}
    
    bool StartPathing(/*Array of waypoints*/)
	{
		// Check if last waypoint...
		if (tempHolder)
		{
			// Grab distance from next waypoint
			float dist = (transform.position - wayPoints[wayPointID].position).sqrMagnitude;

			// Checking waypoint reached...
			if (dist == 0) 
			{
				tempHolder = false;
				// Update to next waypoint
				//wayPointPlaceHolder = nextWayPoint;
				
				// Checking clip range reached...
			} 
			else if (dist > clipRange) 
			{
				// Transform position and rotations...
				if (toggleLerpSlerp)
					transform.position = Vector3.Lerp (transform.position, wayPoints[wayPointID].position, waypointSpeed * Time.deltaTime);		// Move towards the waypoint.
				else
					transform.position = Vector3.Slerp (transform.position, wayPoints[wayPointID].position, waypointSpeed * Time.deltaTime);	// Move towards the waypoint.
				transform.rotation = Quaternion.Lerp (transform.rotation, wayPoints[wayPointID].rotation, waypointSpeed * Time.deltaTime);		// Rotate towards waypoints forward vector.
				dist = (transform.position - wayPoints[wayPointID].position).sqrMagnitude;
				
				// Checking clip range reached after transforms, and storing speed values...
				if (dist < clipRange) {
					Debug.Log ("Storing Clip Speeds...");
					clippingSpeed[0] = Vector3.Distance(transform.position, wayPoints[wayPointID].position) * waypointSpeed * Time.deltaTime;
					clippingSpeed[1] = Quaternion.Angle(transform.rotation, wayPoints[wayPointID].rotation) * waypointSpeed * Time.deltaTime;
					Debug.Log(clippingSpeed[0]);
					Debug.Log(clippingSpeed[1]);
				}
				// Clip range reached: Transform position and rotation by stored speed values...
			}
			else 
			{
				//@CHECK: May only need to use transform.Rotate() and transform.Translate() here...
				transform.position = Vector3.MoveTowards(transform.position, wayPoints[wayPointID].position, clippingSpeed[0]);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, wayPoints[wayPointID].rotation, clippingSpeed[1]);
			}
		}
		return !tempHolder; 
	}
}