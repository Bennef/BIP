using UnityEngine;
using System.Collections;

public class SwitchedWallLasers : MonoBehaviour 
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public GameObject[] lasers;        // An array to store the lasers.
    public LaserButton buttonScript;   // Reference to the button script.
    // ----------------------------------------------- End Data members ------------------------------------------
    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Turn all the lasers on.
    public void TurnLasersOn()
    {
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(true);
        }
    }
    // --------------------------------------------------------------------
    // Turn all the lasers off.
    public void TurnLasersOff()
    {
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false);
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
