using UnityEngine;

public class LaserProjectile : MonoBehaviour 
{
	public int damage = 64;					// The amount of damage the laser causes. Set it here instead of editor.
	GameObject prefab;
	private AudioSource audioSource;
	public GameObject spark;
		
	// Use this for initialization
	void Start() 
	{
		audioSource = GetComponent<AudioSource>();
		prefab = Resources.Load("laser_impulse_flare_003") as GameObject;
	}
	
	// If laser collides with something.
	void OnCollisionEnter(Collision c)
	{
		// If the collision is with Bip.
		if (c.collider.CompareTag("Player")) 
		{
			// Then we hurt him
			if (c.gameObject != null) 
			{
				// Deal some damage to him.
				c.transform.GetComponent<PlayerHealth>().TakeDamage(damage);
				// and knock him back
				//c.collider.GetComponent<PlayerMovement>().Knockback(1000);
			}
		}
        // If the collision is with anything else OTHER than an Enemy
        // If this is undesirable, i'll write some logic so that it only does nothing on a collision
        // with the drone who made it.
        if (c.transform.CompareTag(Tags.Enemies))
		{
			// Play the sound. 
			//audioSource.Play();
			// Get the contact point of the collision.
			ContactPoint cp = c.contacts[0];

			GameObject.Instantiate(spark, cp.point, Quaternion.identity);
			// Destroy the laser projectile when it hits something, other than itself
			Destroy(this.gameObject);
		}
	}	
}