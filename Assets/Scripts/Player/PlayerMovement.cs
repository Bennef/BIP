using UnityEngine;
using System.Collections;

public class PlayerMovement: MonoBehaviour
{
	// Class only handles movement of Bip
	public float speed = 10;				// Speed of Bip lateral movement.
    [HideInInspector]
	public Vector3 movement;				// For Bip movement.
	public float jumpVelocity;			    // For jump height.
	public float jumpReduction;		        // The degree to which jump is variable.
    public float doubleJumpVelocity;        // For jump height.
    public float doubleJumpReduction;       // The degree to which jump is variable.
    public Vector3 maxVelocityCap;			// To cap velocity.

	private bool hasJumped = false;				// To check if player has pressed jump.
    public bool canDoubleJump = false;          // True if Bip can double jump.
    public bool hasDoubleJumped;                // True when Bip has already double jumped this jump.
	private bool cutJumpShort = false;			// If true, player has stopped holding button.
	public bool isHandlingInput;                // True when the player has control.
    public bool is2D;                           // If true, Bip can only move in 2 directions along the x axis.
    public bool bipCanJump;                     // True if player is able to jump.

    public new Rigidbody rigidbody;
	public Animator anim;
	public PlayerStates state;
	public LayerMask layerMask;
	public LayerMask ledgeMask;
    public Camera MainCam;

	private Rigidbody grabbedLedge;
    private CharacterController controller;
    private CharacterSoundManager SoundManager;
    public CapsuleCollider headCollider;

    public enum MovementType
    {
        MovePosition,
        Velocity
    }

    public MovementType movementType;
	
	void Awake()
	{
        //DontDestroyOnLoad(this);
		rigidbody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		state = GetComponent<PlayerStates>();
        MainCam = Camera.main;
        controller = GetComponent<CharacterController>();
        SoundManager = GetComponent<CharacterSoundManager>();
    }
    
    void Start() => movementType = MovementType.Velocity;

    // Called 50 times a second. Put physics stuff in here.
    void FixedUpdate()
	{
        // Create a sphere at the player's feet. If the sphere collides with anything on the layer(s) layerMask
        // return true
        if (Physics.CheckSphere(transform.position, 0.1f, layerMask))
            anim.SetBool(state.isGroundedBool, true); // If sphere collides, we're touching ground.
        else
            anim.SetBool(state.isGroundedBool, false); // Otherwise, we're in the air.

        if (!anim.GetBool(state.isGroundedBool) && !anim.GetBool(state.isClimbingBool))
        {
            // We can double jump if we're airbourne and not grabbing a ledge
            canDoubleJump = true;
        }
        else
        {
            canDoubleJump = false;
            hasDoubleJumped = false;    // So Bip can double jump the next time he jumps.
        }

        // Don't allow Bip to jump if we are in 2D mode.
        if (bipCanJump)
            ApplyJumpPhysics();

        if (!controller.enabled)
            return;

        if (movement != Vector3.zero && !anim.GetBool(state.isPushingBool)) 
			rigidbody.transform.rotation = Quaternion.LookRotation(movement);
 
        if (anim.GetBool(state.isGroundedBool))
        {
            headCollider.enabled = true;
            if (movementType == MovementType.Velocity)
                rigidbody.velocity = new Vector3(movement.x, rigidbody.velocity.y, movement.z);
            else
                rigidbody.MovePosition(Vector3.MoveTowards(transform.position, transform.position + movement, Time.fixedDeltaTime * speed));
        }
        else if (!Physics.Raycast(controller.Head.position, transform.forward, 1f, layerMask, QueryTriggerInteraction.Ignore) && 
            !Physics.Raycast(transform.position, transform.forward, 1f, layerMask, QueryTriggerInteraction.Ignore))
        {
            rigidbody.velocity = new Vector3(movement.x, rigidbody.velocity.y, movement.z);
            headCollider.enabled = false; 
        }
                
		// Reset movement at the end of every frame.
		// This needs to happen, if we grab a ledge and movement still has a value, we won't stop moving.
		movement = Vector3.zero;

        // Check if Bip is climbing
		if (anim.GetBool(state.isClimbingBool)) 
		{
            // If he is, disable gravity and override velocity.
			rigidbody.useGravity = false;
            rigidbody.velocity = Vector3.zero;

			if (Input.GetAxis("Vertical") < 0) 
				anim.SetBool(state.isClimbingBool, false);
		} 
		else 
		{
			rigidbody.useGravity = true;
            grabbedLedge = null;
        }

		// Check if Bip is not already climbing and if there is a ledge accessible. 
		if (!anim.GetBool(state.isClimbingBool) && !anim.GetBool(state.isGroundedBool)) 
			DetectClimbableLedge();

        // Cap our velocity to a certain amount.
        CapVelocity();
    }
	
	// To handle movement.
	public void ManageMovement(float h, float v)
	{
		if (!isHandlingInput)
			return;
		if (anim.GetBool(state.isClimbingBool))
			return;
		if (anim.GetBool(state.isPushingBool))
		{
			ManagePullingMovement(h, v);
			return;
		}
		// Find the new forward and right vectors to move along.
		Vector3 forwardMove = Vector3.Cross(MainCam.transform.right, Vector3.up);
		Vector3 horizontalMove = MainCam.transform.right;

        // If we are in 2D, make sure only the horizontal movement is recieved.
        if (is2D)
            v = 0f;

        // Multiply the direction vectors by the Input.GetAxis floats.
        movement = forwardMove * v + horizontalMove * h;
		
		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed;
		// Set velocity for animation states
		state.Velocity = movement;
	}
	
	public void ManagePullingMovement(float h, float v)
	{
		// Find the new forward and right vectors to move along.
		Vector3 forwardMove = transform.forward;
        float moveValue;

        //float angle = Mathf.Abs(Vector3.Angle(forwardMove, Vector3.Cross(Camera.main.transform.right, Vector3.up)));
        float angle = Vector3.Angle(forwardMove, Vector3.Cross(MainCam.transform.right, Vector3.up));

        if (angle <= 45)
            moveValue = v;
        else if (angle <= 135 && angle > 45)
        {
            float sideAngle = Vector3.Angle(transform.right, Vector3.Cross(MainCam.transform.right, Vector3.up));
 
            if (sideAngle <= 135)
                moveValue = -h;
            else
                moveValue = h;
        }
        else if (angle > 135 && angle <= 180)
            moveValue = -v;
        else
            moveValue = v;

        anim.SetFloat(state.speedFloat, moveValue);

		// Multiply the direction vectors by the Input.GetAxis floats.	
		movement = forwardMove * moveValue;

		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed;

		// Set velocity for animation states.
		state.Velocity = movement;
    }
    
    // To make Bip jump. 
    public void Jump() 
	{
		if (anim.GetBool(state.isPushingBool) || !isHandlingInput)
			return;
        SoundManager.PlayClip(SoundManager.jump);
        hasJumped = true;
		anim.SetBool(state.isClimbingBool, false);
	}
	
	// To make Bip jump.
	public void CutJumpShort() => cutJumpShort = true;

    private void ApplyJumpPhysics()
	{
		if (hasJumped)
        {
            if (!canDoubleJump)
            {
                // Normal primary jump.
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpVelocity, rigidbody.velocity.z);
            }
            else
            {
                // Double jump if Bip hasn't already.
                if (!hasDoubleJumped)
                {
                    SoundManager.PlayClip(SoundManager.jump);
                    rigidbody.velocity = new Vector3(rigidbody.velocity.x, doubleJumpVelocity, rigidbody.velocity.z);
                    hasDoubleJumped = true;  // So Bip can't jump more than once per jump.
                }
            }
            rigidbody.useGravity = true;
            hasJumped = false;
        }

		// Cancel the jump when the button is no longer pressed
		if (cutJumpShort)
		{
			if (rigidbody.velocity.y > jumpReduction)
				rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpReduction, rigidbody.velocity.z);
			cutJumpShort = false;
		}
	}
	
	// Check for a ledge in front of Bip that he can climb.
	void DetectClimbableLedge()
	{
		//Debug.Log ("Climbable ledge accessible");
		Vector3 fwd = transform.forward;	// The forward direction of Bip.

		// If player is not rising...
		if (!anim.GetBool(state.isRisingBool))
		{
            // Raycast from Bips head forwards.
            //Debug.DrawRay(controller.Head.position, transform.forward * 1f);
            if (Physics.Raycast(controller.Head.position, fwd, out RaycastHit hit, 1f, ledgeMask, QueryTriggerInteraction.Collide) && rigidbody.velocity.y < 0)
            {
                // Get the rigidibody of the ledge. 
                grabbedLedge = hit.rigidbody;
                // Get Bip to look at the object he is grabbing.
                Vector3 temp = Vector3.Cross(transform.up, hit.normal);
                transform.rotation = Quaternion.LookRotation(-temp);
                transform.Rotate(0, 270, 0);    // Make him face the wall.
                rigidbody.position += (hit.distance) * transform.forward;

                anim.SetBool(state.isClimbingBool, true);
                hasDoubleJumped = false;
            }
        }
	}
	
	public void Knockback(float strength)
	{
		if (this.gameObject.GetComponent<CharacterController>().isDead)
			return;
		rigidbody.AddForce(-movement.normalized * strength, ForceMode.Force);
		isHandlingInput = false;
	}
	
	public IEnumerator KnockedBackCo()
	{
		yield return new WaitForSeconds(0.8f);
		isHandlingInput = true;
	}
	
	// To cap velocity so Bip doesn't fall too fast.
	void CapVelocity() 
	{
		Vector3 _velocity = GetComponent<Rigidbody>().velocity;
		_velocity.x = Mathf.Clamp(_velocity.x, -maxVelocityCap.x, maxVelocityCap.x);
		_velocity.y = Mathf.Clamp(_velocity.y, -maxVelocityCap.y, maxVelocityCap.y);
		_velocity.z = Mathf.Clamp(_velocity.z, -maxVelocityCap.z, maxVelocityCap.z);
		rigidbody.velocity = _velocity;
    }
    
    void OnCollisionEnter(Collision col)
    {
        // If Bip lands on a moving platform...
        if (anim.GetBool(state.isGroundedBool) && col.transform.CompareTag("Moving Platform"))
            movementType = MovementType.MovePosition;
    }
    
    void OnCollisionStay(Collision col)
    {
        // If Bip lands on a moving platform...
        if (anim.GetBool(state.isGroundedBool) && col.transform.CompareTag("Moving Platform"))
            movementType = MovementType.MovePosition;
    }
    
    void OnCollisionExit(Collision col)
    {
        // If Bip leaves a moving platform...
        if (col.transform.CompareTag("Moving Platform"))
            movementType = MovementType.Velocity;
    }
}