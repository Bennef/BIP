using UnityEngine;

public class TimedWallLasers : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public float onTime;               // The time lasers are on for.
    public float offTime;              // The time lasers are off for.
    private float timer;               // Timer to time the blinking on and off.

    public GameObject[] laserBeams;    // An array to store the lasers.
    public bool isSwitchedOn;
    public bool isOn;                  // True if the lasers are on. 
    // ----------------------------------------------- End Data members ------------------------------------------
    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    private void Start()
    {
       if (isOn)
        {
            TurnLasersOn();
        }
        else
        {
            TurnLasersOff();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isSwitchedOn)
        {
            // Increment the timer by the amount of time since the last frame.
            timer += Time.deltaTime;

            // If the beam is on and the onTime has been reached...
            if (isOn && timer >= onTime)
            {
                // Switch the lasers off.
                TurnLasersOff();
            }
            // If the beam is off and the offTime has been reached...
            if (!isOn && timer >= offTime)
            {
                // Switch the lasers on.
                TurnLasersOn();
            }
        }
    }
    // --------------------------------------------------------------------
    // Turn all the lasers on.
    public void TurnLasersOn()
    {
        timer = 0f; // Reset the timer.

        foreach (GameObject laser in laserBeams)
        {
            // Turn laser on.
            laser.SetActive(!laser.activeInHierarchy);
        }
        isOn = true;
    }
    // --------------------------------------------------------------------
    // Turn all the lasers off.
    public void TurnLasersOff()
    {
        timer = 0f; // Reset the timer.

        foreach (GameObject laser in laserBeams)
        {
            // Turn laser off.
            laser.SetActive(false);
        }
        isOn = false;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
