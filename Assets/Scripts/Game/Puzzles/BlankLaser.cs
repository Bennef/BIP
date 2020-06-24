using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class BlankLaser : MonoBehaviour
    {
        public bool activated = true;
        public Transform target;
        public float speed;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (activated)
                AimAtTarget2();
        }

        public void AimAtTarget()
        {
            Vector3 direction = target.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(transform.forward, direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.time);
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(transform.position, fwd * 50, Color.green);
        }

        public void AimAtTarget2() => transform.LookAt(target);
    }
}