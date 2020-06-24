using Scripts.NPCs.AI;
using UnityEngine;

namespace Scripts.NPCs.Atrax
{
    public class AtraxMind : Mind
    {
        [Tooltip("Turning Speed")]
        public float turningSpeed;
        private AtraxStates _state;
        private Animator _anim;
        public BoxCollider swipeCollider;

        // Use this for initialization
        void Awake()
        {
            _state = GetComponent<AtraxStates>();
            _anim = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            Quaternion rotation = CalculateRotationOnlyY();
            Move();
        }

        protected override void Move()
        {
            if (_anim.GetBool("isWalking") == true)
            {
                Debug.Log("walking");
            }
        }
    }
}