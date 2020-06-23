using UnityEngine;
using UnityEngine.Timeline;

public class PlayTimelineOnEnter : MonoBehaviour
{
    private DirectorControlPlayable _timelinePop;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bip")
            _timelinePop.director.Play(); // Play the timeline.
    }
}