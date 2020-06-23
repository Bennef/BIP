using UnityEngine;
using System.Collections;

public class DroneLaser : MonoBehaviour 
{
	// Handles drone lasers.
	public Transform target;			// Bip will be the target.

	public float fireRate;				// How quickly lasers fire.
	public Vector3 startPosition;		// Start position of drone.
		
	public bool inChaseRange = false;	// True if Bip is in chasing range.
	public bool droneJammed;			// True if near a signal jammer.
	public bool isFiring = false;		// True if currently firing.

	GameObject prefab;					// The laser pulse.

	private AudioSource audioSource;
	
	// Use this for initialization.
	void Start() 
	{
		prefab = Resources.Load("laser_impulse_projectile_001") as GameObject;
		audioSource = GetComponent<AudioSource>();
	}
	
	void FixedUpdate()
	{
        if (GameManager.Instance.IsPaused)
            return;
		// If Bip is not in range.
        if (!droneJammed && inChaseRange)
		{
			transform.rotation = Quaternion.LookRotation (target.position - transform.position);
			if (isFiring == false)
				StartCoroutine ("FireLaserCo");
		}
	}
	
	public IEnumerator FireLaserCo()
	{
		isFiring = true;
		yield return new WaitForSeconds(fireRate);

		FireLaser();
		isFiring = false;
	}
	
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

		// Play the sound. 
		audioSource.Play();
	}
}