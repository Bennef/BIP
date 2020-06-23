using UnityEngine;

public class PlayerStates : MonoBehaviour
{
	private int idleState;
	private int runningState;
	private int riseState;
	private int fallState;
	private int pushState;
	private int surpriseState;
	private int isRunningBool;
	private int isRisingBool;
	private int isPushingBool;
	private int isGroundedBool;
	private int isClimbingBool;
	private int speedFloat;

	private Vector3 _velocity;

    public Vector3 Velocity { get => _velocity; set => _velocity = value.normalized; }
    public int IdleState { get => idleState; set => idleState = value; }
    public int RunningState { get => runningState; set => runningState = value; }
    public int RiseState { get => riseState; set => riseState = value; }
    public int FallState { get => fallState; set => fallState = value; }
    public int PushState { get => pushState; set => pushState = value; }
    public int SurpriseState { get => surpriseState; set => surpriseState = value; }
    public int IsRunningBool { get => isRunningBool; set => isRunningBool = value; }
    public int IsRisingBool { get => isRisingBool; set => isRisingBool = value; }
    public int IsPushingBool { get => isPushingBool; set => isPushingBool = value; }
    public int IsGroundedBool { get => isGroundedBool; set => isGroundedBool = value; }
    public int IsClimbingBool { get => isClimbingBool; set => isClimbingBool = value; }
    public int SpeedFloat { get => speedFloat; set => speedFloat = value; }

    void Awake()
	{
		// Assigning HashIDs
		IdleState = Animator.StringToHash("Base Layer.Idle");
		RunningState = Animator.StringToHash("Base Layer.Run");
		RiseState = Animator.StringToHash("Base Layer.Rise");
		FallState = Animator.StringToHash("Base Layer.Fall");
		PushState = Animator.StringToHash("Base Layer.Push");
		SurpriseState = Animator.StringToHash("Base Layer.Surprise!");

		IsRunningBool = Animator.StringToHash("isRunning");
		IsRisingBool = Animator.StringToHash("isRising");
		IsPushingBool = Animator.StringToHash("isPushing");
		IsGroundedBool = Animator.StringToHash("isGrounded");
		IsClimbingBool = Animator.StringToHash("isClimbing");

        SpeedFloat = Animator.StringToHash("Speed");
	}
}