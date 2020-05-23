using UnityEngine;

public class DamageByCollision : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public PlayerHealth player;     // A reference to the player.
    public CharacterController charController;
    public bool isOn;
    public enum damageType
    {
        continuous,
        instantHit
    }

    public damageType type;         // To choose the damage type.
    public int damage;              // The amount of health to remove.
    // ----------------------------------------------- End Data members ------------------------------------------
    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Start()
    {
        player = GameObject.Find("Bip").GetComponent<PlayerHealth>();
        charController = GameObject.Find("Bip").GetComponent<CharacterController>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (type == damageType.instantHit && !charController.isDead && isOn)
        {
            if (col.gameObject.tag == "Player" && this.transform.GetComponent<Transform>().gameObject.activeSelf == true)
            {
                player.TakeDamage(damage);  // Player takes damage. 
            }
        }
    }
    // --------------------------------------------------------------------
    void OnCollisionStay(Collision col)
    {
        if (type == damageType.continuous && !charController.isDead && isOn)
        {
            if (col.gameObject.tag == "Player" && this.transform.GetComponent<Transform>().gameObject.activeSelf == true)
            {
                player.TakeDamage(damage);  // Player takes damage.
            }
        }
    }
}
// --------------------------------------------------------------------
// --------------------------------------------------- End Methods --------------------------------------------
