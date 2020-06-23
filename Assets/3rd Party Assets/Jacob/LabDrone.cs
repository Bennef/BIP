using UnityEngine;

public class LabDrone : MonoBehaviour 
{
	public Transform target;
	public Vector3 newtarget;
    public float bipHeadOffset;
    public float hSpeed;                // Speed when chasing.
    public float vSpeed;                // Speed of hovering.
    public float amplitude;             // Height difference of hovering.
    public float damping = 5.0f;        // For chasing.
    public Vector3 startPosition;       // Start position of drone.
    public Vector3 tempPosition;		// For hovering.
        
    // Use this for initialization
    void Update() 
    {
		newtarget = new Vector3 (target.position.x, target.position.y + bipHeadOffset, target.position.z);
		// Rotate the camera every frame so it keeps looking at the target 
		transform.LookAt(newtarget);
	}
    
    void FixedUpdate()
    {
        // Hover in current position.
        tempPosition.y = (Mathf.Sin(Time.realtimeSinceStartup * vSpeed) * amplitude) + 18;
        tempPosition.x = transform.position.x;
        tempPosition.z = transform.position.z;
        transform.position = tempPosition;
    }
}