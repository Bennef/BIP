using UnityEngine;
using UnityEngine.Timeline;

public class PlayTimelineOnEnter : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public DirectorControlPlayable timelinePop;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bip")
        {
            // Play the timeline.
            timelinePop.director.Play();
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
