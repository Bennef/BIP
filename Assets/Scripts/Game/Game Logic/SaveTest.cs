using UnityEngine;

public class SaveTest : MonoBehaviour
{
    public string levelToLoad;
    public Vector3 bipStartPos;
    public CarManager carManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            GameManager.Instance.save.SceneName = levelToLoad;
            GameManager.Instance.currentCheckpointPos = bipStartPos;
            LocalSceneManager.Instance.LoadScene(levelToLoad);
        }

        if (Input.GetKeyDown("c"))
            carManager.MoveTheDamnCars();
    }
}