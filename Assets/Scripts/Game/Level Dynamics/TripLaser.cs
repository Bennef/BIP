using UnityEngine;

public class TripLaser : MonoBehaviour 
{
	// ----------------------------------------------- Data members ----------------------------------------------
	// Handles Trip Lasers.
	public Transform startPoint, endPoint;	// The two points to draw the laser between. Set them in the editor.
	private LineRenderer laserLine;			// The LineRemderer that draws the laser.
    public bool isOn;
    public TripLaserPuzzle tripLaserPuzzle;
	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	// Use this for initialization.
	void Start() 
	{
        tripLaserPuzzle = GameObject.Find("Trip Laser Puzzle").GetComponent<TripLaserPuzzle>();
		laserLine = GetComponentInChildren<LineRenderer>();
		laserLine.startWidth = 0.2f;
        laserLine.endWidth = 0.2f;
        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, endPoint.position);
	}
	// --------------------------------------------------------------------
    public void Update()
    {
        if (isOn)
        {
            RaycastHit hit;
            if (Physics.Raycast(startPoint.position, endPoint.position - startPoint.position, out hit))
            {
                // Set target.
                if (hit.transform.gameObject.tag == "Player")
                {
                    tripLaserPuzzle.bipTriggered = true;
                    TriggerAlarm();
                }
                else if (hit.transform.gameObject.tag == "Enemy" && hit.transform.gameObject.name != "Nano Drone")
                {
                    tripLaserPuzzle.bipTriggered = false;
                    TriggerAlarm();
                }
            }
        }
    }
    // --------------------------------------------------------------------
    public void TriggerAlarm()
    {
        if (!tripLaserPuzzle.alarm)
        {
            tripLaserPuzzle.GetComponent<AudioSource>().Play();
            tripLaserPuzzle.alarm = true;
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
