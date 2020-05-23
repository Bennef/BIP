using UnityEngine;

public class HidePooledObject : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    // Deactivates the pooled object after a certain amount of time.
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    //Gets called on enable
    void OnEnable()
    {
        //Calls 'HideObject' after 2 seconds
        Invoke("HideObject", 2f);
    }
    // --------------------------------------------------------------------
    //method to deactivte the object
    void HideObject()
    {
        gameObject.SetActive(false);
    }
    // --------------------------------------------------------------------
    //Gets called on deactivation
    void OnDisable()
    {
        CancelInvoke();
    }
    // --------------------------------------------------- End Methods --------------------------------------------
}