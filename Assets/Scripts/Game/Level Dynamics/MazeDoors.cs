using UnityEngine;

public class MazeDoors : MonoBehaviour {
    // ----------------------------------------------- Data members ----------------------------------------------
    public GameObject key;		// To check key type against door type.
	public bool isPassable;		// True if the door is passable.
	
	public GameObject keyHandler;		//to check key type against door type
	
	public enum eDoorType
	{
		RED,
		BLUE,
		YELLOW
	};
	
	public eDoorType doorType;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Use this for initialization
    void Start ()
    {	
		//doorType = eDoorType.RED;
		isPassable = false;
	}
    // --------------------------------------------------------------------
    void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Player")
		{
			SetPassable();
		}
	}
    // --------------------------------------------------------------------
    public void SetPassable()
	{
		// If player has a key. 
		if (doorType == eDoorType.RED && key.gameObject.GetComponent<KeyHandler>().playerHasKey == true)
		{
			// Let the player in.
			gameObject.GetComponent<Collider>().enabled = false;
			
			if (doorType == eDoorType.RED && keyHandler.gameObject.GetComponent<KeyHandler>().redKeys > 0)
			{
				//let the player in
				this.gameObject.GetComponent<Collider>().enabled = false;
				isPassable = true;
			}
			
			if (doorType == eDoorType.BLUE && keyHandler.gameObject.GetComponent<KeyHandler>().blueKeys > 0)
			{
				//let the player in
				this.gameObject.GetComponent<Collider>().enabled = false;
				isPassable = true;
			}
			
			if (doorType == eDoorType.YELLOW && keyHandler.gameObject.GetComponent<KeyHandler>().yellowKeys > 0)
			{
				//let the player in
				this.gameObject.GetComponent<Collider>().enabled = false;
				isPassable = true;
			}
		}
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}