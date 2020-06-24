using UnityEngine;

namespace Scripts.Game.Level_Dynamics
{
    // Fall death.
    public class DeathCube : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Bip")
                other.GetComponent<Player.CharacterController>().StartFallDeath();
        }
    }
}