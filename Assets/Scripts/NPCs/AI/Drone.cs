using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour 
{
	// Handles drone behaviour. 
	public Transform target;			// Bip will be the target.
	public float hSpeed;				// Speed when chasing.
	public float vSpeed;				// Speed of hovering.
	public float amplitude;				// Height difference of hovering.
	public float damping;				// For damping the movment.
	public float fireRate;				// How quickly lasers fire.
	public Vector3 startPosition;		// Start position of drone.
	public Vector3 tempPosition;		// For hovering.
	
	public bool inChaseRange = false;	// True if Bip is in chasing range.
	public bool droneJammed;			// True if near a signal jammer.
	public bool isFiring = false;		// True if currently firing.

	GameObject prefab;					// The laser pulse.
	private float angle;				// For checking if drone is looking at bip.
	private float lastShotTime = float.MinValue;
	
	// Use this for initialization.
	void Start() 
	{
		tempPosition = transform.position;
		prefab = Resources.Load("laser_impulse_projectile_001") as GameObject;
	}
	
	// Update is called once per frame
	void Update() 
	{
		//CheckIfJammed();
		//Work out the direction you need to be facing to be moving towards the target
		if (target != null && inChaseRange == true && droneJammed == false) 
		{
			// Calculate angle to turn and face bip.
			Quaternion rotation = Quaternion.LookRotation (target.position - transform.position);
			
			// Actually turn in that direction - only on y axis. - not for demo - BF
			//rotation.x = 0.0f;
			//rotation.z = 0.0f;
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);
			
			// Move the drone toward bip. - We want this to be physics based so it doesn't go through stuff - BF
			//transform.Translate (Vector3.forward * Time.deltaTime * hSpeed);
		}
	}
	
	void FixedUpdate()
	{
		// If Bip is not in range.
		if (!droneJammed && !inChaseRange) 
		{
			// Hover menacingly in current position.
			tempPosition.y = (Mathf.Sin (Time.realtimeSinceStartup * vSpeed) * amplitude) + 9;
			tempPosition.x = transform.position.x;
			tempPosition.z = transform.position.z;
			transform.position = tempPosition;
		}
		else
		// If the drone is facing Bip...not finished - BF - try other method
		//float dot = Vector3.Dot(transform.forward, (target.transform.position -	transform.position).normalized);
		{
			//if (dot > 0.7f && Time.time > lastShotTime * (3.0f / fireRate)) 
			if (isFiring == false)
				StartCoroutine("FireLaserCo");
		}
	}
	
	public IEnumerator FireLaserCo()
	{
		isFiring = true;
		yield return new WaitForSeconds(fireRate);

		FireLaser();
		isFiring = false;
	}
	
	// If Bip is close enough, he is in range.
	void OnTriggerEnter(Collider c)
	{
		if (c.CompareTag("Player"))
		{

			// Start chasing bip.
			if (c.gameObject != null)
				inChaseRange = true;
		}
	}
	
	// If Bip is out of range.
	void OnTriggerExit(Collider c)
	{
		if (c.CompareTag("Player"))
		{
			// Stop chasing bip.
			if (c.gameObject != null)
				inChaseRange = false;
		}
	}
	
	// Jam drone if laser is jammed.
	//void CheckIfJammed()
	//{
		//if (laser.GetComponent<DroneLaser>().laserJammed == true) 
		//{
		//	droneJammed = true;
		//}
		//else 
		//{
		//	droneJammed = false;
		//}
	//}
	
	// Fire the laser at poor Bip.
	public void FireLaser()
	{
		// Create a projectile.
		GameObject projectile = Instantiate(prefab) as GameObject;

		// Position it in the right place.
		projectile.transform.position = transform.position;

		// Rotate the projectile to it's correct rotation.
		projectile.transform.rotation = transform.rotation;

		// Get the projectile's rigidbody.
		Rigidbody rb = projectile.GetComponent<Rigidbody>();

		// Make the damn thing fly towards target.
		rb.velocity = transform.forward * 40;
	}
}