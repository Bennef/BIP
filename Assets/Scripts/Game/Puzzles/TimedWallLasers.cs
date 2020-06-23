using UnityEngine;

public class TimedWallLasers : MonoBehaviour
{
    public float onTime;               // The time lasers are on for.
    public float offTime;              // The time lasers are off for.
    private float timer;               // Timer to time the blinking on and off.

    public GameObject[] laserBeams;    // An array to store the lasers.
    public bool isSwitchedOn;
    public bool isOn;                  // True if the lasers are on. 
    
    private void Start()
    {
       if (isOn)
            TurnLasersOn();
        else
            TurnLasersOff();
    }
    
    void Update()
    {
        if (isSwitchedOn)
        {
            timer += Time.deltaTime;

            // If the beam is on and the onTime has been reached...
            if (isOn && timer >= onTime)
                TurnLasersOff();
            // If the beam is off and the offTime has been reached...
            if (!isOn && timer >= offTime)
                TurnLasersOn();
        }
    }
    
    // Turn all the lasers on.
    public void TurnLasersOn()
    {
        timer = 0f; // Reset the timer.
        foreach (GameObject laser in laserBeams)
            laser.SetActive(!laser.activeInHierarchy); // Turn laser on.
        isOn = true;
    }
    
    // Turn all the lasers off.
    public void TurnLasersOff()
    {
        timer = 0f; // Reset the timer.
        foreach (GameObject laser in laserBeams)
            laser.SetActive(false); // Turn laser off.
        isOn = false;
    }
}