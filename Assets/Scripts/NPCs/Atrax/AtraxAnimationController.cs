using UnityEngine;

public class AtraxAnimationController : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private Animator anim;
    private AtraxStates state;
    
    // Use this for initialization
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        state = GetComponent<AtraxStates>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // If Atrax is moving, and is currently grounded, he's walking.
        //if (rigidbody.velocity.x > 0f || rigidbody.velocity.z > 0f)
       // {
            //StartMovingState();
       // }
       // else    // Otherwise he's not walking.
       // {
        //    StopMovingState();
        //}
    }
    
    public void StartMovingState() => anim.SetBool(state.isWalkingBool, true);
    
    public void StopMovingState() => anim.SetBool(state.isWalkingBool, false);
    
    public void StartSwipeState() => anim.SetBool(state.isSwipingBool, true);

    public void StopSwipeState() => anim.SetBool(state.isSwipingBool, false);
    
    public void StartJumpState() => anim.SetBool(state.isJumpingBool, true);
    
    public void StopJumpState() => anim.SetBool(state.isJumpingBool, false);
    
    public void StartTurningLeftState() => anim.SetBool(state.isJumpingBool, true);
    
    public void StopTurningLeftState() => anim.SetBool(state.isJumpingBool, false);
    
    public void StartTurningRightState() => anim.SetBool(state.isJumpingBool, true);
    
    public void StopTurningRightState() => anim.SetBool(state.isJumpingBool, false);
}