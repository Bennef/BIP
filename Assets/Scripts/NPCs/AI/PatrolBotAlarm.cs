using UnityEngine;

public class PatrolBotAlarm : MonoBehaviour 
{
    public PatrolBotMind mind;
    
    void Awake() => mind = transform.GetComponentInParent<PatrolBotMind>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            mind.inLineOfSight = true;
			mind.aSrc.PlayOneShot(mind.alarm);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Player))
            mind.inLineOfSight = false;
    }   
}