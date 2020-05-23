using UnityEngine;

public class JumpRangeDetector : MonoBehaviour
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
            anim.StartJumpState();
        }
    }
    // --------------------------------------------------------------------
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.StopJumpState();
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
