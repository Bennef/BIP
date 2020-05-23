using UnityEngine;
using System.Collections;

public class Camera2DforDumpsterJacob : MonoBehaviour 
{
	// ----------------------------------------------- Data members ----------------------------------------------
	// The special camera for the dumpster level. This acts like a 2D camera and does not rotate.
	Vector3 velocity;

	public float minX, maxX, maxY, minY;
	public float minZ;
	public float maxZ;
	public float CamDistance;
	public float smoothTimeX, smoothTimeY, smoothTimeZ;
	public GameObject player;		// The player object (Bip).
	public float posX, posY, posZ;	// The position values for the camera.
	public Vector3 offset;			// The offset for the camera position from Bip.
	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		posZ = Mathf.SmoothDamp (transform.position.z, player.transform.position.z + 8f , ref velocity.z, smoothTimeZ);
		posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y + 4f, ref velocity.y, smoothTimeY);
		transform.position = new Vector3 (posX, posY, posZ);
	}
	// --------------------------------------------------------------------
	void Update()
	{
		if (player.transform.position.x > minX && player.transform.position.x < maxX) 
		{
			posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		}

		if (player.transform.position.z > minZ && player.transform.position.z < maxZ) 
		{
			posZ = Mathf.SmoothDamp (transform.position.z, player.transform.position.z + CamDistance , ref velocity.z, smoothTimeZ);
		}

		if (player.transform.position.y > -55f && player.transform.position.y < 22f) 
		{
			posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y + 2f , ref velocity.y, smoothTimeZ);
		}
 		//posY = Mathf.SmoothDamp (transform.position.y, BipPlayer.transform.position.y + 2f, ref velocity.y, smoothTimeY);

		transform.position = new Vector3 (posX, posY, posZ);
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}
