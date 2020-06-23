using UnityEngine;

public class LevelManager : MonoBehaviour 
{	
	// Will manage spawning of Bip in the Level.
	public GameObject currentCheckpoint;       		    // To access the current check point.
	private PlayerMovement playerMovementScript;	    // To access the player.
	
	// Use this for initialization.
	void Start() => playerMovementScript = FindObjectOfType<PlayerMovement>();

	// Respawn Bip at the current Checkpoint.
	public void RespawnPlayer() => playerMovementScript.transform.position = currentCheckpoint.transform.position;
}