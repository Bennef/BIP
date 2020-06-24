using UnityEngine;
using System.Collections.Generic;

namespace Scripts.Game.Level_Dynamics
{
    public class FollowPath : MonoBehaviour
    {
        public enum FollowType
        {
            MoveTowards,
            Lerp,
            Slerp
        }

        public bool isActive;       // If true, the thing is working, if false, it does not move.

        public FollowType type = FollowType.MoveTowards;
        public PathDefinition path;
        public float speed = 1f;
        public float MaxDistanceToGoal = 0.1f;

        private IEnumerator<Transform> _currentPoint;

        protected new Rigidbody rigidbody;

        public void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

            if (path == null)
            {
                Debug.LogError("Path cannot be null", gameObject);
                return;
            }

            _currentPoint = path.GetPathsEnumerator();
            _currentPoint.MoveNext();

            if (_currentPoint.Current == null)
                return;
            // Platform starts on first point in the path
            transform.position = _currentPoint.Current.position;
        }

        public void FixedUpdate()
        {
            if (isActive)
            {
                if (_currentPoint == null || _currentPoint.Current == null)
                    return;

                if (type == FollowType.MoveTowards)
                {
                    // Built in Unity function. MoveTowards is kind of jerky
                    rigidbody.MovePosition(Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * speed));
                }
                else if (type == FollowType.Lerp)
                {
                    // whereas Lerp is smooth, but somewhat slower
                    rigidbody.MovePosition(Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * speed));
                }
                else if (type == FollowType.Slerp)
                    rigidbody.MovePosition(Vector3.Slerp(transform.position, _currentPoint.Current.position, Time.deltaTime * speed));

                float distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
                if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
                {
                    _currentPoint.MoveNext();
                }
            }
        }
    }
}