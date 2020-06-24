using UnityEngine;

namespace Scripts.Game.Game_Logic
{
    public class RoomDeactivator : MonoBehaviour
    {
        public GameObject[] roomsToDeactivate;

        void OnEnable()
        {
            foreach (GameObject room in roomsToDeactivate)
                room.SetActive(false);
        }
    }
}