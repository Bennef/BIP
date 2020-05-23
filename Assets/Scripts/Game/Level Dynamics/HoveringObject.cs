﻿using UnityEngine;

public class HoveringObject : MonoBehaviour 
{
    // ----------------------------------------------- Data members ----------------------------------------------
    /*		Summary
	 * 		This class simply makes an object hover using Physics
	 * 		hovering objects require a rigidbody that is using gravity.
	 */
    public GameObject[] hoverPoints;		// Points to hover.
	public float hoverForce = 50f;			// Hover force. Just under the force of gravity.
	public float hoverHeight = 8.0f;		// Height to hover at. 
	public LayerMask layerMask;				// For the hovering.
	public Rigidbody rb;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Awake()
	{
		rb = transform.GetComponent<Rigidbody>();
	}
    // --------------------------------------------------------------------
    void FixedUpdate()
	{
		// Hovering.
		rb.useGravity = true;
		RaycastHit hit;
		for (int i = 0; i < hoverPoints.Length; i++)
		{
			// Get a reference to the hoverpoints.
			var hoverPoint = hoverPoints[i];
			if (Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit, hoverHeight, layerMask)) 
			{
				//Debug.Log (Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)));
                //Debug.Log(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)));
				rb.AddForceAtPosition(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
			}
		}
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
