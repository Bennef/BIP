using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class SlidingTile : MonoBehaviour
    {
        public bool isBeingStoodOn, canMove, isPlacedCorrectly;
        public Transform correctPos;

        void Update()
        {
            if (transform.position == correctPos.position)
                isPlacedCorrectly = true;
            else
                isPlacedCorrectly = false;
        }

        void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                isBeingStoodOn = true;
        }
    }
}