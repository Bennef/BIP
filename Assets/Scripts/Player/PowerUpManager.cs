using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour 
{
	// Stores the items that Bip collects throughout the game. Also stores the powerups and keys.
	public float blastTime;					// The time in seconds the EMP blast lasts for.
	public float coolDown;					// To prevent spamming of EMP.
	public GameObject EMPObject;			// A reference to the EMP Game Object.
	private GameObject bipObject;           // A reference to the player Game Object.
    public Transform head;                  // So we have a position for the pulse.
	private PlayerMovement playerMovement;	// A reference to the PlayerMovement script for Speed Boost.
	private PlayerPower powerBar;		    // A reference to the PowerBar script for Speed Boost and EMP.
    private CharacterSoundManager SoundManager;
  
    // Use this for initialization.
    void Start()
	{
		bipObject = GameObject.Find("Bip");
        EMPObject = GameObject.Find("EMP Object");
		playerMovement = GetComponent<PlayerMovement>();
		powerBar = GetComponent<PlayerPower>();
        if (EMPObject != null)
            EMPObject.SetActive(false);   // Set EMP object to non-active until we perform an EMP Blast.
        SoundManager = GetComponent<CharacterSoundManager>();
    }
	
	// Sends an electromagnetic pulse to a spherical area around Bip.
	public void EMPBlast()	
	{
		if (GameManager.Instance.PowerUps.canEMP == true) 
		{
			GameManager.Instance.PowerUps.canEMP = false;
			EMPObject.SetActive(true);
            
            // Play EMP sound.
            SoundManager.PlayClip(SoundManager.EMP);

            StartCoroutine(BlastWait(blastTime));
			powerBar.TakeDamage (127);
			StartCoroutine(ChargeWait(coolDown));	// So player can't spam EMP.
		}	// Else play a sound maybe?
	}
	
	// The duration of the EMP blast.
	private IEnumerator BlastWait(float seconds)
	{
		yield return new WaitForSeconds(seconds);			// Ideally we want it to fade after a second.
		EMPObject.SetActive(false);							// Set EMP to non-active until we perform an EMP Blast.
		//EMPObject.transform.parent = bipObject.transform;	// Reparent the EMP object to Bip.
		EMPObject.transform.position = bipObject.transform.position;
	}
	
	// The time it takes for the blast to recharge.
	private IEnumerator ChargeWait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GameManager.Instance.PowerUps.canEMP = true;	// Enable EMP once again.
	}
	
	// Make Bip sprint for a short time. 
	public void SpeedBoost()
	{
		if (GameManager.Instance.PowerUps.canBoost)
		{
			GameManager.Instance.PowerUps.canBoost = false;
			playerMovement.speed = 8;				// Enable Bip to move twice as fast. Changed to 8 for demo
			powerBar.TakeDamage(128);				// Deplete 50% of power.
			StartCoroutine(SpeedBoostWait(3));		// Wait for x seconds.
			Debug.Log ("Speed Boosted");
		}
	}
	
	private IEnumerator SpeedBoostWait(float seconds)
	{
		yield return new WaitForSeconds(seconds);	// Wait for x seconds while we can run fast.
		playerMovement.speed = 8;					// Set speed back to normal.
		GameManager.Instance.PowerUps.canBoost = true;		// Enable Speed Boost once again.
	}
    
    public void DoubleJump()
    {
        if (GameManager.Instance.PowerUps.canDoubleJump)
        {
            if (powerBar.value >= 16)
            {
                powerBar.TakeDamage(16);                // Deplete 25% of power.
                playerMovement.Jump();
                SoundManager.PlayClip(SoundManager.doubleJump);    // Play the sound.
            }
        }
    }
}