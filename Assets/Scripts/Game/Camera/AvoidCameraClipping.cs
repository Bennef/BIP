using UnityEngine;

public class AvoidCameraClipping : MonoBehaviour
{
    public LayerMask avoidanceLayers;       // Layers to move away from to avoid clipping.
	public float minDistance = 0.7f;        // The minimum distance from the player.
	public float smoothTime = 0.25f;        // The smoothing applied to camera movement.

	private float m_YOffsetRaycastPos;      // Offset in Y from the camera's position to the edge of the near clip plane.
	private float m_XOffsetRaycastPos;      // Offset in X from the camera's position to the edge of the near clip plane.
	private Vector3 m_OriginalPosition;     // The local position the camera should try to return to.
	private Vector3 m_Velocity;             // Reference velocity for smoothing.
    
    void Start()
	{
		// Calculate the offsets from the camera's position to the near clip plane.
		CalculateOffsets();

		// Store the original local position.
		m_OriginalPosition = transform.localPosition;
	}
    
    void CalculateOffsets()
	{
		// The Y offset is the opposite side of a right angled triangle with the near clip plane.
		m_YOffsetRaycastPos = Camera.main.nearClipPlane * Mathf.Tan (0.5f * Mathf.Deg2Rad * Camera.main.fieldOfView);

		// The X offset is relative to the Y offset.
		m_XOffsetRaycastPos = m_YOffsetRaycastPos * Camera.main.aspect;
	}
    
    Ray GetRay (bool top, bool right)
	{
		// Start the origin in the middle of the near clip plane.
		Vector3 origin = transform.position + Camera.main.nearClipPlane * transform.forward;

		// Move the origin either left or right by the X offset.
		origin += right ? transform.right * m_XOffsetRaycastPos : -transform.right * m_XOffsetRaycastPos;

		// Move the origin either up or down by the Y offset.
		origin += top ? transform.up * m_YOffsetRaycastPos : -transform.up * m_YOffsetRaycastPos;

		// Create and return the ray based on the origin and forward vector.
		Ray ray = new Ray(origin, transform.forward);
		return ray;
	}
    
    float DoubleRaycast (Ray ray, float rayLength)
	{
		// The new distance from the player is by default it's original position.
		float newDistance = Mathf.Abs(m_OriginalPosition.z);
		RaycastHit hit;

		// Raycast and if something is hit, adjust the new distance accordingly.
		if (Physics.Raycast (ray, out hit, rayLength, avoidanceLayers))
			newDistance -= hit.distance;

		// Put the ray's origin at the end of the ray and reverse the direction (raycast back along the ray).
		ray.origin = ray.origin + ray.direction * rayLength;
		ray.direction = -ray.direction;

		// Raycast and if there is another hit that is less than the new distance, adjust accordingly.
		if (Physics.Raycast (ray, out hit, rayLength, avoidanceLayers))
		if (hit.distance < newDistance)
			newDistance = hit.distance;

		// Return the new distance for the camera.
		return newDistance;
	}
    
    float FindNewDistance (float rayLength)
	{
		// Make sure that the default camera position is definitely the original distance.
		float newDistance = Mathf.Abs(m_OriginalPosition.z);

		// Double raycast from the top right and adjust the new distance is set accordingly.
		float raycastHitDistance = DoubleRaycast (GetRay (true, true), rayLength);
		if (raycastHitDistance < newDistance)
			newDistance = raycastHitDistance;

		// Double raycast from the top left...
		raycastHitDistance = DoubleRaycast(GetRay(true, false), rayLength);
		if (raycastHitDistance < newDistance)
			newDistance = raycastHitDistance;

		// The bottom right...
		raycastHitDistance = DoubleRaycast(GetRay(false, true), rayLength);
		if (raycastHitDistance < newDistance)
			newDistance = raycastHitDistance;

		// And the bottom left.
		raycastHitDistance = DoubleRaycast(GetRay(false, false), rayLength);
		if (raycastHitDistance < newDistance)
			newDistance = raycastHitDistance;

		// Return the adjusted new distance.
		return newDistance;
	}
    
    void LateUpdate()
	{
		// Find the new distance for the camera.
		float newDistance = FindNewDistance (Mathf.Abs(transform.localPosition.z));

		// Make sure the distance is between the minimum distance and the original.
		newDistance = Mathf.Clamp (newDistance, minDistance, -m_OriginalPosition.z);

		// Create a new position based on the new distance.
		Vector3 newLocalPos = m_OriginalPosition;
		newLocalPos.z = -newDistance;

		// Smoothly adjust the camera's position based on this new position.
		transform.localPosition = Vector3.SmoothDamp (transform.localPosition, newLocalPos, ref m_Velocity,
			smoothTime);
	}
}