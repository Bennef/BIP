using UnityEngine;
using System.Collections;

public class PatrolBotAlarm : MonoBehaviour 
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public PatrolBotMind mind;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Awake()
    {
        mind = transform.GetComponentInParent<PatrolBotMind>();
    }
    // --------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.Player)
        {
            mind.inLineOfSight = true;
			mind.aSrc.PlayOneShot(mind.alarm);
        }
    }
    // --------------------------------------------------------------------
    void OnTriggerExit(Collider other)
    {
        if(other.tag == Tags.Player)
        {
            mind.inLineOfSight = false;
        }
    }
    // --------------------------------------------------------------------
}
