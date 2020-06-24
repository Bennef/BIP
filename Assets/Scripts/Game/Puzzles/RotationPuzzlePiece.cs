using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class RotationPuzzlePiece : MonoBehaviour
    {
        public RotationPuzzle rotationPuzzle;

        // Update is called once per frame
        void Update()
        {
            float distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position); // Gets the distance between the player and a tile (can be any tile)

            if (Input.GetButtonDown("Grab") && distance < 1.2f) // If player if close enough to a tile
            {
                transform.Rotate(Vector3.right * 90); // Rotates tile
                rotationPuzzle.TileRotated(gameObject);
            }
        }
    }
}