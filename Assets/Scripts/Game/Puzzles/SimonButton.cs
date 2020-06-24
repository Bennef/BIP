using Assets.Scripts.Game.Puzzles;
using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class SimonButton : MonoBehaviour
    {
        public SimonPuzzle simonPuzzle;    // Attach main simon puzzle gameobject in editor.
        public GameObject button;   // The button to move.
        private Vector3 buttonPressedPos = new Vector3(0, -0.0999f, 0);    // The position of the button when presssed.
        private Vector3 buttonRaisedPos = new Vector3(0, 0, 0);        // The position of the button when not presssed.
        public Light simonLight;    // Attach Light in editor. 

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.name == "Bip")
                LowerButton();

            if (simonPuzzle.playMode)
                simonLight.enabled = true;
        }

        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.name == "Bip")
            {
                RaiseButton();
                simonLight.enabled = false;
            }
        }

        public void LowerButton()
        {
            simonPuzzle.ButtonPressed(gameObject.transform.GetChild(0).gameObject);
            button.transform.localPosition = buttonPressedPos;  // Make the button depress.
        }

        public void RaiseButton() => button.transform.localPosition = buttonRaisedPos;  // Make the button un-depress.
    }
}