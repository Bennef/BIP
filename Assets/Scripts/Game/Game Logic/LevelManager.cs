using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{	
	// ----------------------------------------------- Data members ----------------------------------------------
	// Will manage spawning of Bip in the Level.
	public GameObject currentCheckpoint;       		    // To access the current check point.
	private PlayerMovement playerMovementScript;	    // To access the player.
	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	// Use this for initialization.
	void Start() 
	{	
		playerMovementScript = FindObjectOfType<PlayerMovement>();
	}
	// --------------------------------------------------------------------
	// Respawn Bip at the current Checkpoint.
	public void RespawnPlayer()
	{
		playerMovementScript.transform.position = currentCheckpoint.transform.position;
    }
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}
