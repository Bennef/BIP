using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class Conveyor : MonoBehaviour
    {
        public bool isOn;
        public Transform targetPos, resetPos;
        public float speed;
        protected new Rigidbody rigidbody;

        void Start() => rigidbody = GetComponent<Rigidbody>();

        void FixedUpdate()
        {
            if (isOn)
                rigidbody.MovePosition(Vector3.MoveTowards(transform.position, targetPos.position, Time.deltaTime * speed));
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.GetComponent<Collider>().gameObject.name == "Reset Collider")
                transform.position = resetPos.position;
        }
    }
}