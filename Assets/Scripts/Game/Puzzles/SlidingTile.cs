using UnityEngine;

public class SlidingTile : MonoBehaviour
{
    public bool isBeingStoodOn, canMove, isPlacedCorrectly;
    public Transform correctPos;
    
    private void Update()
    {
        if (transform.position == correctPos.position)
            isPlacedCorrectly = true;
        else
            isPlacedCorrectly = false;
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isBeingStoodOn = true;
    }
}