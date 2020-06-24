using UnityEngine;

namespace Scripts.UI
{
    public class CoBotMainMenu : MonoBehaviour
    {
        public Transform target;            // Bip will be the target.
        public float hSpeed;                // Speed when chasing.
        public float vSpeed;                // Speed of hovering.
        public float amplitude;             // Height difference of hovering.
        public float damping = 5.0f;        // For chasing.
        public Vector3 startPosition;       // Start position of drone.
        public Vector3 tempPosition;        // For hovering.

        // Use this for initialization.
        void Start() => tempPosition = transform.position;

        // Update is called once per frame
        void Update()
        {
            Vector3 temp = Input.mousePosition;
            temp.z = 100.0f;
            target.transform.position = Camera.main.ScreenToWorldPoint(temp);

            // Make CoBot look at Mouse.
            transform.LookAt(target);

            // Move CoBot toward Mouose.
            transform.Translate(Vector3.forward * Time.deltaTime * hSpeed);
        }

        void FixedUpdate()
        {
            // Hover in current position.
            tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * vSpeed) * amplitude + 420;
            tempPosition.x = transform.position.x;
            tempPosition.z = transform.position.z;
            transform.position = tempPosition;
        }
    }
}