using Scripts.Game.Game_Logic;
using UnityEngine;

namespace Scripts.Game.Cameras
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private bool isFixed; // True if player can move camera.
        [SerializeField] private bool shouldReset = true; // True if the camera should reset it's position.
        [SerializeField] private Transform target; // What the camera will be looking at
        [SerializeField] private float distance = 10.0f; // How far the camera is from the target
        [SerializeField] private float xSpeed = 10.0f; // Longtitudal speed of camera
        [SerializeField] private float ySpeed = 10.0f; // Latitudal speed of camera
        [SerializeField] private float yMinLimit = 10f; // Clamp the y angle of the camera.
        [SerializeField] private float yMaxLimit = 80f; // Clamp the y angle of the camera.
        [SerializeField] private float xMinLimit = -360f; // Clamp the x angle of the camera.
        [SerializeField] private float xMaxLimit = 360f; // Clamp the x angle of the camera.
        [SerializeField] private float distanceMin = 0.5f; // Minimum distance camera can be from target
        [SerializeField] private float distanceMax = 10f; // Maximum distance camera can be from target
        [SerializeField] private float thinRadius = 0.15f;
        [SerializeField] private float thickRadius = 0.3f;
        [SerializeField] private LayerMask _layerMask;
        private Quaternion _rotation; // Local reference to rotation
        private float _x, _y = 0.0f; // angles of camera
        private Player.CharacterController _charController;

        public bool IsFixed { get => isFixed; set => isFixed = value; }
        public Transform Target { get => target; set => target = value; }
        public bool ShouldReset { get => shouldReset; set => shouldReset = value; }

        // Use this for initialization
        void Start()
        {
            Target = GameObject.Find("Camera Target").transform;
            _charController = GameObject.Find("Bip").GetComponent < Player.CharacterController>();
            Vector3 angles = transform.eulerAngles;
            _x = angles.y;
            _y = angles.x;
        }

        void LateUpdate()
        {
            if (Target && !GameManager.Instance.IsPaused && !_charController.IsDead && !IsFixed)        // Does target exist? (Not Null)
            {
                // Move the camera using mouse or joystick.
                CameraMove();
                _rotation = Quaternion.Euler(_y, _x, 0);   // Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).

                // Zoom out if we're not at maximum zoom distance.
                if (distance < distanceMax)
                    distance = Mathf.Lerp(distance, distanceMax, Time.deltaTime * 2f);
                // We'll declare a new Vector3 storing -distance. This will be 0 if we can see Bip, so nothing will happen.
                // However, if we can't see Bip, position.z will be equal to distance * -1, flipping it.
                Vector3 distanceVector = new Vector3(0.0f, 0.0f, -distance);
                // Camera follows target
                Vector3 position = _rotation * distanceVector + Target.position;
                transform.rotation = _rotation;
                transform.position = position;

                CameraCollision(); // Stop player from pushing Camera through walls.
            }

            // If we are loading a checkpoint, reset the position here...  
            if (ShouldReset && GameManager.Instance.currentCheckpoint != null)
            {
                _x = GameManager.Instance.currentCheckpoint.cameraStartX;
                _y = GameManager.Instance.currentCheckpoint.cameraStartY;
                ShouldReset = false;
            }
        }

        public void CameraMove()
        {
            // Offset the angles by the mouse, when the mouse is moved.
            _x += Input.GetAxis("Mouse X") * xSpeed;
            _y -= Input.GetAxis("Mouse Y") * ySpeed;
            _x += Input.GetAxis("Pad X") * xSpeed * 0.15f;
            _y -= Input.GetAxis("Pad Y") * ySpeed * 0.15f;

            _x = ClampAngle(_x, xMinLimit, xMaxLimit);
            _y = ClampAngle(_y, yMinLimit, yMaxLimit);
        }

        public float ClampAngle(float angle, float min, float max)
        {
            // Ensure that angle is between -360 and 360, because it is a float
            if (angle < -360F)
                angle += 360F;

            if (angle > 360F)
                angle -= 360F;
            // Then call Mathf.Clamp to actually clamp the angle.
            return Mathf.Clamp(angle, min, max);
        }

        void CameraCollision()
        {
            Vector3 normal, thickNormal;                            // Normal of the cast collisions.
            Vector3 occRay = transform.position - Target.position;  // Direction for the SphereCasts.

            // The position of the thin SphereCast collision.
            Vector3 colPoint = GetCollisionSimple(transform.position, thinRadius, out normal, true);
            // The position of the thick SphereCast collision.
            Vector3 colPointThick = GetCollisionSimple(transform.position, thickRadius, out thickNormal, false);
            // The position of the RayCast collision.
            Vector3 colPointRay = GetCollision(transform.position);

            // Collision Position from thick SphereCast, projected onto the RayCast.
            Vector3 colPointThickProjectedOnRay = Vector3.Project(colPointThick - Target.position, occRay.normalized) + Target.position;
            // Direction to push the camera.
            Vector3 vecToProjected = (colPointThickProjectedOnRay - colPointThick).normalized;
            // Thick Collision Position projected onto thin SphereCast.
            Vector3 colPointThickProjectedOnThinCapsule = colPointThickProjectedOnRay - vecToProjected * thinRadius;
            // Distance between thick sphere and thin sphere collisions. Used to calculate where to push Camera.
            float thin2ThickDist = Vector3.Distance(colPointThickProjectedOnThinCapsule, colPointThick);
            float thin2ThickDistNorm = thin2ThickDist / (thickRadius - thinRadius);

            // Distance between target and thin sphere collision.
            float currentColDistThin = Vector3.Distance(Target.position, colPoint);
            // Distance between target and Thick sphere collision.
            float currentColDistThick = Vector3.Distance(Target.position, colPointThickProjectedOnRay);
            // Smoothly interpolating between distance and new distance.
            float currentColDist = Mathf.Lerp(currentColDistThick, currentColDistThin, thin2ThickDistNorm);

            // Thick point can be actually projected IN FRONT of the character due to double projection to avoid sphere moving through the walls
            // In this case we should only use thin point
            bool isThickPointIncorrect = transform.InverseTransformDirection(colPointThick - Target.position).z > 0;
            isThickPointIncorrect = isThickPointIncorrect || currentColDistThin < currentColDistThick;
            if (isThickPointIncorrect)
                currentColDist = currentColDistThin;

            // if currentColDist is smaller than distance, zoom in, otherwise, zoom out.
            if (currentColDist < distance)
                distance = currentColDist;
            else
                distance = Mathf.SmoothStep(distance, currentColDist, Time.deltaTime * 100 * Mathf.Max(distance * 0.1f, 0.1f));
            // Clamp distance to our min and max values.
            distance = Mathf.Clamp(distance, distanceMin, distanceMax);
            // Move the camera to avoid going through objects!!!!
            transform.position = Target.position + occRay.normalized * distance;

            if (Vector3.Distance(Target.position, colPoint) > Vector3.Distance(Target.position, colPointRay))
                transform.position = colPointRay;
        }

        Vector3 GetCollisionSimple(Vector3 cameraOptPos, float radius, out Vector3 normal, bool pushByNormal)
        {
            // Double Sphere Casting.
            // Length of the cast.
            float farEnough = 1;

            // Local reference to target.position
            Vector3 origin = Target.position;
            // Cast direction.
            Vector3 occRay = origin - cameraOptPos;
            // Dot product of transform.forward, and the ray.
            float dt = Vector3.Dot(transform.forward, occRay);
            if (dt < 0)
                occRay *= -1;

            // Project the sphere in an opposite direction of the desired character->camera vector to get some space for the real spherecast
            if (Physics.SphereCast(origin, radius, occRay.normalized, out RaycastHit occHit, farEnough, _layerMask))
                origin += occRay.normalized * occHit.distance;
            else
                origin += occRay.normalized * farEnough;

            // Do final spherecast with offset origin
            occRay = origin - cameraOptPos;
            if (Physics.SphereCast(origin, radius, -occRay.normalized, out occHit, occRay.magnitude, _layerMask))
            {
                normal = occHit.normal;

                if (pushByNormal)
                    return occHit.point + occHit.normal * radius;
                else
                    return occHit.point;
            }
            else
            {
                normal = Vector3.zero;
                return cameraOptPos;
            }
        }

        Vector3 GetCollision(Vector3 cameraOptPos)
        {
            // Local reference to target.position
            Vector3 origin = Target.position;
            // Direction for raycast
            Vector3 occRay = cameraOptPos - origin;

            RaycastHit hit;
            if (Physics.Raycast(origin, occRay.normalized, out hit, occRay.magnitude, _layerMask))
            {
                // Return position of hit + the normal of that hit, multiplied by a smoothing variable.
                return hit.point + hit.normal * 0.15f;
            }
            // or we just return the camera position we passed in.
            return cameraOptPos;
        }
    }
}