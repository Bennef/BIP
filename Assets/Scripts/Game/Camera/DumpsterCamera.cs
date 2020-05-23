using UnityEngine;

public class DumpsterCamera : MonoBehaviour 
{
	// ----------------------------------------------- Data members ----------------------------------------------
	public Transform target;			// What the camera will be looking at
	public float distance = 5.0f;		// How far the camera is from the target
	public float xSpeed = 10.0f;		// Longtitudal speed of camera
	public float ySpeed = 10.0f;		// Latitudal speed of camera

	public float maxXRightBoundary;		// To stop the camera panning right too much.
	public float maxXleftBoundary;		// To stop the camera panning left too much.
	public float maxYTopBoundary;		// To stop the camera panning up too much.
	public float maxYBottomBoundary;	// To stop the camera panning down too much.
    public LayerMask layerMask;

    private Rigidbody rigidbody;		// Camera rigidbody, required because we're going to be (line)casting
	private Vector3 position;			// Local reference to position
	// ----------------------------------------------- End Data members -------------------------------------------

	// --------------------------------------------------- Methods ------------------------------------------------
	// --------------------------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		rigidbody = GetComponent<Rigidbody>();

		// Make the rigid body not change rotation
		if (rigidbody != null)
		{
			rigidbody.freezeRotation = true;
		}
	}
	// --------------------------------------------------------------------
	void LateUpdate () 
	{
		if (target)		// Does target exist? (Not Null)
		{
			//transform.position = position;
		}
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods ---------------------------------------------
}