using UnityEngine;
using System.Collections;

public class SlidingTileGame : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public bool isComplete, canSwapTiles;
    public LockableDoors doorToUnlock;
    public SlidingTile[] tileArray;
    public Transform[] posArray;
    public int blankTilePos, correctCount;
    public Transform blankTile;
    public AudioSource aSrc;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    private void Start()
    {
        aSrc = GetComponent<AudioSource>();
        correctCount = 0;
        canSwapTiles = true;
    }
    // --------------------------------------------------------------------
    void Update()
    {
        if (!isComplete)
        {
            CheckForComplete();
            CheckBlankTilePos();
            AssignMoveableTiles();
            CheckForTileToMove();
        }
        else if (doorToUnlock.locked)
        {
            doorToUnlock.UnlockDoor();
        }
    }
    // --------------------------------------------------------------------
    public void CheckForComplete()
    {
        correctCount = 0;
        foreach(SlidingTile tile in tileArray)
        {
            if (tile.isPlacedCorrectly)
            {
                correctCount++;
            }
            if (correctCount == 9)
            {
                isComplete = false;
            }
        }
    }
    // --------------------------------------------------------------------
    public void CheckBlankTilePos()
    {
        foreach (Transform position in posArray)
        {
            if (position.position == tileArray[8].transform.position)
            {
                blankTilePos = int.Parse(position.gameObject.name);
            }
        }
    }
    // --------------------------------------------------------------------
    public IEnumerator MoveTile(SlidingTile tileToMove, Vector3 positionToMoveTileTo)
    {
        canSwapTiles = false;
        blankTile.position = tileToMove.gameObject.transform.position;
        tileToMove.gameObject.transform.position = positionToMoveTileTo;
        tileToMove.isBeingStoodOn = false;
        aSrc.Play();
        yield return new WaitForSeconds(0.3f);
        canSwapTiles = true;
    }
    // --------------------------------------------------------------------
    public void AssignMoveableTiles()
    {
        switch (blankTilePos)
        {
            case 1:
                foreach (SlidingTile tile in tileArray)
                {
                    if (tile.transform.position == posArray[1].position || tile.transform.position == posArray[3].position)
                    {
                        tile.canMove = true;
                    }
                    else
                    {
                        tile.canMove = false;
                    }
                }
                break;
            case 2:
                foreach (SlidingTile tile in tileArray)
                {
                    if (tile.transform.position == posArray[0].position || tile.transform.position == posArray[2].position
                        || tile.transform.position == posArray[4].position)
                    {
                        tile.canMove = true;
                    }
                    else
                    {
                        tile.canMove = false;
                    }
                }
                break;
            case 3:
                foreach (SlidingTile tile in tileArray)
                {
                    if (tile.transform.position == posArray[1].position || tile.transform.position == posArray[5].position)
                    {
                        tile.canMove = true;
                    }
                    else
                    {
                        tile.canMove = false;
                    }
                }
                break;
            case 4:
                foreach (SlidingTile tile in tileArray)
                {
                    if (tile.transform.position == posArray[0].position || tile.transform.position == posArray[4].position
                        || tile.transform.position == posArray[6].position)
                    {
                        tile.canMove = true;
                    }
                    else
                    {
                        tile.canMove = false;
                    }
                }
                break;
            case 5:
                foreach (SlidingTile tile in tileArray)
                {
                    if (tile.transform.position == posArray[1].position || tile.transform.position == posArray[3].position
                        || tile.transform.position == posArray[5].position || tile.transform.position == posArray[7].position)
                    {
                        tile.canMove = true;
                    }
                    else
                    {
                        tile.canMove = false;
                    }
                }
                break;
            case 6:
                foreach (SlidingTile tile in tileArray)
                {
                    if (tile.transform.position == posArray[2].position || tile.transform.position == posArray[4].position
                        || tile.transform.position == posArray[8].position)
                    {
                        tile.canMove = true;
                    }
                    else
                    {
                        tile.canMove = false;
                    }
                }
                break;
            case 7:
                foreach (SlidingTile tile in tileArray)
                {
                    if (tile.transform.position == posArray[3].position || tile.transform.position == posArray[7].position)
                    {
                        tile.canMove = true;
                    }
                    else
                    {
                        tile.canMove = false;
                    }
                }
                break;
            case 8:
                foreach (SlidingTile tile in tileArray)
                {
                    if (tile.transform.position == posArray[4].position || tile.transform.position == posArray[6].position
                        || tile.transform.position == posArray[8].position)
                    {
                        tile.canMove = true; 
                    }
                    else
                    {
                        tile.canMove = false;
                    }
                }
                break;
            case 9:
                foreach (SlidingTile tile in tileArray)
                {
                    if (tile.transform.position == posArray[5].position || tile.transform.position == posArray[7].position)
                    {
                        tile.canMove = true;
                    }
                    else
                    {
                        tile.canMove = false;
                    }
                }
                break;
        }
    }
    // --------------------------------------------------------------------
    void CheckForTileToMove()
    {
        if (canSwapTiles)
        {
            foreach (SlidingTile tile in tileArray)
            {
                if (tile.canMove && tile.isBeingStoodOn)
                {
                    StartCoroutine(MoveTile(tile, blankTile.position));
                }
            }
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
