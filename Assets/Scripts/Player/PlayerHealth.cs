using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : Health 
{
    public bool isActive, damaged, isRegenerating;

	public float regenRate;
	public float delay = 1;

    public float flashSpeed = 20f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 1f, 1f, 0.1f);     // The colour the damageImage is set to, to flash.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public Transform head;  // So we have a position for the sparks.

    private CharacterSoundManager SoundManager;
    
    void Start()
    {
        SoundManager = GetComponent<CharacterSoundManager>();
        if (GameObject.Find("Damage Image") != null)
            damageImage = GameObject.Find("Damage Image").GetComponent<Image>();
    }
    
    void Update()
	{
        if (isActive)
        {
            // If the player has just been damaged...
            if (damaged)
                damageImage.color = flashColour;
            else if (damageImage.color != Color.clear)
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

            // Reset the damaged flag.
            damaged = false;

            if (isRegenerating)
            {
                Regenerate();
                if (value >= maxValue)
                    isRegenerating = false;
            }
        }
	}
    
    void Regenerate() => IncreaseValue(regenRate * Time.deltaTime);

    public override void TakeDamage(float _value)
	{
        damaged = true;
        if (isInvincible) 
			return;
        Spark();
        Invincible();
		isRegenerating = false;
		value -= _value;
		value = Mathf.Clamp (value, 0, maxValue);
		StartCoroutine(RegenDelayCo());
	}
    
    IEnumerator RegenDelayCo()
	{
		yield return new WaitForSeconds (delay);
		isRegenerating = true;
	}
    
    // Make sparks fly.
    void Spark()
    {
        GameObject spark = (GameObject)Instantiate(Resources.Load("Flare"));
        spark.transform.position = head.position;
        SoundManager.PlayClip(SoundManager.hit);
    }
}