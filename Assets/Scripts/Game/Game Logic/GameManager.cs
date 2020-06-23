using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public class GameManager : MonoBehaviour
{ 
	// Will manage spawning of Bip in the Level.
	public static GameManager Instance;
    public Transform Player;
    public CheckPoint currentCheckpoint;       // To access the current check point.
    public Vector3 currentCheckpointPos;       // The position of the current CheckPoint.
    public GameObject mainCam;                 // Main Game Camera.
    public bool isPaused;                      // True if game is paused.
	public PowerUps powerUps;
    public CompassionChoices compChoices;      // Compassion choices.
	public GameOptions options;
	public Snapshot save;
	public string FilePath;
    public Scene scene;
    public string CurrentScene;		           // The current Scene.
    public bool isLoadingSaveGame;
    public LayerMask ObstacleAvoidanceLayerMask;
    
    private CharacterController charController;
    private PlayerMovement playerMovement;
    
    AudioSource[] audios;
    AudioSource pauseMenu;
    
    void Awake()
	{
		DontDestroyOnLoad(transform.gameObject); 
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        Player = GameObject.Find("Bip").GetComponent<Transform>();

		powerUps = new PowerUps();;
		save = new Snapshot();
        //CurrentScene = "1 Dumpster";    // The first scene we will want to save is dumpster
        CurrentScene = "Portfolio";       // For portfolio, we load a different scene.

        options = GetComponent<GameOptions>();

        isPaused = false;

        scene = SceneManager.GetActiveScene();
        string str = UnityEngine.StackTraceUtility.ExtractStackTrace();
        // If we are not in the main menu scene...
        if (scene.name != "Main Menu Portfolio" && scene.name != "Main Menu Web")
        {
            charController = GameObject.Find("Bip").GetComponent<CharacterController>();
            pauseMenu = GameObject.Find("Pause Menu Canvas").GetComponent<AudioSource>();
        }
        else
        {
            playerMovement = Player.GetComponent<PlayerMovement>();
            playerMovement.isHandlingInput = false;
        }
        //audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }
    
    void OnLevelWasLoaded()
    {
        if (scene.name == "Main Menu Portfolio" || scene.name == "Main Menu Web")
            GameManager.Instance.isPaused = false;
    }
	
    void Update()
    {
        audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        if (GameManager.Instance.isPaused)
        {
            Time.timeScale = 0f;
            foreach (AudioSource aud in audios)
            {
                if (aud != pauseMenu)
                    aud.Pause();
            }
        }
        else
        {
            Time.timeScale = 1f;
            foreach (AudioSource aud in audios)
                aud.UnPause();
        }
    }

    public void SetSaveFile(string saveName) => save.Name = saveName;
    
    public void SetSceneName(string sceneName) => save.SceneName = sceneName;
    
    public void CreateNewGame() => StartCoroutine(CreateNewGameCo());
    
    public IEnumerator CreateNewGameCo()
    {
        //save.SceneName = "1 Dumpster";
        save.SceneName = "Portfolio";
        //save.currentCheckpointPos = new SerializedVector3(20.93f, -0.28f, 6.89f); // Bip starting pos in Dumpster.
        save.currentCheckpointPos = new SerializedVector3(0.0f, -0.2f, 0.0f); // Bip starting pos in Portfolio.
        if (!Directory.Exists(string.Format(@"{0}\Saves", Application.dataPath)))
            Directory.CreateDirectory(string.Format(@"{0}\Saves", Application.dataPath));

        // Open up a stream, that allows us to write data.
        var stream = new FileStream(string.Format(@"{0}\Saves\{1}.bip", Application.dataPath, save.Name), FileMode.Create, FileAccess.Write, FileShare.Write);

        // Create a Binary Formatter, and serialize the data into a Binary format.
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, save);

        // Then close stream.
        stream.Close();
        yield return new WaitForSeconds(2f);  // For safety in case it takes time to save game.
        Load(save.Name);
    }
    
    public void Save()
	{
		save.powerUps = this.powerUps;
		save.SceneName = this.CurrentScene;
		save.compChoices = this.compChoices;
		save.currentCheckpointPos = new SerializedVector3(currentCheckpointPos.x, currentCheckpointPos.y, currentCheckpointPos.z);
		// Create a directory to save to if one does not exist.
		if (!Directory.Exists(string.Format(@"{0}\Saves", Application.dataPath)))
			Directory.CreateDirectory(string.Format(@"{0}\Saves", Application.dataPath));
		
		// Open up a stream, that allows us to write data.
		var stream = new FileStream(string.Format(@"{0}\Saves\{1}.bip", Application.dataPath, save.Name), FileMode.Create, FileAccess.Write, FileShare.Write);
		
        // Create a Binary Formatter, and serialize the data into a Binary format.
		var formatter = new BinaryFormatter();
		formatter.Serialize(stream, save);

        // Then close stream.
        stream.Close();
    }

    public void Load(string fileName)
    {
        isLoadingSaveGame = true;
        FilePath = Application.dataPath + "/Saves/" + fileName + ".bip";
        using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            var formatter = new BinaryFormatter();
            var obj = (Snapshot)formatter.Deserialize(stream);
            save = obj;
            LocalSceneManager.Instance.LoadScene(save.SceneName);
            this.powerUps = save.powerUps;
            this.compChoices = save.compChoices;
            this.currentCheckpointPos = save.currentCheckpointPos.ToVector3();
        }
    }

    // Delete a save file to free up a slot.
    public void DeleteSave(string fileName)
    {
        if (File.Exists(Application.dataPath + "/Saves/" + fileName + ".bip"))
            File.Delete(Application.dataPath + "/Saves/" + fileName + ".bip"); // Delete the save file.
    }
    
    // Quit the application.
    public void Quit() => Application.Quit();

    // Sets the current checkpoint.
    public void SetCurrentCheckpoint(Vector3 checkpointPos) => currentCheckpointPos = checkpointPos;

    public void ResetPositions()
	{
		// Get an Array of GameObjects with the tag Moveable
		GameObject[] ObjectsToReset;
		ObjectsToReset = GameObject.FindGameObjectsWithTag(Tags.Moveable);
        
		// For each GameObject in that array, reset position.
		foreach (GameObject obj in ObjectsToReset)
		{
			if (obj.GetComponent<GrabbableObject>())
				obj.GetComponent<GrabbableObject>().ResetPosition();
            
            if (obj.GetComponent<NanoDroneMind>())
                obj.GetComponent<NanoDroneMind>().ResetPosition();
        }

        // Get an Array of GameObjects with the tag Enemy.
        GameObject[] EnemiesToReset;
        EnemiesToReset = GameObject.FindGameObjectsWithTag(Tags.Enemies);

        // For each GameObject in that array, reset position.
        foreach (GameObject obj in ObjectsToReset)
        {
            if (obj.GetComponent<HomingIco>())
                obj.GetComponent<HomingIco>().ResetPosition();
        }
    }
    
    public void Pause() => isPaused = true;

    public void Resume() => isPaused = false;

    public void isKinematic(bool value)
    {
        Rigidbody[] rigidbodies;
        rigidbodies = GameObject.FindObjectsOfType<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
            rigidbody.isKinematic = value;
    }   
}