using Scripts.Game.Game_Logic;
using UnityEngine;

namespace Scripts.Game.Level_Dynamics
{
    public class FirstCheckPoint : MonoBehaviour
    {
        // Use this for initialization
        void Start()    // Changed this from Awake to Start - may go badly...
        {
            // If we're not loading a save game
            if (!GameManager.Instance.IsLoadingSaveGame)
            {
                // Set the current checkpoint as the starting position in GameManager.
                //GameManager.Instance.currentCheckpointPos = this.transform.position;

                // Inform the Game Manager that they need not fear our power.
                // GameManager.Instance.isLoadingSaveGame = false;
            }
        }
    }
}