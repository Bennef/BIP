using UnityEngine;

public class SlidingTile : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public bool isBeingStoodOn, canMove, isPlacedCorrectly;
    public Transform correctPos;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    private void Update()
    {
        if (transform.position == correctPos.position)
        {
            isPlacedCorrectly = true;
        }
        else
        {
            isPlacedCorrectly = false;
        }
    }
    // --------------------------------------------------------------------
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isBeingStoodOn = true;
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
