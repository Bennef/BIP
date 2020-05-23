using UnityEngine;

public class AtraxStates : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public int idleState;
    public int walkingState;
    public int swipingState;
    public int jumpingState;
    public int turningLeftState;
    public int turningrightState;

    public int isWalkingBool;
    public int isSwipingBool;
    public int isJumpingBool;
    public int isTurningLeftBool;
    public int isTurningRightBool;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Use this for initialization
    void Awake()
    {
        idleState = Animator.StringToHash("Base Layer.Idle");
        walkingState = Animator.StringToHash("Base Layer.Atrax_Walk");
        swipingState = Animator.StringToHash("Base Layer.Atrax_Swipe_Attack");
        jumpingState = Animator.StringToHash("Base Layer.Atrax_Jump_Attack");
        jumpingState = Animator.StringToHash("Base Layer.Atrax_Jump_Attack");
        jumpingState = Animator.StringToHash("Base Layer.Atrax_Jump_Attack");

        isWalkingBool = Animator.StringToHash("isMoving");
        isSwipingBool = Animator.StringToHash("isSwiping");
        isJumpingBool = Animator.StringToHash("isJumping");
        isTurningLeftBool = Animator.StringToHash("isTurningLeft");
        isTurningRightBool = Animator.StringToHash("isTurningRight");
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
