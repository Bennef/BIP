using UnityEngine;
using System.Collections;

public class DemoEnd : MonoBehaviour 
{
    // ----------------------------------------------- Data members ----------------------------------------------
    [Tooltip("How long will this screen remain open?")]
    public float time;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Start()
    {
        StartCoroutine("RestartCo"); 
    }
    // --------------------------------------------------------------------
    IEnumerator RestartCo()
    {
        yield return new WaitForSeconds(time);
        LocalSceneManager.Instance.LoadScene("Main Menu Demo");
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
