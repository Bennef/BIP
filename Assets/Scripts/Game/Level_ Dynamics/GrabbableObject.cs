using Scripts.Player;
using UnityEngine;

namespace Scripts.Game.Level_Dynamics
{
    public class GrabbableObject : MonoBehaviour
    {
        public bool isGrabbable;            // True if Bip is able to grab this object.
        public bool isGrabbing = false;     // True if Bip is already grabbing the object.
        private Rigidbody _grabbableRigidbody;
        private Rigidbody _player;           
        public LayerMask layerMask;         // To allow only the player to grab the object.

        public Vector3 closestGrabPoint;    // To store the grab point closest to Bip.

        public float grabbingMass;          // To change when object is grabbed.

        public Vector3 startingPosition;    // So we can reset the cubes position on death.
        private Player.CharacterController charController;

        public GrabbableObject[] otherCubes;
        public CapsuleCollider headCollider;  // Disable this when we grab.
        public Animator bipAnim;

        void Awake()
        {
            _grabbableRigidbody = GetComponent<Rigidbody>();
            startingPosition = transform.position;
            charController = GameObject.Find("Bip").GetComponent<Player.CharacterController>();
            headCollider = GameObject.Find("CTRL_Head").GetComponent<CapsuleCollider>();
            bipAnim = GameObject.Find("Bip").GetComponent<Animator>();
            _player = GameObject.Find("Bip").GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (Input.GetButtonDown("Grab"))
            {
                if (isGrabbable && !isGrabbing)
                {
                    headCollider.enabled = false;

                    // For each other cube...
                    foreach (GrabbableObject cube in otherCubes)
                        cube.isGrabbable = false;  // ... make sure Bip cannot grab it.
                    Grab(_player);
                }
            }

            if (Input.GetButtonUp("Grab"))
            {
                LetGo();
                headCollider.enabled = true;
            }

            if (_player)
            {
                if (!_player.GetComponent<Animator>().GetBool(_player.GetComponent<PlayerStates>().IsGroundedBool) && isGrabbing)
                    LetGo();
            }
        }

        public void Grab(Rigidbody _player)
        {
            if (!isGrabbing)
            {
                isGrabbing = true;
                _grabbableRigidbody.mass = grabbingMass;
                GetClosestGrabPoint();

                // Move Bip to the remaining stored point as it is the closest one to him.
                _player.transform.position = closestGrabPoint;

                // Make Bip look at the centre of the object.
                Vector3 targetPosition = new Vector3(transform.position.x, _player.position.y, transform.position.z);
                _player.transform.LookAt(targetPosition);

                // When grabbed, add a FixedJoint component to this object.
                gameObject.AddComponent<FixedJoint>();
                /* Then we connect this joint to the _player's rigidbody.
                This connects this objects movement to that of the players, this goes both ways
                so this object can also pull the player if it moves due to something else.
                */
                gameObject.GetComponent<FixedJoint>().connectedBody = _player;
                _player.velocity = Vector3.zero;
                _player.gameObject.GetComponent<Animator>().SetBool(_player.gameObject.GetComponent<PlayerStates>().IsPushingBool, true);
            }
        }

        public Vector3 GetClosestGrabPoint()
        {
            float smallestdistance = 1000;  // The smallest distance between Bip and Grabbable points around the object. Initialise to a high number.
            float currentDistance;          // The distance from Bip to the current point.

            // Loop through all the children in the object. They are grabbable points that Bip can hold on to.
            foreach (Transform point in transform)
            {
                // Measure the distance between Bip and the point.
                currentDistance = Vector3.Distance(_player.transform.position, point.transform.position);

                // If the current point distance is smaller than the current one then overwrite the variable with it.
                if (currentDistance < smallestdistance)
                {
                    smallestdistance = currentDistance;             // Store this current distance in the smallest distance variable.
                    closestGrabPoint = point.transform.position;    // Store the Vector3 point location.
                }
            }
            return closestGrabPoint;
        }

        public void LetGo()
        {
            isGrabbing = false;
            _grabbableRigidbody.mass = 1000;

            // When this object isn't being grabbed, we destroy the joint.
            if (gameObject.GetComponent<FixedJoint>())
                Destroy(gameObject.GetComponent<FixedJoint>());

            // Player control shit.
            if (_player)
                _player.gameObject.GetComponent<Animator>().SetBool(_player.gameObject.GetComponent<PlayerStates>().IsPushingBool, false);
            //isGrabbable = true; // not sure if we need this
        }

        void OnTriggerStay(Collider other)
        {
            // If we collide with the player.
            if (other.gameObject.CompareTag("Player") && otherCubes.Length > 0)
            {
                foreach (GrabbableObject cube in otherCubes)
                {
                    if (cube.isGrabbable)
                        cube.isGrabbable = false;
                    if (other.gameObject.CompareTag("Player"))
                        isGrabbable = true;
                }
            }
        }

        public void OnTriggerExit(Collider other) => isGrabbable = false;

        public void ResetPosition() => transform.position = startingPosition;
    }
}