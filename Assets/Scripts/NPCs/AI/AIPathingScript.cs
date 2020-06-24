using UnityEngine;

namespace Scripts.NPCs.AI
{
    public class AIPathingScript : MonoBehaviour
    {
        public string currentState;
        public float smooth;
        public float resetTime;
        public bool performingIdleAction = false;
        public Vector3 newPosition; // New postion enemy is moving towards.
        public Vector3 nextDestination; // Next intended position enemy will be moved towards. Used to compare with overwritten path positions.
        public GameObject pathGroup;
        public PathingTypes pathType;

        public enum PathingTypes
        {
            Default,
            Retrace,
            RandomDirect,
            RandomAround,
            RandomWithin,
        };

        private Transform[] pathPosition; // Positions of the path points.
        private BoxCollider[] pathArea; // Defines the area the enemy can move in.
        private SphereCollider[] pathRadius; // How far the enemy can travel away from its path point.

        private int nextPosID; // Holds the array ID of the current path point.
        private int incDir; // Used when calculating "Retrace" pathing for changing direction.

        void Awake()
        {
            // Assign Pathing Route...
            if (pathGroup == null)
                Debug.LogError("No pathing group assigned to object");
            else
            {
                pathArea = pathGroup.GetComponentsInChildren<BoxCollider>();
                pathPosition = pathGroup.GetComponentsInChildren<Transform>();
                pathRadius = pathGroup.GetComponentsInChildren<SphereCollider>();
            }

            // Error Checking...
            switch (pathType)
            {
                case PathingTypes.Default:
                case PathingTypes.Retrace:
                case PathingTypes.RandomDirect:
                    break;
                case PathingTypes.RandomAround:
                    if (pathPosition.Length > pathRadius.Length) Debug.LogError("Path is missing a radius (SphereCollider)");
                    break;
                case PathingTypes.RandomWithin:
                    if (pathPosition.Length > pathArea.Length) Debug.LogError("Path is missing an area (BoxCollider)");
                    break;
            }
        }

        // Use this for initialization
        void Start()
        {
            switch (pathType)
            {
                case PathingTypes.Default:
                    nextPosID = -1;
                    break;
                case PathingTypes.Retrace:
                    nextPosID = -1;
                    incDir = 1;
                    break;
                case PathingTypes.RandomDirect:
                    nextPosID = -1; //Random.Range(0, pathPosition.Length);
                    break;
                case PathingTypes.RandomAround:
                    //pathRadius = pathGroup.GetComponentsInChildren<SphereCollider>();
                    break;
                case PathingTypes.RandomWithin:
                    //pathArea = pathGroup.GetComponentInChildren<BoxCollider>();
                    break;
            }
            // Start invoking method...
            ChangeTarget();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!performingIdleAction)
            {
                if (transform.position == nextDestination)
                    ChangeTarget();
                else if (transform.rotation.y != Quaternion.LookRotation(nextDestination - transform.position).y)// || transform.rotation.y > Quaternion.LookRotation(nextDestination - transform.position).y + 1.0f)
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(nextDestination - transform.position), Time.deltaTime * 80.0f);
                else
                    transform.position = Vector3.MoveTowards(transform.position, newPosition, smooth * Time.deltaTime);
            }
        }

        // Changes the moving platform target.
        void ChangeTarget()
        {
            performingIdleAction = false; // No longer preforming idle actions

            // Changes targeted direction based on path settings
            switch (pathType)
            {
                case PathingTypes.Default:  // Goes through each point and then returns back to its original positoin.
                    if (++nextPosID == pathPosition.Length)
                        nextPosID = 0;
                    break;
                case PathingTypes.Retrace:  // Goes through each point and then follows the route back.
                    nextPosID += incDir;
                    if (nextPosID < pathPosition.Length || nextPosID < 0)
                    {
                        incDir *= -1;
                        nextPosID += incDir;
                    }
                    break;
                case PathingTypes.RandomDirect: // Goes towards a randomly selected point and then ANOTHER on each pass.
                    int posID = Random.Range(0, pathPosition.Length);
                    while (posID == nextPosID)
                        posID = Random.Range(0, pathPosition.Length);
                    nextPosID = posID;
                    break;
                case PathingTypes.RandomAround: // Goes towards a position within the radius a random path point.
                    Vector3 pathDirection = Random.insideUnitSphere;        // Finds random direction
                    pathDirection.y = 0;
                    nextPosID = Random.Range(0, pathPosition.Length);       // Finds a random path point
                    newPosition = pathPosition[nextPosID].position + pathDirection * pathRadius[nextPosID].radius * Random.value;
                    break;
                case PathingTypes.RandomWithin: // Goes towards a position within the area defined by the path points.
                    nextPosID = Random.Range(0, pathPosition.Length);
                    Vector3 pathLocation = pathArea[nextPosID].size;
                    pathLocation.Set(Random.Range(0, pathLocation.x),
                                     Random.Range(0, pathLocation.y),
                                     Random.Range(0, pathLocation.z));
                    newPosition = pathPosition[nextPosID].position + pathLocation;
                    break;
            }

            // Set new position to head towards.
            if (pathType != PathingTypes.RandomAround &&
               pathType != PathingTypes.RandomWithin)
                newPosition = pathPosition[nextPosID].position;

            // Update State Debugging...
            nextDestination = newPosition;
            currentState = "Moving To: " + nextDestination;
        }
    }
}