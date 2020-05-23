using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;


public class SaveTest : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public string levelToLoad;
    public Vector3 bipStartPos;
    public CarManager carManager;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown("k"))
        {
            GameManager.Instance.save.SceneName = levelToLoad;
            GameManager.Instance.currentCheckpointPos = bipStartPos;
            LocalSceneManager.Instance.LoadScene(levelToLoad);
        }

        if (Input.GetKeyDown("c"))
        {
            carManager.MoveTheDamnCars();
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
