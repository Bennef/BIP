using UnityEngine;
using System.Collections;
using Scripts.Player;
using Scripts.Game.Game_Logic;
using Scripts.Game.Level_Dynamics;

namespace Scripts.NPCs.AI
{
    public class DroneBotMind : MindActor
    {
        public Transform target;
        public Vector3 targetPosition;
        public float chaseSpeed = 1f;           // Speed when chasing.
        public float vSpeed = 3f;               // Speed of hovering.
        public float speed = 5f;                // Movement speed.

        private SphereCollider trigger;         // This is what Bip walks into to set the drone to CHASE mode.
        public bool toggleReturnPath = true;

        public float chaseLimit = 5.6f;
        public float smoothing = 2f;

        private Vector3 originalPosition;       // Stores position to return to after chasing.
        private Quaternion originalRotation;    // Stores rotation to return to after chasing.

        private State currentState;             // Current state of the DroneBots mind.

        private DroneLaser laserScript;         // Reference to laser gun script.
        private HoveringObject hoverScript;

        private Health targetHealth;            // A reference to Bip's health.
        private Health droneHealth;
        private AudioSource audioSource;        // A reference to the AudioSource.
        public AudioClip alert;
        public bool isDead;

        private enum State
        {
            IDLE,
            CHASE,
            RETURN,
            JAMMED,
        };

        // Quick fix for MindController issues ----
        void Awake()
        {
            droneHealth = GetComponent<Health>();
            laserScript = transform.GetComponentInChildren<DroneLaser>();
            hoverScript = GetComponent<HoveringObject>();
            trigger = transform.GetComponent<SphereCollider>();
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            targetPosition = originalPosition;
            //Init();
            //pathingScript = GetComponent<AIPathingScript>();
            currentState = State.IDLE;

            target = GameManager.Instance.Player;
            targetHealth = target.GetComponentInParent<PlayerHealth>();     // This means we can only shoot at player.
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (GameManager.Instance.IsPaused || isDead)
                return;
            // Run Think. If it returns false, we're dead.
            if (!Think() || droneHealth.value <= 0)
                Destroy();
        }

        // Main "Thought" process of the AI. Return 'false' if the bot has died.
        public override bool Think()
        {
            switch (currentState)
            {
                case State.IDLE:
                    break;
                case State.CHASE:
                    ChaseTarget();
                    laserScript.inChaseRange = true;
                    break;
                case State.RETURN:
                    ReturnToPath();
                    break;
                case State.JAMMED:
                    //JammingDrone();	
                    break;
                // Error Handling...
                default:
                    Debug.LogError("Invalid State. Check if currentState is defined.");
                    break;
            }

            if (currentState != State.CHASE)
                laserScript.inChaseRange = false;
            //Debug.Log ("Drone State: " + currentState);
            return true;
        }

        // Used for handling any end state behaviour.
        public override void Destroy()
        {
            isDead = true;
            GameObject deadDrone = (GameObject)Instantiate(Resources.Load("Broken Drone"));
            deadDrone.transform.position = transform.position;
            gameObject.SetActive(false);
        }

        // Unique DroneBot Method Implementations --
        void OnTriggerEnter(Collider other)
        {
            // Checks if Bip was the collided object.
            if (other.CompareTag("Player") && !isDead)
                TurnAlarmOn();

            // Checks if a Signal Jammer was the collided object.
            if (other.CompareTag("Jammer"))
            {
                targetPosition = transform.position;
                currentState = State.JAMMED;
            }
        }

        void OnTriggerExit(Collider other)
        {
            // Checks if Bip as the collide object.
            if (other.CompareTag("Player"))
                StartCoroutine("CheckDistance");
        }

        void FixedUpdate()
        {
            if (GameManager.Instance.IsPaused)
                return;
            // The direction we wish to go in.
            Vector3 movementDirection;
            // If we're chasing, we want to use a different speed.
            if (currentState == State.CHASE)
                movementDirection = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * chaseSpeed);
            else
                movementDirection = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

            // Move towards target.
            transform.position = movementDirection;

            // Rotate the bot to look where it's going
            if (currentState == State.RETURN)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementDirection.normalized), Time.deltaTime * smoothing);
            else if (currentState == State.IDLE)
            {
                // Rotate the bot back to it's original rotation when it gets home.
                transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * smoothing);
            }
        }

        void ChaseTarget()
        {
            if (targetHealth.value <= 0)
                currentState = State.RETURN;

            // Face Bip.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), Time.deltaTime * smoothing);

            // Update target position.
            targetPosition = target.position - transform.forward * chaseLimit;
        }

        void ReturnToPath()
        {
            targetPosition = originalPosition;

            if (Vector3.Distance(transform.position, targetPosition) < 0.1)
                currentState = State.IDLE;
        }

        IEnumerator CheckDistance()
        {
            // Distance from Bip.
            yield return new WaitForSeconds(2.0f);
            float dist = Vector3.Distance(target.position, transform.position);
            // if outside of detection range, return to last position.
            if (dist > trigger.radius * transform.localScale.x)
                currentState = State.RETURN;
        }

        public void EnterChaseState()
        {
            if (currentState != State.CHASE)
                audioSource.PlayOneShot(alert);
            currentState = State.CHASE;
        }
        /*
        void JammingDrone()
        {
            // Wait or do something whilst jammed/disabled...
            if (false)//condition to be un-jammed TO DO
            {
                //targetPosition = nextDestination;
                currentState = State.IDLE;				//@Notes: If Bip is still within proximity sphere the bot wont go to CHASE state. (testing needed)
            }
        }
        */
        public void TurnAlarmOn()
        {
            EnterChaseState();                          // Needed as just turning the alarm to TRUE, doesn't seem to work at all.
            StartCoroutine(CheckDistance());            // Needed as a check if the Drone has been set to chase and Bip is not within its detection sphere.
        }
    }
}