using UnityEngine;

public class ChessCollider : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public ChessPuzzle chessPuzzle;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Chess Collider")
        {
            chessPuzzle.correctPieceCount++;
        }
    }
    // --------------------------------------------------------------------
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Chess Collider")
        {
            chessPuzzle.correctPieceCount--;
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
