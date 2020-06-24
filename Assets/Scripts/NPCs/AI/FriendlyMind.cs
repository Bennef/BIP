using Scripts.Game.Game_Logic;
using UnityEngine;

namespace Scripts.NPCs.AI
{
    public class FriendlyMind : Mind
    {
        public float maxDistance;
        [SerializeField]
        protected bool onScreen;

        protected new Rigidbody rigidbody;

        void Awake() => rigidbody = GetComponent<Rigidbody>();

        protected void Unstuck()
        {
            // Check if the distance between Buddy and player is greater than max allowed distance.
            if (Vector3.Distance(target.position, transform.position) >= maxDistance)
            {
                // Check if character is visible to camera
                if (Physics.Linecast(transform.position + raycastOffset, Camera.main.transform.position, GameManager.Instance.ObstacleAvoidanceLayerMask, QueryTriggerInteraction.Ignore) || !onScreen)
                {
                    Vector3 newPos;
                    newPos = Camera.main.transform.position - Camera.main.transform.forward;
                    rigidbody.position = newPos;
                }
            }
        }
    }
}