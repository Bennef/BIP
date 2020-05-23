using UnityEngine;
using System.Collections;

// Handles NanoDrone behaviour. 
public class NanoDroneMind : FriendlyMind
{
    // ----------------------------------------------- Data members ----------------------------------------------  
    public bool active, pausing;
    public float brakingSpeed;          // Speed that NanoDrone brakes.
    public float hoverHeight;
    public Vector3 MaxVelocity;         // So NanoDrone does not fly too fast.
    private Vector3 targetPosition;     // World osition of the target.
    public GameObject startingPosition;
    public Transform bipTarget, icoTarget;
    private TripLaserPuzzle tripLaserPuzzle;
    public GameObject deathRay;
    private AudioSource aSrc;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        bipTarget = GameObject.Find("CTRL_Head").transform;
        icoTarget = GameObject.Find("Homing Ico 3").transform;
        tripLaserPuzzle = GameObject.Find("Trip Laser Puzzle").GetComponent<TripLaserPuzzle>();
        aSrc = GetComponent<AudioSource>();
        target = bipTarget; // So they watch him if he messes with them.
    }
    // --------------------------------------------------------------------
    void FixedUpdate()
    {
        Quaternion rotation = CalculateRotation();  // Find rotation to look at.
        rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed));  // Actually turn in that direction.
        
        if (active)
        {
            Move();
        }
        else
        {
            MoveHome();
            deathRay.gameObject.SetActive(false);
        }

        if (shouldFollow)
        {
            rigidbody.drag = 0.2f;
        }
        else
        {
            rigidbody.drag = 1.0f;  // So he doesn't float off into the distance.
        }

        // Cap velocity to prevent super speeds.
        CapVelocity();
    }
    // --------------------------------------------------------------------
    // Move drone.
    protected override void Move()
    {
        // Store a temp position so that NanoDrone will only take into account horizontal distance between itself and Bip.
        Vector3 tempPosition = transform.position;
        tempPosition.y = target.position.y;
        
        float currentDistance = Vector3.Distance(tempPosition, target.position);  // Get the horizontal distance between NanoDrone and target.
        PreferredDistance = 3f;
        MinDistance = 1.5f;

        if (currentDistance > PreferredDistance)
        {
            shouldFollow = true;
            rigidbody.velocity = transform.forward * speed;  // If NanoDrone is too far away, move in.
        }
        else if (currentDistance > MinDistance)
        {
            shouldFollow = false;
            // If NanoDrone is at a respectful distance, apply the brakes.
            rigidbody.velocity = new Vector3(rigidbody.velocity.x * (1 - brakingSpeed / 100),
            rigidbody.velocity.y, rigidbody.velocity.z * (1 - brakingSpeed / 100));
        }
        else
        {
            shouldFollow = false;
            rigidbody.velocity = -transform.forward * speed;
        }
        
        if (transform.position.y < target.position.y)
        {
            // Maintain an elevation similar to Bip's head.
            rigidbody.AddForce(Vector3.up * (target.position.y - transform.position.y), ForceMode.VelocityChange);
        }
    }
    // --------------------------------------------------------------------
    public void MoveHome()
    {
        if (!tripLaserPuzzle.alarm)
        {
            target = startingPosition.transform;  // Go home, stupid drones.
        }

        PreferredDistance = 0.5f;
        MinDistance = 0f;
        shouldFollow = false;
        // Store a temp position so that NanoDrone will only take into account horizontal distance between itself and Bip.
        Vector3 tempPosition = transform.position;
        tempPosition.y = target.position.y;

        // Get the horizontal distance between NanoDrone and target
        float currentDistance = Vector3.Distance(tempPosition, target.position);

        if (currentDistance > PreferredDistance)
        {
            // If NanoDrone is too far away, move in.
            rigidbody.velocity = transform.forward * speed;
        }
        else if (currentDistance > MinDistance)
        {
            // If NanoDrone is at a respectful distance, apply the brakes.
            rigidbody.velocity = new Vector3(rigidbody.velocity.x * (1 - brakingSpeed / 100),
            rigidbody.velocity.y, rigidbody.velocity.z * (1 - brakingSpeed / 100));
            target = bipTarget;
        }
    }
    // --------------------------------------------------------------------
    // Make sure NanoDrone does not fly faster than we want.
    void CapVelocity()
    {
        Vector3 _velocity = GetComponent<Rigidbody>().velocity;
        _velocity.x = Mathf.Clamp(_velocity.x, -MaxVelocity.x, MaxVelocity.x);
        _velocity.y = Mathf.Clamp(_velocity.y, -MaxVelocity.y, MaxVelocity.y);
        _velocity.z = Mathf.Clamp(_velocity.z, -MaxVelocity.z, MaxVelocity.z);
        rigidbody.velocity = _velocity;
    }
    // --------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" && target == bipTarget && active) || (other.tag == "Moveable" && target == icoTarget && active))
        {
            deathRay.gameObject.SetActive(true);
        }
    }
    // --------------------------------------------------------------------
    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "Player" && target == bipTarget) || (other.tag == "Moveable" && target == icoTarget))
        {
            if (!pausing)
            {
                pausing = true;
                deathRay.gameObject.SetActive(false);
                StartCoroutine(Pause());
            }
        }
    }
    // --------------------------------------------------------------------
    public IEnumerator Pause()
    {
        yield return new WaitForSeconds(1.0f);
        pausing = false;
    }
    // --------------------------------------------------------------------
    public void ResetPosition()
    {
        transform.position = startingPosition.transform.position;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
