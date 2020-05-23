using UnityEngine;

public class AtraxMind : Mind
{
    // ----------------------------------------------- Data members ----------------------------------------------
    [Tooltip("Turning Speed")]
    public float turningSpeed;				    // Turning speed.
    AtraxStates state;
    //new Rigidbody rigidbody;
    Animator anim;
    public BoxCollider swipeCollider;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Use this for initialization
    void Awake()
    {
        //rigidbody = GetComponent<Rigidbody>();
        state = GetComponent<AtraxStates>();
        anim = GetComponent<Animator>();
    }
    // --------------------------------------------------------------------
    void FixedUpdate()
    {
        Quaternion rotation = CalculateRotationOnlyY();
        Move();
        //GetComponent<NavMeshAgent>().SetDestination(target.position);
    }
    // --------------------------------------------------------------------
    protected override void Move()
    {
        if (anim.GetBool("isWalking") == true)
        {
            Debug.Log("s");
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
