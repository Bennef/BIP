using Scripts.Player;
using UnityEngine;

namespace Scripts.Game.Level_Dynamics
{
    public class DamageByCollision : MonoBehaviour
    {
        public PlayerHealth player;  
        public Player.CharacterController charController;
        public bool isOn;

        public enum DamageType
        {
            continuous,
            instantHit
        }

        public DamageType type; // To choose the damage type.
        public int damage; // The amount of health to remove.

        void Start()
        {
            player = GameObject.Find("Bip").GetComponent<PlayerHealth>();
            charController = GameObject.Find("Bip").GetComponent<Player.CharacterController>();
        }

        void OnCollisionEnter(Collision col)
        {
            if (type == DamageType.instantHit && !charController.IsDead && isOn)
            {
                if (col.gameObject.CompareTag("Player") && transform.GetComponent<Transform>().gameObject.activeSelf == true)
                    player.TakeDamage(damage);  // Player takes damage. 
            }
        }

        void OnCollisionStay(Collision col)
        {
            if (type == DamageType.continuous && !charController.IsDead && isOn)
            {
                if (col.gameObject.CompareTag("Player") && transform.GetComponent<Transform>().gameObject.activeSelf == true)
                    player.TakeDamage(damage);  // Player takes damage.
            }
        }
    }
}