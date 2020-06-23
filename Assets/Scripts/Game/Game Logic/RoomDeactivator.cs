using UnityEngine;

public class RoomDeactivator : MonoBehaviour
{
    public GameObject[] roomsToDeactivate;
        
    // Use this for initialization
    void OnEnable()
    {
		foreach (GameObject room in roomsToDeactivate)
            room.SetActive(false);
	}
}