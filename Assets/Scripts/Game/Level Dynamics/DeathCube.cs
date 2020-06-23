using UnityEngine;

// Fall death.
public class DeathCube : MonoBehaviour 
{
    void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Bip") 
		{
            other.GetComponent<CharacterController>().StartFallDeath();
		}
	}
}