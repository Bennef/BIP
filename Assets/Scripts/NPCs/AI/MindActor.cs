using UnityEngine;

abstract public class MindActor : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public AIPathingScript pathingScript;	// To be able to modify bot translations.

    //	private bool disabled;					// When the bot has stopped working but not destroyed.

	// Main Abstract Functions
	//public abstract void Init();			// Used for initialisation.
	public abstract bool Think();			// Used for main executing main methods.
	public abstract void Destroy();         // Used for handling any endState processes.
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------

    // --------------------------------------------------------------------


    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
