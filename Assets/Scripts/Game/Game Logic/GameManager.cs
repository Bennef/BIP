using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public CheckPoint currentCheckpoint;       // To access the current check point.
    public Vector3 currentCheckpointPos;       // The position of the current CheckPoint.
    private GameObject mainCam;                 // Main Game Camera.
    public CompassionChoices compChoices;      // Compassion choices.
	public GameOptions options;
	public Snapshot save;
	public string FilePath;
    private Scene _scene;
    private LayerMask _obstacleAvoidanceLayerMask;
    private PlayerMovement _playerMovement;
    private AudioSource[] audios;
    private AudioSource pauseMenu;
    
    public static GameManager Instance { get; set; }
    public string CurrentScene { get; set; }
    public bool IsLoadingSaveGame { get; set; }
    public bool IsPaused { get; set; }
    public LayerMask ObstacleAvoidanceLayerMask { get => _obstacleAvoidanceLayerMask; set => _obstacleAvoidanceLayerMask = value; }
    public Transform Player { get; set; }
    public PowerUps PowerUps { get; set; }

    void Awake()
	{
		DontDestroyOnLoad(transform.gameObject); 
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        Player = GameObject.Find("Bip").GetComponent<Transform>();

		PowerUps = new PowerUps();;
		save = new Snapshot();
        //_currentScene = "1 Dumpster";    // The first scene we will want to save is dumpster
        CurrentScene = "Portfolio";       // For portfolio, we load a different scene.

        options = GetComponent<GameOptions>();

        IsPaused = false;

        _scene = SceneManager.GetActiveScene();
        //string str = UnityEngine.StackTraceUtility.ExtractStackTrace();
        // If we are not in the main menu scene...
        if (_scene.name != "Main Menu Portfolio" && _scene.name != "Main Menu Web")
            pauseMenu = GameObject.Find("Pause Menu Canvas").GetComponent<AudioSource>();
        else
        {
            _playerMovement = Player.GetComponent<PlayerMovement>();
            _playerMovement.isHandlingInput = false;
        }
        //audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu Portfolio" || scene.name == "Main Menu Web")
            GameManager.Instance.IsPaused = false;
    }
	
    void Update()
    {
        audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        if (GameManager.Instance.IsPaused)
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
		save.powerUps = this.PowerUps;
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
        IsLoadingSaveGame = true;
        FilePath = Application.dataPath + "/Saves/" + fileName + ".bip";
        using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            var formatter = new BinaryFormatter();
            var obj = (Snapshot)formatter.Deserialize(stream);
            save = obj;
            LocalSceneManager.Instance.LoadScene(save.SceneName);
            this.PowerUps = save.powerUps;
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
    
    public void Pause() => IsPaused = true;

    public void Resume() => IsPaused = false;
    
    public void IsKinematic(bool value)
    {
        Rigidbody[] rigidbodies;
        rigidbodies = GameObject.FindObjectsOfType<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
            rigidbody.isKinematic = value;
    }   
}