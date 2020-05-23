using UnityEngine;

public class LightReceivers : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public MeshRenderer receiverA, receiverB;
    public LockableDoors doorToUnlock;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if (receiverA.enabled && receiverB.enabled)
        {
            if (doorToUnlock.locked)
            {
                doorToUnlock.UnlockDoor(); 
            }
        }
        else if (!doorToUnlock.locked)
        {
            doorToUnlock.LockDoor();
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
