using Assets.Scripts.Game.Puzzles;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class ChessCollider : MonoBehaviour
    {
        [SerializeField] private ChessPuzzle chessPuzzle;

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "Chess Collider")
                chessPuzzle.correctPieceCount++;
        }

        void OnTriggerExit(Collider other)
        {
            if (other.name == "Chess Collider")
                chessPuzzle.correctPieceCount--;
        }
    }
}