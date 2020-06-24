using UnityEngine;

namespace Scripts.NPCs.AI
{
    public class CoBotMind : FriendlyMind
    {
        public float brakingSpeed;          // Speed that CoBot brakes.
        public float hoverHeight;
        public Vector3 MaxVelocity;         // So CoBot does not fly too fast.
        public bool isControllable;         // True if the player is controlling CoBot.

        void Awake() => rigidbody = GetComponent<Rigidbody>();

        void FixedUpdate()
        {
            if (Camera.main)
            {
                float angle = Mathf.Abs(Vector3.Angle(Camera.main.transform.forward, transform.position - Camera.main.transform.position));
                onScreen = angle < Camera.main.fieldOfView;
            }

            Quaternion rotation = CalculateRotation();
            rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed));
            Move();
            if (shouldFollow)
                rigidbody.drag = 0.2f;
            else
                rigidbody.drag = 1.0f;  // So he doesn't float off into the distance.

            CapVelocity(); // Cap velocity to prevent super speeds.
            Unstuck(); // Get CoBot Unstuck, should he be stuck.
        }

        protected override void Move()
        {
            // Store a temp position so that CoBot will only take into account horizontal distance between itself and Bip.
            Vector3 tempPosition = transform.position;
            tempPosition.y = target.position.y;
            // Get the horizontal distance between CoBot and target
            float currentDistance = Vector3.Distance(tempPosition, target.position);

            if (currentDistance > PreferredDistance)
            {
                shouldFollow = true;
                // If CoBot is too far away, move in.
                rigidbody.velocity = transform.forward * speed;
            }
            else if (currentDistance > MinDistance)
            {
                // If CoBot is at a respectful distance, apply the brakes.
                rigidbody.velocity = new Vector3(rigidbody.velocity.x * (1 - brakingSpeed / 100),
                    rigidbody.velocity.y, rigidbody.velocity.z * (1 - brakingSpeed / 100));
                shouldFollow = false;
            }
            else
            {
                // If CoBot is too close, move away.
                rigidbody.velocity = -transform.forward * speed;
                shouldFollow = false;
            }

            if (transform.position.y < target.position.y)
            {
                // Maintain an elevation similar to Bip's head.
                rigidbody.AddForce(Vector3.up * (target.position.y - transform.position.y), ForceMode.VelocityChange);
            }
        }

        void CapVelocity()
        {
            Vector3 _velocity = GetComponent<Rigidbody>().velocity;
            _velocity.x = Mathf.Clamp(_velocity.x, -MaxVelocity.x, MaxVelocity.x);
            _velocity.y = Mathf.Clamp(_velocity.y, -MaxVelocity.y, MaxVelocity.y);
            _velocity.z = Mathf.Clamp(_velocity.z, -MaxVelocity.z, MaxVelocity.z);
            rigidbody.velocity = _velocity;
        }
    }
}