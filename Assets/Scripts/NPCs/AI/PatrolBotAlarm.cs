using Scripts.Game;
using UnityEngine;

namespace Scripts.NPCs.AI
{
    public class PatrolBotAlarm : MonoBehaviour
    {
        private PatrolBotMind _mind;

        void Awake() => _mind = transform.GetComponentInParent<PatrolBotMind>();

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Player))
            {
                _mind.inLineOfSight = true;
                _mind.aSrc.PlayOneShot(_mind.alarm);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.Player))
                _mind.inLineOfSight = false;
        }
    }
}