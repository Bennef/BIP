using UnityEngine;

public class CorrectPosition : MonoBehaviour 
{
	// DOESN'T WORK, NEED TO DO SOME READING


	// ----------------------------------------------- Data members ----------------------------------------------
	public BoxCollider levelBounds;
	private Rigidbody rigidbody;
	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	void Awake()
	{
		levelBounds = GameObject.FindGameObjectWithTag(Tags.levelBounds).GetComponent<BoxCollider>();
		rigidbody = GetComponent<Rigidbody>();
	}
	// --------------------------------------------------------------------
	void Update()
	{
		Vector3 _transform = new Vector3(0,0,0);
		if(transform.position.x > levelBounds.size.x || transform.position.x < -levelBounds.size.x ||
			transform.position.y > levelBounds.size.y || transform.position.y < -levelBounds.size.y ||
			transform.position.z > levelBounds.size.z || transform.position.z < -levelBounds.size.z)
		{
			// Correct x axis
			if(transform.position.x > levelBounds.size.x)
			{
				_transform.x = levelBounds.size.x;
			}

			if(transform.position.x < -levelBounds.size.x)
			{
				_transform.x = -levelBounds.size.x;
			}

			// Correct y axis
			if(transform.position.y > levelBounds.size.y)
			{
				_transform.y = levelBounds.size.y;
			}

			if(transform.position.y < -levelBounds.size.y)
			{
				_transform.y = -levelBounds.size.y;
			}

			// Correct z axis
			if(transform.position.z > levelBounds.size.z)
			{
				_transform.z = levelBounds.size.z;
			}

			if(transform.position.z < -levelBounds.size.z)
			{
				_transform.z = -levelBounds.size.z;
			}
			rigidbody.velocity = Vector3.zero;
			transform.position = _transform;
		}
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}
