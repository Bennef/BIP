using UnityEngine;
using UnityEngine.UI;

public class ShowCanvas : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public ToggleExplanation inputManager;
    public Text newText, newTitleText;
    public GameObject expl, title;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Start()
    {
        inputManager = GameObject.Find("Input Manager").GetComponent<ToggleExplanation>();
        newText = gameObject.GetComponent<Text>();
        //newTitleText = GetComponentInChildren<Text>();
        newTitleText = gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Text>();
    }
    // --------------------------------------------------------------------
    void OnTriggerEnter(Collider obj)
    {
        // If the player enters the trigger...
        if (obj.name == "Bip")
        {
            if (inputManager.showing == false)
            {
                inputManager.Show();
            }
            expl = GameObject.Find("Expl");
            expl.GetComponent<Text>().text = newText.text;
            title = GameObject.Find("TitleLabel");
            title.GetComponent<Text>().text = newTitleText.text;
        }
    }
    // --------------------------------------------------------------------
    void OnTriggerExit(Collider obj)
    {
        // If the player enters the trigger...
        if (obj.name == "Bip")
        {
            if (inputManager.showing == true)
            {
                inputManager.Hide();
            }
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
