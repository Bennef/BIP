using UnityEngine;

public class SimonButton : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public SimonPuzzle simonPuzzle;    // Attach main simon puzzle gameobject in editor.
    public GameObject button;   // The button to move.
    private Vector3 buttonPressedPos = new Vector3(0, -0.0999f, 0);    // The position of the button when presssed.
    private Vector3 buttonRaisedPos = new Vector3(0, 0, 0);        // The position of the button when not presssed.
    public Light simonLight;    // Attach Light in editor. 
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == ("Bip"))
        {
            LowerButton();
        }

        if (simonPuzzle.playMode)
        {
            simonLight.enabled = true;
        }
    }
    // --------------------------------------------------------------------
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == ("Bip"))
        {
            RaiseButton();
            simonLight.enabled = false;
        }
    }
    // --------------------------------------------------------------------
    public void LowerButton()
    {
        simonPuzzle.ButtonPressed(this.gameObject.transform.GetChild(0).gameObject);
        button.transform.localPosition = buttonPressedPos;  // Make the button depress.
    }
    // --------------------------------------------------------------------
    public void RaiseButton()
    {
        button.transform.localPosition = buttonRaisedPos;  // Make the button un-depress.
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}