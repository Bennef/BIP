using UnityEngine;

namespace Scripts.Game.Game_Logic
{
    public class SwitchingManager : MonoBehaviour
    {
        // Switch control to other character.
        public static void SwitchCharacter(Transform nextPlayer)
        {
            GameManager.Instance.Player.GetComponent<Player.CharacterController>().OnCharacterSwitch();
            GameManager.Instance.Player.GetComponent<Player.CharacterController>().enabled = true; // Enable the Character Controller for the new character.
            GameManager.Instance.Player.tag = Tags.Player; // Change the tag to Player for the new character.
        }
    }
}