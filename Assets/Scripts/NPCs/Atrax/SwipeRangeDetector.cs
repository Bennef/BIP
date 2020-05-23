using UnityEngine;

public class SwipeRangeDetector : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public AtraxAnimationController anim;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.StartSwipeState();
        }
    }
    // --------------------------------------------------------------------
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.StopSwipeState();
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
