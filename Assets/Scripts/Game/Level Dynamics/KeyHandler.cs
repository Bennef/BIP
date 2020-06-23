using UnityEngine;

public class KeyHandler : MonoBehaviour 
{
	// BF - we should integrate this class with the Inventory class and have the unlocking functionality on the doors.
	public GameObject key;		// To access the key type.
	public MazeDoors door;		// To access the door type.
	public bool playerHasKey;	// Debug stuff - to see if player should be able to pass or not.

	public int redKeys = 0;
	public int blueKeys = 0;
	public int yellowKeys = 0;
	
	// Update is called once per frame
	void Update() => OpenDoor();

	public void OpenDoor()
	{
		// If key type is one specified then door should open.
		if (key.gameObject.GetComponent<KeyBehaviour>().keyType == KeyBehaviour.eKeyType.RED) 
		{
			playerHasKey = true;
			door.SetPassable();
		}
	}
}