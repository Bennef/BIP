using UnityEngine;

public class TileFloorPuzzle : MonoBehaviour {
    // ----------------------------------------------- Data members ----------------------------------------------
    public int correctTiles;            // The numebr of correct tiles at this time.    
    public LockableDoors doorsToUnlock; // The doors to unlock if all target tiles have cubes.
    public bool puzzleComplete;         // True once puzzle is complete;
    public Component[] tiles;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    private void Start()
    {
        tiles = GetComponentsInChildren<TargetTile>();
    }
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if (!puzzleComplete)
        {
            foreach (TargetTile tile in tiles)
            {
                // If there is a cube on the tile, increase count by one.
                if (tile.isCorrect)
                {
                    correctTiles++; 
                }
            }
            
            if (correctTiles < 0)
            {
                correctTiles = 0;
            }

            // If all tiles are complete, unlock the door.
            if (correctTiles == 3)
            {
                doorsToUnlock.UnlockDoor();
                puzzleComplete = true;
            }
            correctTiles = 0;
        }
	}
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
