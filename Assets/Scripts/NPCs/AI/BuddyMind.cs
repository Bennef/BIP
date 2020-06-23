using UnityEngine;
using System.Collections;

public class BuddyMind : FriendlyMind
{
    public float brakingSpeed;
    public float jumpingDistance;
    private PlayerStates state;										// Stores the state of the player. Model.
    private PlayerMovement playerMovement;
    private Animator anim;

    private bool hasJumped;
    [SerializeField]
    private bool isWaiting;
    
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        state = GetComponent<PlayerStates>();
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (Input.GetButtonUp("Wait"))
            isWaiting = !isWaiting;

        // If the angle between the camera's forward direction and the Buddy is greater than the camera field of view...
        // this buddy is not on screen.
        if (Camera.main)
        {
            float angle = Mathf.Abs(Vector3.Angle(Camera.main.transform.forward, transform.position - Camera.main.transform.position));
            onScreen = angle < Camera.main.fieldOfView;
        }
    }
    
    void FixedUpdate()
    {
        Quaternion rotation = CalculateRotationOnlyY();

        // Actually turn in that direction.
        rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed));
        
        if (!isWaiting)
        {
            Unstuck();
            Move();
            ApplyJumpPhysics();
        }
        else
            state.Velocity = rigidbody.velocity;
    }
    
    public void Wait() => isWaiting = true;
    
    protected override void Move()
    {
        // Handle locomotion in this method.
        // Store a temp position so that CoBot will only take into account horizontal distance between itself and Bip.
        Vector3 tempPosition = transform.position;
        tempPosition.y = target.position.y;
        // Get the horizontal distance between CoBot and target
        currentDistance = Vector3.Distance(tempPosition, target.position);
        // Velocity to be applied at end of method.
        Vector3 newVelocity = new Vector3(0f, rigidbody.velocity.y, 0f);

        if (currentDistance >= PreferredDistance)
        {
            // If Buddy is further than preferred distance, they should follow.
            shouldFollow = true;
            // If you are higher than buddy for more than one second,
            // buddy also gets high.
            if (target.position.y > transform.position.y + 1 && currentDistance <= jumpingDistance)
                StartCoroutine(JumpCo());

            if (anim.GetBool(state.IsGroundedBool) && target.position.y >= transform.position.y - 1)
            {
                if (!Physics.Raycast(transform.position + raycastOffset, transform.forward - raycastOffset, 2f, GameManager.Instance.ObstacleAvoidanceLayerMask, QueryTriggerInteraction.Ignore))
                    Jump();
            }

            if (!Physics.Raycast(transform.position + raycastOffset, transform.forward, ObstacleDistance, GameManager.Instance.ObstacleAvoidanceLayerMask, QueryTriggerInteraction.Ignore))
            {
                // Raycast directly in front of buddy
                // If raycast hits, buddy does not move for they would be running into a wall.
                // If Buddy is too far away, they must get closer.
                newVelocity = transform.forward * speed;
                // We don't touch Y velocity to maintain integrity of gravity.
                newVelocity = new Vector3(newVelocity.x, rigidbody.velocity.y, newVelocity.z);
            }
        }
        else if (currentDistance > MinDistance)
        {
            //Debug.Log(trigger);
            Vector3 targetVelocity = target.GetComponent<Rigidbody>().velocity;
            targetVelocity.y = 0;
            if (targetVelocity.magnitude == 0f)
            {
                // If Buddy is at a respectful distance, apply the brakes.
                newVelocity = new Vector3(rigidbody.velocity.x * (1 - brakingSpeed / 100),
                rigidbody.velocity.y, rigidbody.velocity.z * (1 - brakingSpeed / 100));
            }        
            shouldFollow = false;
        }
        else
        {
            // If Buddy is too close, move away.
            newVelocity = -transform.forward * speed;
            // We don't touch Y velocity to maintain integrity of gravity.
            newVelocity = new Vector3(newVelocity.x, rigidbody.velocity.y, newVelocity.z);
            shouldFollow = false;
        }

        rigidbody.velocity = newVelocity;
        // AddForce with a cap instead of setting velocity directly 
        state.Velocity = rigidbody.velocity;
    }
    
    void Jump() => hasJumped = true;
    
    void ApplyJumpPhysics()
    {
        if (hasJumped)
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, playerMovement.jumpVelocity, rigidbody.velocity.z);
        hasJumped = false;
    }
    
    IEnumerator JumpCo()
    {
        yield return new WaitForSeconds(1f);
        if (target.position.y > transform.position.y + 1)
            Jump();
    }
}