using UnityEngine;

public class RotationPuzzlePiece : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public RotationPuzzle rotationPuzzle;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update () {
        float distance = Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position); // Gets the distance between the player and a tile (can be any tile)

        if (Input.GetButtonDown("Grab") && distance < 1.2f) // If player if close enough to a tile
        {
            this.transform.Rotate(Vector3.right * 90); // Rotates tile
            rotationPuzzle.TileRotated(this.gameObject);
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
