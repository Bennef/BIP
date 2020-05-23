using UnityEngine;
using System.Collections;

/* _____________________________________________
 * Information Regarding Units and Distances
 * ---------------------------------------------
 * 10cm = ~1.4 Units in Unity
 * attackRange has to been > chaseLimit and < detectionRange.
 * _____________________________________________
 * Outlined Brief: -----------------------------
 * 
-	Traverse on a set path.
-	Will sound an alarm if the player comes within their line of sight.
-	Will shock the player if in their line of sight and within a 6cm distance.

 */

public class PatrolBotMind : MindActor 
{	
	// Unique PatrolBot Variables -------------------------------------------------
	// ----------------------------------------------------------------------------

	public Transform target;
	public float attackRate = 2.0f;				// Rate at which the PatrolBot will shock Bip at in Seconds.
	public float attackRange = 1.5f;			// Range at which the PatrolBot will shock its target.
	public float detectionRange = 6.0f;			// Range at which the target will be detected.
	public float detectionRadius = 60.0f;		// Radius of cone infront of the PatrolBot to determin Line of Sight.
	public float scanSpeed = 30.0f;				// Speed of rotation during idle scanning.
	public IdleFeatureTypes idleFeature;		// Current idle feature for when PatrolBot reaches a destination.

	private bool shockReady = true;				// If PatrolBot is ready to send another Shock at its target.
	private float alarmRange;					// Range of the alarm. (Sphere Collider Radius)
	public bool inLineOfSight = false;			// If Bip is in line of sight.
	private Vector3 previousPath;				// For storing last position before chasing target.
	private State currentState;					// Current state of the PatrolBots mind.
	private PlayerHealth healthScript;			// Player health script to modify values when Shocked().
    private PlayerMovement playerMovementScript;

    private Light alarmLight;

    //Shock = GameObject.Find("Shock");

    public AudioSource aSrc;
    public AudioClip alarm, zap;

	private enum State 
    {
		IDLE,
	};

	public enum IdleFeatureTypes 
    {
		NONE,
		SCAN,
		HOVER,
	};

	private float scanAngle;
	private uint scanCount = 0;
	private float hoverTimer = 270;

	// Quick fix for MindController issues ----------------------------------------
	// ----------------------------------------------------------------------------
	void Awake()
	{
		pathingScript = GetComponent<AIPathingScript>();
		alarmRange = GetComponent<SphereCollider>().radius;
        target = GameManager.Instance.Player;
		healthScript = target.GetComponent<PlayerHealth>();
        playerMovementScript = target.GetComponent<PlayerMovement>();

		// Preset Variables
		currentState = State.IDLE;
        aSrc = GetComponent<AudioSource>();
        alarmLight = GetComponentInChildren<Light>();
        alarmLight.enabled = false;
	}

	void Update()
	{
		if (!Think())
		{
			Destroy();
		}
	}

	// Overriden Method Implementations for PatrolBot -----------------------------
	// ----------------------------------------------------------------------------

	// Main "Thought" process of the AI. Return 'false' if the bot has died.
	public override bool Think()
	{
		switch (currentState) 
        {
			case State.IDLE:	IdleFeatures();	break;

			// Error Handling...
			default: Debug.LogError ("Invalid BotState. Check if currentState is defined.");
				break;
		}

		return true;
	}

	// Used for handling any end state behaviour.
	public override void Destroy()
	{
	}

	// Unique PatrolBot Method Implemenations -------------------------------------
	// ----------------------------------------------------------------------------
	void OnTriggerStay(Collider other)
	{
		// Check if Bip is in line of sight.
		if (other.tag == Tags.Enemies) 
		{	
			// Check if drones are within Alarm range.
			if (inLineOfSight && other.transform.GetComponent<DroneBotMind>() != null) 
			{
				other.gameObject.GetComponent<DroneBotMind>().TurnAlarmOn ();
			}
            alarmLight.enabled = true;
			DetectionCheck ();
		}
	}

	void DetectionCheck()
	{
		// Distance from Bip.
		float dist = Vector3.Distance (target.position, transform.position);
		//inLineOfSight = false;

		// Attack target if in range and have Line of Sight.
		if (inLineOfSight && shockReady && dist < attackRange)
		{
			//StartCoroutine (AttackTarget (attackRate));
		}
	}

	IEnumerator AttackTarget(float waitTime)
	{
		// Decrease Bip health 20% per shock.
		shockReady = false;
		healthScript.TakeDamage(64);
        aSrc.PlayOneShot(zap);
        playerMovementScript.Knockback(1000);

		yield return new WaitForSeconds(waitTime);
		shockReady = true;
	}

	void IdleFeatures()
	{
		if(idleFeature != IdleFeatureTypes.NONE && (pathingScript.nextDestination == transform.position || pathingScript.preformingIdleAction))
		{
			if(!pathingScript.preformingIdleAction)
			{
				pathingScript.preformingIdleAction = true;

				switch(idleFeature)
				{
				case IdleFeatureTypes.HOVER:
					hoverTimer = 270.0f;
					StartCoroutine(DelayChangeTarget(5.0f));
					break;

				case IdleFeatureTypes.SCAN:
					scanAngle = transform.eulerAngles.y;
					break;
				}
			}

			switch(idleFeature)
			{
			case IdleFeatureTypes.SCAN:		Scanning();		break;
			case IdleFeatureTypes.HOVER:	Hovering();		break;
			}
		}
	}

	// Idle Feature List ---------------------------------------------------------
	// ---------------------------------------------------------------------------
	void Scanning()
	{
		if(transform.eulerAngles.y < scanAngle + 0.1f && transform.eulerAngles.y > scanAngle - 0.1f)
		{
			scanAngle = Random.Range(0.0f, 360.0f);			// Angle to scan towards.

            if (++scanCount > 3)        // How many scans happen before moving onto next position.
            {							
				scanCount = 0;
				pathingScript.preformingIdleAction = false;
				//ChangeTarget();
			}
		}

/*		if (transform.rotation.y == scanAngle) 			Debug.Log ("Rotation - ");
		if (transform.eulerAngles.y == scanAngle)		Debug.Log ("Euler Angles - ");
		if (transform.localRotation.y == scanAngle)		Debug.Log ("Local Rotation - ");
		if (transform.localEulerAngles.y == scanAngle)	Debug.Log ("Local Euler Angles - ");
*/
		transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.AngleAxis(scanAngle, Vector3.up), scanSpeed*Time.deltaTime);
	}

	void Hovering()
	{
		transform.position = new Vector3(transform.position.x, 2.4f + 1.0f * Mathf.Sin (1.0f * hoverTimer), transform.position.z);
		hoverTimer += Time.deltaTime;
	}

	IEnumerator DelayChangeTarget(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if (idleFeature == IdleFeatureTypes.HOVER) 
        {
			//Vector3 tempPos = transform.position;
			//tempPos.y = 1.4f;
			transform.position = pathingScript.nextDestination;
		}
		pathingScript.preformingIdleAction = false;
		//ChangeTarget ();
	}
}
