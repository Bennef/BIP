using UnityEngine;

public class CutSceneTrigger : MonoBehaviour 
{
    //public GameObject cutSceneManagerObj;        // A reference to the CutSceneManager object. Assigned in editor.
    public string cutSceneID;                    // This object will have a unique ID used to decide which cutscene to play. Assigned in editor.
    public CutSceneManager cutSceneScript;      // The CutSceneManager sript.
    
    public void Awake()
    {
        //cutSceneScript = cutSceneManagerObj.GetComponent<CutSceneManager>();    // Get the script from the CutSceneManager object.
        Debug.Log(cutSceneScript.ToString());
    }
    
    // If Bip collides with this object, play the cutscene according to the ID.
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("collided");
            cutSceneScript.PlayCutScene(cutSceneID);
        }
    }
}