using UnityEngine;

public class ResetAlarm : MonoBehaviour
{
    public TripLaserPuzzle tripLaserPuzzle;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Collider front")
            tripLaserPuzzle.ResetCo();
    }
}