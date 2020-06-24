using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class TargetTile : MonoBehaviour
    {
        [SerializeField] private bool isCorrect = false;

        public bool IsCorrect { get => isCorrect; set => isCorrect = value; }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Moveable"))
                isCorrect = true;
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Moveable"))
                isCorrect = false;
        }
    }
}