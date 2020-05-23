using UnityEngine;

public class SwitchingManager : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Switch control to other character.
    public static void SwitchCharacter(Transform newCharacter)
    {
        GameManager.Instance.Player.GetComponent<CharacterController>().OnCharacterSwitch();
        GameManager.Instance.Player.GetComponent<CharacterController>().enabled = true; // Enable the Character Controller for the new character.
        GameManager.Instance.Player.tag = Tags.Player; // Change the tag to Player for the new character.
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
