using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public bool isFixed;                // True if player can move camera.
    public Transform target;			// What the camera will be looking at
    public Vector3 startingPosition;    // The position of the camera when Bip starts a level.
	public float distance = 10.0f;		// How far the camera is from the target
    public float xSpeed = 10.0f;		// Longtitudal speed of camera
	public float ySpeed = 10.0f;		// Latitudal speed of camera

	public float yMinLimit = 10f;       // Clamp the y angle of the camera.
    public float yMaxLimit = 80f;       // Clamp the y angle of the camera.

    public float xMinLimit = -360f;     // Clamp the x angle of the camera.
    public float xMaxLimit = 360f;      // Clamp the x angle of the camera.

    public float distanceMin = 0.5f;    // Minimum distance camera can be from target
	public float distanceMax = 10f;		// Maximum distance camera can be from target

	public float thinRadius = 0.15f;
    public float thickRadius = 0.3f;
	public LayerMask layerMask;

	private Quaternion rotation;		// Local reference to rotation
	private Vector3 position;            // Local reference to position
    private float x = 0.0f;             // x angle of camera
    private float y = 0.0f;				// y angle of camera
	private bool isColliding;

    //public GameObject bipMesh;          // A reference to Bip's mesh GameObject.
    //public Material bipSkin;            // The value of the mesh renderer for Bip.
    public CharacterController charController;
    public bool shouldReset = true;            // True if the camera should reset it's position.
    // ----------------------------------------------- End Data members -------------------------------------------

    // --------------------------------------------------- Methods ------------------------------------------------
    // --------------------------------------------------------------------
    // Use this for initialization
    void Start() 
	{
        target = GameObject.Find("Camera Target").transform;
        charController = GameObject.Find("Bip").GetComponent<CharacterController>();
        // Get a reference to Bip's meshRenderer so we can make him transparent.
        //bipSkin = bipMesh.GetComponent<SkinnedMeshRenderer>().material; //Debug.Log(bipSkin);
        //bipSkin = 0f;
        //bipSkin.EnableKeyword("_ALPHABLEND_ON");
        //Debug.Log(bipSkin);
        // Going to use default Unity functionality for this.
        Vector3 angles = this.transform.eulerAngles;
		x = angles.y;
		y = angles.x;
	}
	// --------------------------------------------------------------------
	void LateUpdate() 
	{
		if (target && !GameManager.Instance.isPaused && !charController.isDead && !isFixed)		// Does target exist? (Not Null)
		{
            // Move the camera using mouse or joystick.
            CameraMove();
            rotation = Quaternion.Euler(y, x, 0);   // Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).
            
			// Zoom out if we're not at maximum zoom distance.
			if (distance < distanceMax && isColliding == false)
			{
				distance = Mathf.Lerp(distance, distanceMax, Time.deltaTime * 2f);
			}
			// We'll declare a new Vector3 storing -distance. This will be 0 if we can see Bip, so nothing will happen.
			// However, if we can't see Bip, position.z will be equal to distance * -1, flipping it.
			Vector3 distanceVector = new Vector3(0.0f, 0.0f, -distance);
			// Camera follows target
			Vector3 position = rotation * distanceVector + target.position;
			transform.rotation = rotation;
			transform.position = position;

            // Stop player from pushing Camera through walls.
            CameraCollision();

            // Make Bip transparent if camera is too close.
            //ApplyTransparencyToBip();
		}

        // If we are loading a checkpoint, reset the position here...  
        if (shouldReset && GameManager.Instance.currentCheckpoint != null)
        {
            x = GameManager.Instance.currentCheckpoint.cameraStartX;
            y = GameManager.Instance.currentCheckpoint.cameraStartY;
            shouldReset = false;
        }
	}
    // --------------------------------------------------------------------
    public void CameraMove()
    {
        // Offset the angles by the mouse, when the mouse is moved.
        x += Input.GetAxis("Mouse X") * xSpeed;
        y -= Input.GetAxis("Mouse Y") * ySpeed;
        x += Input.GetAxis("Pad X") * xSpeed * 0.15f;
        y -= Input.GetAxis("Pad Y") * ySpeed * 0.15f;

        x = ClampAngle(x, xMinLimit, xMaxLimit);
        y = ClampAngle(y, yMinLimit, yMaxLimit);
    }
	// --------------------------------------------------------------------
	public float ClampAngle(float angle, float min, float max)
	{
		// Ensure that angle is between -360 and 360, because it is a float
		if (angle < -360F)
		{
			angle += 360F;
		}

		if (angle > 360F)
		{
			angle -= 360F;
		}
		// Then call Mathf.Clamp to actually clamp the angle.
		return Mathf.Clamp(angle, min, max);
	}
	// -------------------------------------------------------------------
	void CameraCollision()
	{
		Vector3 normal, thickNormal;							// Normal of the cast collisions.
		Vector3 occRay = transform.position - target.position;	// Direction for the SphereCasts.

		// The position of the thin SphereCast collision.
		Vector3 colPoint = GetCollisionSimple(transform.position, thinRadius, out normal, true);
		// The position of the thick SphereCast collision.
		Vector3 colPointThick = GetCollisionSimple(transform.position, thickRadius, out thickNormal, false);
		// The position of the RayCast collision.
		Vector3 colPointRay = GetCollision(transform.position);

		// Collision Position from thick SphereCast, projected onto the RayCast.
		Vector3 colPointThickProjectedOnRay = Vector3.Project(colPointThick - target.position, occRay.normalized) + target.position;
		// Direction to push the camera.
		Vector3 vecToProjected = (colPointThickProjectedOnRay - colPointThick).normalized;
		// Thick Collision Position projected onto thin SphereCast.
		Vector3 colPointThickProjectedOnThinCapsule = colPointThickProjectedOnRay - vecToProjected * thinRadius;
		// Distance between thick sphere and thin sphere collisions. Used to calculate where to push Camera.
		float thin2ThickDist = Vector3.Distance(colPointThickProjectedOnThinCapsule, colPointThick);
		float thin2ThickDistNorm = thin2ThickDist / (thickRadius - thinRadius);

		// Distance between target and thin sphere collision.
		float currentColDistThin = Vector3.Distance (target.position, colPoint);
		// Distance between target and Thick sphere collision.
		float currentColDistThick = Vector3.Distance (target.position, colPointThickProjectedOnRay);
		// Smoothly interpolating between distance and new distance.
		float currentColDist = Mathf.Lerp (currentColDistThick, currentColDistThin, thin2ThickDistNorm);

		// Thick point can be actually projected IN FRONT of the character due to double projection to avoid sphere moving through the walls
		// In this case we should only use thin point
		bool isThickPointIncorrect = transform.InverseTransformDirection(colPointThick - target.position).z > 0;
		isThickPointIncorrect = isThickPointIncorrect || (currentColDistThin < currentColDistThick);
		if (isThickPointIncorrect) 
		{
			currentColDist = currentColDistThin;
		}

		// if currentColDist is smaller than distance, zoom in
		if (currentColDist < distance) 
		{
			distance = currentColDist;
		}
		else
		{
			// Otherwise, zoom out.
			distance = Mathf.SmoothStep (distance, currentColDist, Time.deltaTime * 100 * Mathf.Max (distance * 0.1f, 0.1f));
		}
		// Clamp distance to our min and max values.
        distance = Mathf.Clamp(distance, distanceMin, distanceMax);
        // Move the camera to avoid going through objects!!!!
        transform.position = target.position + occRay.normalized * distance;
       	
        if (Vector3.Distance(target.position, colPoint) > Vector3.Distance(target.position, colPointRay))
        {
			transform.position = colPointRay;
        } 
    }
	// -------------------------------------------------------------------
    Vector3 GetCollisionSimple(Vector3 cameraOptPos, float radius, out Vector3 normal, bool pushByNormal)
    {
		// Double Sphere Casting.
		// Length of the cast.
        float farEnough = 1;

        RaycastHit occHit;
        // Local reference to target.position
        Vector3 origin = target.position;
        // Cast direction.
        Vector3 occRay = origin - cameraOptPos;
        // Dot product of transform.forward, and the ray.
        float dt = Vector3.Dot(transform.forward, occRay);
        if (dt < 0)
        {
			occRay *= -1;
        }
       
        // Project the sphere in an opposite direction of the desired character->camera vector to get some space for the real spherecast
        if (Physics.SphereCast(origin, radius, occRay.normalized, out occHit, farEnough, layerMask))
        {
            origin = origin + occRay.normalized * occHit.distance;
        }
        else
        {
            origin += occRay.normalized * farEnough;
        }
       
        // Do final spherecast with offset origin
        occRay = origin - cameraOptPos;
        if (Physics.SphereCast(origin, radius, -occRay.normalized, out occHit, occRay.magnitude, layerMask))
        {
            normal = occHit.normal;

            if (pushByNormal)
            {
				return occHit.point + occHit.normal*radius;
            }
            else
            {
				return occHit.point;
            }
        }
        else
        {
            normal = Vector3.zero;
            return cameraOptPos;
        }
    }
	// -------------------------------------------------------------------
    Vector3 GetCollision(Vector3 cameraOptPos)
    {
		// Local reference to target.position
        Vector3 origin = target.position;
        // Direction for raycast
        Vector3 occRay = cameraOptPos - origin;
       
        RaycastHit hit;
        if (Physics.Raycast(origin, occRay.normalized, out hit, occRay.magnitude, layerMask))
        {
			// Return position of hit + the normal of that hit, multiplied by a smoothing variable.
            return hit.point + hit.normal * 0.15f;
        }
       	// or we just return the camera position we passed in.
        return cameraOptPos;
    }
    // -------------------------------------------------------------------
    void ApplyTransparencyToBip()
    {
        if (distance < 7)
        {
            
        }
    }
    // -------------------------------------------------------------------
    // --------------------------------------------------- End Methods ---------------------------------------------
}