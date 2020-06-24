using UnityEngine;

namespace Scripts.Player
{
    public class BipAnimation : CharacterAnimationController
    {
        // Update is called once per frame
        void Update()
        {
            // If Bip is moving, and is currently grounded, he's running
            if (state.Velocity.magnitude > 0 && anim.GetBool(state.IsGroundedBool))
                StartRunState();
            else    // Otherwise he's not running
                StopRunState();
            // If Bip is moving up, and is not grounded, he's rising!
            if (GetComponent<Rigidbody>().velocity.y > 0.01f && !anim.GetBool(state.IsGroundedBool))
                StartRiseState();
            else if (GetComponent<Rigidbody>().velocity.y < 0.01f || anim.GetBool(state.IsGroundedBool))
                StopRiseState(); // If we're moving down and we aren't grounded, he's falling (not rising.)
        }
    }
}