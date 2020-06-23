using UnityEngine;

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