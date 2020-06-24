using UnityEngine;
using UnityEngine.Timeline;

namespace Scripts.Game.Game_Logic
{
    public class PlayTimelineOnEnter : MonoBehaviour
    {
        private DirectorControlPlayable _timelinePop;

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "Bip")
                _timelinePop.director.Play(); // Play the timeline.
        }
    }
}