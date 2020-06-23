using UnityEngine;

public abstract class Mind : MonoBehaviour
{
    [Tooltip("Movement Speed")]
    public float speed;				    // Movement speed.
    [Tooltip("The location this Actor will move towards")]
    public Transform target;			// What the actor will follow/look at.
    [Tooltip("How far Actor will stay away from obstacles")]
    public float ObstacleDistance;
    [Tooltip("Should the Actor move towards it's target?")]
    public bool shouldFollow;            // True if the actor is following Bip.
    public float currentDistance;
    [Tooltip("Preferred distance from target, will try to maintain this.")]
    public float PreferredDistance;     // The distance the actor will stop moving towards Bip at.
    [Tooltip("Minimum distance from target, won't get closer than this.")]
    public float MinDistance;           // Min Distance from Target.
    [Tooltip("Offset for Raycast origin. Rays will be cast from transform.position + raycastOffset")]
    public Vector3 raycastOffset;
    
    // Calculate the rotation that Actor should turn.
    protected virtual Quaternion CalculateRotation()
    {
        // Return the new rotation for the actor.
        return Quaternion.LookRotation(CalculateLookAtDirection((target.position - transform.position).normalized));
    }
    
    protected virtual Quaternion CalculateRotationOnlyY()
    {
        // Store a temp position so that the actor will only take into account horizontal distance between itself and Bip.
        Vector3 tempPosition = target.position;
        tempPosition.y = transform.position.y;
        Vector3 direction = CalculateLookAtDirection((tempPosition - transform.position).normalized);

        // Return the new rotation for the actor.
        return Quaternion.LookRotation(direction);
    }
    
    protected virtual Vector3 CalculateLookAtDirection(Vector3 direction)
    {
        if (shouldFollow)
        {
            // Build the positions for left/right ray origin point.
            Vector3 leftR = transform.forward - (transform.right);
            Vector3 rightR = transform.forward + (transform.right);

            if (Physics.Raycast(transform.position + raycastOffset, leftR, out RaycastHit hit, ObstacleDistance, GameManager.Instance.ObstacleAvoidanceLayerMask, QueryTriggerInteraction.Ignore)
                && Physics.Raycast(transform.position + raycastOffset, rightR, out hit, ObstacleDistance, GameManager.Instance.ObstacleAvoidanceLayerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform != transform)
                    return direction;
            }

            // Check for forward raycast.
            Debug.DrawRay(transform.position + raycastOffset, transform.forward * ObstacleDistance, Color.yellow);
            if (Physics.Raycast(transform.position + raycastOffset, transform.forward, out hit, ObstacleDistance, GameManager.Instance.ObstacleAvoidanceLayerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform != transform)
                    direction += hit.normal * 50;
            }
            // Check for left and right raycast.
            Debug.DrawRay(transform.position + raycastOffset, leftR * ObstacleDistance, Color.red);
            Debug.DrawRay(transform.position + raycastOffset, rightR * ObstacleDistance, Color.red);
            if (Physics.Raycast(transform.position + raycastOffset, leftR, out hit, ObstacleDistance, GameManager.Instance.ObstacleAvoidanceLayerMask, QueryTriggerInteraction.Ignore)
                || Physics.Raycast(transform.position + raycastOffset, rightR, out hit, ObstacleDistance, GameManager.Instance.ObstacleAvoidanceLayerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform != transform)
                {
                    // If Raycast hit something in the environment, rotate by the exact amount needed to avoid the colliding object.
                    direction += hit.normal * ObstacleDistance;
                }
            }
        }
        return direction;
    }
    
    protected virtual void Move()
    {
        // Handle locomotion in this method. Needs overriding.
    }   
}