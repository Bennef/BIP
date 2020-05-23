using System.Collections;
using UnityEngine;

public class TripLaserPuzzle : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public bool alarm, turnedRed, turnedBlue, bipTriggered, hasBeenCompleted;
    public NanoDroneMind[] nanoDrones;
    public AlarmLight alarmLight;
    public Transform[] dronePlinths;
    public GameObject[] tripLasers;
    //public AudioSource aSrc;
    public CharacterController bip;
    public HomingIco ico;
    public LockableDoors doorToUnlock;
    public OverheadUI bipUI;
    public Sprite exclamation;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    private void Start()
    {
        tripLasers = GameObject.FindGameObjectsWithTag("Trip Laser");
        turnedBlue = true;
    }
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if (alarm && !turnedRed)
        {
            // Turn on Alarm Light.
            alarmLight.alarmOn = true;

            // Activate drones!
            foreach (NanoDroneMind drone in nanoDrones)
            {
                if (bipTriggered)
                {
                    drone.target = drone.bipTarget;
                }
                else
                {
                    drone.target = drone.icoTarget;
                }
                drone.active = true;
            }

            // Turn holo red.
            foreach (Transform droneHolo in dronePlinths)
            {
                droneHolo.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                droneHolo.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                droneHolo.GetChild(2).GetComponent<MeshRenderer>().enabled = true;
                droneHolo.GetChild(3).GetComponent<MeshRenderer>().enabled = true;
            }

            // Turn off all the lasers.
            foreach (GameObject tripLaser in tripLasers)
            {
                tripLaser.GetComponent<LineRenderer>().enabled = false;
                tripLaser.GetComponent<TripLaser>().isOn = false;
            }
            turnedRed = true;
            bipUI.ShowUI(exclamation, 3f);
        }

        if (bip.isDead)
        {
            StartCoroutine(ResetEverything());
        }

        if (ico.isDead && !hasBeenCompleted)
        {
            hasBeenCompleted = true;
            StartCoroutine(IcoDestroyed());
        }
    }

    // --------------------------------------------------------------------
    public void ResetCo()
    {
        StartCoroutine(ResetEverything());
    }
    // --------------------------------------------------------------------
    public IEnumerator ResetEverything()
    {
        // Deactivate drones.
        foreach (NanoDroneMind drone in nanoDrones)
        {
            drone.active = false;
            drone.target = drone.bipTarget;
        }

        yield return new WaitForSeconds(1.0f);
        alarm = false;
        alarmLight.alarmOn = false;

        TurnHoloBlue();

        // Turn on all the lasers.
        foreach (GameObject tripLaser in tripLasers)
        {
            tripLaser.GetComponent<LineRenderer>().enabled = true;
            tripLaser.GetComponent<TripLaser>().isOn = true;
        }
        bipTriggered = false;
        turnedRed = false;
        turnedBlue = true;
    }
    // --------------------------------------------------------------------
    public IEnumerator IcoDestroyed()
    {
        // Deactivate drones.
        foreach (NanoDroneMind drone in nanoDrones)
        {
            drone.active = false;
            //drone.target = drone.bipTarget;
        }
        
        alarm = false;
        alarmLight.alarmOn = false;
        TurnHoloBlue();
        bipTriggered = false;
        turnedRed = false;
        turnedBlue = true;

        yield return new WaitForSeconds(2.0f);
        doorToUnlock.UnlockDoor();
    }
    // --------------------------------------------------------------------
    public void TurnHoloBlue()
    {
        foreach (Transform droneHolo in dronePlinths)
        {
            droneHolo.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            droneHolo.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
            droneHolo.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
            droneHolo.GetChild(3).GetComponent<MeshRenderer>().enabled = false;
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
