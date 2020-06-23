using UnityEngine;

public class AtraxSetFootIKTarget : SetFootIKTarget
{
    [Tooltip("How high should this limb be allowed to go from the ground if it is being animated?")]
    public float animationOffset;
    [Tooltip("The starting frame of the foot step, when the foot leaves the ground.")]
    public int startFrame;
    [Tooltip("The last frame of the foot step, when the foot touches the ground again.")]
    public int endFrame;
    [Tooltip("The total number of frames in the walk cycle.")]
    public int walkingAnimFrames;

    private Animator anim;
    private AtraxStates state;
    
    void Awake()
    {
        anim = GetComponentInParent<Animator>();
        state = GetComponentInParent<AtraxStates>();
    }
    
    void Update()
    {
        RaycastHit hit;
        if (Debugging)
        {
            // If debugging is true, draw the rays
            Debug.DrawRay(transform.position + (-transform.up * castOffset), transform.up * castLength, Color.red);
        }

        if (Physics.SphereCast(transform.position + (-transform.up * castOffset), castRadius, transform.up, out hit, castLength, layerMask, QueryTriggerInteraction.Ignore))
        {
            // Spherecast from just above the foot, to just below the foot.
            // Ensure that IK is active.
            ikSolver.IsActive = true;
            // If the Atrax is walking.
            if (anim.GetBool(state.isWalkingBool))
            {
                // Get the current animation frame.
                int currentFrame = GetCurrentAnimationFrame(walkingAnimFrames);
                // If the current frame is whilst the foot is in the air
                if (currentFrame > startFrame && currentFrame < endFrame)
                {
                    // Offset the target so that it is raised.
                    ikSolver.SetTarget(hit.point - (transform.up * animationOffset));
                }
                else
                {
                    // Otherwise, foot finds floor
                    ikSolver.SetTarget(hit.point);
                }
            }
        }
        else
        {
            // If the raycast does not hit anything, don't solve IK
            ikSolver.IsActive = false;
            ikSolver.SetTarget(Vector3.zero);
        }
    }
    
    int GetCurrentAnimationFrame(int numberOfFrames)
    {
        // Get the current animation frame.
        // This is done by multiplying the normalizedTime by the total number of frames in the animation
        // and then using modulo to the total number of frames.
        // I did something similar in my final year project at Uni to ensure animation was framerate independant. - Luke
        return ((int)(anim.GetCurrentAnimatorStateInfo(0).normalizedTime * (numberOfFrames))) % numberOfFrames;
    }
}