using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    // Manages the Checkpoints in the game. When Bip has reached a certain point, we want the game to remember 
    // where he got to and start there if you die.

    // ----------------------------------------------- End Data members ------------------------------------------
    public LockableDoors doorToLock;  // The door to lock behind Bip when we hit the CheckPoint.
    public GameObject doorToActivate, doorToDeactivate, roomToActivate, roomToDeactivate;
    public float cameraStartX;        // The X angle we want the camera to start at.
    public float cameraStartY;        // The Y angle we want the camera to start at.
    public BoxCollider doorCollider;  // Collider of door to turn off.
    public bool shouldLock = true;
    public bool floppyHasBeenShown;
    private Image floppy;
    private Fade fade;
    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    private void Start()
    {
        floppy = GameObject.Find("Checkpoint Floppy").GetComponent<Image>();
        fade = GameObject.Find("Checkpoint Floppy").GetComponent<Fade>();
    }
    // --------------------------------------------------------------------
    // If Bip collides with a Checkpoint, the Checkpoint becomes the current Checkpoint.
    void OnTriggerEnter(Collider obj)
	{
        // If the player enters the trigger...
        if (obj.name == "Bip")
        {
            // Make this the current CheckPoint.
            GameManager.Instance.currentCheckpoint = this;
            GameManager.Instance.currentCheckpointPos = this.transform.position;
            RoomManagement(); // added this line in for demo
            // Lock the door behind him.
            if (doorToLock != null)
            {
                if (!doorToLock.locked && shouldLock == true)
                {
                    doorToLock.CloseDoors();
                    
                    doorCollider.enabled = false;
                    StartCoroutine(Wait());
                }
            }

            // Save the game.
            GameManager.Instance.Save();

            if (!floppyHasBeenShown)
            {
                StartCoroutine(FloppyDisk());
                floppyHasBeenShown = true;
            }
        }
	}
    // --------------------------------------------------------------------
    public IEnumerator FloppyDisk()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(0.6f);
        fade.FadeOut();
        yield return new WaitForSeconds(0.6f);
        fade.FadeIn();
        yield return new WaitForSeconds(0.6f);
        fade.FadeOut();
    }
    // --------------------------------------------------------------------
    public IEnumerator Wait()
    {
        shouldLock = false;
        yield return new WaitForSeconds(0.5f);
        doorToLock.LockDoor();
        RoomManagement();
    }
    // --------------------------------------------------------------------
    public void RoomManagement()
    {
        if (roomToActivate != null)
        {
            ActivateRoom(); 
        }

        if (roomToDeactivate != null)
        {   
            DeactivateRoom();
        }
    }
    // --------------------------------------------------------------------
    public void ActivateRoom()
    {
        roomToActivate.SetActive(true);
        if (doorToActivate != null)
        {
            doorToActivate.SetActive(true);
        }
    }
    // --------------------------------------------------------------------
    public void DeactivateRoom()
    {
        roomToDeactivate.SetActive(false);
        if (doorToDeactivate != null)
        {
            doorToDeactivate.SetActive(false);
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
