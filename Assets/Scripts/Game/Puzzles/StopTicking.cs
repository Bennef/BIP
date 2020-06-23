using UnityEngine;

public class StopTicking : MonoBehaviour
{
    public LaserRunner laserRunner;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bip")
            laserRunner.EndSequence();
    }
}