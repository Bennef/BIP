using UnityEngine;

public class TargetTile : MonoBehaviour
{
    public bool isCorrect = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moveable"))
            isCorrect = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Moveable"))
            isCorrect = false;
    }
}