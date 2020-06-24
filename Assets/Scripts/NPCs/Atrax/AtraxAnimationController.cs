using UnityEngine;

namespace Scripts.NPCs.Atrax
{
    public class AtraxAnimationController : MonoBehaviour
    {
        private Rigidbody _rb;
        private Animator _anim;
        private AtraxStates _state;

        // Use this for initialization
        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _anim = GetComponent<Animator>();
            _state = GetComponent<AtraxStates>();
        }

        // Update is called once per frame
        void Update()
        {
            // If Atrax is moving, and is currently grounded, he's walking.
            //if (rigidbody.velocity.x > 0f || rigidbody.velocity.z > 0f)
            // {
            //StartMovingState();
            // }
            // else    // Otherwise he's not walking.
            // {
            //    StopMovingState();
            //}
        }

        public void StartMovingState() => _anim.SetBool(_state.isWalkingBool, true);

        public void StopMovingState() => _anim.SetBool(_state.isWalkingBool, false);

        public void StartSwipeState() => _anim.SetBool(_state.isSwipingBool, true);

        public void StopSwipeState() => _anim.SetBool(_state.isSwipingBool, false);

        public void StartJumpState() => _anim.SetBool(_state.isJumpingBool, true);

        public void StopJumpState() => _anim.SetBool(_state.isJumpingBool, false);

        public void StartTurningLeftState() => _anim.SetBool(_state.isJumpingBool, true);

        public void StopTurningLeftState() => _anim.SetBool(_state.isJumpingBool, false);

        public void StartTurningRightState() => _anim.SetBool(_state.isJumpingBool, true);

        public void StopTurningRightState() => _anim.SetBool(_state.isJumpingBool, false);
    }
}