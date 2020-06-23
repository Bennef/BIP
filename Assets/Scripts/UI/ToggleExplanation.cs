using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleExplanation : MonoBehaviour
{
    public static ToggleExplanation Instance;
    public bool showing;
    private AudioSource aSrc;
    public Scene scene;
    public AudioClip show, hide;
    public GameObject canvasToHide;
        
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (Instance == null)
            Instance = this; 
        else
            Destroy(this.gameObject);
    }
    
    // Use this for initialization
    void Start()
    {
        aSrc = GetComponent<AudioSource>();
        scene = SceneManager.GetActiveScene();
        if (scene.name == "0 Main Menu" || scene.name == "Main Menu Portfolio") { 
            showing = true;
        }
	}
    
    void OnEnable()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Portfolio")
        {
            canvasToHide = GameObject.Find("Canvas 1");
            canvasToHide.SetActive(false);
            showing = false;
        }
        else
        {
            canvasToHide = GameObject.Find("Canvas 0");
            canvasToHide.SetActive(true);
            showing = true;
        }
    }
    
    // Update is called once per frame
    void Update() => CheckInput();

    public void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Q)){ 
            if (showing)
                Hide();
            else
                Show();
        }
    }
    
    public void Show()
    {
        aSrc.clip = show;
        aSrc.Play();
        showing = true;
        canvasToHide.SetActive(true);
    }
    
    public void Hide()
    {
        aSrc.clip = hide;
        aSrc.Play();
        showing = false;
        canvasToHide.SetActive(false);
    }
}