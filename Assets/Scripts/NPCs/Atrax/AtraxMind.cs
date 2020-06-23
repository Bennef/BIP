using UnityEngine;

public class AtraxMind : Mind
{
    [Tooltip("Turning Speed")]
    public float turningSpeed;		
    AtraxStates state;
    Animator anim;
    public BoxCollider swipeCollider;
    
    // Use this for initialization
    void Awake()
    {
        //rigidbody = GetComponent<Rigidbody>();
        state = GetComponent<AtraxStates>();
        anim = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        Quaternion rotation = CalculateRotationOnlyY();
        Move();
        //GetComponent<NavMeshAgent>().SetDestination(target.position);
    }
    
    protected override void Move()
    {
        if (anim.GetBool("isWalking") == true)
        {
            Debug.Log("s");
        }
    }
}