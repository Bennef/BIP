using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlankMind : Mind
{
    
    public enum State
    {
        Idle,
        Patrolling,
        Chasing,
        Stunned
    }

    private Animator anim;                                      // Animator component.
    private new Rigidbody rigidbody;                            // The rigidbody of Blank.  
    public List<Transform> Waypoints = new List<Transform>();   // A list of the waypoints on patrol path.
    public bool canSeeYou;                                      // True when Blank spots player.
    public State state;                                         // 
    public Transform Head;                                      // Reference to target's Head.
    public float maxAttackHeight;
    public Light redLight;
    public Light blueLight;
    
    private int targetIndex;                                    
    [SerializeField]
    private float patrolSpeed;                                  // Patrolling speed.
    [SerializeField]
    private float chaseSpeed;                                   // Chasing speed.
    [SerializeField]
    private float damage;                                       // Damage Blank inflicts.

    private bool stunned;                                       // True when Blank is stunned.

    public GameObject EMPObject;			                    // A reference to the EMP Game Object.
    public AudioSource aSrc;                                    // To play sounds.
    public AudioClip attack, alert;
   
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        aSrc = GetComponent<AudioSource>();

        shouldFollow = true;
        Head = transform.Find("Head");
        // If the Blank has no waypoints, it die.
        if (Waypoints.Count == 0)
        {
            Debug.Log("Blank with no patrol waypoints!");
            this.enabled = false;
        }
        targetIndex = 0;
        redLight.enabled = false;
        EMPObject.SetActive(false);	// Set EMP object to non-active until we perform an EMP Blast.
    }
    
    void Update()
    {
        // If the player is in view...
        if (canSeeYou)
            SetState(State.Chasing);
    }
    
    void FixedUpdate()
    {
        if (state == State.Chasing)
            Chase();
        else if (state == State.Idle || state == State.Stunned)
            return;
        else
            Patrol();

        // If the player is in view...
        Quaternion rotation = CalculateRotationOnlyY();

        // Actually turn in that direction.
        rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * speed));
    }
    
    protected override void Move()
    {
        // Handle locomotion in this method.
        // Store a temp position so that Blank will only take into account horizontal distance between itself and Bip.
        Vector3 tempPosition = transform.position;
        tempPosition.y = target.position.y;
        // Get the horizontal distance between Blank and target
        currentDistance = Vector3.Distance(tempPosition, target.position);
        // Velocity to be applied at end of method.
        Vector3 newVelocity = new Vector3(0f, rigidbody.velocity.y, 0f);

        if (currentDistance >= PreferredDistance)
        {
            if (!Physics.Raycast(transform.position + raycastOffset, transform.forward, ObstacleDistance, GameManager.Instance.ObstacleAvoidanceLayerMask, QueryTriggerInteraction.Ignore))
            {
                // Raycast directly in front of Blank
                // If raycast hits, Blank does not move for they would be running into a wall.
                // If Blank is too far away, they must get closer.
                newVelocity = transform.forward * speed;
                // We don't touch Y velocity to maintain integrity of gravity.
                newVelocity = new Vector3(newVelocity.x, rigidbody.velocity.y, newVelocity.z);
            }
        }
        else
        {
            if (target.CompareTag(Tags.Player) && target.transform.position.y < maxAttackHeight)
                Attack();
            else
            {
                if (RandomBool())
                    StartCoroutine(IdleWait());
            }
        }
        rigidbody.velocity = newVelocity;
    }
    
    // Patrol in a set pattern.
    public void Patrol()
    {
        if (!Waypoints.Contains(target))
            FindNearestWaypoint();

        if (currentDistance <= PreferredDistance)
        {
            if (targetIndex < Waypoints.Count - 1)
                targetIndex++;
            else
                targetIndex = 0;

            target = Waypoints[targetIndex];
        }
        speed = patrolSpeed;
        redLight.enabled = false;
        blueLight.enabled = true;
        Move();
    }
    
    // Chase the player.
    public void Chase()
    {
        target = GameManager.Instance.Player.transform;
        speed = chaseSpeed;
        redLight.enabled = true;
        blueLight.enabled = false;
        Move();
    }
    
    // Attack the player.
    public void Attack()
    {
        // Start particle effects.
        EMPObject.SetActive(true);

        // Play attacing sound.
        if (!aSrc.isPlaying)
        {
            aSrc.clip = attack;
            aSrc.Play();
        }

        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
            targetHealth.TakeDamage(damage);
        Reset();
    }
    
    // Return to patrolling our path by heading towards the nearest waypoint.
    public void FindNearestWaypoint()
    {
        // Create a temporary variable to hold the nearest waypoint index.
        int nearestWaypointIndex = 0;
        // Loop through the waypoints
        for (int i = 0; i < Waypoints.Count; i++)
        {
            // if the distance between the Blank and current iteration index is less than the distance between Blank and nearestWaypointIndex
            // Update the nearestWaypointIndex to the current iteration index.
            if (Vector3.Distance(transform.position, Waypoints[i].position) < Vector3.Distance(transform.position, Waypoints[nearestWaypointIndex].position))
                nearestWaypointIndex = i;
        }
        // Set the new target.
        target = Waypoints[nearestWaypointIndex];
    }
    
    public IEnumerator IdleWait()
    {
        SetState(State.Idle);
        yield return new WaitForSeconds(2.0f);
        if (canSeeYou == false && state == State.Idle)
            Reset();
    }
    
    public IEnumerator Stun()
    {
        SetState(State.Stunned);
        yield return new WaitForSeconds(2.0f);
        SetState(State.Idle);
    }
    
    private void Reset()
    {
        canSeeYou = false;
        SetState(State.Patrolling);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.EMP))
            StartCoroutine(Stun());
    }
    
    public void SetState(State value)
    {
        if (state == State.Stunned)
        {
            if (value == State.Idle)
            {
                state = value;
                StartCoroutine(IdleWait());
            }
            else
                return;
        }
        state = value;
    }
    
    private bool RandomBool()
    {
        float num = Random.Range(0f, 1f);
        if (num >= 0.5)
            return true;
        return false;
    }
}