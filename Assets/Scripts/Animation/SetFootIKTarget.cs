using UnityEngine; 

public class SetFootIKTarget : MonoBehaviour
{
    public bool Debugging;
    [Tooltip("The IK Solver for this limb.")]
    public IKSolver ikSolver;
    [Tooltip("What layers should the casts hit?")]
    public LayerMask layerMask;
    [Tooltip("The Offset for the SphereCast start position, on the Y Axis.")]
    public float castOffset;
    [Tooltip("The Length of the cast, along the relative Y axis.")]
    public float castLength;
    [Tooltip("The Radius of the SphereCast")]
    public float castRadius;
    
    void Update()
    {
        if (Debugging)
        {
            // If debugging is true, draw the rays
            Debug.DrawRay(transform.position + (-transform.up * castOffset), transform.up * castLength, Color.red);
        }

        if (Physics.SphereCast(transform.position + (-transform.up * castOffset), castRadius, transform.up, out RaycastHit hit, castLength, layerMask, QueryTriggerInteraction.Ignore))
        {
            // Spherecast from just above the foot, to just below the foot.
            // If it hits anything, the point that was hit is our foot IK target
            // We also ensure that IK is active.
            ikSolver.IsActive = true;
            ikSolver.SetTarget(hit.point);
        }
        else
        {
            // If the raycast does not hit anything, don't solve IK
            ikSolver.IsActive = false;
            ikSolver.SetTarget(Vector3.zero);
        }
    }
}