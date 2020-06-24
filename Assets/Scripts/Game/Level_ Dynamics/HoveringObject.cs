using UnityEngine;

namespace Scripts.Game.Level_Dynamics
{
    public class HoveringObject : MonoBehaviour
    {
        /*		Summary
         * 		This class simply makes an object hover using Physics
         * 		hovering objects require a rigidbody that is using gravity.
         */
        public GameObject[] hoverPoints;        // Points to hover.
        public float hoverForce = 50f;          // Hover force. Just under the force of gravity.
        public float hoverHeight = 8.0f;        // Height to hover at. 
        public LayerMask layerMask;             // For the hovering.
        public Rigidbody rb;

        void Awake() => rb = transform.GetComponent<Rigidbody>();

        void FixedUpdate()
        {
            rb.useGravity = true;

            for (int i = 0; i < hoverPoints.Length; i++)
            {
                // Get a reference to the hoverpoints.
                var hoverPoint = hoverPoints[i];
                if (Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out RaycastHit hit, hoverHeight, layerMask))
                {
                    //Debug.Log (Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)));
                    //Debug.Log(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)));
                    rb.AddForceAtPosition(Vector3.up * hoverForce * (1.0f - hit.distance / hoverHeight), hoverPoint.transform.position);
                }
            }
        }
    }
}