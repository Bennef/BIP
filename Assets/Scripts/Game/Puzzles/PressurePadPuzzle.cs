using UnityEngine;

public class PressurePadPuzzle : MonoBehaviour
{
    private float startPos; // The start position of the platform
    private float endPos; // The end position of the platform
    private float pingPongTime; // The speed the platform will move

    private Vector3 buttonPressedPos = new Vector3(0, -0.0005f, 0);    // The position of the button when presssed.
    private Vector3 buttonRaisedPos = new Vector3(0, 0, 0);        // The position of the button when not presssed.

    public GameObject movingPlatform; // The platform you want to move
    public GameObject endPositionTransform; // Place a empty gameobject where you want the platform to end up at
    
    // Use this for initialization
    void Start()
    {
        startPos = movingPlatform.transform.position.y; // Gets the platforms current position
        endPos = endPositionTransform.transform.position.y; //gets the desired end position of the platform
    }
    
    // Move Platform
    void OnTriggerStay (Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            // Make the button depress.
            this.transform.GetComponentInParent<Transform>().localPosition = buttonPressedPos;

            // Use this if you want the platform to move back and forth between its start position and the assigned new position
            movingPlatform.transform.position = new Vector3(movingPlatform.transform.position.x, Mathf.Lerp(startPos, endPos, Mathf.PingPong(pingPongTime, 1.0f)), movingPlatform.transform.position.z);
            pingPongTime += Time.deltaTime;
            
            // Use this if you want the platform to move down and stay at the assigned new position
            //movingPlatform.transform.position = new Vector3(movingPlatform.transform.position.x, Mathf.Lerp(startPos, endPos, Time.time / 4), movingPlatform.transform.position.z);
        }
    }
    
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag(("Player")))
        {
            // Make the button un-depress.
            this.transform.GetComponentInParent<Transform>().localPosition = buttonRaisedPos;
        }
    }
}