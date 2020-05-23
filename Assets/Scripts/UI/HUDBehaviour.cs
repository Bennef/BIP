using UnityEngine;
using UnityEngine.UI;

// Manages the UI.
public class HUDBehaviour : MonoBehaviour 
{
	// ----------------------------------------------- Data members ----------------------------------------------
	// Are dataChipCount and bipHealth unique to this class? If so, this is fine, otherwise they should be references to their respective classes, and they should be private. - Luke
	public int healthValue; 	 // Bips health value.
	public int powerValue; 		 // Power Bar value.
	public Slider healthSlider;	 // The slider for health.
	public Slider powerSlider;	 // The slider for power.
	public Health health;
	public PlayerPower powerBar;	 // Ref the PowerBar for info on Power.
	public GameManager GameManagerScript;
    public GameObject player;
    // ----------------------------------------------- End Data members ------------------------------------------
    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    void Start()
	{
        GameManagerScript = GameManager.Instance;
        player = GameObject.Find("Bip");
	}
	// --------------------------------------------------------------------
	void Update()
	{
        if (player != null)
        {
            health = GameManager.Instance.Player.GetComponent<PlayerHealth>(); 
            GetHUDElementValues();
            DisplayHUDValues();
        }
	}
	// --------------------------------------------------------------------
	// Get the information for the HUD values from the respective classes.
	public void GetHUDElementValues()
	{
        if (GameManager.Instance.Player == null)
        {
            return;
        }
                
        powerBar = GameManager.Instance.Player.GetComponent<PlayerPower>();
        if (health != null)
        {
            healthValue = (int)health.value;
        }
        if (powerBar != null)
        {
            powerValue = (int)powerBar.value;
        }
	}
	// --------------------------------------------------------------------
	// Display the values on the HUD on-screen.
	public void DisplayHUDValues()
	{
		healthSlider.value = healthValue;
		powerSlider.value = powerValue;
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}